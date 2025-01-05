namespace AuthorizationService.Models
{
    public class RegisterModel
    {
        public int user_id { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public string phone_number { get; set; }
        public string location { get; set; }
    }
}
