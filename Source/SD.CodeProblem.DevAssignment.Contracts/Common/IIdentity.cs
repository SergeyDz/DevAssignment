//-----------------------------------------------------------------------
// <copyright file="IIdentity.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Contracts.Common
{
    /// <summary>
    /// Identity item interface definition.
    /// </summary>
    /// <typeparam name="T">Identity field type.</typeparam>
    public interface IIdentity<T>
    {
        /// <summary>
        /// Gets or sets identity property definition.
        /// </summary>
        T Id { get; set; }
    }
}
