using System;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] intsToCompress = new int[] {10,15, 20, 25, 30, 12, 34};
            int totalValue;

            DateTime startTime = DateTime.Now;

            totalValue = intsToCompress.Sum();

            Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            Console.WriteLine(totalValue);

            startTime = DateTime.Now;

            totalValue = GetSum(intsToCompress:intsToCompress);

            Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            Console.WriteLine(totalValue);
        }
        static private int GetSum(int[] intsToCompress)
        {
            int totalValue = 0;
            for (int i = 0; i < intsToCompress.Length; i++)
            {
                totalValue += intsToCompress[i];
            }

            return totalValue;
        }
    }
}