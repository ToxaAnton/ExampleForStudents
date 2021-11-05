using System;
using ExampleForStudents.Domain.Enums;

namespace ExampleForStudents.Domain
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Model Model { get; set; }
        public Brand Brand { get; set; }
        public double Price { get; set; }
        public int Mileage { get; set; }
        public int YearCreated { get; set; }
    }
}