using Appointment.Services;
using Appointment.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.Controllers
{
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
            ViewBag.Duration = Helper.GetTimeDropDown();
            ViewBag.PatientList = _appointmentService.PacientList();    
            return View();
        }
    }
}
