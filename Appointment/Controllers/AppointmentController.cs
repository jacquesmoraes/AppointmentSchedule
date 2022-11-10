using Appointment.Services;
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
            return View();
        }
    }
}
