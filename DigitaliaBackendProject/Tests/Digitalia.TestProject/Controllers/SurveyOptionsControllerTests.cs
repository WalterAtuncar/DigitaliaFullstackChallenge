using BusinessLogic.SurveyOptions;
using Digitalia.Fullstack.Challenge.Controllers.SurveyOptions;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Entities;
using Moq;

namespace Digitalia.TestProject.Controllers
{
    public class SurveyOptionsControllerTests
    {
        private readonly Mock<ISurveyoptionsLogic> mockLogic;
        private readonly surveyoptionsController controller;
        private readonly List<SurveyOptions> fakeSurveyOptionsList;
        private readonly SurveyOptions fakeSurveyOption;
        public SurveyOptionsControllerTests()
        {
            mockLogic = new Mock<ISurveyoptionsLogic>();
            controller = new surveyoptionsController(mockLogic.Object);

            fakeSurveyOptionsList = new List<SurveyOptions>
            {
                new SurveyOptions { OptionID = 1, OptionText = "Option 1" },
                new SurveyOptions { OptionID = 2, OptionText = "Option 2" }
            };

            fakeSurveyOption = new SurveyOptions { OptionID = 1, OptionText = "Option 1" };
        }

        [Fact(DisplayName = "GetList returns list of survey options")]
        public void GetList_ReturnsListOfSurveyOptions()
        {
            mockLogic.Setup(logic => logic.GetList()).Returns(fakeSurveyOptionsList);

            var result = controller.GetList();

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            var surveyOptionsList = Assert.IsType<List<SurveyOptions>>(responseDTO.objModel);
            Assert.NotNull(surveyOptionsList);
            Assert.Equal(2, surveyOptionsList.Count);
        }

        [Fact(DisplayName = "GetById returns a survey option")]
        public void GetById_ReturnsASurveyOption()
        {
            int testId = 1;
            mockLogic.Setup(logic => logic.GetById(testId)).Returns(fakeSurveyOption);

            var result = controller.GetById(testId);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            var surveyOption = Assert.IsType<SurveyOptions>(responseDTO.objModel);
            Assert.NotNull(surveyOption);
            Assert.Equal(testId, surveyOption.OptionID);
        }

        [Fact(DisplayName = "Insert adds a new survey option and returns its ID")]
        public void Insert_AddsNewSurveyOptionAndReturnsId()
        {
            var newSurveyOption = new SurveyOptions
            {
                OptionText = "New Option"
            };

            mockLogic.Setup(logic => logic.Insert(newSurveyOption)).Returns(3);

            var result = controller.Insert(newSurveyOption);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            Assert.Equal(200, actionResult.StatusCode);
            Assert.Equal(3, responseDTO.objModel);
        }

        [Fact(DisplayName = "Update modifies an existing survey option and returns success status")]
        public void Update_ModifiesExistingSurveyOptionAndReturnsSuccessStatus()
        {
            var existingSurveyOption = new SurveyOptions
            {
                OptionID = 1,
                OptionText = "Updated Option"
            };

            mockLogic.Setup(logic => logic.Update(existingSurveyOption)).Returns(true);

            var result = controller.Update(existingSurveyOption);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            Assert.Equal(200, actionResult.StatusCode);
            Assert.True((bool)responseDTO.objModel);
        }

    }
}
