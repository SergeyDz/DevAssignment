using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SD.CodeProblem.DevAssignment.Services.Mapping;

namespace DevAssignment.WebApi.App_Start
{
    public class MapperConfig
    {
        public static void ConfigureMappings()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<AccountListMappingProfile>());
            //Mapper.Initialize(cfg => cfg.AddProfile<AccountMappingProfile>());
        }
    }
}