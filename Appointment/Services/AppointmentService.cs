﻿using Appointment.Models;
using Appointment.Models.ViewModels;
using Appointment.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Numerics;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Appointment.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public AppointmentService(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public async Task<int> AddUpdate(ApponitmentViewModel viewModel)
        {
            var startdate = DateTime.Parse(viewModel.StartDate,CultureInfo.CreateSpecificCulture("en-EN"));
            var enddate = DateTime.Parse(viewModel.StartDate, CultureInfo.CreateSpecificCulture("en-EN")).AddMinutes(Convert.ToDouble(viewModel.Duration));
            var patient = _context.Users.FirstOrDefault(x => x.Id == viewModel.PatientId);
            var doctor = _context.Users.FirstOrDefault(x => x.Id == viewModel.DoctorId);
            if (viewModel != null && viewModel.Id>0)
            {
                var appointment = _context.Appointments.FirstOrDefault(x => x.Id == viewModel.Id);
                appointment.Title = viewModel.Title;
                appointment.Description = viewModel.Description;
                appointment.StartDate = startdate;
                appointment.EndDate = enddate;
                appointment.Duration = viewModel.Duration;
                appointment.DoctorId = viewModel.DoctorId;
                appointment.PatientId = viewModel.PatientId;
                appointment.IsDoctorAproved = false;
                appointment.AdminId = viewModel.AdminId;
                await _context.SaveChangesAsync();
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
                await _emailSender.SendEmailAsync(doctor.Email, "Appointment created", $"Your appointment with {patient.Name} is created and is in pending status" );
                await _emailSender.SendEmailAsync(patient.Email, "Appointment created", $"Your appointment with {doctor.Name} is created and is in pending status");

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
                           }).OrderBy(x => x.Name).ToList();
            return patient;
        }

        public ApponitmentViewModel GetById(int id)
        {
            return _context.Appointments.Where(x => x.Id == id).ToList().Select(c => new ApponitmentViewModel()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorAproved = c.IsDoctorAproved,
                PatientId = c.PatientId,
                DoctorId = c.DoctorId,
                PatientName = _context.Users.Where(x => x.Id == c.PatientId).Select(x => x.Name).FirstOrDefault(),
                DoctorName = _context.Users.Where(x => x.Id == c.DoctorId).Select(x => x.Name).FirstOrDefault(),
            }).SingleOrDefault();
        }

        public async Task<int> Delete(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(x => x.Id == id);
            if(appointment != null){
                _context.Appointments.Remove(appointment);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> ConfirmEvent(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(x => x.Id == id);
            if(appointment != null){
                appointment.IsDoctorAproved = true;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
