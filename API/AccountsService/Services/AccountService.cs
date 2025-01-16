using AccountsService.Interfaces;
using AccountsService.Models;
using AccountsService.Models.Account;
using MySql.Data.MySqlClient;
using System.Data;
using AccountsService.Validators;

namespace AccountsService.Services
{
    public class AccountService : IAccountService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _requests;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";
        private readonly Validator _validator;

        public AccountService(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
        }

        public async Task<ResponseModel> GetAccountInformationAsync(int? accountId)
        {
            if (!_validator.IsNotEmpty(accountId?.ToString()))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "AccountId cannot be empty"
                };
            }

            if (!_validator.IsPositiveNumber(accountId?.ToString()))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "AccountId must be a positive number"
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

        public async Task<ResponseModel> GetAccountOrdersAsync(int? accountId)
        {
            if (!_validator.IsNotEmpty(accountId?.ToString()))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "AccountId cannot be empty"
                };
            }

            if (!_validator.IsPositiveNumber(accountId?.ToString()))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "AccountId must be a positive number"
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
                                    CustomerId = reader.GetInt32("CustomerId"),
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

        public async Task<ResponseModel> ChangeNameAsync(string oldName, string newName)
        {
            if (!_validator.IsNotEmpty(oldName))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old name cannot be empty"
                };
            }

            if (!_validator.IsNotEmpty(newName))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New name cannot be empty"
                };
            }

            if (!_validator.IsWithinMaxLength(oldName, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old name length cannot exceed 50 characters"
                };
            }

            if (!_validator.IsWithinMaxLength(newName, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New name length cannot exceed 50 characters"
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
                                    Message = "Old username not found."
                                };

                            if (newNameExists > 0)
                                return new ResponseModel
                                {
                                    Date = currentDateTime,
                                    RequestExecution = false,
                                    Message = "New username is already taken."
                                };
                        }
                    }

                    using (var commandUpdate = new MySqlCommand(_requests["ChangeAccountName"], connection))
                    {
                        commandUpdate.Parameters.AddWithValue("@OldName", oldName);
                        commandUpdate.Parameters.AddWithValue("@NewName", newName);

                        int affectedRows = await commandUpdate.ExecuteNonQueryAsync();
                        if (affectedRows > 0)
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = true,
                                Message = "Name successfully changed."
                            };
                        else
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = false,
                                Message = "Failed to change username."
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


        public async Task<ResponseModel> ChangeEmailAsync(string oldEmail, string newEmail)
        {
            if (!_validator.IsNotEmpty(oldEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old email cannot be empty"
                };
            }

            if (!_validator.IsNotEmpty(newEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New email cannot be empty"
                };
            }

            if (!_validator.IsValidEmail(oldEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Invalid format for old email"
                };
            }

            if (!_validator.IsValidEmail(newEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Invalid format for new email"
                };
            }

            if (!_validator.IsWithinMaxLength(oldEmail, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old email length cannot exceed 50 characters"
                };
            }

            if (!_validator.IsWithinMaxLength(newEmail, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New email length cannot exceed 50 characters"
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

                        if (affectedRows > 0)
                        {
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = true,
                                Message = "Email successfully changed."
                            };
                        }
                        else
                        {
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = false,
                                Message = "Failed to change email."
                            };
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


        public async Task<ResponseModel> ChangePasswordAsync(string oldPasswordHash, string newPasswordHash)
        {
            if (!_validator.IsNotEmpty(newPasswordHash))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "newPasswordHash cannot be empty"
                };
            }

            if (!_validator.IsNotEmpty(oldPasswordHash))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "oldPasswordHash cannot be empty"
                };
            }

            if (!_validator.IsWithinMaxLength(oldPasswordHash, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "oldPasswordHash length cannot exceed 50 characters"
                };
            }

            if (!_validator.IsWithinMaxLength(newPasswordHash, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "newPasswordHash length cannot exceed 50 characters"
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

        public async Task<ResponseModel> DeleteAccountAsync(int? accountId)
        {
            if (!_validator.IsNotEmpty(accountId?.ToString()))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "AccountId cannot be empty"
                };
            }

            if (!_validator.IsPositiveNumber(accountId?.ToString()))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "AccountId must be a positive number"
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
                                Message = "Account and his orders deleted"
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
