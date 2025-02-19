using FactoryMethod.Creator;
using FactoryMethod.ConcreteCreator;
using FactoryMethod.Product;
using System;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            SodaFactory sodaFactory = null;

            Console.Write("Hello! We have soda in cans and bottles... Which one would you like? ");
            string consoleInput = Console.ReadLine()!;
            Console.Write("What flavor? ");
            string flavor = Console.ReadLine()!;
            Console.Write("How many would you like? ");
            int quantity = int.Parse(Console.ReadLine()!);

            switch (consoleInput.ToLower())
            {
                case "bottle":
                    sodaFactory = new BottleFactory("2L", flavor, quantity, 10);
                    break;
                case "can":
                    sodaFactory = new CanFactory("300ml", flavor, quantity, 5);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select either 'bottle' or 'can'.");
                    return;
            }

            // Call the concrete creator
            // The product will be created from it
            Soda soda = sodaFactory.RequestSoda();

            Console.WriteLine("\nYour order details are as follows:\n");
            Console.WriteLine("Soda in {0}\nSize: {1}\nFlavor: {2}\nQuantity: {3}\nTotal: {4}",
                soda.ContainerType, soda.ContainerSize, soda.SodaFlavor, soda.SodaQuantity, soda.TotalValue);
        }

    }
}