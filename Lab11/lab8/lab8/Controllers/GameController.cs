using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab8.Controllers
{
    public class GameController : Controller
    {

        //private int randValue = 0; // trzymac w sesji
        static Random rnd = new Random();

        public IActionResult Set(int setNumber)
        {
            //n = setNumber;
            HttpContext.Session.SetInt32("n", setNumber);
            ViewBag.Message = $"Set random range  from 0 to {setNumber - 1} " + "Go to /Draw to generate random value.";
            return View("Set");
        }

        public IActionResult Draw()
        {
            int randValue = rnd.Next(0, (int)HttpContext.Session.GetInt32("n"));
            HttpContext.Session.SetInt32("randValue", randValue);
            HttpContext.Session.SetInt32("counter", 0);
            ViewBag.Message = $"Generated rand value [0,{(int)HttpContext.Session.GetInt32("n") - 1}] " + "guessing at /Guess,<n>";
            HttpContext.Session.SetString("history", "");

            return View("Draw");
        }
        public IActionResult Guess(int guessedNumber)
        {
            ViewBag.Choice = guessedNumber;
            int randValue = (int)HttpContext.Session.GetInt32("randValue");

            string historyOfGuesses = HttpContext.Session.GetString("history");
            historyOfGuesses = historyOfGuesses + " " + guessedNumber;
            HttpContext.Session.SetString("history", historyOfGuesses);
            ViewBag.History = historyOfGuesses;

            int counter = (int)HttpContext.Session.GetInt32("counter");
            counter++;
            HttpContext.Session.SetInt32("counter", counter);
            ViewBag.Round = counter;

            if (guessedNumber == randValue)
            {
                ViewBag.Message = "Correct!";
                ViewBag.Round = (int)HttpContext.Session.GetInt32("counter");
                ViewBag.Clue = "correct";
            } 
            else if( guessedNumber > randValue)
            {
                ViewBag.Message = $"Too Big! rand value [0,{(int)HttpContext.Session.GetInt32("n") - 1}] ";
                ViewBag.Clue = "toobig";
            } 
            else
            {
                ViewBag.Message = $"Too Low! rand value [0,{(int)HttpContext.Session.GetInt32("n") - 1}] ";
                ViewBag.Clue = "toolow";
            }
            return View("Guess");
        }
    }
}
