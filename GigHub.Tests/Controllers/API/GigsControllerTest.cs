using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Principal;
using Moq;
using GigHub.Data.Interfaces;
using GigHub.Controllers.API;
using GigHub.Tests.Extensions;
using GigHub.Data.Repositories;
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Models;

namespace GigHub.Tests.Controllers.API
{
    [TestClass]
    public class GigsControllerTest
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepo;
        private readonly string _userId = "1";

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = new Mock<IGigRepository>();

            var mockUOW = new Mock<IUnitOfWork>();
            mockUOW.SetupGet(u => u.Gigs).Returns(_mockRepo.Object);

            _controller = new GigsController(mockUOW.Object);
            _controller.MockCurrentUser(_userId, "user1@domain.com");

        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
       { 
            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepo.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUsersGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig { ArtistId = _userId + "-" };

            _mockRepo.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOK()
        {
            var gig = new Gig { ArtistId = _userId };

            _mockRepo.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<OkResult>();
        }

    }
}
