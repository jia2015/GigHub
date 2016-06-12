using GigHub.Models;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using GigHub.Data.Dtos;
using GigHub.Data.Interfaces;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exists = _unitOfWork.Followings.GetFollowing(dto.FolloweeId, userId);
            if (exists != null)
                return BadRequest("Following already exists.");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };
            _unitOfWork.Followings.Add(following);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();
            var following = _unitOfWork.Followings.GetFollowing(id, userId);
            if (following == null)
            {
                return NotFound();
            }
            _unitOfWork.Followings.Remove(following);
            _unitOfWork.Complete();

            return Ok(id);
        }

    }
}
