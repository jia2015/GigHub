using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace GigHub.Controllers.API
{
    public class GigsController : ApiController
    {
        private ApplicationDbContext _Context;
        public GigsController()
        {
            _Context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _Context.Gigs
                .Include(g => g.Attendances.Select(a =>a.Attendee))
                .Single(g => g.Id == id && g.ArtistId == userId);
            
            if (gig.IsCanceled)
                return NotFound();

            gig.Cancel();

            _Context.SaveChanges();

            return Ok();
        }
    }
}
