using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Models.Queries;
using SuperMarket.API.Resources;

namespace SuperMarket.API.Mapping
{
    public class ResourceToModelProfile:Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCategoryResource, Category>();
            CreateMap<SaveProductResource, Product>().ForMember(src => src.UnitOfMeasurement, 
                opt => opt.MapFrom(src => (EUnitOfMeasurement)src.UnitOfMeasurement)); ;
            CreateMap<ProductsQueryResource, ProductsQuery>();
            CreateMap<UserForRegisterResource, User>();
        }
    }
}
