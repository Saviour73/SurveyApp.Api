using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Abstraction.IServices;
using SurveyApp.Application.DTOs;
using SurveyApp.Domain.Models;

namespace SurveyApp.Api.Controllers
{

    [Route("api/[Controller]")]
    [Authorize]

    public class SurveyQuestionController : ControllerBase
    {
        private readonly ISurveyQuestionService _surveyQuestionService;
        private readonly ILogger<Question> _logger;
        public SurveyQuestionController(ISurveyQuestionService surveyQuestionService, ILogger<Question> logger)
        {
            _surveyQuestionService = surveyQuestionService;
            _logger = logger;
        }

        [HttpPost]
        [Route("createQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionDTO createQuestiondto)
        {
            try
            {
                var userIdentity = HttpContext.User;
                string email = userIdentity.Claims.FirstOrDefault(c => c.Type == "Name")?.Value.ToString();

                if (email == "" || email == null)
                {
                    return BadRequest("Email not found");
                }

                var question = await _surveyQuestionService.CreateQuestionAsync(createQuestiondto);

                if (!question)
                {
                    return BadRequest("Question creation failed");
                }

                return Ok("Question created  successfully");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while saving the record.");
            }
        }


        [HttpGet]
        [Route("getQuestion/{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            try
            {
                var userIdentity = HttpContext.User;
                string email = userIdentity.Claims.FirstOrDefault(c => c.Type == "Name")?.Value.ToString();

                if (email == "" || email == null)
                {
                    return BadRequest("Email not found");
                }

                var question = await _surveyQuestionService.GetQuestionByIdAsync(id);

                if (question == null)
                {
                    return BadRequest($"No Question found with the ID: {id}");
                }

                return Ok(question);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while saving the record.");
            }
        }

        [HttpGet]
        [Route("getAllQuestion")]
        public async Task<IActionResult> GetAllQuestion()
        {
            try
            {
                var userIdentity = HttpContext.User;
                string email = userIdentity.Claims.FirstOrDefault(c => c.Type == "Name")?.Value.ToString();

                if (email == "" || email == null)
                {
                    return BadRequest("Email not found");
                }

                var questions = await _surveyQuestionService.GetAllQuestionAsync();
               
                return Ok(questions);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while saving the record.");
            }

        }

        [HttpPut]
        [Route("updateQuestion/{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] UpdateQuestionDTO update)
        {
            try
            {
                var userIdentity = HttpContext.User;
                string email = userIdentity.Claims.FirstOrDefault(c => c.Type == "Name")?.Value.ToString();

                if (email == "" || email == null)
                {
                    return BadRequest("Email not found");
                }

                var updatedQuestion = await _surveyQuestionService.UpdateQuestionAsync(id, update);

                if (updatedQuestion == null)
                {
                    return NotFound($"No Question found with the ID: {id}");
                }
                return Ok(updatedQuestion);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while saving the record.");
            }

        }

        [HttpDelete]
        [Route("deleteQuestion/{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                var userIdentity = HttpContext.User;
                string email = userIdentity.Claims.FirstOrDefault(c => c.Type == "Name")?.Value.ToString();

                if (email == "" || email == null)
                {
                    return BadRequest("Email not found");
                }

                var deletedQuestion = await _surveyQuestionService.DeleteQuestionAsync(id);

                if (!deletedQuestion)
                {
                    return NotFound($"No Question found with the ID: {id}");
                }

                return NoContent();

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while saving the record.");
            }
        }

    }
}
