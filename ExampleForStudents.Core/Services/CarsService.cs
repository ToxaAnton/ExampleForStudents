using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ExampleForStudents.Contracts;
using ExampleForStudents.Core.Abstractions;
using ExampleForStudents.Domain;
using ExampleForStudents.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ExampleForStudents.Core.Services
{
    public class CarsService : ICarsService
    {
        private readonly ILogger _logger; //in case if you want to log something
        private readonly IMapper _mapper;
        private readonly ICarsRepository _repository;

        public CarsService(ICarsRepository repository, IMapper mapper, ILogger<CarsService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ResponseWrapperDto<IEnumerable<CarDto>>> GetAsyncBySearchFilter(CarsSearchFilterDto filter)
        {
            var cars = new List<CarDto>();
            await foreach (var car in _repository.GetBySearchFilterAsync(_mapper.Map<CarsSearchFilter>(filter)))
                cars.Add(_mapper.Map<CarDto>(car));

            return new ResponseWrapperDto<IEnumerable<CarDto>>(cars);
        }

        public async Task<ResponseWrapperDto<CarDto>> GetAsync(Guid id)
        {
            var car = await _repository.GetAsync(id);
            return car is null
                ? new ResponseWrapperDto<CarDto>("Such car is absent") { StatusCode = HttpStatusCode.NotFound }
                : new ResponseWrapperDto<CarDto>(_mapper.Map<CarDto>(car));
        }

        public async Task<ResponseWrapperDto<CarDto>> CreateAsync(CarDto car)
        {
            //Search by some unique fields to find if such object 
            if (await _repository.GetAsync(car.Id) is not null)
                return new ResponseWrapperDto<CarDto>("Such item already exists")
                    { StatusCode = HttpStatusCode.BadRequest };
            var result = await _repository.CreateAsync(_mapper.Map<Car>(car));

            return new ResponseWrapperDto<CarDto>(_mapper.Map<CarDto>(result));
        }

        public async Task<ResponseWrapperDto<CarDto>> UpdateAsync(CarDto car)
        {
            if (await _repository.GetAsync(car.Id) is null)
                return new ResponseWrapperDto<CarDto>("Such Car does not exist")
                    { StatusCode = HttpStatusCode.NotFound };
            var result = await _repository.UpdateAsync(_mapper.Map<Car>(car));

            return new ResponseWrapperDto<CarDto>(_mapper.Map<CarDto>(result));
        }

        public async Task<ResponseWrapperDto<object>> DeleteAsync(Guid id)
        {
            if (await _repository.GetAsync(id) is null)
                return new ResponseWrapperDto<object>("Such Car does not exist")
                    { StatusCode = HttpStatusCode.NotFound };
            await _repository.DeleteAsync(id);

            return new ResponseWrapperDto<object>();
        }
    }
}