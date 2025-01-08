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

            return orders;
        }

    }
}
