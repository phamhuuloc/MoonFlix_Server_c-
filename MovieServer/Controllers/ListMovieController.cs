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
            listMovieServices list_movie_services = new listMovieServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
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
        // Get All Movie Of  list id 
        [HttpGet("listMovie/{id}")]

        public IActionResult allMovieOfList(int id)
        {

            try
            {
                listMovieServices list_movie_services = new listMovieServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
                var lists = list_movie_services.allMovieOfList( id);
                if (lists != null)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Get  list Movie succesfully!",
                        Data = lists
                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Get list Movie failer!"
                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Update list Movie failer 404 !"
                });
            }

        }


        // DELETE list movie 
        [HttpPost("/listMovie/delete/list/{id}")]
        [Authorize]
        public IActionResult deleteListMovie(int id)
        {
            listMovieServices list_movie_services = new listMovieServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
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
        // Delete Movie of list
        [HttpPost("/listMovie/delete/movie")]
        [Authorize]
        public IActionResult deleteMovieOfList(ListMovie listMovie)
        {
            listMovieServices list_movie_services = new listMovieServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            var count = list_movie_services.deleteMovieOfList(listMovie);
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
