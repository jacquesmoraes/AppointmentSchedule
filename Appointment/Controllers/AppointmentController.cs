using Appointment.Services;
using Appointment.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService service)
        {
            _appointmentService = service;
        }

      
        public IActionResult Index()
        {
            ViewBag.DoctorList = _appointmentService.DoctorList();
            ViewBag.PatientList = _appointmentService.PatientList();
            ViewBag.Duration = Helper.GetTimeDropDown();
            return View();
        }
    }
}
