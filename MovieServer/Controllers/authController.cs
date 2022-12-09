using Microsoft.AspNetCore.Mvc;
using MovieServer.Models;
using MovieServer.Services;
using System.Net.WebSockets;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class authController : ControllerBase
    {
       
      

        // POST api/<authController>
        [HttpPost("user/login")]
        public IActionResult login(Auth userInfo)
        {
            try
            {
                authServices authservices = new authServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
                object DTO = authservices.login(userInfo);
                Boolean checkUser = Convert.ToBoolean(DTO.GetType().GetProperty("Success").GetValue(DTO, null));
                
                if (checkUser == false){
                    return BadRequest(DTO);
                }
               
                return Ok(DTO);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("user/register")]
        public IActionResult register(Auth userInfo)
        {
            authServices authservices = new authServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
            object DTO = authservices.register(userInfo);
            try
            {
               
                Boolean registerUser = Convert.ToBoolean(DTO.GetType().GetProperty("Success").GetValue(DTO, null));
                if (registerUser == false)
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




    }
}
