using System.ComponentModel.DataAnnotations;
namespace OrderService.Models
{

    public class Supplier
    {
        [Key]
        [Required]
        public int SupplierId { get; set; }

        [Required]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        public string ContacNumber { get; set; } = string.Empty;

        // Contact Information properties can be added here

        public ICollection<SupplierProductsOrder> SupplierProductsOrders { get; set; } = new List<SupplierProductsOrder>();

    }
}