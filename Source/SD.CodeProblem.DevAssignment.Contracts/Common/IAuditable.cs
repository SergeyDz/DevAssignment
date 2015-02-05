//-----------------------------------------------------------------------
// <copyright file="IAuditable.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Contracts.Common
{
    using System;

    /// <summary>
    /// Auditable interface definition.
    /// </summary>
    /// <typeparam name="T">Identity type definition.</typeparam>
    public interface IAuditable<T> : IIdentity<T>
    {
        /// <summary>
        /// Gets or sets Created By principal.
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets Created By principal.
        /// </summary>
        DateTime? UpdatedOn { get; set; }
    }
}
