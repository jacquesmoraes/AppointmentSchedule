using Appointment.Models;
using Appointment.Models.ViewModels;
using Appointment.Utilities;
using System.Linq;

namespace Appointment.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public  List<DoctorVM> DoctorList()
        {
            var doctors = (from user in _context.Users
                           join userRoles in _context.UserRoles on user.Id equals userRoles.UserId
                           join roles in _context.Roles.Where(x => x.Name == Helper.Doctor) on userRoles.RoleId equals roles.Id
                           select new DoctorVM
                           {
                               Id = user.Id,
                               Name = user.Name,
                           }).ToList();
            return  doctors;
        }

        public List<PacientVM> PacientList()
        {
            var pacient = (from user in _context.Users
                           join userRoles in _context.UserRoles on user.Id equals userRoles.UserId
                           join roles in _context.Roles.Where(x => x.Name == Helper.Pacient) on userRoles.RoleId equals roles.Id
                           select new PacientVM
                           {
                               Id = user.Id,
                               Name = user.Name,
                           }).ToList();
            return pacient;
        }
    }
}
