namespace InformationService.Models.Products
{
    public class ColorModel
    {
        public int ColorId { get; set; }
        public int ModuleId { get; set; }
        public string FacadeColorName { get; set; }
        public string FacadeColorImageUrl { get; set; }
        public string BodyColorName { get; set; }
        public string BodyColorImageUrl { get; set; }
    }
}
