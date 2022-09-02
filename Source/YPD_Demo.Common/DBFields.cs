using System;
using System.Collections.Generic;
using System.Text;

namespace YPD_Demo.Common
{
    public class DBFields
    {
        public class PatientDetails
        {
            //Patient Details
            public const string Patient_ID = "Patient_ID";
            public const string Patient_Roll_No = "Patient_Roll_No";
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string Email = "Email";
            public const string DateOfBirth = "DateOfBirth";
            public const string BloodGroup = "BloodGroup";
            public const string Gender = "Gender";
            public const string Address = "Address";
            public const string MobileNumber = "MobileNumber";
            public const string Status = "Status";
        }
        public class Medicines
        {
            public const string SymptomName = "SymptomName";
            public const string TabletSyrup = "TabletSyrup";
            public const string Age = "Age";
            public const string Dosage = "Dosage";
        }
        public class PatientVisit
            {
                public const string PVR_ID = "PVR_ID";
                public const string PatientName = "PatientName";
                public const string SymptomName = "SymptomName";
            public const string Symptom= "Symptom";
                public const string PatientIN_Date = "PatientIN_Date";
                public const string PatientOut_Date = "PatientOut_Date";
            }
       
    }
}
