using AccountsService.Interfaces;
using AccountsService.Models.Response;
using AccountsService.Models.Account;
using MySql.Data.MySqlClient;
using System.Data;
using AccountsService.Validation;

namespace AccountsService.Services
{
    public class AccountService : IAccountService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _requests;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";
        public ValidationMethods validationMethods = new ValidationMethods();

        public AccountService(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
        }

        public async Task<ResponseModel> GetAccountInformationAsync(int? accountId)
        {
            if (!validationMethods.IsNotEmptyValue(accountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID cannot be empty"
                };
            }

            if (!validationMethods.IsValidDigit(accountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID must be a positive number"
                };
            }

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(_requests["GetAccountInformation"], connection))
                    {
                        command.Parameters.AddWithValue("@AccountId", accountId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var accountInformation = new AccountInformationModel
                                {
                                    AccountName = reader.GetString("AccountName"),
                                    Email = reader.GetString("Email")
                                };

                                return new ResponseModel
                                {
                                    Date = currentDateTime,
                                    RequestExecution = true,
                                    Message = "Account information successfully retrieved",
                                    Data = accountInformation
                                };
                            }
                            else
                            {
                                return new ResponseModel
                                {
                                    Date = currentDateTime,
                                    RequestExecution = false,
                                    Message = "Account not found"
                                };
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        public async Task<ResponseModel> GetAccountOrdersAsync(int? accountId)
        {
            if (!validationMethods.IsNotEmptyValue(accountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID cannot be empty"
                };
            }

            if (!validationMethods.IsValidDigit(accountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID must be a positive number"
                };
            }

            try
            {
                var orders = new List<AccountOrdersModel>();

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(_requests["GetOrders"], connection))
                    {
                        command.Parameters.AddWithValue("@AccountId", accountId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                orders.Add(new AccountOrdersModel
                                {
                                    OrderId = reader.GetInt32("OrderId"),
                                    OrderDate = reader.GetString("OrderDate"),
                                    AccountId = reader.GetInt32("AccountId"),
                                    PhoneNumber = reader.GetString("PhoneNumber"),
                                    Country = reader.GetString("Country"),
                                    Region = reader.GetString("Region"),
                                    District = reader.GetString("District"),
                                    City = reader.GetString("City"),
                                    Village = reader.GetString("Village"),
                                    Street = reader.GetString("Street"),
                                    HouseNumber = reader.GetString("HouseNumber"),
                                    ApartmentNumber = reader.GetString("ApartmentNumber"),
                                    OrderText = reader.GetString("OrderText"),
                                    DeliveryType = reader.GetString("DeliveryType"),
                                    OrderStatus = reader.GetString("OrderStatus")
                                });
                            }
                        }
                    }
                }

                if (orders.Count == 0)
                {
                    return new ResponseModel
                    {
                        Date = currentDateTime,
                        RequestExecution = false,
                        Message = "Orders not found"
                    };
                }

                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = true,
                    Message = "Orders successfully retrieved",
                    Data = orders
                };
            }
            catch (MySqlException ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        public async Task<ResponseModel> ChangeNameAsync(string oldName, string newName)
        {
            if (!validationMethods.IsNotEmptyValue(oldName))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old name cannot be empty"
                };
            }

            if (!validationMethods.IsNotEmptyValue(newName))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New name cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(oldName, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old name exceeds the maximum allowed length of 50 characters"
                };
            }

            if (!validationMethods.IsValidLength(newName, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New name exceeds the maximum allowed length of 50 characters"
                };
            }

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var commandCheck = new MySqlCommand(_requests["CheckAccountNames"], connection))
                    {
                        commandCheck.Parameters.AddWithValue("@OldName", oldName);
                        commandCheck.Parameters.AddWithValue("@NewName", newName);

                        using (var reader = await commandCheck.ExecuteReaderAsync())
                        {
                            await reader.ReadAsync();

                            var oldNameExists = reader.GetInt32("OldNameExists");
                            var newNameExists = reader.GetInt32("NewNameExists");

                            if (oldNameExists == 0)
                                return new ResponseModel
                                {
                                    Date = currentDateTime,
                                    RequestExecution = false,
                                    Message = "Old username not found"
                                };

                            if (newNameExists > 0)
                                return new ResponseModel
                                {
                                    Date = currentDateTime,
                                    RequestExecution = false,
                                    Message = "New username is already in use"
                                };
                        }
                    }

                    using (var commandUpdate = new MySqlCommand(_requests["ChangeAccountName"], connection))
                    {
                        commandUpdate.Parameters.AddWithValue("@OldName", oldName);
                        commandUpdate.Parameters.AddWithValue("@NewName", newName);

                        int affectedRows = await commandUpdate.ExecuteNonQueryAsync();

                        return new ResponseModel
                        {
                            Date = currentDateTime,
                            RequestExecution = true,
                            Message = "Name successfully changed"
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
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }


        public async Task<ResponseModel> ChangeEmailAsync(string oldEmail, string newEmail)
        {
            if (!validationMethods.IsNotEmptyValue(oldEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old email cannot be empty"
                };
            }

            if (!validationMethods.IsNotEmptyValue(newEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New email cannot be empty"
                };
            }

            if (!validationMethods.IsValidEmail(oldEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old email address format is invalid"
                };
            }

            if (!validationMethods.IsValidEmail(newEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New email address format is invalid"
                };
            }

            if (!validationMethods.IsValidLength(oldEmail, 254))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old email exceeds the maximum allowed length of 254 characters"
                };
            }

            if (!validationMethods.IsValidLength(newEmail, 254))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New email exceeds the maximum allowed length of 254 characters"
                };
            }

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var checkOldEmailCommand = new MySqlCommand(_requests["CheckOldEmail"], connection))
                    {
                        checkOldEmailCommand.Parameters.AddWithValue("@OldEmail", oldEmail);
                        object oldEmailResult = await checkOldEmailCommand.ExecuteScalarAsync();
                        if (oldEmailResult == null || Convert.ToInt32(oldEmailResult) == 0)
                        {
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = false,
                                Message = "The specified old email was not found."
                            };
                        }
                    }

                    using (var checkNewEmailCommand = new MySqlCommand(_requests["CheckNewEmail"], connection))
                    {
                        checkNewEmailCommand.Parameters.AddWithValue("@NewEmail", newEmail);
                        object newEmailResult = await checkNewEmailCommand.ExecuteScalarAsync();
                        if (newEmailResult != null && Convert.ToInt32(newEmailResult) > 0)
                        {
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = false,
                                Message = "New email is already in use."
                            };
                        }
                    }

                    using (var changeEmailCommand = new MySqlCommand(_requests["ChangeEmail"], connection))
                    {
                        changeEmailCommand.Parameters.AddWithValue("@OldEmail", oldEmail);
                        changeEmailCommand.Parameters.AddWithValue("@NewEmail", newEmail);
                        int affectedRows = await changeEmailCommand.ExecuteNonQueryAsync();

                        return new ResponseModel
                        {
                            Date = currentDateTime,
                            RequestExecution = true,
                            Message = "Email successfully changed."
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
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }


        public async Task<ResponseModel> ChangePasswordAsync(string oldPasswordHash, string newPasswordHash)
        {
            if (!validationMethods.IsNotEmptyValue(newPasswordHash))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New password cannot be empty"
                };
            }

            if (!validationMethods.IsNotEmptyValue(oldPasswordHash))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old password cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(oldPasswordHash, 128))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old email exceeds the maximum allowed length of 128 characters"
                };
            }

            if (!validationMethods.IsValidLength(newPasswordHash, 128))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New email exceeds the maximum allowed length of 128 characters"
                };
            }

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand(_requests["ChangePassword"], connection))
                    {
                        command.Parameters.AddWithValue("@OldPasswordHash", oldPasswordHash);
                        command.Parameters.AddWithValue("@NewPasswordHash", newPasswordHash);

                        int affectedRows = await command.ExecuteNonQueryAsync();
                        if (affectedRows > 0)
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = true,
                                Message = "Password changed"
                            };
                        else
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = false,
                                Message = "Old password not found"
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
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        public async Task<ResponseModel> DeleteAccountAsync(int? accountId)
        {
            if (!validationMethods.IsNotEmptyValue(accountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID cannot be empty"
                };
            }

            if (!validationMethods.IsValidDigit(accountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID must be a positive number"
                };
            }

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand(_requests["DeleteAccount"], connection))
                    {
                        command.Parameters.AddWithValue("@AccountId", accountId);

                        int affectedRows = await command.ExecuteNonQueryAsync();
                        if (affectedRows > 0)
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = true,
                                Message = "Account and orders is deleted"
                            };
                        else
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = false,
                                Message = "Account not found"
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
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }


    }
}
