using MovieServer.Models;
using MovieServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace MovieServer.Controllers
{
    [Route("api/payment/")]
    [ApiController]
    public class paymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;

        public paymentController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        // get url of payment 
        [HttpPost("/api/user/vn_payment")]
        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Ok(new
            {
                Success = true,
                Message = "URL using for vnpay",
                url = url
            });


        }
        [HttpPost("api/user/vnpay_ipn")]
        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Ok(new
            {
                Success = true,
                Message = "URL using for vnpay",
                url = response
            });
        }
    }
}
