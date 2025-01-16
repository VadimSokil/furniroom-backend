using MySql.Data.MySqlClient;
using AccountsService.Interfaces;
using AccountsService.Models.Request;
using AccountsService.Models;
using AccountsService.Validators;

namespace AccountsService.Services
{
    public class RequestService : IRequestService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _requests;
        private readonly Validator _validator;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";

        public RequestService(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
        }

        public async Task<ResponseModel> AddOrderAsync(OrderModel order)
        {
            if (!_validator.IsNotEmpty(order.OrderId?.ToString()))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "OrderId cannot be empty"
                };
            }

            if (!_validator.IsPositiveNumber(order.OrderId?.ToString()))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "OrderId must be a positive number"
                };
            }

            if (!_validator.IsNotEmpty(order.OrderDate))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "OrderDate cannot be empty"
                };
            }

            if (!_validator.IsNotEmpty(order.AccountId?.ToString()))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "AccountId cannot be empty"
                };
            }

            if (!_validator.IsPositiveNumber(order.AccountId?.ToString()))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "AccountId must be a positive number"
                };
            }

            if (!_validator.IsNotEmpty(order.PhoneNumber))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "PhoneNumber cannot be empty"
                };
            }

            if (!_validator.IsWithinMaxLength(order.PhoneNumber, 15))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "PhoneNumber exceeds maximum length (15 characters)"
                };
            }

            if (!_validator.IsNotEmpty(order.Country))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Country cannot be empty"
                };
            }

            if (!_validator.IsWithinMaxLength(order.Country, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Country exceeds maximum length (100 characters)"
                };
            }

            if (!_validator.IsNotEmpty(order.Region))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Region cannot be empty"
                };
            }

            if (!_validator.IsWithinMaxLength(order.Region, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Region exceeds maximum length (100 characters)"
                };
            }

            if (!_validator.IsNotEmpty(order.District))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "District cannot be empty"
                };
            }

            if (!_validator.IsWithinMaxLength(order.District, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "District exceeds maximum length (100 characters)"
                };
            }

            if (!_validator.IsWithinMaxLength(order.City, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "City exceeds maximum length (100 characters)"
                };
            }

            if (!_validator.IsWithinMaxLength(order.Village, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Village exceeds maximum length (100 characters)"
                };
            }

            if (!_validator.IsNotEmpty(order.Street))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Street cannot be empty"
                };
            }

            if (!_validator.IsWithinMaxLength(order.Street, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Street exceeds maximum length (100 characters)"
                };
            }

            if (!_validator.IsNotEmpty(order.HouseNumber))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "HouseNumber cannot be empty"
                };
            }

            if (!_validator.IsWithinMaxLength(order.HouseNumber, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "HouseNumber exceeds maximum length (50 characters)"
                };
            }

            if (!_validator.IsWithinMaxLength(order.ApartmentNumber, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "ApartmentNumber exceeds maximum length (50 characters)"
                };
            }

            if (!_validator.IsNotEmpty(order.OrderText))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "OrderText cannot be empty"
                };
            }

            if (!_validator.IsWithinMaxLength(order.OrderText, 500))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "OrderText exceeds maximum length (500 characters)"
                };
            }

            if (!_validator.IsNotEmpty(order.DeliveryType))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "DeliveryType cannot be empty"
                };
            }

            if (!_validator.IsWithinMaxLength(order.DeliveryType, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "DeliveryType exceeds maximum length (100 characters)"
                };
            }

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
                                Message = "OrderId is already taken"
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
                    Message = "Order successfully added"
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

        public async Task<ResponseModel> AddQuestionAsync(QuestionModel question)
        {
            if (!_validator.IsNotEmpty(question.QuestionId?.ToString()))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "QuestionId cannot be empty"
                };
            }

            if (!_validator.IsPositiveNumber(question.QuestionId?.ToString()))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "QuestionId must be a positive number"
                };
            }

            if (!_validator.IsNotEmpty(question.QuestionDate))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "QuestionDate cannot be empty"
                };
            }

            if (!_validator.IsNotEmpty(question.UserName))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "UserName cannot be empty"
                };
            }

            if (!_validator.IsWithinMaxLength(question.UserName, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "UserName exceeds maximum length (100 characters)"
                };
            }

            if (!_validator.IsNotEmpty(question.PhoneNumber))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "PhoneNumber cannot be empty"
                };
            }

            if (!_validator.IsWithinMaxLength(question.PhoneNumber, 15))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "PhoneNumber exceeds maximum length (15 characters)"
                };
            }

            if (!_validator.IsNotEmpty(question.QuestionText))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "QuestionText cannot be empty"
                };
            }

            if (!_validator.IsWithinMaxLength(question.QuestionText, 500))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "QuestionText exceeds maximum length (500 characters)"
                };
            }

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
                                Message = "QuestionId is already taken"
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
                    Message = "Question successfully added"
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
    }
}
