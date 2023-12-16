using BusinessLogic.Votes;
using Digitalia.Fullstack.Challenge.Controllers.Votes;
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
    public class VotesControllerTests
    {
        private readonly Mock<IVotesLogic> mockLogic;
        private readonly votesController controller;
        private readonly List<Votes> fakeVotesList;
        private readonly Votes newVote;
        private readonly Votes existingVote;

        public VotesControllerTests()
        {
            mockLogic = new Mock<IVotesLogic>();
            controller = new votesController(mockLogic.Object);

            fakeVotesList = new List<Votes>
            {
                new Votes { VoteID = 1, SurveyID = 1, OptionID = 1, UserID = 1 },
                new Votes { VoteID = 2, SurveyID = 1, OptionID = 2, UserID = 2 }
            };

            newVote = new Votes { SurveyID = 2, OptionID = 3, UserID = 3 };
            existingVote = new Votes { VoteID = 3, SurveyID = 2, OptionID = 3, UserID = 3 };
        }

        [Fact(DisplayName = "GetList returns list of votes")]
        public void GetList_ReturnsListOfVotes()
        {
            mockLogic.Setup(logic => logic.GetList()).Returns(fakeVotesList);

            var result = controller.GetList();

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            var votesList = Assert.IsType<List<Votes>>(responseDTO.objModel);
            Assert.NotNull(votesList);
            Assert.Equal(2, votesList.Count);
        }

        [Fact(DisplayName = "GetById returns a vote")]
        public void GetById_ReturnsAVote()
        {
            int testId = 1;
            mockLogic.Setup(logic => logic.GetById(testId)).Returns(fakeVotesList[0]);

            var result = controller.GetById(testId);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            var vote = Assert.IsType<Votes>(responseDTO.objModel);
            Assert.NotNull(vote);
            Assert.Equal(testId, vote.VoteID);
        }

        [Fact(DisplayName = "Insert adds a new vote and returns its ID")]
        public void Insert_AddsNewVoteAndReturnsId()
        {
            mockLogic.Setup(logic => logic.Insert(newVote)).Returns(5);

            var result = controller.Insert(newVote);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            Assert.Equal(200, actionResult.StatusCode);
            Assert.Equal(5, responseDTO.objModel);
        }

        [Fact(DisplayName = "Update modifies an existing vote and returns success status")]
        public void Update_ModifiesExistingVoteAndReturnsSuccessStatus()
        {
            mockLogic.Setup(logic => logic.Update(existingVote)).Returns(true);

            var result = controller.Update(existingVote);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(actionResult.Value);
            Assert.NotNull(responseDTO);
            Assert.True((bool)responseDTO.objModel);
        }

    }
}
