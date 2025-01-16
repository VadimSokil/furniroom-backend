using System.Net.Mail;
using System.Text.RegularExpressions;

namespace AccountsService.Validators
{
    public class Validator
    {
        public bool IsNotEmpty(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }


        public bool IsPositiveNumber(string value)
        {
            if (decimal.TryParse(value, out decimal number))
            {
                return number > 0;
            }
            return false;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase);
        }


        public bool IsWithinMaxLength(string value, int maxLength)
        {
            if (value.Length > maxLength)
            {
                return false;
            }
            else return true;
        }
    }
}
