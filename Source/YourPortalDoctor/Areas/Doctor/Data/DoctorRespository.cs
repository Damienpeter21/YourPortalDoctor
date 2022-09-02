using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using YourPortalDoctor.Areas.Doctor.Models;
using YourPortalDoctor.Common;
using YPD_Demo.Common;
using YPD_Demo.DBEngine;

namespace YourPortalDoctor.Areas.Doctor.Data
{
    public interface IDoctorRespository
    {
        void SavePatientDetails(PatientDetailsModels objModel);
        void SaveMedicinesDetails(PatientDetailsModels objModel);
        void SaveAgeDetails(PatientDetailsModels objModel);
        void SavePatientVistRecord(PatientVisitRecord objModel);
        DataTable GetPatientDetails();
        DataTable GetPatientVisitRecord();
        DataTable GetMedications();
        DataTable Editpatientdetails(int PatientID);
        DataTable EditPatientVisitRecord(int PVR_ID);
        void DeletePatientDetails(int PatientID);
        void DeletePatientVisitRecord(int PVR_ID);
        DataSet CreateCustomersTable();
        DataSet PatientVisitRecord();
        DataTable GetSymptoms(int SymptomId);
        DataTable GetDosage(int AgeId);

    }

    public class DoctorRespository : IDoctorRespository
    {
        public readonly IConfiguration _configuration;
        private readonly ISqlDBConnection _sqlDBConnection;

