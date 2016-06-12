using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using GigHub.ViewModels;
using GigHub.Data.Repositories;
using GigHub.Data.Interfaces;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var userId = User.Identity.GetUserId();

            var upcomingGigs = _unitOfWork.Gigs.GetUpcomingGigs(query);

            var attendances = _unitOfWork.Attendances.GetFutureAttendances(userId)
                                .ToLookup(a => a.GigId);

            var viewmodel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = attendances
            };

            return View("Gigs", viewmodel);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}