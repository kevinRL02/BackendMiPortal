using Microsoft.AspNetCore.Mvc;
using ProductsService.Data;
using ProductsService.Models;
using System.Collections.Generic;

namespace ProductsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierProductsOrdersController : ControllerBase
    {
        private readonly IProductRepo _repository;

        public SupplierProductsOrdersController(IProductRepo repository)
        {
            _repository = repository;
        }

        // GET: api/supplierproductsorders
        [HttpGet]
        public ActionResult<IEnumerable<SupplierProductsOrder>> GetAllSupplierProductsOrders()
        {
            var supplierProductsOrderItems = _repository.GetAllSupplierProductsOrders();
            return Ok(supplierProductsOrderItems);
        }

        // GET: api/supplierproductsorders/{id}
        [HttpGet("{id}", Name = "GetSupplierProductsOrderById")]
        public ActionResult<SupplierProductsOrder> GetSupplierProductsOrderById(int id)
        {
            var supplierProductsOrderItem = _repository.GetSupplierProductsOrderById(id);
            if (supplierProductsOrderItem == null)
            {
                return NotFound();
            }
            return Ok(supplierProductsOrderItem);
        }

        // POST: api/supplierproductsorders
        [HttpPost]
        public ActionResult<SupplierProductsOrder> CreateSupplierProductsOrder(SupplierProductsOrder supplierProductsOrder)
        {
            _repository.CreateSupplierProductsOrder(supplierProductsOrder);
            _repository.SaveChanges();

            return CreatedAtRoute(nameof(GetSupplierProductsOrderById), new { id = supplierProductsOrder.PurchaseOrderId }, supplierProductsOrder);
        }

        // PUT: api/supplierproductsorders/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateSupplierProductsOrder(int id, SupplierProductsOrder supplierProductsOrder)
        {
            var supplierProductsOrderFromRepo = _repository.GetSupplierProductsOrderById(id);
            if (supplierProductsOrderFromRepo == null)
            {
                return NotFound();
            }

            supplierProductsOrderFromRepo.OrderaDate = supplierProductsOrder.OrderaDate;
            supplierProductsOrderFromRepo.ProductId = supplierProductsOrder.ProductId;
            supplierProductsOrderFromRepo.SupplierId = supplierProductsOrder.SupplierId;
            supplierProductsOrderFromRepo.Quantity = supplierProductsOrder.Quantity;
            supplierProductsOrderFromRepo.Price = supplierProductsOrder.Price;

            _repository.UpdateSupplierProductsOrder(supplierProductsOrderFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE: api/supplierproductsorders/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteSupplierProductsOrder(int id)
        {
            var supplierProductsOrderFromRepo = _repository.GetSupplierProductsOrderById(id);
            if (supplierProductsOrderFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteSupplierProductsOrder(supplierProductsOrderFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
