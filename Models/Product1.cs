using System;
// Services like data validation can be added using Data Annotations
// Server side validation
using System.ComponentModel.DataAnnotations;

namespace WebApp_DFA_EFC.Models
{
    public partial class Product1
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0 and 999999.99")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, 10, ErrorMessage = "Quantity cannot be negative and more than 10")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Created date is required.")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
    }
}
