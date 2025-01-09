using AccountsService.Interfaces;
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

        public async Task<AccountInformationModel> GetAccountInformationAsync(int accountId)
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
                            return new AccountInformationModel
                            {
                                AccountName = reader.GetString("AccountName"),
                                Email = reader.GetString("Email")
                            };
                        }
                        else
                        {
                            throw new Exception("Аккаунт не найден.");
                        }
                    }
                }
            }
        }

        public async Task<List<AccountOrdersModel>> GetAccountOrdersAsync(int accountId)
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
                                OrderDate = !reader.IsDBNull(reader.GetOrdinal("OrderDate")) ? reader.GetString("OrderDate") : null,
                                CustomerId = reader.GetInt32("CustomerId"),
                                PhoneNumber = !reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? reader.GetString("PhoneNumber") : null,
                                Country = !reader.IsDBNull(reader.GetOrdinal("Country")) ? reader.GetString("Country") : null,
                                Region = !reader.IsDBNull(reader.GetOrdinal("Region")) ? reader.GetString("Region") : null,
                                District = !reader.IsDBNull(reader.GetOrdinal("District")) ? reader.GetString("District") : null,
                                City = !reader.IsDBNull(reader.GetOrdinal("City")) ? reader.GetString("City") : null,
                                Village = !reader.IsDBNull(reader.GetOrdinal("Village")) ? reader.GetString("Village") : null,
                                Street = !reader.IsDBNull(reader.GetOrdinal("Street")) ? reader.GetString("Street") : null,
                                HouseNumber = !reader.IsDBNull(reader.GetOrdinal("HouseNumber")) ? reader.GetString("HouseNumber") : null,
                                ApartmentNumber = !reader.IsDBNull(reader.GetOrdinal("ApartmentNumber")) ? reader.GetString("ApartmentNumber") : null,
                                OrderText = !reader.IsDBNull(reader.GetOrdinal("OrderText")) ? reader.GetString("OrderText") : null,
                                DeliveryType = !reader.IsDBNull(reader.GetOrdinal("DeliveryType")) ? reader.GetString("DeliveryType") : null,
                                OrderStatus = !reader.IsDBNull(reader.GetOrdinal("OrderStatus")) ? reader.GetString("OrderStatus") : null
                            });
                        }
                    }
                }
            }

            return orders;
        }

        public async Task<string> ChangeNameAsync(string oldName, string newName)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(_requests["ChangeName"], connection))
                {
                    command.Parameters.AddWithValue("@OldName", oldName);
                    command.Parameters.AddWithValue("@NewName", newName);

                    int affectedRows = await command.ExecuteNonQueryAsync();
                    if (affectedRows > 0)
                        return "Имя успешно изменено.";
                    else
                        return "Имя не найдено или новое имя уже занято.";
                }
            }
        }

        public async Task<string> ChangeEmailAsync(string oldEmail, string newEmail)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(_requests["ChangeEmail"], connection))
                {
                    command.Parameters.AddWithValue("@OldEmail", oldEmail);
                    command.Parameters.AddWithValue("@NewEmail", newEmail);

                    int affectedRows = await command.ExecuteNonQueryAsync();
                    if (affectedRows > 0)
                        return "Email успешно изменен.";
                    else
                        return "Email не найден или новый email уже используется.";
                }
            }
        }

        public async Task<string> ChangePasswordAsync(string oldPasswordHash, string newPasswordHash)
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
                        return "Пароль успешно изменен.";
                    else
                        return "Старый пароль не найден.";
                }
            }
        }

        public async Task<string> DeleteAccountAsync(int accountId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(_requests["DeleteAccount"], connection))
                {
                    command.Parameters.AddWithValue("@AccountId", accountId);

                    int affectedRows = await command.ExecuteNonQueryAsync();
                    if (affectedRows > 0)
                        return "Аккаунт и связанные заказы успешно удалены.";
                    else
                        return "Аккаунт не найден.";
                }
            }
        }


    }
}
