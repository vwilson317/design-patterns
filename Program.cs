using System;
using System.Collections.Generic;
using System.Linq;

namespace design_patterns
{
    public class Node
    {
        public Node()
        {
            IsVisted = false;
        }
        public bool IsVisted { get; set; }
        public Node[] Nodes { get; set; }
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            FactoryPattern();

            var array = Enumerable.Range(1, 10).ToList();
            var random = new Random();
            var queue = new Queue<Node>();
            // array.ForEach(x => queue.Enqueue(
            //     new Node { IsVisted = false, Name = x.ToString() }));

            // 1 => 2,
            // 1 => 3,
            // 3 => 4,
            // 2 => 5, 6, 7
            // 7 => 8,
            // 8 => 9,
            // 8 => 10



            var tree = new Node
            {
                Name = "1",
                Nodes = new[]{
                    new Node{
                        Name = "2",
                        Nodes = new []{
                            new Node{Name = "5"},
                            new Node{Name = "6"},
                            new Node{Name = "7",
                            Nodes = new []{
                                new Node{Name= "8",
                                Nodes = new []{
                                    new Node{Name= "9"},
                                    new Node{Name= "10"}
                                }}
                            }}
                        }
                    },
                    new Node{
                        Name = "3",
                        Nodes = new []{
                            new Node {
                                Name = "4"
                            }
                        }
                    }
                }
            };

            queue.Enqueue(tree);

            var actualValBF = BreadthFirst(queue, string.Empty);
            
            var stack = new Stack<Node>();
            stack.Push(tree);
            var actualValDF = DeepthFirst(stack, string.Empty);
            var expectedBF = "1,2,3,5,6,7,4,8,9,10";
            var expectedDF = "5,6,9,10,8,7,2,4,3,1";
            var result = actualValBF == expectedBF;


            Console.WriteLine(result);
        }

        public static string BreadthFirst(Queue<Node> visited, string output)
        {
            Node currentNode = null;
            visited.TryDequeue(out currentNode);
            if (currentNode != null)
            {
                //first one
                if (output == string.Empty)
                {
                    output = currentNode.Name;
                }
                else
                {
                    output += $",{currentNode.Name}";
                }
                if (currentNode.Nodes != null)
                {
                    currentNode.Nodes.ToList().ForEach(x => visited.Enqueue(x));
                }
                output = BreadthFirst(visited, output);
            }

            return output;
        }

        private static void FactoryPattern()
        {
            var car = Factory.Create<Car>();
            var ninjaMoterBike = Factory.Create<Ninja>();
            var birdScooter = Factory.Create<Bird>();

            var vehicles = new[]{
                car,
                ninjaMoterBike,
                birdScooter
            }.ToList();

            vehicles.ForEach(x => Console.WriteLine($"Type: {x.GetType()} Wheel#: {x.Wheels}"));
        }
    }

    //singleton
    public class Time
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class Universe
    {
        private readonly Time _time;

        public Universe()
        {
            _time = _time ?? new Time();
        }
    }

    public class Car : Vehicle, ICar
    {
        public Car()
        {
            Wheels = 4;
        }
    }
    public interface ICar : IVehicle
    {

    }

    public class Ninja : Vehicle, IMotorBike
    {
        public Ninja()
        {
            Wheels = 2;
        }

    }
    public interface IMotorBike : IVehicle
    {

    }

    public class Bird : Vehicle, IScooter
    {
        public Bird()
        {
            Wheels = 2;
        }

    }
    public interface IScooter : IVehicle
    {

    }

    public interface ITrain : IVehicle
    {

    }

    public abstract class Vehicle : IVehicle
    {
        public int Wheels { get; internal set; }
    }

    public interface IVehicle
    {
        int Wheels { get; }
    }

    public static class Factory
    {
        public static IVehicle Create<TVehicle>()
        where TVehicle : new()
        {
            return new TVehicle() as IVehicle;
        }
    }
}
