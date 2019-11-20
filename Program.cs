using System;
using System.Linq;

namespace design_patterns
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var car = Factory.Create<Car>();
            var ninjaMoterBike = Factory.Create<Ninja>();
            var birdScooter = Factory.Create<Bird>();

            var vehicles = new []{
                car,
                ninjaMoterBike,
                birdScooter
            }.ToList();

            vehicles.ForEach(x => Console.WriteLine($"Type: {x.GetType()} Wheel#: {x.Wheels}"));


        }
    }

    public class Car : Vehicle, ICar
    {
        public Car()
        {
            Wheels = 4;
        }
    }
    public interface ICar: IVehicle{

    }

    public class Ninja : Vehicle, IMotorBike
    {
        public Ninja()
        {
            Wheels = 2;
        }

    }
    public interface IMotorBike : IVehicle{

    }

    public class Bird : Vehicle, IScooter
    {
        public Bird()
        {
            Wheels = 2;
        }

    }
    public interface IScooter: IVehicle{

    }

    public interface ITrain: IVehicle{

    }

    public abstract class Vehicle : IVehicle
    {
        public int Wheels { get; internal set; }
    }

    public interface IVehicle {
        int Wheels {get;}
    }

    public static class Factory{
        public static IVehicle Create<TVehicle>()
        where TVehicle : new()
        {
            return new TVehicle() as IVehicle;
        }
    }
}
