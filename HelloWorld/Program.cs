using System;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string myString = "Hello. Every. Word. Has. A. Period.";
            string[] myStringSplit = myString.Split(". ");

            foreach(string part in myStringSplit)
            {
                Console.WriteLine(part);
            }
        }
    }
}