using BusinessLogic.SurveyOptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Digitalia.Fullstack.Challenge.Controllers.SurveyOptions
{
    [Route("api/surveyoptions")]
    [ApiController]
    public class surveyoptionsController : ControllerBase
    {
        private ISurveyoptionsLogic _surveyoptions;
        public ResponseDTO _ResponseDTO;
        public surveyoptionsController(ISurveyoptionsLogic surveyoptions)
        {
            _surveyoptions = surveyoptions;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            _ResponseDTO = new ResponseDTO();
            try
            {
                return Ok(_ResponseDTO.Success(_ResponseDTO, _surveyoptions.GetList()));
            }
            catch (Exception e)
            {
                return BadRequest(_ResponseDTO.Failed(_ResponseDTO, e.Message));
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            _ResponseDTO = new ResponseDTO();
            try
            {
                return Ok(_ResponseDTO.Success(_ResponseDTO, _surveyoptions.GetById(id)));
            }
            catch (Exception e)
            {
                return BadRequest(_ResponseDTO.Failed(_ResponseDTO, e.Message));
            }
        }


        [HttpPost]
        public IActionResult Insert([FromBody] Models.Entities.SurveyOptions obj)
        {
            _ResponseDTO = new ResponseDTO();
            try
            {
                return Ok(_ResponseDTO.Success(_ResponseDTO, _surveyoptions.Insert(obj)));
            }
            catch (Exception e)
            {
                return BadRequest(_ResponseDTO.Failed(_ResponseDTO, e.Message));
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Models.Entities.SurveyOptions obj)
        {
            _ResponseDTO = new ResponseDTO();
            try
            {
                return Ok(_ResponseDTO.Success(_ResponseDTO, _surveyoptions.Update(obj)));
            }
            catch (Exception e)
            {
                return BadRequest(_ResponseDTO.Failed(_ResponseDTO, e.Message));
            }
        }
    }
}
