using MySql.Data.MySqlClient;
using AccountsService.Interfaces;
using AccountsService.Models.Request;
using AccountsService.Validators.Request;

namespace AccountsService.Services
{
    public class RequestService : IRequestService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _requests;
        private readonly OrderValidator _orderValidator;
        private readonly QuestionValidator _questionValidator;

        public RequestService(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;

            _orderValidator = new OrderValidator(connectionString, requests);
            _questionValidator = new QuestionValidator(connectionString, requests);
        }

        public async Task AddOrderAsync(OrderModel order)
        {
            var orderErrors = _orderValidator.Validate(order);
            if (orderErrors.Count > 0)
            {
                throw new ArgumentException($"Ошибка валидации заказа: {string.Join(", ", orderErrors)}");
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_requests["AddOrder"], connection))
                {
                    command.Parameters.AddWithValue("OrderId", order.OrderId);
                    command.Parameters.AddWithValue("OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("AccountId", order.AccountId);
                    command.Parameters.AddWithValue("PhoneNumber", order.PhoneNumber);
                    command.Parameters.AddWithValue("Country", order.Country);
                    command.Parameters.AddWithValue("Region", order.Region);
                    command.Parameters.AddWithValue("District", order.District);
                    command.Parameters.AddWithValue("City", order.City);
                    command.Parameters.AddWithValue("Village", order.Village);
                    command.Parameters.AddWithValue("Street", order.Street);
                    command.Parameters.AddWithValue("HouseNumber", order.HouseNumber);
                    command.Parameters.AddWithValue("ApartmentNumber", order.ApartmentNumber);
                    command.Parameters.AddWithValue("OrderText", order.OrderText);
                    command.Parameters.AddWithValue("DeliveryType", order.DeliveryType);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AddQuestionAsync(QuestionModel question)
        {
            var questionErrors = _questionValidator.Validate(question);
            if (questionErrors.Count > 0)
            {
                throw new ArgumentException($"Ошибка валидации вопроса: {string.Join(", ", questionErrors)}");
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_requests["AddQuestion"], connection))
                {
                    command.Parameters.AddWithValue("QuestionId", question.QuestionId);
                    command.Parameters.AddWithValue("QuestionDate", question.QuestionDate);
                    command.Parameters.AddWithValue("UserName", question.UserName);
                    command.Parameters.AddWithValue("PhoneNumber", question.PhoneNumber);
                    command.Parameters.AddWithValue("QuestionText", question.QuestionText);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
