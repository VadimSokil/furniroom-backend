using AccountsService.Interfaces;
using AccountsService.Models;
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

        public async Task SendEmailAsync(string recipientEmail, string messageBody)
        {
            var message = new MailMessage
            {
                From = new MailAddress(_serviceEmail, "Furniroom"),
                Subject = "Secure Notification",
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

        public async Task<bool> CheckEmailAsync(string email)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_requests["EmailCheck"], connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    var result = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return result > 0;
                }
            }
        }

        public async Task<string> GenerateCodeAsync(string email)
        {
            int verificationCode = Random.Shared.Next(1000, 9999);

            try
            {
                string messageBody = $"Hi, your verification code: {verificationCode}";
                await SendEmailAsync(email, messageBody);
                return $"Код отправлен на {email}. Ваш код: {verificationCode}";
            }
            catch (Exception ex)
            {
                return $"Ошибка при отправке письма: {ex.Message}";
            }
        }

        public async Task<bool> LoginAsync(LoginModel login)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_requests["Login"], connection))
                {
                    command.Parameters.AddWithValue("@Email", login.Email);

                    var result = await command.ExecuteScalarAsync();

                    if (result == null)
                        return false;

                    string storedHashPassword = result.ToString();
                    return storedHashPassword == login.PasswordHash;
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

                    try
                    {
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0 ? "Пользователь успешно добавлен" : "Не удалось добавить пользователя";
                    }
                    catch (Exception ex)
                    {
                        return $"Ошибка при добавлении пользователя: {ex.Message}";
                    }
                }
            }
        }

        public async Task<string> ResetPasswordAsync(string email)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var checkCommand = new MySqlCommand(_requests["EmailCheck"], connection))
                {
                    checkCommand.Parameters.AddWithValue("@Email", email);

                    var result = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());
                    if (result <= 0)
                    {
                        return "Email не найден в базе данных.";
                    }
                }

                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                string newPassword = new string(Enumerable.Repeat(chars, 8)
                    .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());

                string hashedPassword = HashPasswordWithMD5(newPassword);

                using (var updateCommand = new MySqlCommand(_requests["ResetPassword"], connection))
                {
                    updateCommand.Parameters.AddWithValue("@Email", email);
                    updateCommand.Parameters.AddWithValue("@Password", hashedPassword);

                    int rowsAffected = await updateCommand.ExecuteNonQueryAsync();
                    if (rowsAffected <= 0)
                    {
                        return "Не удалось сбросить пароль.";
                    }
                }

                try
                {
                    await SendEmailAsync(email, newPassword);
                    return "Новый пароль успешно отправлен на указанный email.";
                }
                catch (Exception ex)
                {
                    return $"Ошибка при отправке письма: {ex.Message}";
                }
            }
        }


    }
}
