
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
            DataContextEF entityFramework = new DataContextEF();

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

            entityFramework.Add(myPC);
            entityFramework.SaveChanges();

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
                Computer.ComputerId,
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
                Console.WriteLine("'" + computer.ComputerId
                + "','" + computer.Motherboard 
                + "','" + computer.HasWifi
                + "','" + computer.HasLTE
                + "','" + computer.ReleaseDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                + "','" + computer.Price.ToString("0.00", CultureInfo.InvariantCulture) // Using Globalization
                + "','" + computer.VideoCard + "'");
            }

            IEnumerable<Computer>? computersEF = entityFramework.Computer?.ToList<Computer>();

            if (computersEF != null)
            {
                foreach (Computer computer in computersEF)
                {
                    Console.WriteLine("'" + computer.ComputerId
                    + "','" + computer.Motherboard 
                    + "','" + computer.HasWifi
                    + "','" + computer.HasLTE
                    + "','" + computer.ReleaseDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                    + "','" + computer.Price.ToString("0.00", CultureInfo.InvariantCulture) // Using Globalization
                    + "','" + computer.VideoCard + "'");
                }
            }

        }
    }
}