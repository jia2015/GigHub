using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Data.Repositories
{
    public interface IUserNotificationRepository
    {
        IEnumerable<UserNotification> GetUserNotificationsFor(string userId);
    }
}
