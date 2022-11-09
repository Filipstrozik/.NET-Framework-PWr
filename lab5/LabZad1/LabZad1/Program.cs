using System;
using System.Collections.Generic;

namespace LabZad1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Zadanie 1:");

            double a, b, c;

            a = Convert.ToDouble(Console.ReadLine());
            b = Convert.ToDouble(Console.ReadLine());
            c = Convert.ToDouble(Console.ReadLine());

            List<double> res =  SolveQuadraticEquation(a, b, c);
            if(res.Count == 0)
            {
                Console.WriteLine("Dla równania kwadratowego: {0}x^2 + ({1})x + ({2}) = 0." +
                    " nie istnieje rozwiazanie:", a, b, c);
            }
            else if(res.Count == 2)
            {
                Console.WriteLine($"Dla równania kwadratowego: {a}x^2 + ({b})x + ({c}) = 0." +
                    $" istnieja dwa rozwiązania: {res[0]} oraz {res[1]}");
            } 
            else if(res.Count == 1)
            {
                Console.WriteLine("Dla równania kwadratowego: {0}x^2 + ({1})x + ({2}) = 0." +
                    " istnjeje jedno rozwiązanie: {3}", a, b, c, res[0]);
            } 
            else if (res.Count == 3)
            {
                Console.WriteLine("Dla równania kwadratowego: {0}x^2 + ({1})x + ({2}) = 0." +
                    " nieskonczenie wiele rozwiązan :", a, b, c);
            }

        }

        private static List<double> SolveQuadraticEquation(double a, double b, double c)
        {

            if (a == 0 && b == 0 && c == 0) return new List<double> { 0, 0, 0 };
            if (a == 0 && b == 0 && c != 0) return new List<double>();
            List<double> result = new();
            double d = b * b - 4 * a * c, x1, x2;
            if(d == 0)
            {
                x1 = -b / (2.0 * a);
                result.Add(ReFormat(x1,5));
            }
            else if (d > 0)
            {
                x1 = (-b + Math.Sqrt(d)) / (2 * a);
                x2 = (-b - Math.Sqrt(d)) / (2 * a);
                result.Add(ReFormat(x1,5));
                result.Add(ReFormat(x2,5));
            }
            return result;
        }

        public static double ReFormat(double d, int digits)
        {
            if (d == 0) return 0;
            return Math.Round(d, digits - (int)Math.Floor(Math.Log10(Math.Abs(d))) - 1);
        }
    }
}
