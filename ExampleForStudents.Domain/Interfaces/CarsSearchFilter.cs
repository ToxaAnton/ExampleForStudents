using ExampleForStudents.Domain.Enums;

namespace ExampleForStudents.Domain.Interfaces
{
    public class CarsSearchFilter
    {
        public string Name { get; set; }
        public Model Model { get; set; }
        public Brand Brand { get; set; }
        public double Price { get; set; }
        public int Mileage { get; set; }
        public int YearCreated { get; set; }
    }
}