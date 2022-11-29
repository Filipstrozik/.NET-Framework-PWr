using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab8.Controllers
{
    public class GameController : Controller
    {
        static int n = 0;
        static int randValue = 0;
        static int roundCounter = 1;
        static Random rnd = new Random();
        static List<int> history = new List<int>();
        public IActionResult Set(int setNumber)
        {
            n = setNumber;
            ViewBag.Message = $"Set random range  from 0 to {setNumber - 1} " + "Go to /Draw to generate random value.";
            history.Clear();
            return View("Set");
        }

        public IActionResult Draw()
        {
            randValue = rnd.Next(0, n);
            roundCounter = 1;
            ViewBag.Message = $"Generated rand value [0,{n - 1}] " + "guessing at /Guess,<n>";
            history.Clear();
            return View("Draw");
        }
        public IActionResult Guess(int guessedNumber)
        {
            history.Add(guessedNumber);
            ViewBag.History = history;
            ViewBag.Choice = guessedNumber;
            if (guessedNumber == randValue)
            {
                ViewBag.Message = "Correct!";
                ViewBag.Round = roundCounter;
                ViewBag.Clue = "correct";
            } 
            else if( guessedNumber > randValue)
            {
                ViewBag.Message = "Too Big!";
                ViewBag.Clue = "toobig";
                ViewBag.Round = roundCounter++;
            } 
            else
            {
                ViewBag.Message = "Too Low!";
                ViewBag.Clue = "toolow";
                ViewBag.Round = roundCounter++;
            }
            return View("Guess");
        }
    }
}
