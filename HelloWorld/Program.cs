
using System.Globalization;
using HelloWorld.Data;
using HelloWorld.Models;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataContextDapper dapper = new DataContextDapper();

            string sqlCommand = "SELECT GETDATE()";

            // Using Dapper
            DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);

            Console.WriteLine(rightNow);

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

            Console.WriteLine(sql);

            int result = dapper.ExecuteSqlWithRowCount(sql);

            Console.WriteLine(result);

            Console.WriteLine(myPC.ReleaseDate);

            string sqlSelect = @"
            SELECT 
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
            FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            foreach (Computer computer in computers)
            {
                Console.WriteLine("'" + myPC.Motherboard 
                + "','" + myPC.HasWifi
                + "','" + myPC.HasLTE
                + "','" + myPC.ReleaseDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                + "','" + myPC.Price.ToString("0.00", CultureInfo.InvariantCulture) // Using Globalization
                + "','" + myPC.VideoCard + "'");
            }
        }
    }
}