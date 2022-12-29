using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieServer.Models;
using MovieServer.Services;

namespace MovieServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class listController : ControllerBase
    {


        [HttpGet("/lists/")]
        public IActionResult getAlllistMovie()
        {
            try
            {
                listServices list_services = new listServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");

                var list = list_services.getAllList();

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


        [HttpGet("lists/{id}")]
        public IActionResult getListMovieById(int id)
        {

            listServices list_services = new listServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            var list = list_services.getList(id);

            try
            {
                if (list  == null)
                {
                    return NotFound();
                }
                return Ok(new
                {
                    Success = true,
                    message = "Get Movie sucessfully!",
                    Data = list
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        //Get all movie of list

       [HttpGet("lists/movies")]
        public IActionResult getAllMovieOfList()
        {

            listServices list_services = new listServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            var list = list_services.getAllMovieOfList();

            try
            {
                if (list == null)
                {
                    return NotFound();
                }
                return Ok(new
                {
                    Success = true,
                    message = "Get list movie sucessfully!",
                    Data = list
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("lists/movies/{type}")]
        public IActionResult filterMovieWithType(string type)
        {

            listServices list_services = new listServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            var list = list_services.filterMovieWithType(type);

            try
            {
                if (list == null)
                {
                    return NotFound();
                }
                return Ok(new
                {
                    Success = true,
                    message = "Get list movie sucessfully!",
                    Data = list
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        // CREATE NEW MOVIE 

        [HttpPost("/create/list")]
        [Authorize]
        public IActionResult createNewList(List listMovie)
        {
            listServices list_services = new listServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            var count = list_services.createNewList(listMovie);
            try
            {
                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Inset list movie succesfully!",
                        Data = listMovie
                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Insert list  movie failer!"
                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Insert list  movie failer!"
                });
            }

        }
        // UPDATE MOVIE
        [HttpPost("list/update/{id}")]
        [Authorize]
        public IActionResult updateListMovie(List listMovie, string id)
        {

            try
            {
                listServices list_services = new listServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
                var count = list_services.createNewList(listMovie);
                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Update Movie succesfully!",
                        Data = listMovie
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
        
        [HttpPost("/lists/delete/{id}")]
        [Authorize]
        public IActionResult deleteListMovie(int id)
        {
            listServices list_services = new listServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            var count = list_services.deleteListMovie(id);
            try
            {


                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Delete list  Movie succesfully!",

                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Delete list  Movie failer!",

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
    }
}
