using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Models.Queries;
using SuperMarket.API.Extensions;
using SuperMarket.API.Resources;

namespace SuperMarket.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Category, CategoryResource>();

            //This syntax tells AutoMapper to use the new extension method to convert our
            //EUnitOfMeasurement value into a string containing its description.
            CreateMap<Product,ProductResource>()
                .ForMember(src=>src.UnitOfMeasurement,
                    opt => opt.MapFrom(src => src.UnitOfMeasurement.ToDescriptionString()));

            CreateMap<QueryResult<Product>, QueryResultResource<ProductResource>>();
        }
    }
}
