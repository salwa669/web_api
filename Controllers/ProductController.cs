using Ecommerce.Models;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class ProductController : ControllerBase
    {
        IProductRepository productRepository;
       
        public ProductController(IProductRepository product)
        {
            productRepository = product;
           

        }
        [HttpGet]
        public IActionResult getAll()
        {
            List<Product> productList = productRepository.GetAll();
            if (productList == null)
            {
                return BadRequest("Empty Products");
            }
            return Ok(productList);
        }
        [HttpGet("{id:int}")]
       
        public IActionResult getByID(int id)
        {
            Product productList = productRepository.GetById(id);
            if (productList == null)
            {
                return BadRequest("there is no Product with this id");
            }
            return Ok(productList);
        }

        [HttpPost]
        public IActionResult Insert(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    

                        productRepository.Insert(product);
                        return Ok("Product Added Successfully"); 
                }
                catch 
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Can't add  product");
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        public IActionResult Edit(int id, Product product)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    productRepository.Update(id, product);

                    return StatusCode(StatusCodes.Status204NoContent, "Data Saved");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]  

        public IActionResult Delete(int id)
        {
            try
            {
                productRepository.Delete(id);
                return Ok("Product Deleted");
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Can't Delete this Product");
            }


        }
        [HttpPost("uploadImage"), DisableRequestSizeLimit]
     
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();

                //everything else is the same
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
