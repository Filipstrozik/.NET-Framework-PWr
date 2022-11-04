using System;
using System.Collections.Generic;

namespace LabZad1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zadanie 1:");

            int a, b, c;

            a = Convert.ToInt32(Console.ReadLine());
            b = Convert.ToInt32(Console.ReadLine());
            c = Convert.ToInt32(Console.ReadLine());

            List<double> res =  SolveQuadraticEquation(a, b, c);
            
            if(res.Count == 2)
            {
                Console.WriteLine($"Dla równania kwadratowego: {a}x^2 + ({b})x + ({c}) = 0." +
                    $" istnieja dwa rozwiązania: {res[0]} oraz {res[1]}");
            } 
            else if(res.Count == 1)
            {
                Console.WriteLine("Dla równania kwadratowego: {0}x^2 + ({1})x + ({2}) = 0." +
                    " istnjeje jedno rozwiązanie: {3}", a, b, c, res[0]);
            } 
            else
            {

            }

        }

        private static List<double> SolveQuadraticEquation(int a, int b, int c)
        {
            List<double> result = new();
            double d = b * b - 4 * a * c, x1, x2;
            if(d == 0)
            {
                x1 = -b / (2.0 * a);
                result.Add(Math.Round(x1,5));
                //formatowanie zlozone
            }
            else if (d > 0)
            {
                x1 = (-b + Math.Sqrt(d)) / (2 * a);
                x2 = (-b - Math.Sqrt(d)) / (2 * a);
                result.Add(Math.Round(x1,5));
                result.Add(Math.Round(x2,5));
                //interpolacja lancucha znakow
            }
            return result;
        }
    }
}
