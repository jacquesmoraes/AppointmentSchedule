using Microsoft.AspNetCore.Mvc.Rendering;

namespace Appointment.Utilities
{
    public static class Helper
    {

        public static string Admin = "Admin";
        public static string Doctor = "Doctor";
        public static string Pacient = "Pacient";

        public static List<SelectListItem> RolesForDropDown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Value= Helper.Admin, Text=Helper.Admin},
                new SelectListItem{Value= Helper.Doctor, Text=Helper.Doctor},
                new SelectListItem{Value= Helper.Pacient, Text=Helper.Pacient},
            };
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
