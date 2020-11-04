
namespace Vehicles
{
    class Factory
    {
        public static Vehicle Manufacture(string type, string name)
        {
            if (type == "car")
            {
                Car newVehicle = new Car(name);
                System.Console.WriteLine($"New {newVehicle} was manufactured");
                return newVehicle;
            }
            else if (type == "bus")
            {
                Bus newVehicle = new Bus(name);
                System.Console.WriteLine($"New {newVehicle} was manufactured");
                return newVehicle;
            }
            else if (type == "truck")
            {
                Truck newVehicle = new Truck(name);
                System.Console.WriteLine($"New {newVehicle} was manufactured");
                return newVehicle;
            }
            System.Console.WriteLine($"Factory could not manufacture a {type}");
            return null;         
        }
    }
}
