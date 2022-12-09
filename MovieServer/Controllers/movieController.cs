using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieServer.Models;
using MovieServer.Services;

namespace MovieServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class movieController : ControllerBase
    {
        [HttpGet("/movies/")]
        public IActionResult getAllUser()
        {
            try
            {   
                movieServices movieservices = new movieServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");

                var list = movieservices.getAllMovies();

                return Ok(new
                {
                    Success = true,
                    Message = "Get list movies  succesfully",
                    Data = list
                });
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("movies/{id}")]
        public IActionResult getMovieById(int id)
        {

            movieServices movieservices = new movieServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
            var movie = movieservices.getMovie(id);

            try
            {
                if (movie == null)
                {
                    return NotFound();
                }
                return Ok(new
                {
                    Success = true,
                    message = "Get Movie sucessfully!",
                    Data = movie
                });
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("movies/top")]
        public IActionResult getTopMovie()
        {

            movieServices movieservices = new movieServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
            var list  = movieservices.getTopMovie();

            try
            {
                if (list == null)
                {
                    return NotFound();
                }
                return Ok(new
                {
                    Success = true,
                    message = "Get Top 10  Movie sucessfully!",
                    Data = list 
                });
            }
            catch
            {
                return BadRequest();
            }
        }
        //  GET REVENUE OF MOVIE
        [HttpGet("movies/revenue/{id}")]
        public IActionResult getRevenueOfMovie(int id )
        {

            movieServices movieservices = new movieServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
            var ob  = movieservices.getRevenueOfMovie(id);

            try
            {
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(new
                {
                    Success = true,
                    message = "Get revenue of   Movie sucessfully!",
                    Data = ob
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        // CREATE NEW MOVIE 

        [HttpPost("/create/movie")]
        [Authorize]
        public IActionResult createNewMovie(Movie movie)
        {
            movieServices movieservices = new movieServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
            var count = movieservices.createNewMovie(movie);
            try
            {
                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Inset movie succesfully!",
                        Data = movie
                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Insert  movie failer!"
                });

            } 
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Insert movie failer!"
                });
            }

        }
        // UPDATE MOVIE
        [HttpPost("movie/update/{id}")]
        [Authorize]
        public IActionResult updateMovie(Movie movie, string id)
        {

            try
            {
                movieServices movieservices = new movieServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
                var count = movieservices.updateMovie(movie,id);
                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Update Movie succesfully!",
                        Data = movie
                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Update Movie failer!"
                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Update user failer 404 !"
                });
            }

        }



        // DELETE MOVIE 
        [HttpPost("/movie/delete/{id}")]
        [Authorize]
        public IActionResult deleteMovie(int id)
        {
            movieServices movieservices = new movieServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
            var count = movieservices.deleteMovie(id);
            try
            {


                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Delete Movie succesfully!",

                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Delete Movie failer!",

                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Not found Movie!",


                });
            }
        }

        // GET  MOVIE OF USER
        [HttpPost("/movies/categories/{id}")]

        public IActionResult getCategoriesOfMovie(int id)
        {

            try
            {
                movieServices movieservices = new movieServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
              
                var list = movieservices.getCategorieOfMovie(id);

                return Ok(new
                {
                    Success = true,
                    Message = "Get categories of Movie succesfully!",
                    Data = list
                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Get movie of  Movie failer!"
                });
            }

        }
    }
}
