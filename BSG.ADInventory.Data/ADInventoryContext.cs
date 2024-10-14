namespace BSG.ADInventory.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Zcf.Core.Models;
    using Zcf.Data;
    using BSG.ADInventory.Data.Hooks;
    using BSG.ADInventory.Entities;

    public class ADInventoryContext : DataContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<InvDocType> InvDocTypes { get; set; }

        private IEnumerable<IHook> hooks;
        private IList<IPreActionHook> preHooks;
        private IList<IPostActionHook> postHooks;

        public ADInventoryContext()
        {
            this.preHooks = new List<IPreActionHook>();
            this.postHooks = new List<IPostActionHook>();
        }

        /// <summary>
        /// Loads the entities.
        /// </summary>
        /// <param name="asm">The asm.</param>
        /// <param name="modelBuilder">The model builder.</param>
        /// <param name="nameSpace">The name space.</param>
        public virtual void LoadEntities(Assembly asm, DbModelBuilder modelBuilder, string nameSpace)
        {
            //TODO For Order mapping
            //var entityTypes1 = modelAssembly.GetTypes()
            //.Where(type => type.Namespace != null
            //&& (type.Namespace.StartsWith(domainNamespace)
            //&& type.CustomAttributes.All(c => c.AttributeType != typeof(ComplexTypeAttribute))
            //&& !type.IsAbstract && !type.IsEnum))
            //.OrderBy(c => c.CustomAttributes.Any(x => x.AttributeType == typeof(EntityOrderAttribute)) ? c.GetCustomAttribute<EntityOrderAttribute>().OrderNumber : 100).ToList();

            var entityTypes = asm.GetTypes().Where(type =>
                type.BaseType != null &&
                type.Namespace.EndsWith(nameSpace) &&
                type.BaseType.IsAbstract &&
                type.BaseType == typeof(BaseEntity)).ToList();

            var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");
            entityTypes.ForEach(type =>
            {
                entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
            });
        }

        /// <summary>
        /// Loads the entity configurations.
        /// </summary>
        /// <param name="asm">The asm.</param>
        /// <param name="modelBuilder">The model builder.</param>
        /// <param name="nameSpace">The name space.</param>
        public virtual void LoadEntityConfigurations(Assembly asm, DbModelBuilder modelBuilder, string nameSpace)
        {


            var configurations = asm.GetTypes()
                                    .Where(type => type.BaseType != null &&
                                           type.Namespace.EndsWith(nameSpace) &&
                                           type.BaseType.IsGenericType &&
                                           type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>))
                                    .ToList();

            configurations.ForEach(type =>
            {
                dynamic instance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(instance);
            });
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var asm = Assembly.GetAssembly(typeof(BaseEntity));
            this.LoadEntityConfigurations(asm, modelBuilder, "Entities.Mappings");
            this.LoadEntities(asm, modelBuilder, "BSG.ADInventory.Entities");

            modelBuilder.Entity<User>()
                     .HasMany<Project>(s => s.Projects)
                     .WithMany(c => c.Users)
                     .Map(cs =>
                     {
                         cs.MapLeftKey("UserId");
                         cs.MapRightKey("ProjectId");
                         cs.ToTable("UserProjects");
                     });

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of objects written to the underlying database.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">Thrown if the context has been disposed.</exception>
        public override int SaveChanges()
        {
            #region base
            // Set some fields which can automatically filled
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == System.Data.Entity.EntityState.Added).ToList())
            {
                var entityWithGuidKey = entry.Entity as IGuidKey;
                if (entityWithGuidKey != null)
                {
                    if (entityWithGuidKey.Id == default(Guid))
                    {
                        entityWithGuidKey.Id = Guid.NewGuid();
                    }
                }

                var entityWithCreateTime = entry.Entity as ICreateTime;
                if (entityWithCreateTime != null)
                {
                    if (entityWithCreateTime.CreateTime == default(DateTime))
                    {
                        entityWithCreateTime.CreateTime = DateTime.Now;
                    }
                }

                var entityWithLastUpdateTime = entry.Entity as ILastUpdateTime;
                if (entityWithLastUpdateTime != null)
                {
                    if (entityWithLastUpdateTime.LastUpdateTime == default(DateTime))
                    {
                        entityWithLastUpdateTime.LastUpdateTime = entityWithCreateTime == null
                            ? DateTime.Now
                            : entityWithCreateTime.CreateTime;
                    }
                }

                var currentUser = Zcf.Security.Authentication.Default.GetCurrentUserPrincipal(false);
                var entityWithCreateUser = entry.Entity as ICreateUser;
                if (entityWithCreateUser != null)
                {
                    if (entityWithCreateUser.CreateUserId == null && currentUser != null)
                    {
                        entityWithCreateUser.CreateUserId = currentUser.Identity.Id;
                    }
                    else
                    {
                        //if (currentUserFromService != null)
                        //{
                        //    //entityWithCreateUser.CreateUserName = currentUserFromService.UserName;
                        //}
                    }
                }

                var entityWithLastUpdateUser = entry.Entity as ILastUpdateUser;
                if (entityWithLastUpdateUser != null)
                {
                    if (entityWithLastUpdateUser.LastUpdateUserId == null && currentUser != null)
                    {
                        entityWithLastUpdateUser.LastUpdateUserId = entityWithCreateUser == null
                            ? currentUser.Identity.Id
                            : entityWithCreateUser.CreateUserId;
                    }
                }
            }

            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList())
            {
                var entityWithLastUpdateTime = entry.Entity as ILastUpdateTime;
                if (entityWithLastUpdateTime != null)
                {
                    entityWithLastUpdateTime.LastUpdateTime = DateTime.Now;
                }

                var entityWithLastUpdateUser = entry.Entity as ILastUpdateUser;
                if (entityWithLastUpdateUser != null)
                {
                    var currentUser = Zcf.Security.Authentication.Default.GetCurrentUserPrincipal(false);
                    entityWithLastUpdateUser.LastUpdateUserId = currentUser != null ? currentUser.Identity.Id : (Guid?)null;
                    //if (currentUserFromService != null)
                    //{
                    //    //entityWithLastUpdateUser.LastUpdateUserName = currentUserFromService.UserName;
                    //}
                }

            }
            #endregion

            HookedEntityEntry[] modifiedEntries = null;
            bool hooksEnabled = this.HooksEnabled && (preHooks.Count > 0 || postHooks.Count > 0);

            if (hooksEnabled)
            {
                modifiedEntries = this.ChangeTracker.Entries()
                                .Where(x => x.State != System.Data.Entity.EntityState.Unchanged && x.State != System.Data.Entity.EntityState.Detached)
                                .Select(x => new HookedEntityEntry()
                                {
                                    Entity = x.Entity,
                                    PreSaveState = (BSG.ADInventory.Data.Enum.PspEntityState)((int)x.State)
                                })
                                .ToArray();

                if (preHooks.Count > 0)
                {
                    // Regardless of validation (possible fixing validation errors too)
                    ExecutePreActionHooks(modifiedEntries, false);
                }
            }

            if (this.Configuration.ValidateOnSaveEnabled)
            {
                var results = from entry in this.ChangeTracker.Entries()
                              where this.ShouldValidateEntity(entry)
                              let validationResult = entry.GetValidationResult()
                              where !validationResult.IsValid
                              select validationResult;

                if (results.Any())
                {

                    var fail = new DbEntityValidationException(FormatValidationExceptionMessage(results), results);
                    //Debug.WriteLine(fail.Message, fail);
                    throw fail;
                }
            }

            if (hooksEnabled && preHooks.Count > 0)
            {
                ExecutePreActionHooks(modifiedEntries, true);
            }

            bool validateOnSaveEnabled = this.Configuration.ValidateOnSaveEnabled;

            // SAVE NOW!!!
            this.Configuration.ValidateOnSaveEnabled = false;
            int result = this.Commit();
            this.Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;

            if (hooksEnabled && postHooks.Count > 0)
            {
                ExecutePostActionHooks(modifiedEntries);
            }

            return result;

            //return base.SaveChanges();
        }

        #region Hooks
        /// <summary>
        /// Gets or sets the hooks.
        /// </summary>
        /// <value>
        /// The hooks.
        /// </value>
        public IEnumerable<IHook> Hooks
        {
            get
            {
                return hooks ?? Enumerable.Empty<IHook>();
            }
            set
            {
                if (value != null)
                {
                    this.preHooks = value.OfType<IPreActionHook>().ToList();
                    this.postHooks = value.OfType<IPostActionHook>().ToList();
                }
                else
                {
                    this.preHooks.Clear();
                    this.postHooks.Clear();
                }
                hooks = value;
            }
        }

        /// <summary>
        /// Executes the pre action hooks, filtered by <paramref name="requiresValidation"/>.
        /// </summary>
        /// <param name="modifiedEntries">The modified entries to execute hooks for.</param>
        /// <param name="requiresValidation">if set to <c>true</c> executes hooks that require validation, otherwise executes hooks that do NOT require validation.</param>
        private void ExecutePreActionHooks(IEnumerable<HookedEntityEntry> modifiedEntries, bool requiresValidation)
        {
            foreach (var entityEntry in modifiedEntries)
            {
                var entry = entityEntry; // Prevents access to modified closure

                foreach (
                    var hook in
                        preHooks.Where(x => x.HookStates == entry.PreSaveState && x.RequiresValidation == requiresValidation))
                {
                    var metadata = new HookEntityMetadata(entityEntry.PreSaveState);
                    hook.HookObject(entityEntry.Entity, metadata);

                    if (metadata.HasStateChanged)
                    {
                        entityEntry.PreSaveState = metadata.State;
                    }
                }
            }
        }

        /// <summary>
        /// Executes the post action hooks.
        /// </summary>
        /// <param name="modifiedEntries">The modified entries to execute hooks for.</param>
        private void ExecutePostActionHooks(IEnumerable<HookedEntityEntry> modifiedEntries)
        {
            foreach (var entityEntry in modifiedEntries)
            {
                foreach (var hook in postHooks.Where(x => x.HookStates == entityEntry.PreSaveState))
                {
                    var metadata = new HookEntityMetadata(entityEntry.PreSaveState);
                    hook.HookObject(entityEntry.Entity, metadata);
                }
            }
        }

        protected virtual bool HooksEnabled
        {
            get { return true; }
        }

        #endregion

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <returns></returns>
        private int Commit()
        {
            int result = 0;
            bool commitFailed = false;
            do
            {
                commitFailed = false;

                try
                {
                    result = base.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    commitFailed = true;

                    foreach (var entry in ex.Entries)
                    {
                        entry.Reload();
                    }
                }
            }
            while (commitFailed);

            return result;
        }

        /// <summary>
        /// Formats the validation exception message.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <returns></returns>
        private string FormatValidationExceptionMessage(IEnumerable<DbEntityValidationResult> results)
        {
            var sb = new StringBuilder();
            sb.Append("Entity validation failed" + Environment.NewLine);

            //foreach (var res in results)
            //{
            //    var baseEntity = res.Entry.Entity as BaseEntity;
            //    sb.AppendFormat("Entity Name: {0} - Id: {0} - State: {1}",
            //        res.Entry.Entity.GetType().Name,
            //        baseEntity != null ? baseEntity.Id.ToString() : "N/A",
            //        res.Entry.State.ToString());
            //    sb.AppendLine();

            //    foreach (var validationError in res.ValidationErrors)
            //    {
            //        sb.AppendFormat("\tProperty: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
            //        sb.AppendLine();
            //    }
            //}

            return sb.ToString();
        }

        // codehint: sm-add (performance on bulk inserts)
        /// <summary>
        /// Gets or sets a value indicating whether [validate on save enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [validate on save enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool ValidateOnSaveEnabled
        {
            get
            {
                return this.Configuration.ValidateOnSaveEnabled;
            }
            set
            {
                this.Configuration.ValidateOnSaveEnabled = value;
            }
        }


        public System.Data.Entity.DbSet<BSG.ADInventory.Entities.Country> Countries { get; set; }
    }
}
