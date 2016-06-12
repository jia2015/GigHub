using GigHub.Models;
using System;
using System.Collections.Generic;

namespace GigHub.Data.Interfaces
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string artistId, string userId);
        IEnumerable<ApplicationUser> GetFollowees(string userId);
        void Add(Following following);
        void Remove(Following following);
    }
}
