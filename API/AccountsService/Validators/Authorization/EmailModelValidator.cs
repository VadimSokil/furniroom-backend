using FluentValidation;
using MySql.Data.MySqlClient;

namespace AccountsService.Validators.Authorization
{
    public class EmailModelValidator : AbstractValidator<string>
    {
        private readonly string _connectionString;

        public EmailModelValidator(string connectionString)
        {
            _connectionString = connectionString;

            RuleFor(x => x)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync(EmailExists).WithMessage("Email does not exist in the database.");
        }

        private async Task<bool> EmailExists(string email, CancellationToken cancellationToken)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                using (var command = new MySqlCommand("SELECT COUNT(*) FROM Accounts WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    var count = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
                    return count > 0;
                }
            }
        }
    }
}
