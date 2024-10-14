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

    public interface IUserRepository : Zcf.Security.IUserRepository<Entities.User>, IQueryableRepository<Entities.User>
    {
        /// <summary>
        /// Checks the email duplicate.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        bool EmailExists(string email, Guid? userId = null);

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="isActive">if set to <c>true</c> returns the active users otherwise returns inactive ones.</param>
        /// <returns></returns>
        IEnumerable<Entities.User> GetUsers(bool isActive);



        User GetUserByEmail(string email);
    }
}