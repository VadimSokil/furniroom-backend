using RequestService.Models;

namespace RequestService.Interfaces
{
    public interface IQuestionsService
    {
        public Task AddQuestion(QuestionModel question);
    }
}
