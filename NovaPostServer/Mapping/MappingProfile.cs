using AutoMapper;
using NovaPostServer.Data.Entities;
using NovaPostServer.Models.Area;
using NovaPostServer.Models.City;
using NovaPostServer.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaPostServer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AreaItemResponse, AreaEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Cities, opt => opt.Ignore());
            CreateMap<CityItemResponse, CityEntity>()
                .ForMember(dest => dest.AreaId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Area, opt => opt.Ignore())
                .ForMember(dest => dest.Departments, opt => opt.Ignore())
                .ForMember(dest => dest.AreaRef, opt => opt.MapFrom(src => src.Area))
                .ForMember(dest => dest.TypeDescription, opt => opt.MapFrom(src => src.SettlementTypeDescription));
            CreateMap<DepartmentItemResponse, DepartmentEntity>()
                .ForMember(dest => dest.CityId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.City, opt => opt.Ignore())
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.ShortAddress));
        }
    }
}
