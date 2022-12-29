using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieServer.Models;
using MovieServer.Services;

namespace MovieServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class voucherController : ControllerBase {

        [HttpGet("/vouchers")]
        public IActionResult getAllVoucher()
        {
            try
            {
                voucherServices voucherservices = new voucherServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");

                var DTO = voucherservices.getAllVoucher();

                return Ok(new
                {
                    Success = true,
                    Message = "Get list  voucher  succesfully",
                    Data = DTO
                });
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("/voucher/{id}")]
        public IActionResult getVoucherById(int id)
        {
            voucherServices voucherservices = new voucherServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            var voucher = voucherservices.getVoucher(id);

            try
            {
                if (voucher == null)
                {
                    return NotFound();
                }
                return Ok(new
                {
                    Success = true,
                    message = "Get voucher sucessfully!",
                    Data = voucher
                });
            }
            catch
            {
                return BadRequest();
            }
        }
        // CREATE NEW MOVIE 

        [HttpPost("voucher/create")]
        [Authorize]
        public IActionResult createNewVoucher(Voucher voucher)
        {
            voucherServices voucherservices = new voucherServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            var count = voucherservices.createNewVoucher(voucher);
            try
            {
                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Inset voucher succesfully!",
                        Data = voucher
                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Insert  voucher failer!"
                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Insert voucher failer!"
                });
            }

        }
        // UPDATE MOVIE
        [HttpPost("voucher/update/{id}")]
        [Authorize]
        public IActionResult updateVoucher(Voucher voucher, string id)
        {

            try
            {
                voucherServices voucherservices = new voucherServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
                var count = voucherservices.updateVoucher(voucher, id);
                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Update Voucher succesfully!",
                        Data = voucher
                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Update Voucher failer!"
                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Update voucher failer 404 !"
                });
            }

        }



        // DELETE MOVIE 
        [HttpPost("/voucher/delete/{id}")]
        [Authorize]
        public IActionResult deleteVoucher(int id)
        {
            voucherServices voucherservices = new voucherServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            var count = voucherservices.deleteVoucher(id);
            try
            {


                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Delete Voucher succesfully!",

                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Delete Voucher failer!",

                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Delete  Voucher failer 404!",


                });
            }
        }

    }
    

    
}
