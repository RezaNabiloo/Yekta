namespace BSG.ADInventory.Data
{ 	
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.Entity;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Web;
	using Zcf.Data;
	using BSG.ADInventory.Entities;
	using Zcf.Data.Framework;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository" /> class.
    /// </summary>
    /// <param name="dbFactory">The database factory.</param>
    public class UserRepository : DbContextUserRepository<Entities.User>, IUserRepository
    {
        public UserRepository(Zcf.Data.IDbFactory dbFactory)
            : base(dbFactory)
        {

        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public override System.Linq.IQueryable<Entities.User> Query
        {
            get { return base.Query.OrderBy(e => e.Id); }
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="isActive">if set to <c>true</c> returns the active users otherwise returns inactive ones.</param>
        /// <returns></returns>
        public IEnumerable<Entities.User> GetUsers(bool isActive)
        {
            return this.Query.Where(u => u.IsActive == isActive);
        }

        /// <summary>
        /// Checks the email duplicate.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public bool EmailExists(string email, Guid? userId = null)
        {
            var query = this.Query.Where(u =>
                                         u.Email.ToLower() == email.ToLower());

            if (userId != null)
            {
                query = query.Where(u => u.Id != userId.Value);
            }

            return query.Any();
        }

        public User GetUserByEmail(string email)
        {
            var query = this.Query.Where(u => u.Email == email).FirstOrDefault();
            return query;
        }
    }
}
