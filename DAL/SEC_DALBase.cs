using Multi_AddressBook.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Multi_AddressBook.DAL
{
    public class SEC_DALBase : DALConnection
    {
        public DataTable PR_User_SelectByIDPass(String? UserName,String? Password)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
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