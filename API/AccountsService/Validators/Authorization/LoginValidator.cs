using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using AccountsService.Models.Authorization;

namespace AccountsService.Validators.Authorization
{
    public class LoginValidator
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _requests;

        public LoginValidator(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
        }

        public List<string> Validate(LoginModel login)
        {
            var errors = new List<string>();

            if (login == null)
            {
                errors.Add("Данные для входа отсутствуют.");
                return errors;
            }

            ValidateRequiredFields(login, errors);
            ValidateFieldLengths(login, errors);
            ValidateFieldFormats(login, errors);
            ValidateEmailExists(login.Email, errors);

            return errors;
        }

        private void ValidateRequiredFields(LoginModel login, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(login.Email))
                errors.Add("Поле Email обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(login.PasswordHash))
                errors.Add("Поле PasswordHash обязательно для заполнения.");
        }

        private void ValidateFieldLengths(LoginModel login, List<string> errors)
        {
            CheckLength(errors, "Email", login.Email, 100);
            CheckLength(errors, "PasswordHash", login.PasswordHash, 500);
        }

        private static void CheckLength(List<string> errors, string fieldName, string value, int maxLength)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length > maxLength)
                errors.Add($"Поле {fieldName} не должно превышать {maxLength} символов.");
        }

        private void ValidateFieldFormats(LoginModel login, List<string> errors)
        {
            if (!IsValidEmail(login.Email))
                errors.Add("Поле Email должно содержать корректный адрес электронной почты.");
        }

        private static bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private void ValidateEmailExists(string email, List<string> errors)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand(_requests["EmailCheck"], connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    var count = Convert.ToInt32(command.ExecuteScalar());
                    if (count == 0)
                    {
                        errors.Add("Пользователь с указанным Email не найден.");
                    }
                }
            }
        }
    }
}
