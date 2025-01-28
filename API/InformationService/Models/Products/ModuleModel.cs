namespace InformationService.Models.Products
{
    public class ModuleModel
    {
        public int ModuleId { get; set; }
        public int ProductId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleDescription { get; set; }
        public string ModuleImageUrl { get; set; }
    }
}
