using System;

namespace Vehicles
{
    abstract class Vehicle
    {
        private static int NumberOfVehicles;
        protected readonly int VehicleID;
        protected string Name;

        public abstract void Travel(double distance);

        protected Vehicle()
        {
            ++NumberOfVehicles;
            VehicleID = NumberOfVehicles;
        }

        public abstract override string ToString();

        public virtual void Beep()
        {
            Console.WriteLine($"Vehicle {Name} honks!");
        }
    }

    class Car : Vehicle
    {  

        public Car(string name)
        {
            Name = name;
        }

        public override void Travel(double distance)
        {
            Console.WriteLine($"Car {Name} traveled {distance} km.");
        }

        public override string ToString()
        {
            return $"Car ({VehicleID}) {{{Name}}}";
        }

        public new void Beep()
        {
            Console.WriteLine($"Vehicle {Name} beeps!");
        }
    }

    class Bus : Vehicle
    {        
        public static readonly uint passengerLimit = 42;
        private uint Passengers;

        public Bus(string name)
        {
            Passengers = 0;
            Name = name;
        }

        public override void Travel(double distance)
        {
            Console.WriteLine($"Bus {Name} traveled {distance} km with {Passengers} passengers.");
        }

        public bool SetPassengerCount(uint count)
        {
            if (count > 42) return false;
            Passengers = count;
            return true;
        }

        public uint PassengerCount()
        {
            return Passengers;
        }

        public override string ToString()
        {
            return $"Bus ({VehicleID}) {{{Name}}} [ {Passengers}/42 ]";
        }

        public override void Beep()
        {
            Console.WriteLine($"Bus {Name} beeps!");
        }
    }

    class Truck : Vehicle
    {
        
        public static readonly uint capacity = 2500;
        private double currentLoad;

        public Truck(string name)
        {
            currentLoad = 0;
            Name = name;
        }

        public override void Travel(double distance)
        {
            Console.WriteLine($"Truck {Name} traveled {distance} km with load of {currentLoad} kg.");
        }

        public bool SetLoad(double load)
        {
            if (load > 2500 || load < 0) return false;
            currentLoad = load;
            return true;
        }

        public double Load()
        {
            return currentLoad;
        }

        public override string ToString()
        {
            return $"Truck ({VehicleID}) {{{Name}}} [ {currentLoad}/2500 kg ]";
        }

        public override void Beep()
        {
            Console.WriteLine($"Truck {Name} beeps with loud trumpet!");
        }
    }
}
