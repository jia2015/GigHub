﻿using GigHub.Controllers.API;
using GigHub.Data.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigHub.Tests.Extensions;
using GigHub.Data.Dtos;
using FluentAssertions;
using System.Web.Http.Results;
using GigHub.Models;

namespace GigHub.Tests.Controllers.API
{
    [TestClass]
    public class AttendancesControllerTests
    {
        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepository;

        private string _userId = "1";
        private int _gigId = 1;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(g => g.Attendances).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUoW.Object);
            _controller.MockCurrentUser(_userId, "User1@domain.com");
        }

        [TestMethod]
        public void Attend_AttendanceExists_ShouldReturnBadRequest()
        {
            var attendance = new Attendance { AttendeeId = _userId, GigId = _gigId };

            _mockRepository.Setup(r => r.GetAttendance(_gigId, _userId)).Returns(attendance);

            var result = _controller.Attend(new AttendanceDto { GigId = _gigId });

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidRequest_ShouldReturnOk()
        {
            _mockRepository.Setup(r => r.IsAttending(_gigId, _userId)).Returns(false);

            var result = _controller.Attend(new AttendanceDto { GigId = _gigId });

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void DeleteAttendance_AttendanceDoesntExist_ShouldReturnNotFound()
        {
            var result = _controller.DeleteAttendance(_gigId);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void DeleteAttendance_ValidRequest_ShouldReturnOK()
        {
            var attendance = new Attendance { AttendeeId = _userId, GigId = _gigId };

            _mockRepository.Setup(r => r.GetAttendance(_gigId, _userId)).Returns(attendance);

            var result = _controller.DeleteAttendance(_gigId);

            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }
    }
}
