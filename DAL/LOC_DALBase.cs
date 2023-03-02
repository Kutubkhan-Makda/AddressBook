using AddressBook.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace AddressBook.DAL
{
    public class LOC_DALBase
    {

        #region dbo.PR_LOC_State_SelectAll
        public DataTable dbo_PR_LOC_State_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_SelectAll");

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
        public DataTable dbo_PR_LOC_City_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_City_SelectAll");

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

//        #region PR_LOC_Country_Insert

//        public bool? PR_LOC_Country_Insert(string conn, LOC_CountryModel modelLOC_Country)
//        {
//            try
//            {
//                SqlDatabase sqlDB = new SqlDatabase(conn);
//                DbCommand cmd = sqlDB.GetStoredProcCommand("PR_LOC_Country_Insert");
               
//                sqlDB.AddInParameter(cmd, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);
//                sqlDB.AddInParameter(cmd, "CreationDate", SqlDbType.DateTime, modelLOC_Country.CreationDate);
//                sqlDB.AddInParameter(cmd, "ModificationDate", SqlDbType.DateTime, modelLOC_Country.ModificationDate);
//                sqlDB.AddInParameter(cmd, "PhotoPath", SqlDbType.NVarChar, modelLOC_Country.PhotoPath);

//                int vReturnValue = sqlDB.ExecuteNonQuery(cmd);
//                return (vReturnValue == -1 ? false : true);
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }

//        #endregion
//        #region PR_LOC_Country_Insert
//        //public DataTable PR_LOC_Country_UpdateByPK(string conn, LOC_CountryModel modelLOC_Country)
//        //{
//        //    try
//        //    {
//        //        SqlDatabase sqlDB = new SqlDatabase(conn);
//        //        DbCommand cmd = sqlDB.GetStoredProcCommand("PR_LOC_Country_UpdateByPK");
//        //        sqlDB.AddInParameter(cmd, "CountryID", SqlDbType.Int, modelLOC_Country.CountryID);
//        //        sqlDB.AddInParameter(cmd, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);
//        //        sqlDB.AddInParameter(cmd, "CreationDate", SqlDbType.DateTime, modelLOC_Country.CreationDate);
//        //        sqlDB.AddInParameter(cmd, "ModificationDate", SqlDbType.DateTime, modelLOC_Country.ModificationDate);
//        //        sqlDB.AddInParameter(cmd, "PhotoPath", SqlDbType.NVarChar, modelLOC_Country.PhotoPath);
//        //        DataTable dt = new DataTable();
//        //        int vReturnValue = sqlDB.ExecuteNonQuery(cmd);
//        //        return (vReturnValue == -1 ? false : true);

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return null;
//        //    }
//        //}

//        public bool? PR_LOC_Country_UpdateByPK(string conn, LOC_CountryModel modelLOC_Country)
//        {
//            try
//            {
//                SqlDatabase sqlDB = new SqlDatabase(conn);
//                DbCommand cmd = sqlDB.GetStoredProcCommand("PR_LOC_Country_UpdateByPK");
//                         sqlDB.AddInParameter(cmd, "CountryID", SqlDbType.Int, modelLOC_Country.CountryID);
//                        sqlDB.AddInParameter(cmd, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);
//                      sqlDB.AddInParameter(cmd, "CreationDate", SqlDbType.DateTime, modelLOC_Country.CreationDate);
//                       sqlDB.AddInParameter(cmd, "ModificationDate", SqlDbType.DateTime, modelLOC_Country.ModificationDate);
//                        sqlDB.AddInParameter(cmd, "PhotoPath", SqlDbType.NVarChar, modelLOC_Country.PhotoPath);

//                int vReturnValue = sqlDB.ExecuteNonQuery(cmd);
//                return (vReturnValue == -1 ? false : true);
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }
//        #endregion
    }
}
