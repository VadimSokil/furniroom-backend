using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using AccountsService.Models.Authorization;

namespace AccountsService.Validators.Authorization
{
    public class RegisterValidator
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _requests;

        public RegisterValidator(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
        }

        public List<string> Validate(RegisterModel register)
        {
            var errors = new List<string>();

            if (register == null)
            {
                errors.Add("Данные регистрации отсутствуют.");
                return errors;
            }

            ValidateRequiredFields(register, errors);
            ValidateFieldLengths(register, errors);
            ValidateFieldFormats(register, errors);
            ValidateUniqueFields(register, errors);

            return errors;
        }

        private void ValidateRequiredFields(RegisterModel register, List<string> errors)
        {
            if (register.AccountId <= 0)
                errors.Add("Поле AccountId должно быть положительным числом и обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(register.AccountName))
                errors.Add("Поле AccountName обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(register.Email))
                errors.Add("Поле Email обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(register.PasswordHash))
                errors.Add("Поле PasswordHash обязательно для заполнения.");
        }

        private void ValidateFieldLengths(RegisterModel register, List<string> errors)
        {
            CheckLength(errors, "AccountName", register.AccountName, 50);
            CheckLength(errors, "Email", register.Email, 100);
            CheckLength(errors, "PasswordHash", register.PasswordHash, 500);
        }

        private static void CheckLength(List<string> errors, string fieldName, string value, int maxLength)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length > maxLength)
                errors.Add($"Поле {fieldName} не должно превышать {maxLength} символов.");
        }

        private void ValidateFieldFormats(RegisterModel register, List<string> errors)
        {
            if (!IsValidEmail(register.Email))
                errors.Add("Поле Email должно содержать корректный адрес электронной почты.");
        }

        private static bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private void ValidateUniqueFields(RegisterModel register, List<string> errors)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand(_requests["CheckAccountId"], connection))
                {
                    command.Parameters.AddWithValue("@AccountId", register.AccountId);
                    var count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        errors.Add("Этот AccountId уже используется.");
                    }
                }

                using (var command = new MySqlCommand(_requests["CheckAccountName"], connection))
                {
                    command.Parameters.AddWithValue("@AccountName", register.AccountName);
                    var count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        errors.Add("Этот AccountName уже используется.");
                    }
                }

                using (var command = new MySqlCommand(_requests["CheckEmail"], connection))
                {
                    command.Parameters.AddWithValue("@Email", register.Email);
                    var count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        errors.Add("Этот Email уже используется.");
                    }
                }
            }
        }
    }
}
