using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourPortalDoctor.Areas.Doctor.Data;
using YourPortalDoctor.Areas.Doctor.Models;
using YourPortalDoctor.Common;
using YPD_Demo.Common;

namespace YourPortalDoctor.Areas.YourPortalDoctor.Controllers
{
    [Area("Doctor")]
    public class DoctorController : Controller

    {
        private readonly IDoctorRespository _doctorRespository;
        public  DoctorController(IDoctorRespository doctorRespository)
            {
            _doctorRespository = doctorRespository;

         }

        //Index page
         public IActionResult Index()
        {
            return View();
        }
     
        //AddPatientDetails
        [Route(URLRouting.Doctor.AddPatientDetails)]
        public IActionResult AddPatientDetails()
        {
            return View();
        }
        //AddPatientVisitRecord
        [Route(URLRouting.Doctor.AddPatientVisitRecord)]
        public IActionResult AddPatientVisitRecord()
        {
            ViewModelPatientVisitRecord patientVisitRecord = new ViewModelPatientVisitRecord();

            DataSet ds = new DataSet();
            ds = _doctorRespository.PatientVisitRecord();


            if (ds != null && ds.Tables != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    patientVisitRecord.symptoms = (from DataRow row in ds.Tables[0].Rows
                                                          select new Symptoms()
                                                          {
                                                              SymptomsId = Convert.ToInt32(row["Symptom_ID"]),
                                                              SymptomsName = row["SymptomName"].ToString()

                                                          }).ToList();
                }
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    patientVisitRecord.patientdetails = (from DataRow row in ds.Tables[1].Rows
                                                     select new patientdetails()
                                                     {
                                                         PatientId = Convert.ToInt32(row["Patient_ID"]),
                                                         PatientName = row["PatientName"].ToString()

                                                     }).ToList();
                }
            }
            return View(patientVisitRecord);
        }
        //PatientSymptoms
        [Route(URLRouting.Doctor.PatientSymptoms)]
        public IActionResult PatientSymptoms()
        {
            PatientCommonModel patientCommonModel = new PatientCommonModel();

            DataSet ds = new DataSet();
            ds= _doctorRespository.CreateCustomersTable();


            if (ds != null && ds.Tables != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    patientCommonModel.objsymptomslist = (from DataRow row in ds.Tables[0].Rows
                                                          select new Symptoms()
                                                          {
                                                              SymptomsId = Convert.ToInt32(row["Symptom_ID"]),
                                                              SymptomsName = row["SymptomName"].ToString()

                                                          }).ToList();
                }
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    patientCommonModel.objagelist = (from DataRow row in ds.Tables[1].Rows
                                                          select new PatientAge()
                                                          {
                                                              AgeId = Convert.ToInt32(row["Age_ID"]),
                                                              Age = row["Age"].ToString()

                                                          }).ToList();
                }
            }
            return View(patientCommonModel);
        }

        //ViewPatientDetails
        [Route(URLRouting.Doctor.ViewPatientDetails)]
        public IActionResult ViewPatientDetails()
        {
            return View(_doctorRespository.GetPatientDetails());
        }
        [Route(URLRouting.Doctor.ViewPatientVisitRecord)]
        //To view Patient Record
        public IActionResult ViewPatientVisitRecord()
        {
            return View(_doctorRespository.GetPatientVisitRecord());
        }
        //AddMedications
        [Route(URLRouting.Doctor.AddMedications)]
        public IActionResult AddMedications()
        {
            return View();
        }
        //AddDosage
        [Route(URLRouting.Doctor.AddDosage)]
        public IActionResult AddDosage()
        {
            return View();
        }
        //ViewMedications
        [Route(URLRouting.Doctor.ViewMedications)]
        public IActionResult ViewMedications()
        {
            return View(_doctorRespository.GetMedications());
        }

      

        //SavePatientDetails
        public IActionResult SavePatientDetails(PatientDetailsModels PatientDetails)
        {
            try
            {
                _doctorRespository.SavePatientDetails(PatientDetails);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
            return RedirectToAction(CommonList.DoctorController.ActionViewPatientDetails);
        }
        //SavePatientVisitDetails
        public IActionResult SavePatientVistRecord(PatientVisitRecord PatientVisitDetails)
        {
            try
            {
                _doctorRespository.SavePatientVistRecord(PatientVisitDetails);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
            return RedirectToAction(CommonList.DoctorController.ViewPatientVisitRecord);
        }


        //SaveMedicinesDetails
        public IActionResult SaveMedicinesDetails(PatientDetailsModels MedicineDetails)
        {
            try
            {
                _doctorRespository.SaveMedicinesDetails(MedicineDetails);

            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }



            return RedirectToAction(CommonList.DoctorController.ActionViewMedicineDetails);
        }

        public IActionResult SaveAgeDetails(PatientDetailsModels MedicineDetails)
        {
            try
            {
                _doctorRespository.SaveAgeDetails(MedicineDetails);

            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }



            return RedirectToAction(CommonList.DoctorController.ActionViewMedicineDetails);
        }
        //EditPatientDetails
        public IActionResult EditPatientDetails(int PatientID)
        {
            DataTable dataTable = _doctorRespository.Editpatientdetails(PatientID);
            PatientDetailsModels Patient = new PatientDetailsModels();

            try
            {
        
            Patient.Patient_ID = Convert.ToInt32(dataTable.Rows[0][DBFields.PatientDetails.Patient_ID]);
            Patient.Patient_Roll_No = dataTable.Rows[0][DBFields.PatientDetails.Patient_Roll_No].ToString();
            Patient.FirstName = dataTable.Rows[0][DBFields.PatientDetails.FirstName].ToString();
            Patient.LastName = dataTable.Rows[0][DBFields.PatientDetails.LastName].ToString();
            Patient.Email = dataTable.Rows[0][DBFields.PatientDetails.Email].ToString();
            Patient.DateofBirth =Convert.ToDateTime( dataTable.Rows[0][DBFields.PatientDetails.DateOfBirth]);
            Patient.BloodGroup = dataTable.Rows[0][DBFields.PatientDetails.BloodGroup].ToString();
            Patient.Gender = dataTable.Rows[0][DBFields.PatientDetails.Gender].ToString();
            Patient.Address = dataTable.Rows[0][DBFields.PatientDetails.Address].ToString();
            Patient.MobileNumber = dataTable.Rows[0][DBFields.PatientDetails.MobileNumber].ToString();

             
            }

            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }

            return View(CommonList.DoctorController.ViewAddPatientDetails, Patient);
        }
        //EditPatientDetails
        public IActionResult EditPatientVisitRecord(int PVR_ID)
        {
            DataTable dataTable = _doctorRespository.EditPatientVisitRecord(PVR_ID);
            ViewModelPatientVisitRecord PatientVisitRecord = new ViewModelPatientVisitRecord();
            DataSet ds = new DataSet();
            ds = _doctorRespository.PatientVisitRecord();
          
        
            PatientVisitRecord PatientVisit = new PatientVisitRecord();

            try
            {

                PatientVisit.PVR_ID = Convert.ToInt32(dataTable.Rows[0][DBFields.PatientVisit.PVR_ID]);
                PatientVisit.PatientName = dataTable.Rows[0][DBFields.PatientVisit.PatientName].ToString();
                PatientVisit.Symptom = dataTable.Rows[0][DBFields.PatientVisit.Symptom].ToString();
                PatientVisit.PatientIN_Date = Convert.ToDateTime(dataTable.Rows[0][DBFields.PatientVisit.PatientIN_Date]);
                PatientVisit.PatientOut_Date = Convert.ToDateTime(dataTable.Rows[0][DBFields.PatientVisit.PatientIN_Date]);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        PatientVisitRecord.symptoms = (from DataRow row in ds.Tables[0].Rows
                                                       select new Symptoms()
                                                       {
                                                           SymptomsId = Convert.ToInt32(row["Symptom_ID"]),
                                                           SymptomsName = row["SymptomName"].ToString()

                                                       }).ToList();
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        PatientVisitRecord.patientdetails = (from DataRow row in ds.Tables[1].Rows
                                                             select new patientdetails()
                                                             {
                                                                 PatientId = Convert.ToInt32(row["Patient_ID"]),
                                                                 PatientName = row["PatientName"].ToString()

                                                             }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);

            }

            PatientVisitRecord.patientVisitRecord = PatientVisit;
            return View(CommonList.DoctorController.AddPatientVisitRecord, PatientVisitRecord);
        }

        //DeletePatientDetails
        public IActionResult DeletePatientDetails(int PatientID)
        {
            try
            {
                _doctorRespository.DeletePatientDetails(PatientID);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
            return RedirectToAction(CommonList.DoctorController.ActionViewPatientDetails);

        }
        public IActionResult DeletePatientVisitRecord(int PVR_ID)
        {
            try
            {
                _doctorRespository.DeletePatientVisitRecord(PVR_ID);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
            return RedirectToAction(CommonList.DoctorController.ViewPatientVisitRecord);
        }

        [HttpPost]
        public JsonResult GetSymptoms(int SymptomId)
        {
            DataTable dt = new DataTable();
            try
            {
               dt= _doctorRespository.GetSymptoms(SymptomId);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
            return Json(dt);

        }
        [HttpPost]
        public JsonResult GetDosage(int AgeId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = _doctorRespository.GetDosage(AgeId);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
            return Json(dt);

        }
    }
}
     
   
    
