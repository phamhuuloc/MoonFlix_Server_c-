using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieServer.Models;
using MovieServer.Services;

namespace MovieServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ratingController : ControllerBase
    {
        [HttpPost("/create/rating")]
        [Authorize]
        public IActionResult createNewRating (Rating rating)
        {
            ratingServices rating_services = new ratingServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
            var count = rating_services.createNewRating(rating);
            try
            {
                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Inset rating succesfully!",
                        Data = rating
                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Insert  rating failer!"
                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Insert rating failer!"
                });
            }
        }
        [HttpPost("/delete/rating")]
        [Authorize]
        public IActionResult deleteRating(int movie_id)
        {
            ratingServices rating_services = new ratingServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
            var count = rating_services.deleteRating(movie_id);
            try
            {
                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Delete rating succesfully!",
                        
                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Delete  rating failer!"
                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Delete rating failer 404!"
                });
            }
        }
        [HttpPost("/get/ratings/movie")]
        public IActionResult getAllRatringOfMovie(int movie_id)
        {
            {
                ratingServices rating_services = new ratingServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
                var ratings = rating_services.getAllRatringOfMovie(movie_id);
  
                try
                {
                    Boolean success = Convert.ToBoolean(ratings.GetType().GetProperty("Success").GetValue(ratings, null));
                   
                    return Ok(ratings);
                }
                catch
                {
                    return BadRequest();
                }

            }
        // DELETE RATING OR COMMENT OF USER 
        //[HttpPost("/delete/rating")]
    }
}}
