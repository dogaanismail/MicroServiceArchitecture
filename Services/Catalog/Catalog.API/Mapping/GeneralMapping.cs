using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Models;

namespace Catalog.API.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();
        }
    }
}
