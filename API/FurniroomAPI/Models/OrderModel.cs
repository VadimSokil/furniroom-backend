namespace FurniroomAPI.Models
{
    public class OrderModel
    {
        public int order_id { get; set; }
        public string customer_name { get; set; }
        public string phone_number { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string district { get; set; }
        public string city_name { get; set; }
        public string village_name { get; set; }
        public string street_name { get; set; }
        public string house_number { get; set; }
        public string apartment_number { get; set; }
        public string order_text { get; set; }
        public string delivery_type { get; set; }
        public string order_date { get; set; }
    }
}
