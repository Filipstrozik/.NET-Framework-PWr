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
        public IActionResult Set(int setNumber)
        {
            n = setNumber;
            ViewBag.Message = $"Set random range  from 0 to {setNumber - 1}" + "Go to /Draw to generate random value.";
            return View("Set");
        }

        public IActionResult Draw()
        {
            randValue = rnd.Next(0, n);
            roundCounter = 1;
            ViewBag.Message = $"Generated rand value [0,{n - 1}]" + "guessing at /Guess,<n>";
            return View("Draw");
        }
        public IActionResult Guess(int guessedNumber)
        {
            ViewBag.Choice = guessedNumber;
            if (guessedNumber == randValue)
            {
                ViewBag.Message = "Start again at set or draw";
                ViewBag.Round = roundCounter;
                ViewBag.Clue = 0;
            } 
            else if( guessedNumber > randValue)
            {
                ViewBag.Clue = 1;
                ViewBag.Round = roundCounter++;
            } 
            else
            {
                ViewBag.Clue = -1;
                ViewBag.Round = roundCounter++;
            }
            return View("Guess");
        }
    }
}
