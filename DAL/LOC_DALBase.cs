using AddressBook.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace AddressBook.DAL
{
    public class LOC_DALBase
    {

        #region dbo.PR_LOC_State_SelectAll
        public DataTable PR_LOC_State_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectAll");

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


        public DataTable PR_LOC_Country_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectAll");

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


        #region dbo.PR_LOC_City_SelectAll
        public DataTable PR_LOC_City_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectAll");

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

        public bool? PR_LOC_Country_Delete(string conn,int? CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool? PR_LOC_State_Delete(string conn,int? StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool? PR_LOC_City_Delete(string conn,int? CityID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable PR_LOC_State_SelectByDropdownList(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectForDropDown");

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

        public DataTable PR_LOC_State_SelectByPK(string conn, int? StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);

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

        public DataTable PR_LOC_Country_SelectByPK(string conn, int? CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);

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

        public DataTable PR_LOC_City_SelectByPK(string conn, int? CityID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);

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
