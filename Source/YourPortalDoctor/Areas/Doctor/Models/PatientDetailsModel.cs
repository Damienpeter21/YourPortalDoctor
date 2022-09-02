using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourPortalDoctor.Areas.Doctor.Models
{
   
    public class PatientCommonModel
    {
        public PatientDetailsModels objpatient;
        public List<PatientDetailsModels> objPatientList;

        public MedicineDetails objMedicine;
        public List<MedicineDetails> objMedicineList;

        public Symptoms objsymptoms;
        public List<Symptoms> objsymptomslist;

        //public Tabletsyrub objtabletsyrub;
        //public List<Tabletsyrub> objtabletsyrublist;

        public PatientAge objage;
        public List<PatientAge> objagelist;

        public Dosage objdosage;
        public List<Dosage> objdosagelist;


    }
    public class PatientDetailsModels
    {
        // Patient Details
        public int Patient_ID { get; set; }
        public string Patient_Roll_No { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //public DateTime PTIN { get; set; }
        //public DateTime PTOUT { get; set; }
        public DateTime DateofBirth { get; set; }
        public string BloodGroup { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string Status { get; set; }

        // Medicines
   
        public int Symptom_ID { get; set; }
        public string SymptomName { get; set; }
        public int Tablet_ID { get; set; }
        public string TabletSyrup { get; set; }
        public int Dosage_ID { get; set; }
        public string Dosage { get; set; }
        public int Age_ID { get; set; }
        public string Age { get; set; }
    }

    public class Symptoms
    {
        public int SymptomsId { get; set; }
        public string SymptomsName { get; set; }
    }
    public class MedicineDetails
    {
        public int TabletId { get; set; }
        public string TabletSyrup { get; set; }
        public int SymptomsId { get; set; }
    }
    public class PatientAge
    {
        public int AgeId { get; set; }
        public string Age { get; set; }
    }
    public class Dosage
    {
        public int DosageId { get; set; }
        public string DosageName { get; set; }
        public int AgeId { get; set; }
    }
    public class PatientVisitRecord
    {
        public int PVR_ID { get; set; }
        public string PatientName { get; set; }
        public string Symptom{ get; set; }
        public DateTime PatientIN_Date { get; set; }
        public DateTime PatientOut_Date { get; set; }
      
    }
    public class patientdetails
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        
    }
    public class ViewModelPatientVisitRecord
    {
        public PatientVisitRecord patientVisitRecord;
        public List<Symptoms> symptoms;
        public List<patientdetails> patientdetails;
    }
    }
