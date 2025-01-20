namespace InformationService.Models.Response
{
    public class ServiceResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
    }
}
