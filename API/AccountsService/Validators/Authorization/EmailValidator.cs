using System.Text.RegularExpressions;

namespace AccountsService.Validators.Authorization
{
    public class EmailValidator
    {
        public List<string> Validate(string email)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add("Поле Email обязательно для заполнения."); 
            }
            else if (!IsValidEmail(email))
            {
                errors.Add("Поле Email должно содержать корректный адрес электронной почты."); 
            }

            return errors;
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
