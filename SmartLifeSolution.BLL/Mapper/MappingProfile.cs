using AutoMapper;
using SmartLifeSolution.DAL.Dto.Gateway;
using SmartLifeSolution.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Mapper
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
           CreateMap<GatewayDto, Gateway>(); 
        }
    }
}
