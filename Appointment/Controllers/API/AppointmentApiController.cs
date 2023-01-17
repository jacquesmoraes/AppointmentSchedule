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
                commonResponse.status = _appointmentService.AddUpdate(data).Result;
                if(commonResponse.status == 1)
                {
                    commonResponse.message = Helper.appointmentUpdated;
                }
                if (commonResponse.status == 2)
                {
                    commonResponse.message = Helper.appointmentAdded;
                }
                
            }
            catch( Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failureCode;
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
                if(_role == Helper.Patient)
                {
                    commonResponse.dataenum = _appointmentService.PatientEventById(_logInUserId);
                    commonResponse.status = Helper.successCode;
                }
                else if(_role == Helper.Doctor)
                {
                    commonResponse.dataenum = _appointmentService.DoctorEventById(_logInUserId);
                    commonResponse.status = Helper.successCode;
                }
                else
                {

                    commonResponse.dataenum = _appointmentService.DoctorEventById(doctorId);
                    commonResponse.status = Helper.successCode;
                }
            }
            catch(Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failureCode;
            }
            return Ok(commonResponse);

        }

    }
}
