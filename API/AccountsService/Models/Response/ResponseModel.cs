namespace AccountsService.Models.Response
{
    public class ResponseModel
    {
        public string Date { get; set; }
        public bool RequestExecution { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
    }
}
