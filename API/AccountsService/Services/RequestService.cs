using MySql.Data.MySqlClient;
using AccountsService.Interfaces;
using AccountsService.Models.Request;

namespace AccountsService.Services
{
    public class RequestService : IRequestService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _requests;

        public RequestService(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
        }

        public async Task<string> AddOrderAsync(OrderModel order)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var checkCommand = new MySqlCommand(_requests["OrderUniqueCheck"], connection))
                {
                    checkCommand.Parameters.AddWithValue("@OrderId", order.OrderId);
                    var orderExists = Convert.ToInt32(await checkCommand.ExecuteScalarAsync()) > 0;

                    if (orderExists)
                    {
                        return "OrderId is already taken.";
                    }
                }

                using (var command = new MySqlCommand(_requests["AddOrder"], connection))
                {
                    command.Parameters.AddWithValue("@OrderId", order.OrderId);
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@AccountId", order.AccountId);
                    command.Parameters.AddWithValue("@PhoneNumber", order.PhoneNumber);
                    command.Parameters.AddWithValue("@Country", order.Country);
                    command.Parameters.AddWithValue("@Region", order.Region);
                    command.Parameters.AddWithValue("@District", order.District);
                    command.Parameters.AddWithValue("@City", order.City);
                    command.Parameters.AddWithValue("@Village", order.Village);
                    command.Parameters.AddWithValue("@Street", order.Street);
                    command.Parameters.AddWithValue("@HouseNumber", order.HouseNumber);
                    command.Parameters.AddWithValue("@ApartmentNumber", order.ApartmentNumber);
                    command.Parameters.AddWithValue("@OrderText", order.OrderText);
                    command.Parameters.AddWithValue("@DeliveryType", order.DeliveryType);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return "Order successfully added.";
        }

        public async Task<string> AddQuestionAsync(QuestionModel question)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var checkCommand = new MySqlCommand(_requests["QuestionUniqueCheck"], connection))
                {
                    checkCommand.Parameters.AddWithValue("@QuestionId", question.QuestionId);
                    var questionExists = Convert.ToInt32(await checkCommand.ExecuteScalarAsync()) > 0;

                    if (questionExists)
                    {
                        return "QuestionId is already taken.";
                    }
                }

                using (var command = new MySqlCommand(_requests["AddQuestion"], connection))
                {
                    command.Parameters.AddWithValue("@QuestionId", question.QuestionId);
                    command.Parameters.AddWithValue("@QuestionDate", question.QuestionDate);
                    command.Parameters.AddWithValue("@UserName", question.UserName);
                    command.Parameters.AddWithValue("@PhoneNumber", question.PhoneNumber);
                    command.Parameters.AddWithValue("@QuestionText", question.QuestionText);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return "Question successfully added.";
        }
    }
}
