namespace InformationService.Models.Products
{
    public class DrawingModel
    {
        public int DrawingId { get; set; }
        public int ProductId { get; set; }
        public string DrawingName { get; set; }
        public string DrawingDescription { get; set; }
        public string DrawingImageUrl { get; set; }
    }
}
