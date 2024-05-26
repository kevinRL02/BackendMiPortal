using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductsService.Data
{
    public class ProductRepoImp : IProductRepo
    {
        private readonly AppDbContext _context;

        public ProductRepoImp(AppDbContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Products> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Products GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            return product;
        }

        public void CreateProduct(Products product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _context.Products.Add(product);
        }

        public void UpdateProduct(Products product)
        {
            // No se necesita código aquí ya que el contexto rastrea las entidades
        }

        public void DeleteProduct(Products product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _context.Products.Remove(product);
        }

        public IEnumerable<Products> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Where(p => p.ProductCategoryId == categoryId).ToList();
        }


        public IEnumerable<ProductCategory> GetAllProductCategories()
        {
            return _context.ProductCategory.ToList();
        }

        public ProductCategory GetProductCategoryById(int id)
        {
            var productCategory = _context.ProductCategory.FirstOrDefault(pc => pc.Id == id);
            if (productCategory == null)
            {
                return null;
            }
            return productCategory;
        }

        public void CreateProductCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
            {
                throw new ArgumentNullException(nameof(productCategory));
            }
            _context.ProductCategory.Add(productCategory);
        }

        public void UpdateProductCategory(ProductCategory productCategory)
        {
            // No se necesita código aquí ya que el contexto rastrea las entidades
        }

        public void DeleteProductCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
            {
                throw new ArgumentNullException(nameof(productCategory));
            }
            _context.ProductCategory.Remove(productCategory);
        }

        public IEnumerable<ProductRating> GetAllProductRatings()
        {
            return _context.ProductRating.ToList();
        }

        public ProductRating GetProductRatingById(int id)
        {
            var productRating = _context.ProductRating.FirstOrDefault(pr => pr.Id == id);
            if (productRating == null)
            {
                throw new ArgumentNullException(nameof(productRating), "Product rating not found");
            }
            return productRating;
        }

        public void CreateProductRating(ProductRating productRating)
        {
            if (productRating == null)
            {
                throw new ArgumentNullException(nameof(productRating));
            }
            _context.ProductRating.Add(productRating);
        }

        public void UpdateProductRating(ProductRating productRating)
        {
            // No se necesita código aquí ya que el contexto rastrea las entidades
        }

        public void DeleteProductRating(ProductRating productRating)
        {
            if (productRating == null)
            {
                throw new ArgumentNullException(nameof(productRating));
            }
            _context.ProductRating.Remove(productRating);
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return _context.Supplier.ToList();
        }

        public Supplier GetSupplierById(int id)
        {
            var supplier = _context.Supplier.FirstOrDefault(s => s.SupplierId == id);
            if (supplier == null)
            {
                throw new ArgumentNullException(nameof(supplier), "Supplier not found");
            }
            return supplier;
        }

        public void CreateSupplier(Supplier supplier)
        {
            if (supplier == null)
            {
                throw new ArgumentNullException(nameof(supplier));
            }
            _context.Supplier.Add(supplier);
        }

        public void UpdateSupplier(Supplier supplier)
        {
            // No se necesita código aquí ya que el contexto rastrea las entidades
        }

        public void DeleteSupplier(Supplier supplier)
        {
            if (supplier == null)
            {
                throw new ArgumentNullException(nameof(supplier));
            }
            _context.Supplier.Remove(supplier);
        }

        public IEnumerable<SupplierProductsOrder> GetAllSupplierProductsOrders()
        {
            return _context.SupplierProductsOrder.ToList();
        }

        public SupplierProductsOrder GetSupplierProductsOrderById(int id)
        {
            var supplierProductsOrder = _context.SupplierProductsOrder.FirstOrDefault(spo => spo.PurchaseOrderId == id);
            if (supplierProductsOrder == null)
            {
                throw new ArgumentNullException(nameof(supplierProductsOrder), "Supplier products order not found");
            }
            return supplierProductsOrder;
        }

        public void CreateSupplierProductsOrder(SupplierProductsOrder supplierProductsOrder)
        {
            if (supplierProductsOrder == null)
            {
                throw new ArgumentNullException(nameof(supplierProductsOrder));
            }
            _context.SupplierProductsOrder.Add(supplierProductsOrder);
        }

        public void UpdateSupplierProductsOrder(SupplierProductsOrder supplierProductsOrder)
        {
            // No se necesita código aquí ya que el contexto rastrea las entidades
        }

        public void DeleteSupplierProductsOrder(SupplierProductsOrder supplierProductsOrder)
        {
            if (supplierProductsOrder == null)
            {
                throw new ArgumentNullException(nameof(supplierProductsOrder));
            }
            _context.SupplierProductsOrder.Remove(supplierProductsOrder);
        }
    }
}
