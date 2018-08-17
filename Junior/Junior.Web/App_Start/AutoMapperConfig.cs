using AutoMapper;
using Junior.SharedModels.DomainModels;
using Junior.SharedModels.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Junior.Web
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Compound, CompoundDto>();

                config.CreateMap<CompoundElement, ElementDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ElementId))
                    .ForMember(dest => dest.CompoundElementId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.ElementQuantity));

                config.CreateMap<ElementDto, CompoundElement>()
                    .ForMember(dest => dest.ElementId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.ElementQuantity, opt => opt.MapFrom(src => src.Quantity))
                    .ForMember(dest => dest.Id, opt => opt.Ignore());

                config.CreateMap<CompoundElementDto, Compound>();
            });
        }
    }
}