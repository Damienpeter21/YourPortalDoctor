using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using YourPortalDoctor.Areas.Login.Models;
using YourPortalDoctor.Common;
using YPD_Demo.Common;
using YPD_Demo.DBEngine;

namespace YourPortalDoctor.Areas.YourPortalDoctor.Data
{
    public interface ILoginRespository
    {
        void SaveRegisterDetails(EntryModel objModel);
      
    }

    public class LoginRespository : ILoginRespository
    {
        public readonly IConfiguration _configuration;
        private readonly ISqlDBConnection _sqlDBConnection;

        public LoginRespository(IConfiguration configuration, ISqlDBConnection sqlDBConnection)
        {
            _configuration = configuration;
            _sqlDBConnection = sqlDBConnection;
        }

        public void SaveRegisterDetails(EntryModel objModel)
        {
            try
            {
                SqlParameter[] param = {
                                    new SqlParameter("@UserName",objModel.UserName),
                                    new SqlParameter("@Email",objModel.Email),
                                    new SqlParameter("@Password", objModel.Password),
                                    new SqlParameter("@MobileNumber",objModel.MobileNumber),

                                    };

                _sqlDBConnection.ExecuteNonQuery(Storedprocedure.Pro_Save, CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.SendErrorToText(ex);
            }
        }

     

        
    }

        
    
}
