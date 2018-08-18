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

                config.CreateMap<Element, ElementDto>()
                   .ForMember(dest => dest.BoilingTemperature, opt => opt.MapFrom(src => src.BoilingTemperatureK));

                config.CreateMap<CompoundElement, ElementPartialDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ElementId))
                    .ForMember(dest => dest.CompoundElementId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.ElementQuantity));

                config.CreateMap<ElementPartialDto, CompoundElement>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.ElementId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.ElementQuantity, opt => opt.MapFrom(src => src.Quantity));

                config.CreateMap<CompoundElementPartialDto, Compound>();
            });
        }
    }
}