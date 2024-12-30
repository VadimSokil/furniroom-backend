using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestService.Interfaces;
using RequestService.Models;

namespace RequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsService _questionsService;

        public QuestionsController(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        [HttpPost]
        public async Task<ActionResult> AddQuestion([FromBody] QuestionModel question)
        {
            await _questionsService.AddQuestion(question);
            return CreatedAtAction(nameof(AddQuestion), new { id = question.question_id }, question);
        }
    }
}
