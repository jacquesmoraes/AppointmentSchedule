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

    }
}
