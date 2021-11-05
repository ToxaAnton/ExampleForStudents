using AutoMapper;
using ExampleForStudents.Contracts;
using ExampleForStudents.Domain;
using ExampleForStudents.Domain.Interfaces;

namespace ExampleForStudents.Core.MapperProfiles
{
    public class CarMapperProfile : Profile
    {
        public CarMapperProfile()
        {
            CreateMap<CarDto, Car>();
            CreateMap<Car, CarDto>();
            CreateMap<CarsSearchFilterDto, CarsSearchFilter>();
        }
    }
}