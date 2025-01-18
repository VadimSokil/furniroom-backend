using System.ComponentModel.DataAnnotations;

namespace AccountsService.Models.Request
{
    public class OrderModel
    {
        [Required]
        public int? OrderId { get; set; }
        [Required]
        public string? OrderDate { get; set; }
        [Required]
        public int? AccountId { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Country { get; set; }
        [Required]
        public string? Region { get; set; }
        [Required]
        public string? District { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Village { get; set; }
        [Required]
        public string? Street { get; set; }
        [Required]
        public string? HouseNumber { get; set; }
        [Required]
        public string? ApartmentNumber { get; set; }
        [Required]
        public string? OrderText { get; set; }
        [Required]
        public string? DeliveryType { get; set; }
    }
}
