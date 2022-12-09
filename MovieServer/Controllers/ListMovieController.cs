using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieServer.Models;
using MovieServer.Services;

namespace MovieServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ListMovieController : ControllerBase
    {
        [HttpPost("/create/listMovie")]
        [Authorize]
        public IActionResult createNewListMovie(ListMovie listMovie)
        {
            listMovieServices list_movie_services = new listMovieServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
            var count = list_movie_services.createNewListMovie(listMovie);
            try
            {
                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Inset  movie into list succesfully!",
                        Data = listMovie
                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Insert   movie into list  failer!"
                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Insert   movie into list failer 404!"
                });
            }

        }
        // UPDATE MOVIE
        //[HttpPost("listMovie/update/{id}")]

        //public IActionResult updateListMovie(ListMovie listMovie)
        //{

        //    try
        //    {
        //        listMovieServices list_movie_services = new listMovieServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
        //        var count = list_movie_services.updateListMovie(listMovie);
        //        if (count > 0)
        //        {
        //            return Ok(new
        //            {
        //                Success = true,
        //                Message = "Update list Movie succesfully!",
        //                Data = listMovie
        //            });
        //        }
        //        return BadRequest(new
        //        {
        //            Success = false,
        //            Message = "Update list Movie failer!"
        //        });

        //    }
        //    catch
        //    {
        //        return BadRequest(new
        //        {
        //            Success = false,
        //            Message = "Update list Movie failer 404 !"
        //        });
        //    }

        //}


        // DELETE MOVIE 
        [HttpPost("/listMovie/delete/{id}")]
        [Authorize]
        public IActionResult deleteListMovie(int id)
        {
            listMovieServices list_movie_services = new listMovieServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
            var count = list_movie_services.deleteListMovie(id);
            try
            {


                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Delete  Movie from list  succesfully!",

                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Delete   Movie from list failer!",

                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Not found Movie on list 404!",


                });
            }
        }
    }
}
