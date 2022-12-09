using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieServer.Models;
using MovieServer.Services;

namespace MovieServer.Controllers
{
    [Route("api/")]
    [ApiController]     
    public class categoriesController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet("/categories/")]
        public IActionResult getAllCategories()
        {
            try
            {
                categoriesServices categoriesservices = new categoriesServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");

                var list = categoriesservices.getAllCategories();

                return Ok(new
                {
                    Success = true,
                    Message = "Get categories succesfully",
                    Data = list
                });
            }
            catch
            {
                return BadRequest();
            }
        }



        // POST api/<UserController>
        [HttpPost("create/categories")]
        [Authorize]
        public IActionResult createNewCategorie(Categorie categorie)
        {

            try
            {
                categoriesServices categoriesservices = new categoriesServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
                var count = categoriesservices.createNewCategorie(categorie);
                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Inset categories succesfully!",
                        Data = categorie
                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Insert categories failer!"
                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Insert Categories failer!"
                });
            }

        }

        [HttpPost("categories/update/{id}")]
        [Authorize]
        public IActionResult updateCategories(Categorie  categorie,int id)
        {
            categoriesServices categoriesservices = new categoriesServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
            var count = categoriesservices.updateCategories(categorie,id);
            try
            {

                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Update Categories succesfully!",
                        Data = categorie
                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Update Categories  failer!"
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
        [HttpPost("categories/delete/{id}")]
        [Authorize]
        public IActionResult deleteCategories(int id)
        {

            categoriesServices categoriesservices = new categoriesServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
            var count = categoriesservices.deleteCategories(id);
            try
            {

                if (count > 0)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Delete categories succesfully!",

                    });
                }
                return BadRequest(new
                {
                    Success = false,
                    Message = "Delete categories failer!",

                });

            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Not found categories!",


                });
            }
        }

        [HttpPost("/categories/movies/{cate_name}")]
        public IActionResult getMovieOfUser(string  cate_name)
        {

            try
            {
                categoriesServices categoriesservices = new categoriesServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
                var list = categoriesservices.getListMovieOfCategorie(cate_name);

                return Ok(new
                {
                    Success = true,
                    Message = "Get movie  of categorie succesfully!",
                    Data = list
                });



            }
            catch
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Get movie of  categorie failer!"
                });
            }

        }
    }
}
