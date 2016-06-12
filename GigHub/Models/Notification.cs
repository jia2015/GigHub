using System;

namespace GigHub.Models
{
    public enum NotificationType
    {
        GigCanceled = 1,
        GigUpdated = 2,
        GigCreated = 3
    }

    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }          
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }
        //[Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {

        }
        private Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException("gig");

            Gig = gig;
            Type = type;
            DateTime = DateTime.Now;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(NotificationType.GigUpdated, newGig);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }
    }


    public class UserNotification
    {
        //[Key]
        //[Column(Order = 1)]
        public string UserId { get; private set; }
        //[Key]
        //[Column(Order = 2)]
        public int NotificationId { get; private set; }
        public ApplicationUser User { get; private set; }
        public Notification Notification { get; private set; }
        public bool IsRead { get; private set; }

        protected UserNotification()
        {

        }
        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (notification == null)
                throw new ArgumentNullException("notification");

            User = user;
            Notification = notification;
        }

        public void Read()
        {
            IsRead = true;
        }

        public void UnRead()
        {
            IsRead = false;
        }

    }
   
}