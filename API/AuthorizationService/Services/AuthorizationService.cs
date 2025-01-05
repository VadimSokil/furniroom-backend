using AuthorizationService.Interfaces;
using AuthorizationService.Models;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;

namespace AuthorizationService.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly string _connectionString;
        private readonly string _serviceEmail;
        private readonly string _servicePassword;
        private readonly Dictionary<string, string> _sqlRequests;

        private const string SmtpHost = "smtp.gmail.com";
        private const int SmtpPort = 587;

        public AuthorizationService(string connectionString, string serviceEmail, string servicePassword, Dictionary<string, string> sqlRequests)
        {
            _connectionString = connectionString;
            _serviceEmail = serviceEmail;
            _servicePassword = servicePassword;
            _sqlRequests = sqlRequests;
        }

        private void SendEmail(string recipientEmail, int verificationCode)
        {
            var message = new MailMessage
            {
                From = new MailAddress(_serviceEmail, "Furniroom"),
                Subject = "Secure code",
                Body = $"Hi, your code: {verificationCode}",
                IsBodyHtml = false
            };

            message.To.Add(new MailAddress(recipientEmail));

            using (var smtp = new SmtpClient(SmtpHost, SmtpPort))
            {
                smtp.Credentials = new NetworkCredential(_serviceEmail, _servicePassword);
                smtp.EnableSsl = true;

                smtp.Send(message);
            }
        }

        public bool CheckEmailExists(string email)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand(_sqlRequests["EmailCheck"], connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    var result = Convert.ToInt32(command.ExecuteScalar());
                    return result > 0;
                }
            }
        }

        public bool AddNewUser(RegisterModel register)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand(_sqlRequests["AddNewUser"], connection))
                {
                    command.Parameters.AddWithValue("@UserId", register.user_id);
                    command.Parameters.AddWithValue("@Email", register.email);
                    command.Parameters.AddWithValue("@Pass", register.pass);
                    command.Parameters.AddWithValue("@FirstName", register.first_name);
                    command.Parameters.AddWithValue("@SecondName", register.second_name);
                    command.Parameters.AddWithValue("@PhoneNumber", register.phone_number);
                    command.Parameters.AddWithValue("@Location", register.location);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public int GenerateVerificationCode(string email)
        {
            Random random = new Random();
            int verificationCode = random.Next(1000, 9999);

            try
            {
                SendEmail(email, verificationCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отправке письма: {ex.Message}");
                throw;
            }

            return verificationCode;
        }

        public string ResetPassword(string email)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (var checkCommand = new MySqlCommand(_sqlRequests["EmailCheck"], connection))
                {
                    checkCommand.Parameters.AddWithValue("@Email", email);

                    var result = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (result <= 0)
                    {
                        return string.Empty;
                    }
                }

                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                Random random = new Random();
                string newPassword = new string(Enumerable.Repeat(chars, 8)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                using (var updateCommand = new MySqlCommand(_sqlRequests["ResetPassword"], connection))
                {
                    updateCommand.Parameters.AddWithValue("@Email", email);
                    updateCommand.Parameters.AddWithValue("@Password", newPassword);

                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected <= 0)
                    {
                        return string.Empty;
                    }
                }

                try
                {
                    var message = new MailMessage
                    {
                        From = new MailAddress(_serviceEmail, "Furniroom"),
                        Subject = "Reset password",
                        Body = $"Hi, your new password: {newPassword}",
                        IsBodyHtml = false
                    };

                    message.To.Add(new MailAddress(email));

                    using (var smtp = new SmtpClient(SmtpHost, SmtpPort))
                    {
                        smtp.Credentials = new NetworkCredential(_serviceEmail, _servicePassword);
                        smtp.EnableSsl = true;

                        smtp.Send(message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при отправке письма: {ex.Message}");
                    return string.Empty;
                }

                return newPassword;
            }
        }

        public string Login(LoginModel loginModel)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand(_sqlRequests["Login"], connection))
                {
                    command.Parameters.AddWithValue("@Email", loginModel.email);
                    command.Parameters.AddWithValue("@Password", loginModel.password);

                    var result = Convert.ToInt32(command.ExecuteScalar());
                    if (result > 0)
                    {
                        return "Login successful";
                    }
                    else
                    {
                        return "Invalid email or password";
                    }
                }
            }
        }

    }
}
