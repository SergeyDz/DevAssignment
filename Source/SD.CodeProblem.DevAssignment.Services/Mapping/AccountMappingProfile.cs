//-----------------------------------------------------------------------
// <copyright file="AccountMappingProfile.cs" company="SD">
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
    /// AutoMapp profiler to configure mapping for full Account objects tree load.
    /// </summary>
    public class AccountMappingProfile : Profile
    {
        /// <summary>
        /// Profile name.
        /// </summary>
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        /// <summary>
        /// Configure profile at runtime.
        /// </summary>
        protected override void Configure()
        {
            this.CreateMap<data.Account, domain.Account>();
            this.CreateMap<data.Order, domain.Order>();
            this.CreateMap<data.User, domain.User>();

            this.CreateMap<domain.Account, data.Account>();
            this.CreateMap<domain.Order, data.Order>();
            this.CreateMap<domain.User, data.User>();
        }
    }
}
