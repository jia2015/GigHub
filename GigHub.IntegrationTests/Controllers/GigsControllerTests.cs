﻿using GigHub.Controllers;
using GigHub.Data.Infrastructure;
using GigHub.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GigHub.IntegrationTests.Extensions;
using GigHub.ViewModels;
using System.Web.Mvc;

namespace GigHub.IntegrationTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        //[SetUp]
        //public void SetUp()
        //{
        //    _context = new ApplicationDbContext();
        //    _controller = new GigsController(new UnitOfWork(_context));
        //}

        //[TearDown]
        //public void TearDown()
        //{
        //    _context.Dispose();
        //}

        //[Test, Isolated]
        //public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
        //{
        //    //Arrange
        //    var user = _context.Users.First();
        //    _controller.MockCurrentUser(user.Id, user.UserName);

        //    var genre = _context.Genres.First();
        //    var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
        //    _context.Gigs.Add(gig);
        //    _context.SaveChanges();

        //    //Act
        //    var result = _controller.Mine();

        //    //Assert
        //    (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);

        //}

        //[Test, Isolated]
        //public void Update_WhenCalled_ShouldUpdateTheGivenGig()
        //{
        //    //Arrange
        //    var user = _context.Users.First();
        //    _controller.MockCurrentUser(user.Id, user.UserName);

        //    var genre = _context.Genres.Single(g => g.Id == 1);
        //    var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
        //    _context.Gigs.Add(gig);
        //    _context.SaveChanges();

        //    //Act
        //    var result = _controller.Update(
        //        new GigFormVM
        //        {
        //            Id = gig.Id,
        //            Date = DateTime.Today.AddMonths(1).ToString("d MMM yyyy"),
        //            Time = "20:00",
        //            Venue = "Venue",
        //            Genre = 2
        //        });

        //    //Assert
        //    _context.Entry(gig).Reload();
        //    gig.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
        //    gig.Venue.Should().Be("Venue");
        //    gig.GenreId.Should().Be(2);

        //}
    }
}
