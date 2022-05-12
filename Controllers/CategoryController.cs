using Ecommerce.DTO;
using Ecommerce.Models;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository category)
        {
            categoryRepository = category;

        }
        [HttpGet]
        public IActionResult getAll()
        {
            List<Category> categoryList = categoryRepository.GetAll();

            if (categoryList == null)
            {
                return BadRequest("Empty Category List");
            }
            return Ok(categoryList);
        }
        [HttpGet("{id:int}")]

        public IActionResult getByID(int id)
        {
            Category categoryList = categoryRepository.GetById(id);
            if (categoryList == null)
            {
                return BadRequest("there is no Category with this id");
            }
            return Ok(categoryList);
        }

        [HttpPost]
        public IActionResult Insert(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    categoryRepository.Insert(category);
                    return Ok("Category Added Successfully");
                }
                catch //(Exception ex)
                {
                   return StatusCode(StatusCodes.Status400BadRequest, "Can't add  Category");
                    // return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        public IActionResult Edit(int id, Category category)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    categoryRepository.Update(id, category);

                    return StatusCode(StatusCodes.Status204NoContent, "Data are Saved");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public IActionResult Delete (int id)
        {
            try
            {
                categoryRepository.Delete(id);
                return Ok("Category Deleted");
            }
             catch 
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Can't Delete this Category");
            }
            
            
        }
        [HttpGet("Details/{catid:int}")]
        public IActionResult GetCategoryWithProducts(int catid)
        {
            Category catModel = categoryRepository.GetCategoryDetails(catid);

            CategorywithProductsNameDTO CategoryDTO = new CategorywithProductsNameDTO()
            {
                CatgegoryId = catModel.Id,
                CategoryName = catModel.Name

            };
            foreach (var item in catModel.Products)
            {
                CategoryDTO.ProductsName.Add(item.Name);
            }

            return Ok(CategoryDTO);
        }
    }
}
