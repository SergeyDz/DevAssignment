//-----------------------------------------------------------------------
// <copyright file="IPrincipal.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Contracts.Common
{
    /// <summary>
    /// Security principal interface definition.
    /// </summary>
    /// <typeparam name="T">Identiity key filed type.</typeparam>
    public interface IPrincipal<T> : IIdentity<T>
    {
        /// <summary>
        /// Gets or sets principal name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets principal unique identifier.
        /// </summary>
        string Login { get; set; }

        /// <summary>
        /// Gets or sets principal active status key.
        /// </summary>
        bool? IsActive { get; set; }
    }
}
