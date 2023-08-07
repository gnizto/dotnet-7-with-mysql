
using System.Globalization;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Extensions.Configuration;

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

            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES (
                     '" + myPC.Motherboard 
                + "','" + myPC.HasWifi
                + "','" + myPC.HasLTE
                + "','" + myPC.ReleaseDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                + "','" + myPC.Price.ToString("0.00", CultureInfo.InvariantCulture) // Using Globalization
                + "','" + myPC.VideoCard
            + "')";

            File.WriteAllText("log.sql", sql);

            using StreamWriter openFile = new("log.txt", append: true);

            openFile.WriteLine(sql);
            openFile.Close();

            string fileText = File.ReadAllText("log.txt");

            Console.WriteLine(fileText);
        }
    }
}