using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleForStudents.Domain;
using ExampleForStudents.Domain.Enums;
using ExampleForStudents.Domain.Interfaces;

namespace ExampleForStudents.Infrastructure.Repositories
{
    public class CarsRepository: ICarsRepository
    {
        private readonly IList<Car> _cars = new List<Car>
        {
            new Car
            {
                Id = Guid.NewGuid(),
                Brand = Brand.Audi,
                Model = Model.Sedan,
                Mileage = 3000,
                Price = 4000.50,
                Name = "Audi A3",
                YearCreated = 2010
            },
            new Car
            {
                Id = new Guid(),
                Brand = Brand.Toyota,
                Mileage = 1000,
                Model = Model.Universal,
                Name = "Toyota Camry",
                Price = 10.000,
                YearCreated = 2014
            },
            new Car
            {
                Id = new Guid(),
                Brand = Brand.Ford,
                Mileage = 50000,
                Model = Model.Sedan,
                Name = "Ford Mustang",
                Price = 100.000,
                YearCreated = 1998
            }
        };

        public CarsRepository()
        {
            //initialize DbContext;
        }

        public async IAsyncEnumerable<Car> GetBySearchFilterAsync(CarsSearchFilter filter)
        {
            //implement here filter
            
            yield return _cars[0];
            await Task.Delay(100);
            
            yield return _cars[1];
            await Task.Delay(800);
            
            yield return _cars[2];
            await Task.Delay(400);
        }

        public async Task<Car> GetAsync(Guid id)
        {
            await Task.Delay(100);
            return _cars.FirstOrDefault(c => c.Id == id);
        }

        public async Task<Car> CreateAsync(Car car)
        {
            _cars.Add(car);
            await Task.Delay(200);
            return _cars.Single(c => c.Id == car.Id);
        }

        public async Task<Car> UpdateAsync(Car car)
        {
            var updated = _cars.Single(c => c.Id == car.Id);
            updated.Brand = car.Brand;
            updated.Mileage = car.Mileage;
            updated.Model = car.Model;
            updated.Name = car.Name;
            updated.YearCreated = car.YearCreated;

            await Task.Delay(200);

            return updated;
        }

        public async Task DeleteAsync(Car car)
        {
            _cars.Remove(car);
            await Task.Delay(100);
        }

        public async Task DeleteAsync(Guid id)
        {
            var toDelete = _cars.SingleOrDefault(c => c.Id == id);
            _cars.Remove(toDelete);
            await Task.Delay(100);
        }
    }
}