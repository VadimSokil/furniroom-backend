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
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";

        public RequestService(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
        }

        public async Task<ResponseModel> AddOrderAsync(OrderModel order)
        {
            try
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
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = false,
                                Message = "This Order ID is already in use."
                            };
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

                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = true,
                    Message = "Order successfully added."
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

        public async Task<ResponseModel> AddQuestionAsync(QuestionModel question)
        {
            try
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
                            return new ResponseModel
                            {
                                Date = currentDateTime,
                                RequestExecution = false,
                                Message = "This Question ID is already in use."
                            };
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

                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = true,
                    Message = "Question successfully added."
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
    }
}
