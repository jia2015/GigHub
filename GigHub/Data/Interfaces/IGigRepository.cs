using GigHub.Models;
using System;
using System.Collections.Generic;

namespace GigHub.Data.Repositories
{
    public interface IGigRepository
    {
        void AddGig(Gig gig);
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
        IEnumerable<Gig> GetUpcomingGigs(string query = null);
    }
}
