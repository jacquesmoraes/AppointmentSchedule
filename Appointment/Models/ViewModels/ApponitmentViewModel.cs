﻿namespace Appointment.Models.ViewModels
{
    public class ApponitmentViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Duration { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public bool IsDoctorAproved { get; set; }
        public string AdminId { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string AdminName { get; set; }
        public bool IsForClient { get; set; }

    }
}
