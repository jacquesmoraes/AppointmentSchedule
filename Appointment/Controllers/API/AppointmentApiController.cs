using Appointment.Models;
using Appointment.Models.ViewModels;
using Appointment.Services;
using Appointment.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Appointment.Controllers.API
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentApiController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _logInUserId;
        private readonly string _role;

        public AppointmentApiController(IAppointmentService appointmentService, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;
            _httpContextAccessor = httpContextAccessor;
            _logInUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(ApponitmentViewModel data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.Status = _appointmentService.AddUpdate(data).Result;
                if(commonResponse.Status == 1)
                {
                    commonResponse.Message = Helper.appointmentUpdated;
                }
                if (commonResponse.Status == 2)
                {
                    commonResponse.Message = Helper.appointmentAdded;
                }
                
            }
            catch( Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = Helper.failureCode;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarData")]
        public IActionResult GetCalendarData(string doctorId)
        {
            CommonResponse<List<ApponitmentViewModel>> commonResponse = new CommonResponse<List<ApponitmentViewModel>>();
            try
            {
                if(_role == Helper.Pacient)
                {
                    commonResponse.DataEnum = _appointmentService.PacientEventById(_logInUserId);
                    commonResponse.Status = Helper.successCode;
                }
                else if(_role == Helper.Doctor)
                {
                    commonResponse.DataEnum = _appointmentService.DoctorEventById(_logInUserId);
                    commonResponse.Status = Helper.successCode;
                }
                else
                {

                    commonResponse.DataEnum = _appointmentService.DoctorEventById(doctorId);
                    commonResponse.Status = Helper.successCode;
                }
            }
            catch(Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = Helper.failureCode;
            }
            return Ok(commonResponse);

        }

    }
}
