using AutoMapper;
using FormulaOneSite.Dtos;
using FormulaOneSite.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaOneSite.Profiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            CreateMap<PostModel,PostReadDTO>();
            CreateMap<PostCreateDTO, PostModel>().ForMember(dest=>dest.PhotoSrc,
                opts=>opts.MapFrom(src=> config.GetSection("DefaultImage").Value));
        }
    }
}
