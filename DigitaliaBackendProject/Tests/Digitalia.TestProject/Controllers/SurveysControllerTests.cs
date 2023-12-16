using BusinessLogic.Surveys;
using Digitalia.Fullstack.Challenge.Controllers.Surveys;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalia.TestProject.Controllers
{
    public class SurveysControllerTests
    {
        private readonly Mock<ISurveysLogic> mockLogic;
        private readonly surveysController controller;
        private readonly List<Surveys> fakeSurveysList;
        private readonly Surveys newSurvey;
        private readonly Surveys existingSurvey;
        public SurveysControllerTests()
        {
            mockLogic = new Mock<ISurveysLogic>();
            controller = new surveysController(mockLogic.Object);

            fakeSurveysList = new List<Surveys>
            {
                new Surveys { SurveyID = 1, Title = "Survey 1", Description = "Description 1", Question = "Question 1", IsActive = true },
                new Surveys { SurveyID = 2, Title = "Survey 2", Description = "Description 2", Question = "Question 2", IsActive = false }
            };

            newSurvey = new Surveys { Title = "New Survey", Description = "New Description", Question = "New Question", IsActive = true };
            existingSurvey = new Surveys { SurveyID = 3, Title = "Existing Survey", Description = "Existing Description", Question = "Existing Question", IsActive = true };
        }

        [Fact(DisplayName = "GetList returns list of surveys")]
        public void GetList_ReturnsListOfSurveys()
        {
            mockLogic.Setup(logic => logic.GetList()).Returns(fakeSurveysList);

            var result = controller.GetList();

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            var surveysList = Assert.IsType<List<Surveys>>(responseDTO.objModel);
            Assert.NotNull(surveysList);
            Assert.Equal(2, surveysList.Count);
        }

        [Fact(DisplayName = "GetById returns a survey")]
        public void GetById_ReturnsASurvey()
        {
            int testId = 1;
            mockLogic.Setup(logic => logic.GetById(testId)).Returns(fakeSurveysList[0]);

            var result = controller.GetById(testId);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            var survey = Assert.IsType<Surveys>(responseDTO.objModel);
            Assert.NotNull(survey);
            Assert.Equal(testId, survey.SurveyID);
        }

        [Fact(DisplayName = "Insert adds a new survey and returns its ID")]
        public void Insert_AddsNewSurveyAndReturnsId()
        {
            var newSurvey = new Surveys
            {
                Title = "New Survey",
                Description = "New Description",
                Question = "New Question",
                IsActive = true
            };

            mockLogic.Setup(logic => logic.Insert(newSurvey)).Returns(1);

            var result = controller.Insert(newSurvey);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            Assert.Equal(200, actionResult.StatusCode); 
            Assert.Equal(1, responseDTO.objModel); 
        }

        [Fact(DisplayName = "Update modifies an existing survey")]
        public void Update_ModifiesExistingSurvey()
        {
            mockLogic.Setup(logic => logic.Update(existingSurvey)).Returns(true);

            var result = controller.Update(existingSurvey);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            Assert.True((bool)responseDTO.objModel);
        }

    }
}
