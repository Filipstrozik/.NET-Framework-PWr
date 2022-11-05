using System;

namespace LabZad2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SecondMaxNumber();
        }

        private static void SecondMaxNumber()
        {
            Console.WriteLine("Insert n:  (n => 1)");
           
            int n = Convert.ToInt32(Console.ReadLine());
            int biggest = int.MinValue, secondBiggest = int.MinValue;
            int i = 0;
            while (i < n)
            {
                int elem = ReadTillWhite();
                if (elem > biggest)
                {
                    secondBiggest = biggest;
                    biggest = elem;
                }
                else if(elem > secondBiggest && elem < biggest)
                {
                    secondBiggest = elem;
                }
                i++;
            }
            if (secondBiggest.Equals(int.MinValue))
            {
                Console.WriteLine("no solution");
            }
            else {
                Console.WriteLine($"second biggest: {secondBiggest}");
            }
            

        }

        private static int ReadTillWhite()
        {
            bool isNegative = false;
            int digits = 0;
            int number = 0;
            char ch = Convert.ToChar(Console.Read());
            if (ch == '-')
            {
                isNegative = true;
                ch = Convert.ToChar(Console.Read());
            }
            while (!char.IsWhiteSpace(ch))
            {
                number = (int)char.GetNumericValue(ch) + number * 10;
                ch = Convert.ToChar(Console.Read());
            }
            if (isNegative)
            {
                number *= -1;
            }
            return number;
        }
    }
}
