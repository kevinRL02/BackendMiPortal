using Microsoft.AspNetCore.Mvc;
using ProductsService.Data;
using ProductsService.Models;
using System.Collections.Generic;

namespace ProductsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductRatingsController : ControllerBase
    {
        private readonly IProductRepo _repository;

        public ProductRatingsController(IProductRepo repository)
        {
            _repository = repository;
        }

        // GET: api/productratings
        [HttpGet]
        public ActionResult<IEnumerable<ProductRating>> GetAllProductRatings()
        {
            var productRatingItems = _repository.GetAllProductRatings();
            return Ok(productRatingItems);
        }

        // GET: api/productratings/{id}
        [HttpGet("{id}", Name = "GetProductRatingById")]
        public ActionResult<ProductRating> GetProductRatingById(int id)
        {
            var productRatingItem = _repository.GetProductRatingById(id);
            if (productRatingItem == null)
            {
                return NotFound();
            }
            return Ok(productRatingItem);
        }

        // POST: api/productratings
        [HttpPost]
        public ActionResult<ProductRating> CreateProductRating(ProductRating productRating)
        {
            _repository.CreateProductRating(productRating);
            _repository.SaveChanges();

            return CreatedAtRoute(nameof(GetProductRatingById), new { id = productRating.Id }, productRating);
        }

        // PUT: api/productratings/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProductRating(int id, ProductRating productRating)
        {
            var productRatingFromRepo = _repository.GetProductRatingById(id);
            if (productRatingFromRepo == null)
            {
                return NotFound();
            }

            productRatingFromRepo.RatingDate = productRating.RatingDate;
            productRatingFromRepo.ProductId = productRating.ProductId;
            productRatingFromRepo.Rating = productRating.Rating;
            productRatingFromRepo.UserId = productRating.UserId;

            _repository.UpdateProductRating(productRatingFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE: api/productratings/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProductRating(int id)
        {
            var productRatingFromRepo = _repository.GetProductRatingById(id);
            if (productRatingFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteProductRating(productRatingFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
