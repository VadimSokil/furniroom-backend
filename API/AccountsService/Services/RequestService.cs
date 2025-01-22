using MySql.Data.MySqlClient;
using AccountsService.Interfaces;
using AccountsService.Models.Request;
using AccountsService.Models.Response;

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

        public async Task<ServiceResponseModel> AddOrderAsync(OrderModel order)
        {
            return await ExecuteAddCommandAsync(
                uniqueCheckQuery: _requests["OrderUniqueCheck"],
                addQuery: _requests["AddOrder"],
                uniqueParameter: new KeyValuePair<string, object>("@OrderId", order.OrderId),
                parameters: new Dictionary<string, object>
                {
                    { "@OrderId", order.OrderId },
                    { "@OrderDate", order.OrderDate },
                    { "@AccountId", order.AccountId },
                    { "@PhoneNumber", order.PhoneNumber },
                    { "@Country", order.Country },
                    { "@Region", order.Region },
                    { "@District", order.District },
                    { "@City", order.City },
                    { "@Village", order.Village },
                    { "@Street", order.Street },
                    { "@HouseNumber", order.HouseNumber },
                    { "@ApartmentNumber", order.ApartmentNumber },
                    { "@OrderText", order.OrderText },
                    { "@DeliveryType", order.DeliveryType }
                });
        }

        public async Task<ServiceResponseModel> AddQuestionAsync(QuestionModel question)
        {
            return await ExecuteAddCommandAsync(
                uniqueCheckQuery: _requests["QuestionUniqueCheck"],
                addQuery: _requests["AddQuestion"],
                uniqueParameter: new KeyValuePair<string, object>("@QuestionId", question.QuestionId),
                parameters: new Dictionary<string, object>
                {
                    { "@QuestionId", question.QuestionId },
                    { "@QuestionDate", question.QuestionDate },
                    { "@UserName", question.UserName },
                    { "@PhoneNumber", question.PhoneNumber },
                    { "@QuestionText", question.QuestionText }
                });
        }

        private async Task<ServiceResponseModel> ExecuteAddCommandAsync(
            string uniqueCheckQuery,
            string addQuery,
            KeyValuePair<string, object> uniqueParameter,
            Dictionary<string, object> parameters)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Проверка уникальности
                    using (var checkCommand = new MySqlCommand(uniqueCheckQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue(uniqueParameter.Key, uniqueParameter.Value);

                        var exists = Convert.ToInt32(await checkCommand.ExecuteScalarAsync()) > 0;

                        if (exists)
                        {
                            return CreateErrorResponse($"This ID ({uniqueParameter.Value}) is already in use.");
                        }
                    }

                    // Выполнение команды добавления
                    using (var command = new MySqlCommand(addQuery, connection))
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return new ServiceResponseModel
                {
                    Status = true,
                    Message = "Entity successfully added."
                };
            }
            catch (MySqlException ex)
            {
                return CreateErrorResponse($"A database error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return CreateErrorResponse($"An unexpected error occurred: {ex.Message}");
            }
        }

        private ServiceResponseModel CreateErrorResponse(string message)
        {
            return new ServiceResponseModel
            {
                Status = false,
                Message = message
            };
        }
    }
}