        public DoctorRespository(IConfiguration configuration, ISqlDBConnection sqlDBConnection)
        {
            _configuration = configuration;
            _sqlDBConnection = sqlDBConnection;
        }
        //Saving Patient Details
        public void SavePatientDetails(PatientDetailsModels objModel)
        {
            try
            {
                SqlParameter[] param = {
                                    new SqlParameter("@Action_ID", 1),
                                    new SqlParameter("@Patient_ID",objModel.Patient_ID),
                                    new SqlParameter("@Patient_Roll_No",objModel.Patient_Roll_No),
                                    new SqlParameter("@FirstName",objModel.FirstName),
                                    new SqlParameter("@LastName", objModel.LastName),
                                    new SqlParameter("@Email",objModel.Email),
                                    new SqlParameter("@DateofBirth",objModel.DateofBirth.ToString("yyyy-MM-dd")),
                                    new SqlParameter("@BloodGroup",objModel.BloodGroup),
                                    new SqlParameter("@Gender",objModel.Gender),
                                    new SqlParameter("@Address",objModel.Address),
                                    new SqlParameter("@MobileNumber",objModel.MobileNumber),
                                    new SqlParameter("@Status",objModel.Status)

                                    };
                _sqlDBConnection.ExecuteNonQuery(Storedprocedure.Pro_Patient, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
        }


        //Save Symptoms and Medicine Details
        public void SaveMedicinesDetails(PatientDetailsModels objModel)
        {
            try
            {
            SqlParameter[] param = {
                                  
                                    new SqlParameter("@Symptom_ID",objModel.Symptom_ID),
                                    new SqlParameter("@SymptomName",objModel.SymptomName),
                                    new SqlParameter("@TabletSyrup",objModel.TabletSyrup)
                                
                                  
                                 };
                _sqlDBConnection.ExecuteNonQuery(Storedprocedure.PRO_AddSymptomDetails, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
           
        }
        //Save Age and Doasage
        public void SaveAgeDetails(PatientDetailsModels objModel)
        {
            try
            {
                SqlParameter[] param = {

                                     new SqlParameter("@Age_ID",objModel.Age_ID),
                                    new SqlParameter("@Age", objModel.Age),
                                    new SqlParameter("@Dosage",objModel.Dosage)

                                 };
                _sqlDBConnection.ExecuteNonQuery(Storedprocedure.PRO_AddDosageDetails, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }

        }
        //SavePatientVistRecord
        public void SavePatientVistRecord(PatientVisitRecord objModel)
        {
            try
            {
                
                  SqlParameter[] param = {
                                    new SqlParameter("@Action_ID", 1),
                                    new SqlParameter("@PVR_ID",objModel.PVR_ID),
                                    new SqlParameter("@PatientName",objModel.PatientName),
                                    new SqlParameter("@Symptom", objModel.Symptom),
                                    new SqlParameter("@PatientIN_Date",objModel.PatientIN_Date),
                                    new SqlParameter("@PatientOut_Date",objModel.PatientOut_Date)


                                 };
                _sqlDBConnection.ExecuteNonQuery(Storedprocedure.PRO_PatientVisitRecordCRUDE, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }

        }


        //To view the patient details
        public DataTable GetPatientDetails()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = {

            new SqlParameter("@Action_ID", 3)
                };

                dt = _sqlDBConnection.ExecuteTable(Storedprocedure.Pro_Patient, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
            return dt;
                           
        }
        public DataTable GetPatientVisitRecord()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = {

            new SqlParameter("@Action_ID", 2)
                };

                dt = _sqlDBConnection.ExecuteTable(Storedprocedure.PRO_PatientVisitRecordCRUDE, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
            return dt;

        }
        //To view Medicine details
        public DataTable GetMedications()
        {
            DataTable dt = new DataTable();
            try
            {
            
                dt = _sqlDBConnection.ExecuteTable(Storedprocedure.PRO_ViewMedicineDetails, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }


            return dt;
        }
        //To  Patient Details
        public DataTable Editpatientdetails(int PatientID)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] param = {
            new SqlParameter ("@Patient_ID", PatientID ),
            new SqlParameter("@Action_ID", 2)
            };
                dt = _sqlDBConnection.ExecuteTable(Storedprocedure.Pro_Patient, CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
            return dt;

        }
        public DataTable EditPatientVisitRecord(int PVR_ID)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] param = {
            new SqlParameter ("@PVR_ID", PVR_ID ),
            new SqlParameter("@Action_ID", 3)
            };
                dt = _sqlDBConnection.ExecuteTable(Storedprocedure.PRO_PatientVisitRecordCRUDE, CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
            return dt;

        }
        public void DeletePatientDetails(int PatientID)
        {

            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@Patient_ID", PatientID),
             
            };
                _sqlDBConnection.ExecuteNonQuery(Storedprocedure.Pro_DELETE, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
        }
        public void DeletePatientVisitRecord(int PVR_ID)
        {

            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = {

                new SqlParameter("@PVR_ID", PVR_ID),
                    new SqlParameter("@Action_ID", 4)

            };
                _sqlDBConnection.ExecuteNonQuery(Storedprocedure.PRO_PatientVisitRecordCRUDE, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
        }


        public DataSet CreateCustomersTable()
        {


            DataSet ds = new DataSet();
            try
            {

                ds= _sqlDBConnection.ExecuteTableSet(Storedprocedure.PRO_GetSymptoms, CommandType.StoredProcedure, null);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            };
            return ds;
        }
        public DataSet PatientVisitRecord()
        {


            DataSet ds = new DataSet();
            try
            {

                ds = _sqlDBConnection.ExecuteTableSet(Storedprocedure.PRO_GetPatientVisitRecord, CommandType.StoredProcedure, null);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            };
            return ds;
        }

        //GET Symptoms
        public DataTable GetSymptoms(int SymptomId)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@ActionId", 1),
                new SqlParameter("@SymptomId", SymptomId)

                   };
                dt = _sqlDBConnection.ExecuteTable("[dbo].[PRO_GetMedicinesandDosage]", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
            return dt;
        }

        //GET Dosage
        public DataTable GetDosage(int AgeId)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@ActionId", 2),
                new SqlParameter("@AgeId", AgeId)

                   };
                dt = _sqlDBConnection.ExecuteTable("[dbo].[PRO_GetMedicinesandDosage]", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
            return dt;
        }
    }
}