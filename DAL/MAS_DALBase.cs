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

        public bool? PR_ContactCategory_Delete(int? ContactCategoryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_ContactCategory_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, ContactCategoryID);
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

        public DataTable PR_ContactCategory_SelectByPK(int? ContactCategoryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_ContactCategory_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, ContactCategoryID);

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

        public bool? PR_MAS_Save_Contact(int? ContactID,int? CityID,int? StateID,int? CountryID,string? ContactName,string? ContactAddress,int? ContactCategoryID,int? ContactPincode,string? ContactMobile,string? ContactEmail,DateTime? ContactDOB,string? ContactLinkedIN,string? ContactGender,string? ContactTypeOfProfession,string? ContactCompanyName,string? ContactDesignation,string? PhotoPath)
        {
            SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD;
                if(ContactID == null)
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_MAS_Contact_Insert");
                    sqlDB.AddInParameter(dbCMD, "@CreationDate",SqlDbType.Date, DBNull.Value);
                }
                else
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_MAS_Contact_UpdateByPK");
                    sqlDB.AddInParameter(dbCMD, "@ContactID",SqlDbType.Int, ContactID);
                }
                sqlDB.AddInParameter(dbCMD, "@ContactName",SqlDbType.VarChar, ContactName);
                sqlDB.AddInParameter(dbCMD, "@ContactAddress",SqlDbType.VarChar, ContactAddress);
                sqlDB.AddInParameter(dbCMD, "@ContactCategoryID",SqlDbType.Int, ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "@CountryID",SqlDbType.Int, CountryID);
                sqlDB.AddInParameter(dbCMD, "@StateID",SqlDbType.Int, StateID);
                sqlDB.AddInParameter(dbCMD, "@CityID",SqlDbType.Int, CityID);
                sqlDB.AddInParameter(dbCMD, "@ContactPincode",SqlDbType.VarChar, ContactPincode);
                sqlDB.AddInParameter(dbCMD, "@ContactMobile",SqlDbType.VarChar, ContactMobile);
                sqlDB.AddInParameter(dbCMD, "@ContactEmail",SqlDbType.VarChar, ContactEmail);
                sqlDB.AddInParameter(dbCMD, "@ContactDOB",SqlDbType.Date, ContactDOB);
                sqlDB.AddInParameter(dbCMD, "@ContactLinkedIN",SqlDbType.VarChar, ContactLinkedIN);
                sqlDB.AddInParameter(dbCMD, "@ContactGender",SqlDbType.VarChar, ContactGender);
                sqlDB.AddInParameter(dbCMD, "@ContactTypeOfProfession",SqlDbType.VarChar, ContactTypeOfProfession);
                sqlDB.AddInParameter(dbCMD, "@ContactCompanyName",SqlDbType.VarChar, ContactCompanyName);
                sqlDB.AddInParameter(dbCMD, "@ContactDesignation",SqlDbType.VarChar, ContactDesignation);
                sqlDB.AddInParameter(dbCMD, "@ModificationDate",SqlDbType.Date, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "@PhotoPath", SqlDbType.NVarChar, PhotoPath);


                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            try
            {
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool? PR_MAS_Save_ContactCategory(int? ContactCategoryID,string? ContactCategoryName)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(SQL_Connection);
                DbCommand dbCMD;
                if(ContactCategoryID == null)
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_ContactCategory_Insert");
                    sqlDB.AddInParameter(dbCMD, "@CreationDate",SqlDbType.Date, DBNull.Value);
                }
                else
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_ContactCategory_UpdateByPK");
                    sqlDB.AddInParameter(dbCMD, "@ContactCategoryID",SqlDbType.Int, ContactCategoryID);
                }
                sqlDB.AddInParameter(dbCMD, "@ContactCategoryName", SqlDbType.NVarChar, ContactCategoryName);
                sqlDB.AddInParameter(dbCMD, "@ModificationDate", SqlDbType.DateTime, DBNull.Value);


                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
