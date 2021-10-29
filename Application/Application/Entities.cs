using System;
using System.Collections.Generic;

namespace Application
{
    public class Entity
    {
        public Guid Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }

    public class Vehicle : Entity
    {
        public Driver Driver { get; private set; }
    }

    public class Car : Vehicle
    {

    }

    public class Driver : Entity
    {
        public Driver()
        {
            Vehicles = new List<Vehicle>();
        }

        public List<Vehicle> Vehicles { get; set; }

        public void AddCar(Car car)
        {
            Vehicles.Add(car);
        }
    }
}
