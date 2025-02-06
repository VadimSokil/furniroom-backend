using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FurniroomAPI.Models
{
    public class RequestModels
    {
        public class OrderModel
        {
            [Required(ErrorMessage = "Order ID is a required field, must be of type int and cannot be empty")]
            [Range(1, int.MaxValue, ErrorMessage = "Order ID must be a positive number.")]
            [StringLength(10, ErrorMessage = "Order ID cannot exceed 10 characters.")]
            [DefaultValue(1)]
            public int OrderId { get; set; }

            [Required(ErrorMessage = "Order date is a required field, must be of type string and cannot be empty.")]
            [StringLength(20, ErrorMessage = "Order date cannot exceed 20 characters.")]
            [DefaultValue("string")]
            public string OrderDate { get; set; }

            [Required(ErrorMessage = "Account ID is a required field, must be of type int and cannot be empty")]
            [Range(1, int.MaxValue, ErrorMessage = "Account ID must be a positive number.")]
            [StringLength(10, ErrorMessage = "Account ID cannot exceed 10 characters.")]
            [DefaultValue(1)]
            public int AccountId { get; set; }

            [Required(ErrorMessage = "Phone number is a required field, must be of type string and cannot be empty.")]
            [Phone(ErrorMessage = "Phone number must follow the format +CCCXXXXXXXXX, where +CCC is the country code and X is the digits of the number.")]
            [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
            [DefaultValue("string")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Country is a required field, must be of type string and cannot be empty.")]
            [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
            [DefaultValue("string")]
            public string Country { get; set; }

            [Required(ErrorMessage = "Region is a required field, must be of type string and cannot be empty.")]
            [StringLength(100, ErrorMessage = "Region cannot exceed 100 characters.")]
            [DefaultValue("string")]
            public string Region { get; set; }

            [Required(ErrorMessage = "District is a required field, must be of type string and cannot be empty.")]
            [StringLength(100, ErrorMessage = "District cannot exceed 100 characters.")]
            [DefaultValue("string")]
            public string District { get; set; }

            [Required(ErrorMessage = "City is a required field, must be of type string and cannot be empty.")]
            [StringLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
            [DefaultValue("string")]
            public string City { get; set; }

            [Required(ErrorMessage = "Village is a required field, must be of type string and cannot be empty.")]
            [StringLength(100, ErrorMessage = "Village cannot exceed 100 characters.")]
            [DefaultValue("string")]
            public string Village { get; set; }

            [Required(ErrorMessage = "Street is a required field, must be of type string and cannot be empty.")]
            [StringLength(100, ErrorMessage = "Street cannot exceed 100 characters.")]
            [DefaultValue("string")]
            public string Street { get; set; }

            [Required(ErrorMessage = "House number is a required field, must be of type string and cannot be empty.")]
            [StringLength(20, ErrorMessage = "House number cannot exceed 20 characters.")]
            [DefaultValue("string")]
            public string HouseNumber { get; set; }

            [Required(ErrorMessage = "Apartment number is a required field, must be of type string and cannot be empty.")]
            [StringLength(20, ErrorMessage = "Apartment number cannot exceed 20 characters.")]
            [DefaultValue("string")]
            public string ApartmentNumber { get; set; }

            [Required(ErrorMessage = "Order text is a required field, must be of type string and cannot be empty.")]
            [StringLength(5000, ErrorMessage = "Order text cannot exceed 5000 characters.")]
            [DefaultValue("string")]
            public string OrderText { get; set; }

            [Required(ErrorMessage = "Delivery type is a required field, must be of type string and cannot be empty.")]
            [StringLength(20, ErrorMessage = "Delivery type cannot exceed 20 characters.")]
            [DefaultValue("string")]
            public string DeliveryType { get; set; }
        }

        public class QuestionModel
        {
            [Required(ErrorMessage = "Question ID is a required field, must be of type int and cannot be empty")]
            [Range(1, int.MaxValue, ErrorMessage = "Question ID must be a positive number.")]
            [StringLength(10, ErrorMessage = "Question ID cannot exceed 10 characters.")]
            [DefaultValue(1)]
            public int QuestionId { get; set; }

            [Required(ErrorMessage = "Order date is a required field, must be of type string and cannot be empty.")]
            [StringLength(20, ErrorMessage = "Order date cannot exceed 20 characters.")]
            [DefaultValue("string")]
            public string QuestionDate { get; set; }

            [Required(ErrorMessage = "User name is a required field, must be of type string and cannot be empty.")]
            [StringLength(100, ErrorMessage = "User name cannot exceed 100 characters.")]
            [DefaultValue("string")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Phone number is a required field, must be of type string and cannot be empty.")]
            [Phone(ErrorMessage = "Phone number must follow the format +CCCXXXXXXXXX, where +CCC is the country code and X is the digits of the number.")]
            [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
            [DefaultValue("string")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Question text is a required field, must be of type string and cannot be empty.")]
            [StringLength(5000, ErrorMessage = "Question text cannot exceed 5000 characters.")]
            [DefaultValue("string")]
            public string QuestionText { get; set; }
        }
    }
}
