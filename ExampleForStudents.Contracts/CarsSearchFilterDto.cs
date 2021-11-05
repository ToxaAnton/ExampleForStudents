using System.Text.Json.Serialization;
using ExampleForStudents.Contracts.Enums;

namespace ExampleForStudents.Contracts
{
    public class CarsSearchFilterDto
    {
        public string Name { get; set; }
        public Model Model { get; set; }
        public Brand Brand { get; set; }
        public double Price { get; set; }
        public int Mileage { get; set; }
        public int YearCreated { get; set; }
    }
}