using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace GigHub.Data.Configuration
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            this.HasKey(u => new { u.UserId, u.NotificationId });

            this.Property(u => u.UserId).HasColumnOrder(1);
            this.Property(u => u.NotificationId).HasColumnOrder(2);

            this.HasRequired(n => n.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);                
        }
    }
}