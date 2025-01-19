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
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CityItemResponse, CityEntity>()
                .ForMember(dest => dest.AreaId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<DepartmentItemResponse, DepartmentEntity>()
                .ForMember(dest => dest.CityId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore()); //ігнорую id для коректного запису в базу
        }
    }
}
