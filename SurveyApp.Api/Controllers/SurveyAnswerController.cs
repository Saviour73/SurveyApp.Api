using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Abstraction.IServices;
using SurveyApp.Application.DTOs;
using SurveyApp.Domain.Models;

namespace SurveyApp.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]

    public class SurveyAnswerController : ControllerBase
    {
        private readonly ISurveyAnswerService _surveyAnswerService;
        private readonly ILogger<Answer> _logger;

        public SurveyAnswerController(ISurveyAnswerService surveyAnswerService, ILogger<Answer> logger)
        {
            _surveyAnswerService = surveyAnswerService;
            _logger = logger;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitAnswerAsync(SubmitAnswerDTO submitAnswerDTO)
        {
            try
            {
                var result = await _surveyAnswerService.SubmitAnswerAsync(submitAnswerDTO);

                if (result)
                {
                    return Ok("Answer submitted successfully.");
                }
                else
                {
                    return BadRequest("Failed to submit answer. Question not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("question/{questionId}")]
        public async Task<IActionResult> GetAnswersForQuestionAsync(int questionId)
        {
            try
            {
                var answers = await _surveyAnswerService.GetAnswersForQuestionAsync(questionId);

                if (answers != null)
                {
                    return Ok(answers);
                }
                else
                {
                    return BadRequest("Failed to retrieve answers. Question not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
