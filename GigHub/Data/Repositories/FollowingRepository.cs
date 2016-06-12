using GigHub.Data.Infrastructure;
using GigHub.Data.Interfaces;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Data.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;
        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string artistId, string userId)
        {
            return _context.Followings.SingleOrDefault(a => a.FolloweeId == artistId && a.FollowerId == userId);
        }

        public IEnumerable<ApplicationUser> GetFollowees(string userId)
        {
            return _context.Followings
                            .Where(x => x.FollowerId == userId)
                            .Select(x => x.Followee)
                            .ToList();
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}