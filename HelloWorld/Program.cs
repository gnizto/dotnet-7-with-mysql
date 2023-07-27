using System;

namespace HelloWorld
{
    public class Computer
    {
        // field example
        // private string _motherboard;
        public string Motherboard { get; set; }
        public int CPUCores { get; set; }
        public bool HasWifi { get; set; }
        public bool HasLTE { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string VideoCard { get; set; }

        public Computer()
        {
            if (Motherboard == null) Motherboard = "";
            if (VideoCard == null) VideoCard = "";
        }
    }
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