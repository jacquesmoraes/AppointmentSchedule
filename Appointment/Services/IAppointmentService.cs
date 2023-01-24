using Appointment.Models;
using Appointment.Models.ViewModels;

namespace Appointment.Services
{
    public interface IAppointmentService
    {

        public List<DoctorVM> DoctorList();
        public List<PatientVM> PatientList();
        public Task<int>AddUpdate(ApponitmentViewModel viewModel);

        public List<ApponitmentViewModel> DoctorEventById(string doctorId);
        public List<ApponitmentViewModel> PatientEventById(string PatientId);
        public ApponitmentViewModel GetById(int id);

        public Task<int> Delete(int id);
        public Task<int> ConfirmEvent(int id);
    }
}
