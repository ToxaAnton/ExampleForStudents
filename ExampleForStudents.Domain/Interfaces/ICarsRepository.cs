using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleForStudents.Domain.Interfaces
{
    public interface ICarsRepository
    {
        IAsyncEnumerable<Car> GetBySearchFilterAsync(CarsSearchFilter filter);
        Task<Car> GetAsync(Guid id);
        Task<Car> CreateAsync(Car car);
        Task<Car> UpdateAsync(Car car);
        Task DeleteAsync(Car car);
        Task DeleteAsync(Guid id);
    }
}