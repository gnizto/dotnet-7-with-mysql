
using System.Globalization;
using System.Text.Json;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();

            DataContextDapper dapper = new DataContextDapper(config);

            // string sql = @"INSERT INTO TutorialAppSchema.Computer (
            //     Motherboard,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            // ) VALUES (
            //          '" + myPC.Motherboard 
            //     + "','" + myPC.HasWifi
            //     + "','" + myPC.HasLTE
            //     + "','" + myPC.ReleaseDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            //     + "','" + myPC.Price.ToString("0.00", CultureInfo.InvariantCulture) // Using Globalization
            //     + "','" + myPC.VideoCard
            // + "')";

            // File.WriteAllText("log.sql", sql);

            // using StreamWriter openFile = new("log.txt", append: true);

            // openFile.WriteLine(sql);
            // openFile.Close();

            // string fileText = File.ReadAllText("log.txt");

            string computersJson = File.ReadAllText("Computers.json");

            // JsonSerializerOptions options = new()
            // {
            //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase    
            // };

            // IEnumerable<Computer>? computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);
            IEnumerable<Computer>? computers = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            if (computers == null) return;
            
            foreach (Computer computer in computers)
            {
                Console.WriteLine(computer.Motherboard);
            }
        }
    }
}