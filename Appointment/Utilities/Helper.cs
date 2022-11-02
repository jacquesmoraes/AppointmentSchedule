using Microsoft.AspNetCore.Mvc.Rendering;

namespace Appointment.Helper
{
    public static class Utilities
    {

        public static string Admin = "Admin";
        public static string Doctor = "Doctor";
        public static string Pacient = "Pacient";

        public static List<SelectListItem> RolesForDropDown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Value= Utilities.Admin, Text=Utilities.Admin},
                new SelectListItem{Value= Utilities.Doctor, Text=Utilities.Doctor},
                new SelectListItem{Value= Utilities.Pacient, Text=Utilities.Pacient},
            };
        }

    }
}
