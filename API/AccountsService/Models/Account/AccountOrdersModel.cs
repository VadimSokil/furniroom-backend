namespace AccountsService.Models.Account
{
    public class AccountOrdersModel
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public int AccountId { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string? City { get; set; }
        public string? Village { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string? ApartmentNumber { get; set; }
        public string OrderText { get; set; }
        public string DeliveryType { get; set; }
        public string OrderStatus { get; set; }
    }
}
