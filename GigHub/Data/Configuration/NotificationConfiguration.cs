using GigHub.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Data.Configuration
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            this.HasRequired(n => n.Gig);
        }
    }
}