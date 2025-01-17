using MySql.Data.MySqlClient;
using AccountsService.Interfaces;
using AccountsService.Models.Request;
using AccountsService.Models.Response;
using AccountsService.Validation;

namespace AccountsService.Services
{
    public class RequestService : IRequestService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _requests;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";
        ValidationMethods validationMethods = new ValidationMethods();

        public RequestService(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
        }

        public async Task<ResponseModel> AddOrderAsync(OrderModel order)
        {
            if (!validationMethods.IsNotEmptyValue(order.OrderId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Order ID cannot be empty"
                };
            }

            if (!validationMethods.IsValidDigit(order.OrderId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Order ID must be a positive number"
                };
            }

            if (!validationMethods.IsNotEmptyValue(order.OrderDate))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Order date cannot be empty"
                };
            }

            if (!validationMethods.IsNotEmptyValue(order.AccountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "AccountID  cannot be empty"
                };
            }

            if (!validationMethods.IsValidDigit(order.AccountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID must be a positive number"
                };
            }

            if (!validationMethods.IsNotEmptyValue(order.PhoneNumber))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Phone number cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(order.PhoneNumber, 20))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Phone number exceeds the maximum allowed length of 20 characters"
                };
            }

            if (!validationMethods.IsNotEmptyValue(order.Country))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Country cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(order.Country, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Country exceeds the maximum allowed length of 100 characters"
                };
            }

            if (!validationMethods.IsNotEmptyValue(order.Region))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Region cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(order.Region, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Region exceeds the maximum allowed length of 100 characters"
                };
            }

            if (!validationMethods.IsNotEmptyValue(order.District))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "District cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(order.District, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "District exceeds the maximum allowed length of 100 characters"
                };
            }

            if (!validationMethods.IsValidLength(order.City, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "City exceeds the maximum allowed length of 100 characters"
                };
            }

            if (!validationMethods.IsValidLength(order.Village, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Village exceeds the maximum allowed length of 100 characters"
                };
            }

            if (!validationMethods.IsNotEmptyValue(order.Street))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Street cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(order.Street, 100))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Street exceeds the maximum allowed length of 100 characters"
                };
            }

            if (!validationMethods.IsNotEmptyValue(order.HouseNumber))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "House number cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(order.HouseNumber, 20))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "House number exceeds the maximum allowed length of 20 characters"
                };
            }

            if (!validationMethods.IsValidLength(order.ApartmentNumber, 20))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Apartment number exceeds the maximum allowed length of 20 characters"
                };
            }

            if (!validationMethods.IsNotEmptyValue(order.OrderText))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Order text cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(order.OrderText, 5000))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Order text exceeds the maximum allowed length of 5000 characters"
                };
            }

            if (!validationMethods.IsNotEmptyValue(order.DeliveryType))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Delivery type cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(order.DeliveryType, 20))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Delivery type exceeds the maximum allowed length of 20 characters"
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
                                Message = "This Order ID is already in use"
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
            if (!validationMethods.IsNotEmptyValue(question.QuestionId?.ToString()))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Question ID cannot be empty"
                };
            }

            if (!validationMethods.IsValidDigit(question.QuestionId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Question ID must be a positive number"
                };
            }

            if (!validationMethods.IsNotEmptyValue(question.QuestionDate))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Question date cannot be empty"
                };
            }

            if (!validationMethods.IsNotEmptyValue(question.UserName))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "User name cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(question.UserName, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "User name exceeds the maximum allowed length of 50 characters"
                };
            }

            if (!validationMethods.IsNotEmptyValue(question.PhoneNumber))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Phone number cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(question.PhoneNumber, 20))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Phone number exceeds the maximum allowed length of 20 characters"
                };
            }

            if (!validationMethods.IsNotEmptyValue(question.QuestionText))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Question text cannot be empty"
                };
            }

            if (!validationMethods.IsValidLength(question.QuestionText, 5000))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Question text exceeds the maximum allowed length of 5000 characters"
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
                                Message = "This Question ID is already in use"
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
