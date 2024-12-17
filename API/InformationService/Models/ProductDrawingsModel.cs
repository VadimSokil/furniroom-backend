namespace InformationService.Models
{
    public class ProductDrawingsModel
    {
        public int product_id { get; set; }
        public int drawing_id { get; set; }
        public string drawing_name { get; set; }
        public string drawing_description { get; set; }
        public string drawing_img_url { get; set; }
    }
}
