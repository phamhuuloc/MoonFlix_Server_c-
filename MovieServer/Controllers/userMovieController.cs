using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieServer.Models;
using MovieServer.Services;

namespace MovieServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class userMovieController : ControllerBase
    {
        [HttpPost("userMovie/by-movie")]
        [Authorize]
        public IActionResult byMovie(UserMovie user_movie)
        {

            try
            {
                userMovieServieces user_movie_services   = new userMovieServieces("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
                object  DTO = user_movie_services.byMovie(user_movie);
                Boolean sucess = Convert.ToBoolean(DTO.GetType().GetProperty("Success").GetValue(DTO, null));
               if(sucess == false)
                {
                    return BadRequest(DTO);
                }
                return Ok(DTO);
            }
            catch
            {
                return BadRequest();
            }

        }

        //[HttpPost("userMovie/update/{id}")]

        //public IActionResult updateSupplier(UserMovie user_movie, string id)
        //{
        //    userMovieServieces user_movie_services = new userMovieServieces("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
        //    var count = user_movie_services.updateUserMovie(user_movie, id);
        //    try
        //    {

        //        if (count > 0)
        //        {
        //            return Ok(new
        //            {
        //                Success = true,
        //                Message = "Update userMovie succesfully!",
        //                Data = user_movie
        //            });;
        //        }
        //        return BadRequest(new
        //        {
        //            Success = false,
        //            Message = "Update userMovie failer!"
        //        });

        //    }
        //    catch
        //    {
        //        return BadRequest(new
        //        {
        //            Success = false,
        //            Message = "Update userMovie failer 404 !"
        //        });
        //    }

        //}




        //// DELETE api/<UserController>/5
        //[HttpPost("userMovie/delete/{id}")]
        //public IActionResult deleteSupplier(int id)
        //{
        //    userMovieServieces user_movie_services = new userMovieServieces("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
        //    int count = user_movie_services.deleteUserMovie(id);
        //    try
        //    {

        //        if (count > 0)
        //        {
        //            return Ok(new
        //            {
        //                Success = true,
        //                Message = "Delete supplier succesfully!",

        //            });
        //        }
        //        return BadRequest(new
        //        {
        //            Success = false,
        //            Message = "Delete supplier failer!",

        //        });

        //    }
        //    catch
        //    {
        //        return BadRequest(new
        //        {
        //            Success = false,
        //            Message = "Not found supplier!",


        //        });
        //    }
        //}
    }
}
