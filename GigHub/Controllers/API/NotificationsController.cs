using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using GigHub.Models;
using GigHub.Data.Dtos;
using AutoMapper;
using GigHub.Data.Interfaces;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.Notifications.GetNewNotificationsFor(userId);

            return notifications.Select(n => Mapper.Map<Notification, NotificationDto>(n));             
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.UserNotifications.GetUserNotificationsFor(userId);
            notifications.ToList().ForEach(x => x.Read());

            _unitOfWork.Complete();
            return Ok();
        }

    }
}