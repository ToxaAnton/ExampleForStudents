using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleForStudents.Contracts;

namespace ExampleForStudents.Core.Abstractions
{
    public interface ICarsService
    {
        Task<ResponseWrapperDto<IEnumerable<CarDto>>> GetAsyncBySearchFilter(CarsSearchFilterDto filter);
        Task<ResponseWrapperDto<CarDto>> GetAsync(Guid id);
        Task<ResponseWrapperDto<CarDto>> CreateAsync(CarDto car);
        Task<ResponseWrapperDto<CarDto>> UpdateAsync(CarDto car);
        Task<ResponseWrapperDto<object>> DeleteAsync(Guid id);
    }
}