namespace AccountsService.Validation
{
    public class ValidationMethods
    {
        public bool IsNotEmptyValue(object value)
        {
            if (value == null) return false;

            if (value is string str && string.IsNullOrWhiteSpace(str))
                return false;

            return true;
        }

        public bool IsValidDigit(object value)
        {
            if (value is string str && int.TryParse(str, out int result))
            {
                return result > 0;
            }

            if (value is int intValue)
            {
                return intValue > 0;
            }

            return false;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidLength(string value, int lengthLimit)
        {
            if (value == null) return false;

            return value.Length <= lengthLimit;
        }

        public bool IsString(object value)
        {
            if (value is string)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsDigit(object value)
        {
            if (value is int)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
