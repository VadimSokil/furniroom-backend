using AccountsService.Models.Request;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace AccountsService.Validators.Request
{
    public class OrderValidator
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _requests;

        public OrderValidator(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
        }

        public List<string> Validate(OrderModel order)
        {
            var errors = new List<string>();

            if (order == null)
            {
                errors.Add("Данные заказа отсутствуют.");
                return errors;
            }

            ValidateRequiredFields(order, errors);
            ValidateFieldLengths(order, errors);
            ValidateFieldFormats(order, errors);
            ValidateUniqueOrderId(order.OrderId, errors);

            return errors;
        }

        private void ValidateUniqueOrderId(int orderId, List<string> errors)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(_requests["OrderUniqueCheck"], connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    var count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        errors.Add("Данный OrderId уже используется.");
                    }
                }
            }
        }

        private static void ValidateRequiredFields(OrderModel order, List<string> errors)
        {
            if (order.OrderId <= 0)
                errors.Add("Поле OrderId должно быть положительным числом и обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(order.OrderDate))
                errors.Add("Поле OrderDate обязательно для заполнения.");

            if (order.AccountId <= 0)
                errors.Add("Поле AccountId должно быть положительным числом и обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(order.PhoneNumber))
                errors.Add("Поле PhoneNumber обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(order.Country))
                errors.Add("Поле Country обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(order.Region))
                errors.Add("Поле Region обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(order.Street))
                errors.Add("Поле Street обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(order.HouseNumber))
                errors.Add("Поле HouseNumber обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(order.OrderText))
                errors.Add("Поле OrderText обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(order.DeliveryType))
                errors.Add("Поле DeliveryType обязательно для заполнения.");
        }

        private static void ValidateFieldLengths(OrderModel order, List<string> errors)
        {
            CheckLength(errors, "OrderDate", order.OrderDate, 20);
            CheckLength(errors, "PhoneNumber", order.PhoneNumber, 20);
            CheckLength(errors, "Country", order.Country, 500);
            CheckLength(errors, "Region", order.Region, 500);
            CheckLength(errors, "District", order.District, 500);
            CheckLength(errors, "City", order.City, 500);
            CheckLength(errors, "Village", order.Village, 500);
            CheckLength(errors, "Street", order.Street, 500);
            CheckLength(errors, "HouseNumber", order.HouseNumber, 10);
            CheckLength(errors, "ApartmentNumber", order.ApartmentNumber, 10);
            CheckLength(errors, "DeliveryType", order.DeliveryType, 50);
        }

        private static void CheckLength(List<string> errors, string fieldName, string value, int maxLength)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length > maxLength)
                errors.Add($"Поле {fieldName} не должно превышать {maxLength} символов.");
        }

        private static void ValidateFieldFormats(OrderModel order, List<string> errors)
        {
            if (!DateTime.TryParseExact(order.OrderDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                errors.Add("Поле OrderDate должно быть датой в формате 'дд.мм.гггг'.");

            if (!string.IsNullOrWhiteSpace(order.PhoneNumber))
            {
                if (!order.PhoneNumber.All(char.IsDigit))
                    errors.Add("Поле PhoneNumber должно содержать только цифры.");

                if (order.PhoneNumber.Length < 10 || order.PhoneNumber.Length > 15)
                    errors.Add("Поле PhoneNumber должно содержать от 10 до 15 цифр.");
            }

            ValidateNumericField(errors, "HouseNumber", order.HouseNumber);

            if (!string.IsNullOrWhiteSpace(order.ApartmentNumber))
                ValidateNumericField(errors, "ApartmentNumber", order.ApartmentNumber);

            if (order.OrderText.Length > 10000)
                errors.Add("Поле OrderText не должно превышать 10000 символов.");
        }

        private static void ValidateNumericField(List<string> errors, string fieldName, string value)
        {
            if (!int.TryParse(value, out _))
                errors.Add($"Поле {fieldName} должно быть числом.");
        }
    }
}
