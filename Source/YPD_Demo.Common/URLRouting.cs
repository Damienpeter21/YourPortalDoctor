using System;
using System.Collections.Generic;
using System.Text;

namespace YPD_Demo.Common
{
   public class URLRouting
    {
        public class Login {
            public const string Signin = "~/Login";
            public const string Registration = "~/Register";
            public const string Dashboard = "~/Dashboard";
            public const string Overview = "~/Overview";
            public const string PatientDashboard = "~/PatientDashboard";
            }
        public class Doctor
        {
            public const string AddPatientDetails = "~/AddPatientDetail";
            public const string AddPatientVisitRecord = "~/AddPatientVisitRecord";
            public const string ViewPatientVisitRecord = "~/ViewPatientVisitRecord";
            public const string ViewPatientDetails = "~/ViewPatientDetail";
            public const string AddMedications = "~/AddMedications";
            public const string AddDosage = "~/AddDosage";
            public const string ViewMedications = "~/ViewMedications";
            public const string PatientSymptoms = "~/PatientSymptoms";
        }
    }
}
