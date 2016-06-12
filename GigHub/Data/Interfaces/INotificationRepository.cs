using GigHub.Models;
using System;
using System.Collections.Generic;

namespace GigHub.Data.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNewNotificationsFor(string userId);
    }
}
