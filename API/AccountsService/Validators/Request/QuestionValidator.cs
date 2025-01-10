using AccountsService.Models.Request;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace AccountsService.Validators.Request
{
    public class QuestionValidator
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _requests;

        public QuestionValidator(string connectionString, Dictionary<string, string> requests)
        {
            _connectionString = connectionString;
            _requests = requests;
        }

        public List<string> Validate(QuestionModel question)
        {
            var errors = new List<string>();

            if (question == null)
            {
                errors.Add("Данные вопроса отсутствуют.");
                return errors;
            }

            ValidateRequiredFields(question, errors);
            ValidateFieldLengths(question, errors);
            ValidateFieldFormats(question, errors);
            ValidateUniqueQuestionId(question.QuestionId, errors);

            return errors;
        }

        private void ValidateUniqueQuestionId(int questionId, List<string> errors)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(_requests["QuestionUniqueCheck"], connection))
                {
                    command.Parameters.AddWithValue("@QuestionId", questionId);
                    var count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        errors.Add("Поле QuestionId должно быть уникальным.");
                    }
                }
            }
        }

        private static void ValidateRequiredFields(QuestionModel question, List<string> errors)
        {
            if (question.QuestionId <= 0)
                errors.Add("Поле QuestionId должно быть положительным числом и обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(question.QuestionDate))
                errors.Add("Поле QuestionDate обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(question.UserName))
                errors.Add("Поле UserName обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(question.PhoneNumber))
                errors.Add("Поле PhoneNumber обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(question.QuestionText))
                errors.Add("Поле QuestionText обязательно для заполнения.");
        }

        private static void ValidateFieldLengths(QuestionModel question, List<string> errors)
        {
            CheckLength(errors, "QuestionDate", question.QuestionDate, 20);
            CheckLength(errors, "UserName", question.UserName, 255);
            CheckLength(errors, "PhoneNumber", question.PhoneNumber, 20);

            if (question.QuestionText.Length > 10000)
                errors.Add("Поле QuestionText не должно превышать 10000 символов.");
        }

        private static void CheckLength(List<string> errors, string fieldName, string value, int maxLength)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length > maxLength)
                errors.Add($"Поле {fieldName} не должно превышать {maxLength} символов.");
        }

        private static void ValidateFieldFormats(QuestionModel question, List<string> errors)
        {
            if (!DateTime.TryParseExact(question.QuestionDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                errors.Add("Поле QuestionDate должно быть датой в формате 'дд.мм.гггг'.");

            if (!string.IsNullOrWhiteSpace(question.PhoneNumber) && !question.PhoneNumber.All(char.IsDigit))
                errors.Add("Поле PhoneNumber должно содержать только цифры.");
        }
    }
}
