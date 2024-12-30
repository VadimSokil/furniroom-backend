using RequestService.Interfaces;
using RequestService.Models;
using MySql.Data.MySqlClient;

namespace RequestService.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _sqlQueries;

        public OrdersService(string connectionString, Dictionary<string, string> sqlQueries)
        {
            _connectionString = connectionString;
            _sqlQueries = sqlQueries;
        }
        public async Task AddOrder(OrderModel order)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_sqlQueries["AddOrder"], connection))
                {
                    command.Parameters.AddWithValue("@order_id", order.order_id);
                    command.Parameters.AddWithValue("@customer_name", order.customer_name);
                    command.Parameters.AddWithValue("@phone_number", order.phone_number);
                    command.Parameters.AddWithValue("@country", order.country);
                    command.Parameters.AddWithValue("@region", order.region);
                    command.Parameters.AddWithValue("@district", order.district);
                    command.Parameters.AddWithValue("@city_name", order.city_name);
                    command.Parameters.AddWithValue("@village_name", order.village_name);
                    command.Parameters.AddWithValue("@street_name", order.street_name);
                    command.Parameters.AddWithValue("@house_number", order.house_number);
                    command.Parameters.AddWithValue("@apartment_number", order.apartment_number);
                    command.Parameters.AddWithValue("@order_text", order.order_text);
                    command.Parameters.AddWithValue("@delivery_type", order.delivery_type);
                    command.Parameters.AddWithValue("@order_date", order.order_date);


                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
