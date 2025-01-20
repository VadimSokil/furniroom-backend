namespace FurniroomAPI.Models.Response
{
    public class GatewayResponseModel
    {
        public string Date { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
