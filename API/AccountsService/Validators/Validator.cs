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
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool IsWithinMaxLength(string value, int maxLength)
        {
            return value != null && value.Length <= maxLength;
        }
    }
}
