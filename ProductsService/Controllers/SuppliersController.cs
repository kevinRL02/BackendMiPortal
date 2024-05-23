using Microsoft.AspNetCore.Mvc;
using ProductsService.Data;
using ProductsService.Models;
using System.Collections.Generic;

namespace ProductsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly IProductRepo _repository;

        public SuppliersController(IProductRepo repository)
        {
            _repository = repository;
        }

        // GET: api/suppliers
        [HttpGet]
        public ActionResult<IEnumerable<Supplier>> GetAllSuppliers()
        {
            var supplierItems = _repository.GetAllSuppliers();
            return Ok(supplierItems);
        }

        // GET: api/suppliers/{id}
        [HttpGet("{id}", Name = "GetSupplierById")]
        public ActionResult<Supplier> GetSupplierById(int id)
        {
            var supplierItem = _repository.GetSupplierById(id);
            if (supplierItem == null)
            {
                return NotFound();
            }
            return Ok(supplierItem);
        }

        // POST: api/suppliers
        [HttpPost]
        public ActionResult<Supplier> CreateSupplier(Supplier supplier)
        {
            _repository.CreateSupplier(supplier);
            _repository.SaveChanges();

            return CreatedAtRoute(nameof(GetSupplierById), new { id = supplier.SupplierId }, supplier);
        }

        // PUT: api/suppliers/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateSupplier(int id, Supplier supplier)
        {
            var supplierFromRepo = _repository.GetSupplierById(id);
            if (supplierFromRepo == null)
            {
                return NotFound();
            }

            supplierFromRepo.CompanyName = supplier.CompanyName;
            supplierFromRepo.ContacNumber = supplier.ContacNumber;

            _repository.UpdateSupplier(supplierFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE: api/suppliers/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteSupplier(int id)
        {
            var supplierFromRepo = _repository.GetSupplierById(id);
            if (supplierFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteSupplier(supplierFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
