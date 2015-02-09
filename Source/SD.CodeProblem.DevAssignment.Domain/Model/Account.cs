//-----------------------------------------------------------------------
// <copyright file="Account.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Domain.Model
{
    using SD.CodeProblem.DevAssignment.Contracts.Common;

    /// <summary>
    /// Partial domain model file definition.
    /// </summary>
    public partial class Account : IAuditable<int>
    {
        /// <summary>
        /// Gets or sets total count of orders per each account.
        /// </summary>
        public int OrdersCount { get; set; }
    }
}
