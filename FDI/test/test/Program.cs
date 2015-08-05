using System;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            demo(8);
            Console.ReadLine();
        }

        static void demo(int n)
        {
            for (var iC = 0; iC < n; iC++)
            {
                for (var iC3 = n-1; iC3 < iC; iC3--)
                    Console.Write(' ');
                for (var iC2 = 0; iC2 <= iC*2+1; iC2++)
                {
                    Console.Write('*');
                }
                Console.WriteLine();
            }
        }
    }
}
