
using System.Globalization;
using System.Text.Json;
using AutoMapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

            // File.WriteAllText("log.sql", sql);

            // using StreamWriter openFile = new("log.txt", append: true);

            // openFile.WriteLine(sql);
            // openFile.Close();

            // string fileText = File.ReadAllText("log.txt");

            string computersJson = File.ReadAllText("Computers.json");

            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase    
            };

            JsonSerializerSettings settings = new()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            IEnumerable<Computer>? computersNewtonsoft = JsonConvert
                .DeserializeObject<IEnumerable<Computer>>(computersJson);

            IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer
                .Deserialize<IEnumerable<Computer>>(computersJson, options);
            

            if (computersNewtonsoft == null) return;
            
            foreach (Computer computer in computersNewtonsoft)
            {
                string sql = @"INSERT INTO TutorialAppSchema.Computer (
                    Motherboard,
                    HasWifi,
                    HasLTE,
                    ReleaseDate,
                    Price,
                    VideoCard
                ) VALUES (
                         '" + EscapeSingleQuote(computer.Motherboard)
                    + "','" + computer.HasWifi
                    + "','" + computer.HasLTE
                    + "','" + computer.ReleaseDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                    + "','" + computer.Price.ToString("0.00", CultureInfo.InvariantCulture) // Using Globalization
                    + "','" + EscapeSingleQuote(computer.VideoCard)
                + "')";

                dapper.ExecuteSql(sql);
                // Console.WriteLine(computer.Motherboard);
            }

            string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonsoft, settings);

            File.WriteAllText("computersCopyNewtonsoft.json", computersCopyNewtonsoft);
            
            string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);

            File.WriteAllText("computersCopySystem.json", computersCopySystem);

            string computerSnake = File.ReadAllText("ComputersSnake.json");

            Mapper mapper = new Mapper(new MapperConfiguration((cfg) => {
                cfg.CreateMap<ComputerSnake, Computer>()
                    .ForMember(destination => destination.ComputerId, options =>
                        options.MapFrom(source => source.computer_id))
                    .ForMember(destination => destination.CPUCores, options =>
                        options.MapFrom(source => source.cpu_cores))
                    .ForMember(destination => destination.HasLTE, options =>
                        options.MapFrom(source => source.has_lte))
                    .ForMember(destination => destination.HasWifi, options =>
                        options.MapFrom(source => source.has_wifi))
                    .ForMember(destination => destination.Motherboard, options =>
                        options.MapFrom(source => source.motherboard))
                    .ForMember(destination => destination.VideoCard, options =>
                        options.MapFrom(source => source.video_card))
                    .ForMember(destination => destination.ReleaseDate, options =>
                        options.MapFrom(source => source.release_date))
                    .ForMember(destination => destination.Price, options =>
                        options.MapFrom(source => source.price));
            }));

            IEnumerable<ComputerSnake>? computersSnake = System.Text.Json.JsonSerializer
                .Deserialize<IEnumerable<ComputerSnake>>(computersJson);
            
            if (computersSnake != null)
            {
                IEnumerable<Computer> computerResult = mapper.Map<
                    IEnumerable<Computer>>(computersSnake);

                foreach(Computer computer in computerResult)
                {
                    Console.WriteLine(computer.Motherboard);
                }
            }

        }

        static string EscapeSingleQuote(string input)
        {
            string output = input.Replace("'","''");

            return output;
        }
    }
}