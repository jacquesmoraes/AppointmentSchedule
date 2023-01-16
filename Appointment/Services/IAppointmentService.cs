using Appointment.Models;
using Appointment.Models.ViewModels;

namespace Appointment.Services
{
    public interface IAppointmentService
    {

        public List<DoctorVM> DoctorList();
        public List<PacientVM> PacientList();
        public Task<int>AddUpdate(ApponitmentViewModel viewModel);

        public List<ApponitmentViewModel> DoctorEventById (string eventId);
        public List<ApponitmentViewModel> PacientEventById(string PacientId);

    }
}
