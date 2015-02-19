//-----------------------------------------------------------------------
// <copyright file="AccountBbContext.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------

namespace SD.CodeProblem.DevAssignment.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Database context object.
    /// </summary>
    public partial class AccountDbContext : IAccountDbContext
    {
        /// <summary>
        /// The entity framework database first connection string wrapper that wraps the actual connection string.
        /// </summary>
        private const string EntityFramewordConnectionStringWrapper = "metadata=res://*/Model.AccountDbModel.csdl|res://*/Model.AccountDbModel.ssdl|res://*/Model.AccountDbModel.msl;provider=System.Data.SqlClient;provider connection string='{0}'";

        /// <summary>
        /// The Wilco database connection string.
        /// </summary>
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings[@"AccountDbContext"].ConnectionString;

        /// <summary>
        /// The Wilco entity framework database first connection string.
        /// </summary>
        private static readonly string EntityFrameworkConnectionString = string.Format(EntityFramewordConnectionStringWrapper, ConnectionString);
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountDbContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">
        /// The name or connection string.
        /// </param>
        private AccountDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// Creates an instance of the Wilco database context.
        /// </summary>
        /// <param name="connectionStringName">
        /// The optional name of connection string.
        /// </param>
        /// <returns>
        /// The database context.
        /// </returns>
        public static AccountDbContext Create(string connectionStringName)
        {
            return string.IsNullOrEmpty(connectionStringName)
                ? new AccountDbContext(EntityFrameworkConnectionString)
                : new AccountDbContext(string.Format(EntityFramewordConnectionStringWrapper, ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString));
        }

    }
}
