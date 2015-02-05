//-----------------------------------------------------------------------
// <copyright file="IAccountDbContext.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Data.Model
{
    using System.Data.Entity;

    /// <summary>
    /// Define interface for Database context.
    /// </summary>
    public interface IAccountDbContext
    {
        /// <summary>
        /// Gets or sets account entity.
        /// </summary>
        DbSet<Account> Account { get; set; }

        /// <summary>
        /// Gets or sets order entity.
        /// </summary>
        DbSet<Order> Order { get; set; }
        
        /// <summary>
        /// Gets or sets User entity.
        /// </summary>
        DbSet<User> User { get; set; }
    }
}