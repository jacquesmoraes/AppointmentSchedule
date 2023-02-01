﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Appointment.Utilities
{
    public static class Helper
    {

        public const string Admin = "Admin";
        public static string Doctor = "Doctor";
        public static string Patient = "Patient";
        public static string appointmentAdded = "Appointment added successfully.";
        public static string appointmentUpdated = "Appointment updated successfully.";
        public static string appointmentDeleted = "Appointment deleted successfully.";
        public static string appointmentExists = "Appointment for selected date and time already exists.";
        public static string appointmentNotExists = "Appointment not exists.";
        public static string meetingConfirm = "Meeting confirm successfully.";
        public static string meetingConfirmError = "Error while confirming meeting.";
        public static string appointmentAddError = "Something went wront, Please try again.";
        public static string appointmentUpdatError = "Something went wront, Please try again.";
        public static string somethingWentWrong = "Something went wront, Please try again.";
        public static int successCode = 1;
        public static int failureCode = 0;



        public static List<SelectListItem> RolesForDropDown(bool isAdmin)
        {
            if(isAdmin){
            return new List<SelectListItem>
            {
                new SelectListItem{Value= Helper.Admin, Text=Helper.Admin}
               
            };
            }
            else{
            return new List<SelectListItem>
            {
                new SelectListItem{Value= Helper.Doctor, Text=Helper.Doctor},
                new SelectListItem{Value= Helper.Patient, Text=Helper.Patient}
            };
            }
        }
        public static List<SelectListItem> GetTimeDropDown()
        {
            int minute = 60;
            List<SelectListItem> duration = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Hr" });
                minute = minute + 30;
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Hr 30 min" });
                minute = minute + 30;
            }
            return duration;
        }
    }
}
