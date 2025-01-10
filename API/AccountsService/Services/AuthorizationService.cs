using AccountsService.Interfaces;
using AccountsService.Models.Authorization;
using System.Net.Mail;
using System.Net;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace AccountsService.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly string _connectionString;
        private readonly string _serviceEmail;
        private readonly string _servicePassword;
        private readonly Dictionary<string, string> _requests;

        public AuthorizationService(string connectionString, string serviceEmail, string servicePassword, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _serviceEmail = serviceEmail;
            _servicePassword = servicePassword;
            _requests = requests;
        }

        private const string SmtpHost = "smtp.gmail.com";
        private const int SmtpPort = 587;

        private async Task SendEmailAsync(string recipientEmail, string messageBody, string subject)
        {
            var message = new MailMessage
            {
                From = new MailAddress(_serviceEmail, "Furniroom"),
                Subject = subject,
                Body = messageBody,
                IsBodyHtml = false
            };

            message.To.Add(new MailAddress(recipientEmail));

            using (var smtp = new SmtpClient(SmtpHost, SmtpPort))
            {
                smtp.Credentials = new NetworkCredential(_serviceEmail, _servicePassword);
                smtp.EnableSsl = true;

                await smtp.SendMailAsync(message);
            }
        }

        private string HashPasswordWithMD5(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public async Task<string> CheckEmailAsync(EmailModel email)
        {
            var emailAddress = email.Email;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_requests["EmailCheck"], connection))
                {
                    command.Parameters.AddWithValue("@Email", emailAddress);
                    var result = Convert.ToInt32(await command.ExecuteScalarAsync());
                    if (result > 0)
                    {
                        return "Почта занята.";
                    }
                }
            }
            return "Почта свободна."; 
        }


        public async Task<string> GenerateCodeAsync(EmailModel email)
        {
            var emailAddress = email.Email;

            int verificationCode = Random.Shared.Next(1000, 9999);

            string messageBody = $"Hi, your verification code: {verificationCode}";
            await SendEmailAsync(emailAddress, messageBody, "Verification code");
            return $"Код отправлен на {emailAddress}. Ваш код: {verificationCode}";
        }

        public async Task<int> LoginAsync(LoginModel login)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_requests["Login"], connection))
                {
                    command.Parameters.AddWithValue("@Email", login.Email);
                    command.Parameters.AddWithValue("@PasswordHash", login.PasswordHash);

                    var result = await command.ExecuteScalarAsync();
                    if (result == null)
                    {
                        return 0; 
                    }

                    return Convert.ToInt32(result);
                }
            }
        }



        public async Task<string> RegisterAsync(RegisterModel register)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_requests["AddNewUser"], connection))
                {
                    command.Parameters.AddWithValue("@AccountId", register.AccountId);
                    command.Parameters.AddWithValue("@AccountName", register.AccountName);
                    command.Parameters.AddWithValue("@Email", register.Email);
                    command.Parameters.AddWithValue("@PasswordHash", register.PasswordHash);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return "Аккаунт успешно добавлен.";
        }


        public async Task<string> ResetPasswordAsync(EmailModel email)
        {
            var emailAddress = email.Email;

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string newPassword = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());

            string hashedPassword = HashPasswordWithMD5(newPassword);

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var updateCommand = new MySqlCommand(_requests["ResetPassword"], connection))
                {
                    updateCommand.Parameters.AddWithValue("@Email", emailAddress);
                    updateCommand.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                    int rowsAffected = await updateCommand.ExecuteNonQueryAsync();
                    if (rowsAffected <= 0)
                    {
                        return "Не удалось сбросить пароль.";
                    }
                }

                await SendEmailAsync(emailAddress, $"Hi, your new password: {newPassword}", "Reset Password");
                return $"Пароль отправлен на {emailAddress}. Ваш новый пароль: {newPassword}";
            }
        }
    }
}
