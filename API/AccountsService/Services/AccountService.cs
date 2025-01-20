using AccountsService.Interfaces;
using AccountsService.Models.Response;
using AccountsService.Models.Account;
using MySql.Data.MySqlClient;
using System.Data;

namespace AccountsService.Services
{
    public class AccountService : IAccountService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _requests;

        public AccountService(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
        }

        public async Task<ServiceResponseModel> GetAccountInformationAsync(int? accountId)
        {
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

                                return new ServiceResponseModel
                                {
                                    Status = true,
                                    Message = "Account information successfully retrieved.",
                                    Data = accountInformation
                                };
                            }
                            else
                            {
                                return new ServiceResponseModel
                                {
                                    Status = false,
                                    Message = "Account not found."
                                };
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponseModel> GetAccountOrdersAsync(int? accountId)
        {
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
                    return new ServiceResponseModel
                    {
                        Status = false,
                        Message = "Orders not found."
                    };
                }

                return new ServiceResponseModel
                {
                    Status = true,
                    Message = "Orders information successfully retrieved.",
                    Data = orders
                };
            }
            catch (MySqlException ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponseModel> ChangeNameAsync(ChangeNameModel changeName)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var commandCheck = new MySqlCommand(_requests["CheckAccountNames"], connection))
                    {
                        commandCheck.Parameters.AddWithValue("@OldName", changeName.OldName);
                        commandCheck.Parameters.AddWithValue("@NewName", changeName.NewName);

                        using (var reader = await commandCheck.ExecuteReaderAsync())
                        {
                            await reader.ReadAsync();

                            var oldNameExists = reader.GetInt32("OldNameExists");
                            var newNameExists = reader.GetInt32("NewNameExists");

                            if (oldNameExists == 0)
                                return new ServiceResponseModel
                                {
                                    Status = false,
                                    Message = "Old name not found."
                                };

                            if (newNameExists > 0)
                                return new ServiceResponseModel
                                {
                                    Status = false,
                                    Message = "New name is already in use."
                                };
                        }
                    }

                    using (var commandUpdate = new MySqlCommand(_requests["ChangeAccountName"], connection))
                    {
                        commandUpdate.Parameters.AddWithValue("@OldName", changeName.OldName);
                        commandUpdate.Parameters.AddWithValue("@NewName", changeName.NewName);

                        int affectedRows = await commandUpdate.ExecuteNonQueryAsync();

                        return new ServiceResponseModel
                        {
                            Status = true,
                            Message = "Name successfully changed."
                        };

                    }
                }
            }
            catch (MySqlException ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }


        public async Task<ServiceResponseModel> ChangeEmailAsync(ChangeEmailModel changeEmail)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var checkOldEmailCommand = new MySqlCommand(_requests["CheckOldEmail"], connection))
                    {
                        checkOldEmailCommand.Parameters.AddWithValue("@OldEmail", changeEmail.OldEmail);
                        object oldEmailResult = await checkOldEmailCommand.ExecuteScalarAsync();
                        if (oldEmailResult == null || Convert.ToInt32(oldEmailResult) == 0)
                        {
                            return new ServiceResponseModel
                            {
                                Status = false,
                                Message = "Old email not found."
                            };
                        }
                    }

                    using (var checkNewEmailCommand = new MySqlCommand(_requests["CheckNewEmail"], connection))
                    {
                        checkNewEmailCommand.Parameters.AddWithValue("@NewEmail", changeEmail.NewEmail);
                        object newEmailResult = await checkNewEmailCommand.ExecuteScalarAsync();
                        if (newEmailResult != null && Convert.ToInt32(newEmailResult) > 0)
                        {
                            return new ServiceResponseModel
                            {
                                Status = false,
                                Message = "New email is already in use."
                            };
                        }
                    }

                    using (var changeEmailCommand = new MySqlCommand(_requests["ChangeEmail"], connection))
                    {
                        changeEmailCommand.Parameters.AddWithValue("@OldEmail", changeEmail.OldEmail);
                        changeEmailCommand.Parameters.AddWithValue("@NewEmail", changeEmail.NewEmail);
                        int affectedRows = await changeEmailCommand.ExecuteNonQueryAsync();

                        return new ServiceResponseModel
                        {
                            Status = true,
                            Message = "Email successfully changed."
                        };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }


        public async Task<ServiceResponseModel> ChangePasswordAsync(ChangePasswordModel changePassword)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand(_requests["ChangePassword"], connection))
                    {
                        command.Parameters.AddWithValue("@OldPasswordHash", changePassword.OldPasswordHash);
                        command.Parameters.AddWithValue("@NewPasswordHash", changePassword.NewPasswordHash);

                        int affectedRows = await command.ExecuteNonQueryAsync();
                        if (affectedRows > 0)
                            return new ServiceResponseModel
                            {
                                Status = true,
                                Message = "Password successfully changed."
                            };
                        else
                            return new ServiceResponseModel
                            {
                                Status = false,
                                Message = "Old password not found."
                            };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponseModel> DeleteAccountAsync(int? accountId)
        {
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
                            return new ServiceResponseModel
                            {
                                Status = true,
                                Message = "Account and orders successfully deleted."
                            };
                        else
                            return new ServiceResponseModel
                            {
                                Status = false,
                                Message = "Account not found."
                            };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"A database error occurred: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponseModel
                {
                    Status = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }


    }
}
