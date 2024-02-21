using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Abstraction.IServices;
using SurveyApp.Application.DTOs;
using SurveyApp.Domain.Models;

namespace SurveyApp.Api.Controllers
{
    [Route("api/[Controller]")]
    [Authorize]


    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly ILogger<Survey> _logger;
        public SurveyController(ISurveyService surveyService, ILogger<Survey> logger)
        {
            _surveyService = surveyService;
            _logger = logger;
        }

        [HttpPost]
        [Route("createSurvey")]
        public async Task<IActionResult> CreateSurvey([FromBody] CreateSurveyDTO createSurveydto)
        {
            try
            {
                var userIdentity = HttpContext.User;
                string email = userIdentity.Claims.FirstOrDefault(c => c.Type == "Name")?.Value.ToString();

                if (email == "" || email == null)
                {
                    return BadRequest("Email not found");
                }

                //var survey = await _surveyService.CreateSurveyAsync(createSurveydto);

                //if (!survey)
                //{
                //    return BadRequest("Survey creation failed");
                //}
                var createdSurveyId = await _surveyService.CreateSurveyAsync(createSurveydto);

               
               return Ok("Survey created  successfully");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while saving the record.");
            }
        }



        [HttpGet]
        [Route("getSurvey/{id}")]
        public async Task<IActionResult> GetSurveyById(int id)
        {
            try
            {
                var userIdentity = HttpContext.User;
                string email = userIdentity.Claims.FirstOrDefault(c => c.Type == "Name")?.Value.ToString();
                
                if (email == "" || email == null)
                {
                    return BadRequest("Email not found");
                }

                var survey = await _surveyService.GetSurveyByIdAsync(id);

                if (survey == null)
                {
                    return BadRequest($"No survey found with the ID: {id}");
                }
               
                return Ok(survey);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while saving the record.");
            }
        }

        [HttpGet]
        [Route("getAllSurvey")]
        public async Task<IActionResult> GetAllSurvey()
        {
            try
            {
                var userIdentity = HttpContext.User;
                string email = userIdentity.Claims.FirstOrDefault(c => c.Type == "Name")?.Value.ToString();

                if (email == "" || email == null)
                {
                    return BadRequest("Email not found");
                }

                var surveys = await _surveyService.GetAllSurveyAsync();

                return Ok(surveys);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while saving the record.");

            }
        }

        [HttpPut]
        [Route("updateSurvey/{id}")]
        public async Task<IActionResult> UpdateSurvey( int id ,[FromBody] UpdateSurveyDto update)
        {
            try
            {
                var userIdentity = HttpContext.User;
                string email = userIdentity.Claims.FirstOrDefault(c => c.Type == "Name")?.Value.ToString();

                if (email == "" || email == null)
                {
                    return BadRequest("Email not found");
                }

                var updatedSurvey = await _surveyService.UpdateSurveyAsync(id, update);

                if (updatedSurvey == null)
                {
                    return NotFound($"No survey found with the ID: {id}");
                }
                return Ok(updatedSurvey);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while saving the record.");
            }

        }

        [HttpDelete]
        [Route("deleteSurvey/{id}")]
        public async Task <IActionResult> DeleteSurvey(int id)
        {
            try
            {
                var userIdentity = HttpContext.User;
                string email = userIdentity.Claims.FirstOrDefault(c => c.Type == "Name")?.Value.ToString();

                if (email == "" || email == null)
                {
                    return BadRequest("Email not found");
                }

                var deletedSurvey = await _surveyService.DeleteSurveyAsync(id);

                if (!deletedSurvey)
                {
                    return NotFound($"No survey found with the ID: {id}");
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
