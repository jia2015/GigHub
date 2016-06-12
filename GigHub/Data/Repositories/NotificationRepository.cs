using GigHub.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using GigHub.Data.Infrastructure;

namespace GigHub.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;
        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNewNotificationsFor(string userId)
        {
            return _context.UserNotifications
                            .Where(x => x.UserId == userId && !x.IsRead)
                            .Select(x => x.Notification)
                            .Include(x => x.Gig.Artist)
                            .ToList();
        }
    }
}