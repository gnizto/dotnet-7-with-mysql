using System;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] intsToCompress = new int[] {10,15, 20, 25, 30, 12, 34};

            DateTime startTime = DateTime.Now;
            int totalValue = 0;
            for (int i = 0; i < intsToCompress.Length; i++)
            {
                totalValue += intsToCompress[i];
            }
            Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            Console.WriteLine(totalValue);

            startTime = DateTime.Now;
            totalValue = 0;
            foreach(int intForCompression in intsToCompress)
            {
                totalValue += intForCompression;
            }

            Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            Console.WriteLine(totalValue);
            
            startTime = DateTime.Now;
            totalValue = 0;
            int index = 0;
            while(index < intsToCompress.Length)
            {
                totalValue += intsToCompress[index];
                index++;
            }
            
            Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            Console.WriteLine(totalValue);
            
            startTime = DateTime.Now;
            totalValue = 0;
            index = 0;
    
            do{
                totalValue += intsToCompress[index];
                index++;
            }
            while(index < intsToCompress.Length);
            
            Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            Console.WriteLine(totalValue);

            startTime = DateTime.Now;
            // totalValue = 0;
            totalValue = intsToCompress.Sum();
            Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
            Console.WriteLine(totalValue);
        }
    }
}