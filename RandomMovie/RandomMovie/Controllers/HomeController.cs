using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RandomMovie.Models;

namespace RandomMovie.Controllers
{
    public class HomeController : Controller
    {
        #region Inyección de Dependencia

        private readonly RDBContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(RDBContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tmovie movie)
        {
            // Verify it does not empty title.

            if (movie.Title != null)
            {
                // Verify it does not exists another movie with the same title.

                var existingMovie = _context.Tmovie.Where(x => x.Title.ToLower() == movie.Title.Trim().ToLower()).FirstOrDefault();

                // If it does not (null) exists another movie with the same title, save a new one.

                if (existingMovie == null)
                {
                    _context.Tmovie.Add(movie);
                    var result = _context.SaveChanges();

                    // Result variable allow return a message if the process was succesfully.

                    if (result == 1)
                    {
                        ViewBag.Movie = movie;
                    }
                    else
                    {
                        ViewBag.Error = "Error, the movie was not updated.";
                    }
                }
                else
                {
                    ViewBag.Error = "Already exits a movie with the same title.";
                }
            }
            else
            {
                ViewBag.Error = "Title must not be empty.";
            }

            return View();
        }

        [HttpGet]
        public IActionResult Read()
        {
            ViewBag.ListMovie = _context.Tmovie.ToList();

            return View();
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(Tmovie movie)
        {
            // Verify it does not empty the ID.

            if (movie.Idmovie > 0)
            {
                if (movie.Title != null)
                {
                    // Verify it exists a movie with the ID typed.

                    var updateMovie = _context.Tmovie.Where(x => x.Idmovie == movie.Idmovie).FirstOrDefault();

                    if (updateMovie != null)
                    {
                        // Verify it does not exists another movie with the same title.

                        var existingMovie = _context.Tmovie.Where(x => x.Title.ToLower() == updateMovie.Title.ToLower()).FirstOrDefault();

                        if (existingMovie == null)
                        {
                            _context.Tmovie.Update(movie);
                            var result = _context.SaveChanges();

                            // Result variable allow return a message if the process was succesfully.

                            if (result == 1)
                            {
                                ViewBag.Movie = movie;
                            }
                            else
                            {
                                ViewBag.Error = "Error, the movie was not updated.";
                            }
                        }
                        else
                        {
                            ViewBag.Error = "Already exits a movie with the same title.";
                        }
                    }
                    else
                    {
                        ViewBag.Error = "It does not exists any movie with that ID.";
                    }
                }
                else
                {
                    ViewBag.Error = "Title must not be empty.";
                }
            }
            else
            {
                ViewBag.Error = "ID must not be empty.";
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(Tmovie movie)
        {
            // Verify it does not empty the ID.

            if (movie.Idmovie > 0)
            {
                // Verify it exists a movie with the ID typed.

                var deleteMovie = _context.Tmovie.Where(x => x.Idmovie == movie.Idmovie).FirstOrDefault();

                if (deleteMovie != null)
                {
                    _context.Tmovie.Remove(deleteMovie);
                    var result = _context.SaveChanges();

                    // Result variable allow return a message if the process was succesfully.

                    if (result == 1)
                    {
                        ViewBag.Movie = deleteMovie;
                    }
                    else
                    {
                        ViewBag.Error = "Error, the movie was not deleted.";
                    }

                }
                else
                {
                    ViewBag.Error = "It does not exits a movie with that ID.";
                }
            }
            else
            {
                ViewBag.Error = "ID must not be empty.";
            }

            return View();
        }

        [HttpGet]
        public IActionResult Pick()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Pick(Tmovie movie)
        {
            // Select a random number between zero and total movies are not picked.

            var random = new Random();

            int totalMovie = _context.Tmovie.Where(x => x.Pick == false).ToList().Count();

            int randomNumber = random.Next(0, totalMovie);

            var moviePicked = _context.Tmovie.Where(x => x.Pick == false).Skip(randomNumber - 1).Take(1).FirstOrDefault();

            moviePicked.Pick = true;
            moviePicked.PickDate = DateTime.Now;

            _context.Tmovie.Update(moviePicked);
            var result = _context.SaveChanges();

            if (result == 1)
            {
                ViewBag.Movie = moviePicked;
            }
            else
            {
                ViewBag.Error = "Error, the movie was not updated.";
            }


            return View();
        }

        public IActionResult License()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
