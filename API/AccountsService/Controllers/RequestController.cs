﻿using AccountsService.Interfaces;
using AccountsService.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace AccountsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestsService;

        public RequestController(IRequestService requestsService)
        {
            _requestsService = requestsService;
        }

        [HttpPost("add-order")]
        public async Task<ActionResult> AddOrder([FromBody] OrderModel order)
        {
            await _requestsService.AddOrderAsync(order);
            return CreatedAtAction(nameof(AddOrder), new { id = order.OrderId }, order);
        }

        [HttpPost("add-question")]
        public async Task<ActionResult> AddQuestion([FromBody] QuestionModel question)
        {
            await _requestsService.AddQuestionAsync(question);
            return CreatedAtAction(nameof(AddQuestion), new { id = question.QuestionId }, question);
        }
    }
}