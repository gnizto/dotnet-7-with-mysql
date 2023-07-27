using System;
using HelloWorld.Models;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Computer myPC = new Computer()
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060"
            };

            Console.WriteLine(myPC.ReleaseDate);
        }
    }
}