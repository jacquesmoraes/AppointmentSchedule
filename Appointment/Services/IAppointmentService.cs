using Appointment.Models.ViewModels;

namespace Appointment.Services
{
    public interface IAppointmentService
    {

        public List<DoctorVM> DoctorList();
        public List<PacientVM> PacientList();
    }
}
