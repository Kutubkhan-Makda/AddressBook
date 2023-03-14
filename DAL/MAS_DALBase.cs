using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using Multi_AddressBook.BAL;

namespace Multi_AddressBook.DAL
{
    public class MAS_DALBase : DALConnection
    {
        #region PR_ContactCategory_SelectAll
        public DataTable PR_ContactCategory_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_ContactCategory_SelectAll");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, @CV.UserID());

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
        #endregion


        #region dbo.PR_CON_Contact_SelectAll
        public DataTable PR_MAS_Contact_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MAS_Contact_SelectAll");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, @CV.UserID());

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
        #endregion

        public bool? PR_MAS_Contact_Delete(int? ContactID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MAS_Contact_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "ContactID", SqlDbType.Int, ContactID);
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable PR_ContactCategory_SelectByDropdownList()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MAS_ContactCategory_SelectForDropDown");

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

        public DataTable PR_MAS_Contact_SelectByPK(int? ContactID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MAS_Contact_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "ContactID", SqlDbType.Int, ContactID);

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
