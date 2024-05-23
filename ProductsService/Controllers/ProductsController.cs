using Microsoft.AspNetCore.Mvc;
using ProductsService.Data;
using ProductsService.Models;
using System.Collections.Generic;

namespace ProductsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _repository;

        public ProductsController(IProductRepo repository)
        {
            _repository = repository;
        }

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Products>> GetAllProducts()
        {
            var productItems = _repository.GetAllProducts();
            return Ok(productItems);
        }

        // GET: api/products/{id}
        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<Products> GetProductById(int id)
        {
            var productItem = _repository.GetProductById(id);
            if (productItem == null)
            {
                return NotFound();
            }
            return Ok(productItem);
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<Products> CreateProduct(Products product)
        {
            // Check if the category exists
            var productCategory = _repository.GetProductCategoryById(product.ProductCategoryId);
            if (productCategory == null)
            {
                return NotFound(new { error = "Product category not found", categoryId = product.ProductCategoryId });
            }

            _repository.CreateProduct(product);
            _repository.SaveChanges();

            return CreatedAtRoute(nameof(GetProductById), new { id = product.Id }, product);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, Products product)
        {
            var productFromRepo = _repository.GetProductById(id);
            if (productFromRepo == null)
            {
                return NotFound();
            }

            productFromRepo.Name = product.Name;
            productFromRepo.ProductCategoryId = product.ProductCategoryId;
            productFromRepo.Description = product.Description;
            productFromRepo.UnitCost = product.UnitCost;
            productFromRepo.Stock = product.Stock;
            productFromRepo.ReorderPoint = product.ReorderPoint;

            _repository.UpdateProduct(productFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var productFromRepo = _repository.GetProductById(id);
            if (productFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteProduct(productFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }


        // GET: api/products/by-category/{categoryId}
        [HttpGet("by-category/{categoryId}")]
        public ActionResult<IEnumerable<Products>> GetProductsByCategory(int categoryId)
        {
            var productsByCategory = _repository.GetProductsByCategory(categoryId);
            if (productsByCategory == null || !productsByCategory.Any())
            {
                return NotFound();
            }

            return Ok(productsByCategory);
        }




    }
}
