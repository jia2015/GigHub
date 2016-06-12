using GigHub.Models;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using GigHub.Data.Dtos;
using GigHub.Data.Interfaces;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exists = _unitOfWork.Attendances.GetAttendance(dto.GigId, userId);
            if (exists != null)
            {
                return BadRequest("The attendance already exists.");
            }
            var attendance = new Attendance();
            attendance.GigId = dto.GigId;
            attendance.AttendeeId = userId;

            _unitOfWork.Attendances.Add(attendance);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendance = _unitOfWork.Attendances.GetAttendance(id, userId);
            if (attendance == null)
            {
                return NotFound();
            }
            _unitOfWork.Attendances.Remove(attendance);
            _unitOfWork.Complete();

            return Ok(id);
        }


    }
}
