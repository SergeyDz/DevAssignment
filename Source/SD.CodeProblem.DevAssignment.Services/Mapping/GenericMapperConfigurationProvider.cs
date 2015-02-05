//-----------------------------------------------------------------------
// <copyright file="GenericMapperConfigurationProvider.cs" company="SD">
//     Copyright (c) 2015. All rights reserved.
// </copyright>
// <author>Sergey Dzyuban</author>
//-----------------------------------------------------------------------
namespace SD.CodeProblem.DevAssignment.Services.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.Mappers;

    /// <summary>
    /// Generic mapper provider with Profile type-argument.
    /// </summary>
    /// <typeparam name="T">Mapper profile type.</typeparam>
    public class GenericMapperConfigurationProvider<T> : ConfigurationStore where T : Profile, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericMapperConfigurationProvider"/> class.
        /// </summary>
        public GenericMapperConfigurationProvider() : base(new TypeMapFactory(), MapperRegistry.Mappers)
        {
            this.AddProfile<T>();
        }
    }
}
