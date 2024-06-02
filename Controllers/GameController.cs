using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ViewsControllers.Controllers
{
    public class GameController : Controller
    {
        private const string SessionKeyNumber = "_Number";
        private const string SessionKeyGuessCount = "_GuessCount";

        [HttpGet("/GuessingGame")]
        public IActionResult GuessingGame()
        {
            // Generate a random number and store it in the session
            HttpContext.Session.SetInt32(SessionKeyNumber, new Random().Next(1, 101));
            HttpContext.Session.SetInt32(SessionKeyGuessCount, 0);
            ViewData["Message"] = "Guess a number between 1 and 100!";
            return View();
        }

        [HttpPost("/GuessingGame")]
        public IActionResult GuessingGame(int guess)
        {
            int? number = HttpContext.Session.GetInt32(SessionKeyNumber);
            int? guessCount = HttpContext.Session.GetInt32(SessionKeyGuessCount);

            if (!number.HasValue || !guessCount.HasValue)
            {
                return RedirectToAction("GuessingGame");
            }

            guessCount++;
            HttpContext.Session.SetInt32(SessionKeyGuessCount, guessCount.Value);

            if (guess == number)
            {
                ViewData["Message"] = $"Congratulations! You guessed the number in {guessCount} tries!";
                HttpContext.Session.Remove(SessionKeyNumber);
                HttpContext.Session.Remove(SessionKeyGuessCount);
            }
            else if (guess > number)
            {
                ViewData["Message"] = "Too high! Try again.";
            }
            else
            {
                ViewData["Message"] = "Too low! Try again.";
            }

            ViewData["GuessCount"] = guessCount;
            return View();
        }
    }
}
