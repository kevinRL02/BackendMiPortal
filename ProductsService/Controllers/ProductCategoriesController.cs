using Microsoft.AspNetCore.Mvc;
using ProductsService.Data;
using ProductsService.Models;
using System.Collections.Generic;

namespace ProductsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductRepo _repository;

        public ProductCategoriesController(IProductRepo repository)
        {
            _repository = repository;
        }

        // GET: api/productcategories
        [HttpGet]
        public ActionResult<IEnumerable<ProductCategory>> GetAllProductCategories()
        {
            var categoryItems = _repository.GetAllProductCategories();
            return Ok(categoryItems);
        }

        // GET: api/productcategories/{id}
        [HttpGet("{id}", Name = "GetProductCategoryById")]
        public ActionResult<ProductCategory> GetProductCategoryById(int id)
        {
            var categoryItem = _repository.GetProductCategoryById(id);
            if (categoryItem == null)
            {
                return NotFound();
            }
            return Ok(categoryItem);
        }

        // POST: api/productcategories
        [HttpPost]
        public ActionResult<ProductCategory> CreateProductCategory(ProductCategory category)
        {
            _repository.CreateProductCategory(category);
            _repository.SaveChanges();

            return CreatedAtRoute(nameof(GetProductCategoryById), new { id = category.Id }, category);
        }

        // PUT: api/productcategories/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProductCategory(int id, ProductCategory category)
        {
            var categoryFromRepo = _repository.GetProductCategoryById(id);
            if (categoryFromRepo == null)
            {
                return NotFound();
            }

            categoryFromRepo.Name = category.Name;

            _repository.UpdateProductCategory(categoryFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE: api/productcategories/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProductCategory(int id)
        {
            var categoryFromRepo = _repository.GetProductCategoryById(id);
            if (categoryFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteProductCategory(categoryFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
