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
        [Authorize]
        public IActionResult byMovie(UserVoucher user_voucher)
        {

            try
            {
                userVoucherServices user_voucher_services = new userVoucherServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
                object DTO = user_voucher_services.byVoucher(user_voucher);
                Boolean sucess = Convert.ToBoolean(DTO.GetType().GetProperty("Success").GetValue(DTO, null));
                if (sucess == false)
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
