using GigHub.Models;
using GigHub.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using GigHub.Data.Interfaces;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewmodel = new GigFormVM
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading = "Add a Gig"
            };
            return View("GigForm", viewmodel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormVM viewmodel)
        {
            if (!ModelState.IsValid)
            {
                viewmodel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewmodel);
            }

            var gig = new Gig();
            gig.ArtistId = User.Identity.GetUserId();
            gig.DateTime = viewmodel.GetDateTime();
            gig.GenreId = viewmodel.Genre;
            gig.Venue = viewmodel.Venue;

            _unitOfWork.Gigs.AddGig(gig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormVM viewmodel)
        {
            if (!ModelState.IsValid)
            {
                viewmodel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewmodel);
            }

            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewmodel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewmodel.GetDateTime(), viewmodel.Venue, viewmodel.Genre);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewmodel = new GigFormVM
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Id = gig.Id,
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit a Gig"
            };

            return View("GigForm", viewmodel);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewmodel = new GigsViewModel
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId)
                                .ToLookup(a => a.GigId)
            };

            return View("Gigs", viewmodel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(userId);

            return View(gigs);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);
            if (gig == null)
            {
                return HttpNotFound();
            }

            var gigVM = new GigDetailVM { Gig = gig };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                gigVM.IsAttending = _unitOfWork.Attendances.GetAttendance(gig.Id, userId) != null;
                gigVM.IsFollowing = _unitOfWork.Followings.GetFollowing(gig.ArtistId, userId) != null;  
            }
            return View(gigVM);
        }

    }
}