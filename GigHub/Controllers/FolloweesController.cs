using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using GigHub.Data.Interfaces;

namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public FolloweesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Followees
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var artists = _unitOfWork.Followings.GetFollowees(userId);
            return View(artists);
        }
    }
}