using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieServer.Models;
using MovieServer.Services;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet("/users/")]
        public IActionResult getAllUser()
        {
            try
            {
                userServices userservies = new userServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");

                var list = userservies.getAllUser();

                return Ok(new
                {
                    Success = true,
                    Message = "get user succesfully",
                    Data = list
                });
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("/user/{id}")]
        public IActionResult getUserById(int id)
        {
            try
            {
                userServices userservies = new userServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
                Console.Write(userservies);
                var user = userservies.getUser(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(new
                {
                    Success = true,
                    message = "get user sucessfully",
                    Data = user
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("user/top")]
        public IActionResult getTopUser()
        {
            try
            {
                userServices userservies = new userServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
                object DTO = userservies.getTopUser();

                if ( DTO == null)
                {
                    return NotFound();
                }

                return Ok(DTO);
            }
            catch
            {
                return BadRequest();
            }
        }
        // POST api/<UserController>
        [HttpPost("user/create")]
        [Authorize]
        public IActionResult createNewUser(User user)
        {

            try
            {
                userServices userservies = new userServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
                var count = userservies.createNewUser(user);
                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "insert user succesfully!",
                        Data = user
                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "insert user failer!"
                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "insert user failer!"
                });
            }

        }
        [HttpPost("user/update/{id}")]
        [Authorize]
        public IActionResult updateUser( User user ,   string id )
        {

            try
            {
                userServices userservies = new userServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
                var count = userservies.updateUser(user,id);
                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true, 
                        Message = "Update user succesfully!",
                        Data = user
                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Update user failer!"
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

      


        // DELETE api/<UserController>/5
        [HttpPost("user/delete/{id}")]
        [Authorize]
        public IActionResult deleteUser(int id )
        {
            userServices userservies = new userServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            int count = userservies.deleteUser(id);
            try
            {
                
               
                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Delete user succesfully!",
                        
                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Delete user failer!",
                 
                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Not found user!",
                  

                });
            }
        }

        // GET  MOVIE OF USER
        [HttpGet("user/list-movies/{id}")]
        public IActionResult getMovieOfUser(int id)
        {

            try
            {
                userServices userservies = new userServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
                var list  = userservies.getMovieOfUser(id);

                return Ok(new
                {
                    Success = true,
                    Message = "Get movie  user succesfully!",
                    Data =  list
                });
                
             

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Get movie of  user failer!"
                });
            }

        }
        // GET VOUCHER OF USER 
        [HttpGet("user/list-vouchers/{id}")]
        public IActionResult getVoucherOfUser(int id)
        {

            try
            {
                userServices userservies = new userServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
                var list = userservies.getVoucherOfUser(id);

                return Ok(new
                {
                    Success = true,
                    Message = "Get vouchers of  user succesfully!",
                    Data = list
                });



            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Get vouchers of  user failer!"
                });
            }

        }

        [HttpGet("user/revenue")]
        public IActionResult getRevenue()
        {
            userServices userservies = new userServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            object DTO = userservies.getRevenue();
            try
            {

                Boolean checkData = Convert.ToBoolean(DTO.GetType().GetProperty("Success").GetValue(DTO, null));

                if (checkData == false)
                {
                    return BadRequest(DTO);
                }

                return Ok(DTO);

             
            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Get revenue of  user failer 404!"
                });
            }

        }
        // Get Stats Of user
        [HttpGet("user/status")]
        public IActionResult getStasUser()
        {

            try
            {
                userServices userservies = new userServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
                var list = userservies.getStasUser();

                return Ok(new
                {
                    Success = true,
                    Message = "Get Status  user succesfully!",
                    Data = list
                });



            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Get Status  of  user failer!"
                });
            }

        }
        //[HttpPost("user/vnpay_payment")]
    

    }

  
    
    

}
