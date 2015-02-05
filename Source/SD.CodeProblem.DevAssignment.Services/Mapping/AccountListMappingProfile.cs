//-----------------------------------------------------------------------
// <copyright file="AccountListMappingProfile.cs" company="SD">
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

    using data = SD.CodeProblem.DevAssignment.Data.Model;
    using domain = SD.CodeProblem.DevAssignment.Domain.Model;

    /// <summary>
    /// Account profiler for list with selected properties.
    /// </summary>
    public class AccountListMappingProfile : Profile
    {
        /// <summary>
        /// Profiler name.
        /// </summary>
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        /// <summary>
        /// Configure mapper at runtime.
        /// </summary>
        protected override void Configure()
        {
            this.CreateMap<data.User, domain.User>();

            this.CreateMap<data.Account, domain.Account>().ConvertUsing((value) =>
            {
                domain.Account result = new domain.Account();
                result.Id = value.Id;
                result.Name = value.Name;
                result.Code = value.Code;
                result.CreatedOn = value.CreatedOn;
                result.CreatedBy = Mapper.Map<data.User, domain.User>(value.CreatedBy);
                return result;
            });
        }
    }
}
