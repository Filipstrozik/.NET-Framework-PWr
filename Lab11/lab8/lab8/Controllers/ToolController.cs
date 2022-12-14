using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab8.Controllers
{
    public class ToolController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Solve(int a, int b, int c)
        {
            List<double> res = SolveQuadraticEquation(a, b, c);
            ViewBag.a = a;
            //ViewBag.Ats = $"{a}"; 
            ViewBag.b = b;
            ViewBag.Bts = b >= 0 ? $"+{b}" : $"{b}";
            ViewBag.c = c;
            ViewBag.Cts = c >= 0 ? $"+{c}" : $"{c}";
            ViewBag.resultList = res;
            ViewBag.title = "Solve Quadratic Equation";
            if (res.Count == 0)
            {
                ViewBag.Message = "No results!";
                ViewBag.Type = "noresults";
            }
            else if (res.Count == 2)
            {
                ViewBag.Message = "Two results!";
                ViewBag.Type = "tworesults";
            }
            else if (res.Count == 1)
            {
                ViewBag.Message = "One result!";
                ViewBag.Type = "oneresult";
            }
            else if (res.Count == 3)
            {
                ViewBag.Message = "Infinity results!";
                ViewBag.Type = "toomanyresults";
            }

            return View("Solve");
        }

        private static List<double> SolveQuadraticEquation(double a, double b, double c)
        {

            if (a == 0 && b == 0 && c == 0) return new List<double> { 0, 0, 0 };
            if (a == 0 && b == 0 && c != 0) return new List<double>();
            List<double> result = new();
            double d = b * b - 4 * a * c, x1, x2;
            if (d == 0)
            {
                x1 = -b / (2.0 * a);
                result.Add(ReFormat(x1, 5));
            }
            else if (d > 0)
            {
                x1 = (-b + Math.Sqrt(d)) / (2 * a);
                x2 = (-b - Math.Sqrt(d)) / (2 * a);
                result.Add(ReFormat(x1, 5));
                result.Add(ReFormat(x2, 5));
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
