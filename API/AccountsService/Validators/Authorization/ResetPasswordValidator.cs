using MySql.Data.MySqlClient;

namespace AccountsService.Validators.Authorization
{
    public class ResetPasswordValidator
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _requests;

        public ResetPasswordValidator(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
        }

        public async Task<List<string>> ValidateAsync(string email)
        {
            var errors = new List<string>();
            var emailValidator = new EmailValidator();

            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add("Поле Email обязательно для заполнения."); 
                return errors; 
            }

            var emailErrors = emailValidator.Validate(email);
            errors.AddRange(emailErrors);

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(_requests["EmailCheck"], connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    var count = Convert.ToInt32(await command.ExecuteScalarAsync());
                    if (count == 0)
                    {
                        errors.Add("Пользователь с указанным Email не найден.");
                    }
                }
            }

            return errors;
        }
    }
}
