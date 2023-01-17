using Appointment.Models;
using Appointment.Models.ViewModels;
using Appointment.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Numerics;

namespace Appointment.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddUpdate(ApponitmentViewModel viewModel)
        {
            var startdate = DateTime.Parse(viewModel.StartDate,CultureInfo.CreateSpecificCulture("en-EN"));
            var enddate = DateTime.Parse(viewModel.StartDate, CultureInfo.CreateSpecificCulture("en-EN")).AddMinutes(Convert.ToDouble(viewModel.Duration));
            if(viewModel != null && viewModel.Id>0)
            {
                return 1;
            }
            else
            {
                AppointmentModel appointment = new AppointmentModel()
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    StartDate = startdate,
                    EndDate = enddate,
                    Duration = viewModel.Duration,
                    DoctorId = viewModel.DoctorId,
                    PatientId = viewModel.PatientId,
                    IsDoctorAproved = false,
                    AdminId = viewModel.AdminId
                };
               
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();
                return 2;
            }
        }


        public  List<ApponitmentViewModel> DoctorEventById(string doctorId)
        {
            return  _context.Appointments.Where(x => x.DoctorId == doctorId).ToList().Select(c => new ApponitmentViewModel()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorAproved=c.IsDoctorAproved
            }).ToList();
        }

        public List<ApponitmentViewModel> PatientEventById(string patientId)
        {
            return _context.Appointments.Where(x => x.PatientId == patientId).ToList().Select(c => new ApponitmentViewModel()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorAproved = c.IsDoctorAproved
            }).ToList();
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

        public List<PatientVM> PatientList()
        {
            var patient = (from user in _context.Users
                           join userRoles in _context.UserRoles on user.Id equals userRoles.UserId
                           join roles in _context.Roles.Where(x => x.Name == Helper.Patient) on userRoles.RoleId equals roles.Id
                           select new PatientVM
                           {
                               Id = user.Id,
                               Name = user.Name,
                           }).ToList();
            return patient;
        }
    }
}
