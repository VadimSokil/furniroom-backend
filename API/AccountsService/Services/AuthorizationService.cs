using AccountsService.Interfaces;
using AccountsService.Models.Authorization;
using System.Net.Mail;
using System.Net;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using AccountsService.Models;
using AccountsService.Validation;

namespace AccountsService.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly string _connectionString;
        private readonly string _serviceEmail;
        private readonly string _servicePassword;
        private readonly Dictionary<string, string> _requests;
        private const string SmtpHost = "smtp.gmail.com";
        private const int SmtpPort = 587;
        public ValidationMethods validationMethods = new ValidationMethods();

        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";

        public AuthorizationService(string connectionString, string serviceEmail, string servicePassword, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _serviceEmail = serviceEmail;
            _servicePassword = servicePassword;
            _requests = requests;
        }

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

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
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

        public async Task<ResponseModel> CheckEmailAsync(string email)
        {
            if (!validationMethods.IsNotEmptyValue(email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email cannot be empty"
                };
            }

            if (!validationMethods.IsValidEmail(email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Invalid email address format"
                };
            }

            if (!validationMethods.IsValidLength(email, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email exceeds the maximum length of 100 characters"
                };
            }

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(_requests["CheckEmail"], connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        var result = Convert.ToInt32(await command.ExecuteScalarAsync());

                        if (result > 0)
                        {
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = true,
                                Message = "Email is already taken"
                            };
                        }
                    }
                }

                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = true,
                    Message = "Email is available"
                };
            }
            catch (MySqlException ex) 
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"Database error: {ex.Message}"
                };
            }
            catch (Exception ex) 
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"Unexpected error: {ex.Message}"
                };
            }
        }


        public async Task<ResponseModel> GenerateCodeAsync(string email)
        {
            if (!validationMethods.IsNotEmptyValue(email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email address cannot be empty"
                };
            }

            if (!validationMethods.IsValidEmail(email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Invalid email address format"
                };
            }

            if (!validationMethods.IsValidLength(email, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Maximum number of characters exceeded for email"
                };
            }

            try
            {
                int verificationCode = Random.Shared.Next(1000, 9999);

                string messageBody = $"Hi, your verification code: {verificationCode}";
                await SendEmailAsync(email, messageBody, "Verification code");

                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = true,
                    Message = "Vertification code generated",
                    Data = verificationCode
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"Unexpected error: {ex.Message}"
                };
            }

        }

        public async Task<ResponseModel> ResetPasswordAsync(string email)
        {
            if (!validationMethods.IsNotEmptyValue(email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email address cannot be empty"
                };
            }

            if (!validationMethods.IsValidEmail(email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Invalid email address format"
                };
            }

            if (!validationMethods.IsValidLength(email, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Maximum number of characters exceeded for email"
                };
            }

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var checkEmailCommand = new MySqlCommand(_requests["CheckEmail"], connection))
                    {
                        checkEmailCommand.Parameters.AddWithValue("@Email", email);
                        var emailExists = Convert.ToInt32(await checkEmailCommand.ExecuteScalarAsync()) > 0;

                        if (!emailExists)
                        {
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = false,
                                Message = "Email does not exist in the database"
                            };
                        }
                    }

                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    string newPassword = new string(Enumerable.Repeat(chars, 8)
                        .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());

                    string hashedPassword = HashPasswordWithMD5(newPassword);

                    using (var updateCommand = new MySqlCommand(_requests["ResetPassword"], connection))
                    {
                        updateCommand.Parameters.AddWithValue("@Email", email);
                        updateCommand.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                        await updateCommand.ExecuteNonQueryAsync();
                    }

                    await SendEmailAsync(email, $"Hi, your new password: {newPassword}", "Reset Password");

                    return new ResponseModel
                    {
                        Date = currentDateTime,
                        RequestExecution = true,
                        Message = "Password reseted",
                        Data = newPassword
                    };
                }

            }
            catch (MySqlException ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"Database error: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"Unexpected error: {ex.Message}"
                };
            }
        }

        public async Task<ResponseModel> LoginAsync(LoginModel login)
        {
            if (!validationMethods.IsNotEmptyValue(login.Email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email address cannot be empty"
                };
            }

            if (!validationMethods.IsValidEmail(login.Email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Invalid email address format"
                };
            }

            if (!validationMethods.IsValidLength(login.Email, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Maximum number of characters exceeded for email"
                };
            }

            if (!validationMethods.IsNotEmptyValue(login.PasswordHash))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "PasswordHash cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(login.PasswordHash, 500))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Maximum number of characters exceeded for passwordHash"
                };
            }

            try
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
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = true,
                                Message = $"Invalid login or password"
                            };
                        }

                        return new ResponseModel
                        {
                            Date = currentDateTime,
                            RequestExecution = true,
                            Message = "Login and password is correct",
                            Data = result
                        };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"Database error: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"Unexpected error: {ex.Message}"
                };
            }
        }

        public async Task<ResponseModel> RegisterAsync(RegisterModel register)
        {

            if (!validationMethods.IsNotEmptyValue(register.AccountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "AccountId cannot be empty"
                };
            }

            if (!validationMethods.IsValidDigit(register.AccountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "AccountId must be a positive number"
                };
            }

            if (!validationMethods.IsNotEmptyValue(register.AccountName))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "AccountName cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(register.AccountName, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Maximum number of characters exceeded for accountName"
                };
            }

            if (!validationMethods.IsNotEmptyValue(register.Email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email address cannot be empty"
                };
            }

            if (!validationMethods.IsValidEmail(register.Email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Invalid email address format"
                };
            }

            if (!validationMethods.IsValidLength(register.Email, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Maximum number of characters exceeded for email"
                };
            }

            if (!validationMethods.IsNotEmptyValue(register.PasswordHash))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "PasswordHash cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(register.PasswordHash, 500))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Maximum number of characters exceeded for passwordHash"
                };
            }

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var checkIdCommand = new MySqlCommand(_requests["CheckAccountId"], connection))
                    {
                        checkIdCommand.Parameters.AddWithValue("@AccountId", register.AccountId);
                        var idExists = Convert.ToInt32(await checkIdCommand.ExecuteScalarAsync()) > 0;

                        if (idExists)
                        {
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = false,
                                Message = "AccountId is already taken"
                            };
                        }
                    }

                    using (var checkEmailCommand = new MySqlCommand(_requests["CheckEmail"], connection))
                    {
                        checkEmailCommand.Parameters.AddWithValue("@Email", register.Email);
                        var emailExists = Convert.ToInt32(await checkEmailCommand.ExecuteScalarAsync()) > 0;

                        if (emailExists)
                        {
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = false,
                                Message = "Email is already taken"
                            };
                        }
                    }

                    using (var checkNameCommand = new MySqlCommand(_requests["CheckAccountName"], connection))
                    {
                        checkNameCommand.Parameters.AddWithValue("@AccountName", register.AccountName);
                        var nameExists = Convert.ToInt32(await checkNameCommand.ExecuteScalarAsync()) > 0;

                        if (nameExists)
                        {
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = false,
                                Message = "AccountName is already taken"
                            };
                        }
                    }

                    using (var command = new MySqlCommand(_requests["AddNewUser"], connection))
                    {
                        command.Parameters.AddWithValue("@AccountId", register.AccountId);
                        command.Parameters.AddWithValue("@AccountName", register.AccountName);
                        command.Parameters.AddWithValue("@Email", register.Email);
                        command.Parameters.AddWithValue("@PasswordHash", register.PasswordHash);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = true,
                    Message = "Account successfully created"
                };
            }
            catch (MySqlException ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"Database error: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"Unexpected error: {ex.Message}"
                };
            }

        }
    }
}
