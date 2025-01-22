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
            return await AddEntityAsync(order.OrderId.ToString(), _requests["OrderUniqueCheck"], _requests["AddOrder"], new Dictionary<string, object>
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
            return await AddEntityAsync(question.QuestionId.ToString(), _requests["QuestionUniqueCheck"], _requests["AddQuestion"], new Dictionary<string, object>
            {
                { "@QuestionId", question.QuestionId },
                { "@QuestionDate", question.QuestionDate },
                { "@UserName", question.UserName },
                { "@PhoneNumber", question.PhoneNumber },
                { "@QuestionText", question.QuestionText }
            });
        }

        private async Task<ServiceResponseModel> AddEntityAsync(string entityId, string uniqueCheckQuery, string addQuery, Dictionary<string, object> parameters)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var checkCommand = new MySqlCommand(uniqueCheckQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@EntityId", entityId);
                        var exists = Convert.ToInt32(await checkCommand.ExecuteScalarAsync()) > 0;

                        if (exists)
                        {
                            return CreateErrorResponse("This ID is already in use.");
                        }
                    }

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
