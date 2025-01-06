namespace InformationService.Models.Products
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public int SubcategoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription {  get; set; }
        public string ProductImageUrl { get; set; }
    }
}
