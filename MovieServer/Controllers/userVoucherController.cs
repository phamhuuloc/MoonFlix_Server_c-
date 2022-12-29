using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieServer.Models;
using MovieServer.Services;

namespace MovieServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class userVoucherController : ControllerBase
    {
        [HttpPost("userVoucher/by-voucher")]
        
        public IActionResult byMovie(UserVoucher user_voucher)
        {
            userVoucherServices user_voucher_services = new userVoucherServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            object DTO = user_voucher_services.byVoucher(user_voucher);

            try
            {
                
                Boolean sucess = Convert.ToBoolean(DTO.GetType().GetProperty("Success").GetValue(DTO, null));
                if (sucess == false)
                {
                    return BadRequest(DTO);
                }
                return Ok(DTO); 
            }
            catch
            {
                return BadRequest(DTO);
            }

        }
    }
}
