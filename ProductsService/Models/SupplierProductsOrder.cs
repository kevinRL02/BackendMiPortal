using System.ComponentModel.DataAnnotations;
namespace ProductsService.Models
{

    public class SupplierProductsOrder
    {
        [Key]
        [Required]
        public int PurchaseOrderId { get; set; }
        [Required]
        public DateTime OrderaDate { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int SupplierId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }

        public Products? Products { get; set; }

        public Supplier? Supplier { get; set; }


    }
}