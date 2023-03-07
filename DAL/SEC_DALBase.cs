using AddressBook.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace AddressBook.DAL
{
    public class SEC_DALBase
    {
        public DataTable PR_User_SelectByIDPass(string conn,String? UserName,String? Password)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_User_SelectByIDPass");
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.NVarChar, UserName);
                sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.NVarChar, Password);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}