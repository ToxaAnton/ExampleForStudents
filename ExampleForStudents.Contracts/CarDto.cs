using System;
using System.Text.Json.Serialization;
using ExampleForStudents.Contracts.Enums;

namespace ExampleForStudents.Contracts
{
    public class CarDto
    {
        [JsonIgnore] public Guid Id { get; set; }
        public Model Model { get; set; }
        public Brand Brand { get; set; }
        public string Name { get; set; }
        public int Mileage { get; set; }
        public double Price { get; set; }
        public int YearCreated { get; set; }
    }
}