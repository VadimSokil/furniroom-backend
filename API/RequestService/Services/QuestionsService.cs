using RequestService.Interfaces;
using RequestService.Models;
using MySql.Data.MySqlClient;

namespace RequestService.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _sqlQueries;

        public QuestionsService(string connectionString, Dictionary<string, string> sqlQueries)
        {
            _connectionString = connectionString;
            _sqlQueries = sqlQueries;
        }
        public async Task AddQuestion(QuestionModel question)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_sqlQueries["AddQuestion"], connection))
                {
                    command.Parameters.AddWithValue("@question_id", question.question_id);
                    command.Parameters.AddWithValue("@user_name", question.user_name);
                    command.Parameters.AddWithValue("@phone_number", question.phone_number);
                    command.Parameters.AddWithValue("@question_text", question.question_text);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
