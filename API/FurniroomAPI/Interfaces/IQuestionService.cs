using FurniroomAPI.Models;

namespace FurniroomAPI.Interfaces
{
    public interface IQuestionService
    {
        public Task AddQuestion(QuestionModel question);
    }
}
