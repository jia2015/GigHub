using GigHub.Models;
using GigHub.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewmodel = new GigFormVM();
            viewmodel.Genres = _context.Genres.ToList();
            viewmodel.Heading = "Add a Gig";
            return View("GigForm", viewmodel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormVM viewmodel)
        {
            if (!ModelState.IsValid)
            {
                viewmodel.Genres = _context.Genres.ToList();
                return View("GigForm", viewmodel);
            }

            var gig = new Gig();
            gig.ArtistId = User.Identity.GetUserId();
            gig.DateTime = viewmodel.GetDateTime();
            gig.GenreId = viewmodel.Genre;
            gig.Venue = viewmodel.Venue;

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormVM viewmodel)
        {
            if (!ModelState.IsValid)
            {
                viewmodel.Genres = _context.Genres.ToList();
                return View("GigForm", viewmodel);
            }

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == viewmodel.Id && g.ArtistId == userId);
            gig.Venue = viewmodel.Venue;
            gig.DateTime = viewmodel.GetDateTime();
            gig.GenreId = viewmodel.Genre;

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var viewmodel = new GigFormVM
            {
                Genres = _context.Genres.ToList(),
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
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var viewmodel = new GigsViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending"
            };

            return View("Gigs", viewmodel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId && 
                    g.DateTime > DateTime.Now &&
                    !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();

            return View(gigs);
        }


    }
}