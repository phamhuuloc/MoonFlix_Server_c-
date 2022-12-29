using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieServer.Models;
using MovieServer.Services;

namespace MovieServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class supplierController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet("/suppliers/")]
        public IActionResult getAllSuppliers()
        {
            try
            {
                supplierServices supplierservices = new supplierServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");

                var list = supplierservices.getAllSuppliers();

                return Ok(new
                {
                    Success = true,
                    Message = "Get suppliers succesfully",
                    Data = list
                });
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("supplier/{id}")]
        public IActionResult getSupplierById(int id)
        {
            try
            {
                supplierServices supplierservices = new supplierServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
                
                var supplier = supplierservices.GetSupplierById(id);
                if (supplier == null)
                {
                    return NotFound();
                }
                return Ok(new
                {
                    Success = true,
                    message = "Get Supplier sucessfully",
                    Data = supplier
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST api/<UserController>
        [HttpPost("supplier/create")]
        [Authorize]
        public IActionResult createNewSupplier(Supplier  supplier)
        {

            try
            {
                supplierServices supplierservices = new supplierServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
                var count = supplierservices.createNewSupplier(supplier);
                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "insert user succesfully!",
                        Data = supplier
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

        [HttpPost("supplier/update/{id}")]
        [Authorize]

        public IActionResult updateSupplier(Supplier supplier, string id)
        {
            supplierServices supplierservices = new supplierServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            var count = supplierservices.updateSupplier(supplier, id);
            try
            {

                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Update supplier succesfully!",
                        Data = supplier
                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Update supplier failer!"
                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Update supplier failer 404 !"
                });
            }

        }


        // DELETE api/<UserController>/5
        [HttpPost("supplier/delete/{id}")]
        [Authorize]
        public IActionResult deleteSupplier(int id)
        {
            supplierServices supplierservices = new supplierServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            int count = supplierservices.deleteSupplier(id);
            try
            {

                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Delete supplier succesfully!",

                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Delete supplier failer!",

                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Not found supplier!",


                });
            }
        }

 
    }
}
