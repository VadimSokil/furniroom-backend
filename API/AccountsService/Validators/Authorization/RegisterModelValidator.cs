using AccountsService.Models.Authorization;
using FluentValidation;
using MySql.Data.MySqlClient;

namespace AccountsService.Validators.Authorization
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        private readonly string _connectionString;

        public RegisterModelValidator(string connectionString)
        {
            _connectionString = connectionString;

            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("AccountId cannot be empty.")
                .GreaterThan(0).WithMessage("AccountId must be a positive number.")
                .MustAsync(AccountIdIsUnique).WithMessage("AccountId is already taken.");

            RuleFor(x => x.AccountName)
                .NotEmpty().WithMessage("AccountName cannot be empty.")
                .MaximumLength(50).WithMessage("AccountName cannot exceed 50 characters.")
                .MustAsync(AccountNameIsUnique).WithMessage("AccountName is already taken.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync(EmailIsUnique).WithMessage("Email is already taken.");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("PasswordHash cannot be empty.")
                .MaximumLength(500).WithMessage("PasswordHash cannot exceed 500 characters.");
        }

        private async Task<bool> AccountIdIsUnique(int accountId, CancellationToken cancellationToken)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                using (var command = new MySqlCommand("SELECT COUNT(*) FROM Accounts WHERE AccountId = @AccountId", connection))
                {
                    command.Parameters.AddWithValue("@AccountId", accountId);
                    var count = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
                    return count == 0;
                }
            }
        }

        private async Task<bool> AccountNameIsUnique(string accountName, CancellationToken cancellationToken)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                using (var command = new MySqlCommand("SELECT COUNT(*) FROM Accounts WHERE AccountName = @AccountName", connection))
                {
                    command.Parameters.AddWithValue("@AccountName", accountName);
                    var count = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
                    return count == 0;
                }
            }
        }

        private async Task<bool> EmailIsUnique(string email, CancellationToken cancellationToken)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                using (var command = new MySqlCommand("SELECT COUNT(*) FROM Accounts WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    var count = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
                    return count == 0;
                }
            }
        }
    }
}
