using FurniroomAPI.Interfaces;
using FurniroomAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] QuestionModel question)
        {
            await _questionService.AddQuestion(question);
            return Ok();
        }
    }
}
