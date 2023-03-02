
using AddressBook.DAL;
using AddressBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AddressBook.Controllers
{
    public class CON_ContactController : Controller
    {
        private IConfiguration Configuration;

        public CON_ContactController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #region SelectAll
        public IActionResult Index()
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            CON_DAL dalCON = new CON_DAL();
            DataTable dt = dalCON.PR_CON_Contact_SelectAll(str);
            return View("CON_ContactList", dt);


            //DataTable dt = new DataTable();
            //SqlConnection conn = new SqlConnection(connectionstr);
            //conn.Open();
            //SqlCommand objCmd = conn.CreateCommand();
            //objCmd.CommandType = System.Data.CommandType.StoredProcedure;
            //objCmd.CommandText = "PR_CON_Contact_SelectAll";
            //SqlDataReader objSDR = objCmd.ExecuteReader();
            //dt.Load(objSDR);
        }
        #endregion

        #region Delete
        public ActionResult Delete(int ContactID)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_CON_Contact_DeleteByPK";
            cmd.Parameters.AddWithValue("@ContactID", ContactID);
            DataTable dt = new DataTable();
            cmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? ContactID)
        {

            //String connectionstr7 = this.Configuration.GetConnectionString("myConnectionString");
            //DataTable dt7 = new DataTable();
            //SqlConnection conn7 = new SqlConnection(connectionstr7);
            //conn7.Open();
            //SqlCommand cmd7 = conn7.CreateCommand();
            //cmd7.CommandType = CommandType.StoredProcedure;
            //cmd7.CommandText = "PR_MST_ContactCategory_SelectForDropDown";
            //SqlDataReader objSDR7 = cmd7.ExecuteReader();
            //dt7.Load(objSDR7);

            //List<MST_ContactCategoryDropDownModel> list7 = new List<MST_ContactCategoryDropDownModel>();
            //foreach (DataRow dr in dt7.Rows)
            //{
            //    MST_ContactCategoryDropDownModel modelMST_ContactCategory = new MST_ContactCategoryDropDownModel();
            //    modelMST_ContactCategory.ContactCategoryID = (Convert.ToInt32(dr["ContactCategoryID"]));
            //    modelMST_ContactCategory.ContactCategory = (Convert.ToString(dr["ContactCategory"]));
            //    list7.Add(modelMST_ContactCategory);
            //}
            //ViewBag.CountryList = list7;


            String connectionstr7 = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt7 = new DataTable();
            SqlConnection conn7 = new SqlConnection(connectionstr7);
            conn7.Open();
            SqlCommand cmd7 = conn7.CreateCommand();
            cmd7.CommandType = CommandType.StoredProcedure;
            cmd7.CommandText = "PR_MST_ContactCategory_SelectForDropDown";
            SqlDataReader objSDR7 = cmd7.ExecuteReader();
            dt7.Load(objSDR7);
            conn7.Close();
            List<MST_ContactCategoryDropDownModel> list7 = new List<MST_ContactCategoryDropDownModel>();
            foreach (DataRow dr in dt7.Rows)
            {
                MST_ContactCategoryDropDownModel modelMST_ContactCategory = new MST_ContactCategoryDropDownModel();
                modelMST_ContactCategory.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                modelMST_ContactCategory.ContactCategory = (string)dr["ContactCategory"];
                list7.Add(modelMST_ContactCategory);
            }
            ViewBag.ContactCategoryList = list7;



            //String connectionstr1 = this.Configuration.GetConnectionString("myConnectionString");
            //DataTable dt2 = new DataTable();
            //SqlConnection conn2 = new SqlConnection(connectionstr1);
            //conn2.Open();
            //SqlCommand cmd2 = conn2.CreateCommand();
            //cmd2.CommandType = CommandType.StoredProcedure;
            //cmd2.CommandText = "PR_LOC_Country_SelectForDropDown";
            //SqlDataReader objSDR2 = cmd2.ExecuteReader();
            //dt2.Load(objSDR2);
            //List<LOC_CountryDropDownModel> list1 = new List<LOC_CountryDropDownModel>();
            //foreach (DataRow dr in dt2.Rows)
            //{
            //    LOC_CountryDropDownModel modelLOC_CountryDropDown = new LOC_CountryDropDownModel();
            //    modelLOC_CountryDropDown.CountryID = (Convert.ToInt32(dr["CountryID"]));
            //    modelLOC_CountryDropDown.CountryName = (Convert.ToString(dr["CountryName"]));
            //    list1.Add(modelLOC_CountryDropDown);
            //}
            //ViewBag.CountryList = list1;
            //conn2.Close();
            String str6 = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt6 = new DataTable();
            SqlConnection conn6 = new SqlConnection(str6);
            conn6.Open();
            SqlCommand cmd6 = conn6.CreateCommand();
            cmd6.CommandType = CommandType.StoredProcedure;
            cmd6.CommandText = "PR_LOC_Country_SelectForDropDown";
            SqlDataReader objSDR6 = cmd6.ExecuteReader();
            dt6.Load(objSDR6);
            conn6.Close();

            List<LOC_CountryDropDownModel> list6 = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in dt6.Rows)
            {
                LOC_CountryDropDownModel modelLOC_CountryDropDown = new LOC_CountryDropDownModel();
                modelLOC_CountryDropDown.CountryID = Convert.ToInt32(dr["CountryID"]);
                modelLOC_CountryDropDown.CountryName = (string)dr["CountryName"];
                list6.Add(modelLOC_CountryDropDown);
            }
            ViewBag.CountryList = list6;




            //List<LOC_StateDropDownModel> list = new List<LOC_StateDropDownModel>();
            //ViewBag.StateList = list;

            //List<LOC_CityDropDownModel> list2 = new List<LOC_CityDropDownModel>();
            //ViewBag.CityList = list2;

            List<LOC_StateDropDownModel> list4 = new List<LOC_StateDropDownModel>();
            ViewBag.StateList = list4;

            List<LOC_CityDropDownModel> list5 = new List<LOC_CityDropDownModel>();
            ViewBag.CityList = list5;




            if (ContactID != null)
            {
                String str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_CON_Contact_SelectByPK";
                cmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = ContactID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);
                if (dt.Rows.Count > 0)
                {
                    CON_ContactModel modelCON_Contact = new CON_ContactModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        modelCON_Contact.ContactID = (Convert.ToInt32(dr["ContactID"]));
                        modelCON_Contact.Name = (Convert.ToString(dr["Name"]));
                        modelCON_Contact.Mail = (Convert.ToString(dr["Mail"]));
                        modelCON_Contact.Address = (Convert.ToString(dr["Address"]));
                        modelCON_Contact.CityID = (Convert.ToInt32(dr["CityID"]));
                        modelCON_Contact.StateID = (Convert.ToInt32(dr["StateID"]));
                        modelCON_Contact.CountryID = (Convert.ToInt32(dr["CountryID"]));
                        modelCON_Contact.DateofBirth = (Convert.ToDateTime(dr["DateofBirth"]));
                        modelCON_Contact.Pincode = (Convert.ToInt32(dr["Pincode"]));
                        modelCON_Contact.Gender = (Convert.ToString(dr["Gender"]));
                        modelCON_Contact.Linkedin = (Convert.ToString(dr["Linkedin"]));
                        modelCON_Contact.ContactCategoryID = (Convert.ToInt32(dr["ContactCategoryID"]));
                        modelCON_Contact.PhotoPath = (Convert.ToString(dr["PhotoPath"]));
                    }
                    conn.Close();
                    return View("CON_ContactAddEditcshtml", modelCON_Contact);
                }
            }
            return View("CON_ContactAddEditcshtml");
        }

        [HttpPost]



        public ActionResult Save(CON_ContactModel modelCON_Contact)
        {

            if (modelCON_Contact.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, modelCON_Contact.File.FileName);
                modelCON_Contact.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelCON_Contact.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))

                {
                    modelCON_Contact.File.CopyTo(stream);
                }

            }
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelCON_Contact.ContactID == null)
            {
                cmd.CommandText = "PR_CON_Contact_Insert";
                cmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelCON_Contact.PhotoPath;
            }
            else
            {
                cmd.CommandText = "PR_CON_Contact_UpdateByPK";
                cmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = modelCON_Contact.ContactID;
                cmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelCON_Contact.PhotoPath;
            }
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = modelCON_Contact.Name;
            cmd.Parameters.Add("@Mail", SqlDbType.NVarChar).Value = modelCON_Contact.Mail;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = modelCON_Contact.Address;
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelCON_Contact.CityID;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelCON_Contact.StateID;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelCON_Contact.CountryID;
            cmd.Parameters.Add("@Pincode", SqlDbType.Int).Value = modelCON_Contact.Pincode;
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = modelCON_Contact.Gender;
            cmd.Parameters.Add("@DateofBirth", SqlDbType.Date).Value = modelCON_Contact.DateofBirth;
            cmd.Parameters.Add("@Linkedin", SqlDbType.NVarChar).Value = modelCON_Contact.Linkedin;
            cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelCON_Contact.ContactCategoryID;

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelCON_Contact.ContactID == null)
                {
                    TempData["ContactInsertMsg"] = "Record Inserted Successfully!!!";
                }
                else
                {
                    TempData["ContactInsertMsg"] = "Record Updated Successfully!!!";
                }
            }
            conn.Close();
            return RedirectToAction("Add");

        }
        public ActionResult DropDownByCountry(int CountryID)
        {
            //String connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            //SqlConnection conn1 = new SqlConnection(connectionstr);
            //conn1.Open();
            //SqlCommand cmd1 = conn1.CreateCommand();
            //cmd1.CommandType = CommandType.StoredProcedure;
            //cmd1.CommandText = "PR_LOC_State_SelectForDropDownByCountryID";
            //cmd1.Parameters.AddWithValue("@CountryID", CountryID);
            //DataTable dt1 = new DataTable();
            //SqlDataReader objSDR1 = cmd1.ExecuteReader();
            //dt1.Load(objSDR1);

            //List<LOC_StateDropDownModel> list = new List<LOC_StateDropDownModel>();
            //foreach (DataRow dr in dt1.Rows)
            //{
            //    LOC_StateDropDownModel vl = new LOC_StateDropDownModel();
            //    vl.StateID = (Convert.ToInt32(dr["StateID"]));
            //    vl.StateName = (Convert.ToString(dr["StateName"]));
            //    list.Add(vl);
            //}
            //var vmodel = list;
            //return Json(vmodel);

            string str4 = this.Configuration.GetConnectionString("myConnetionString");
            DataTable dt4 = new DataTable();
            SqlConnection conn4 = new SqlConnection(str4);
            conn4.Open();
            SqlCommand cmd4 = conn4.CreateCommand();
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.CommandText = "PR_LOC_State_SelectForDropDownByCountryID";
            cmd4.Parameters.AddWithValue("@CountryID", CountryID);
            SqlDataReader objSDR4 = cmd4.ExecuteReader();
            dt4.Load(objSDR4);
            conn4.Close();

            List<LOC_StateDropDownModel> list4 = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr in dt4.Rows)
            {
                LOC_StateDropDownModel vlst = new LOC_StateDropDownModel();
                vlst.StateID = Convert.ToInt32(dr["StateID"]);
                vlst.StateName = (string)dr["StateName"];
                list4.Add(vlst);
            }
            var vModel = list4;
            return Json(vModel);
        }
        public ActionResult DropDownByState(int StateID)
        {
            String connectionstr5 = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt5 = new DataTable();
            SqlConnection conn5 = new SqlConnection(connectionstr5);
            conn5.Open();
            SqlCommand cmd5 = conn5.CreateCommand();
            cmd5.CommandType = CommandType.StoredProcedure;
            cmd5.CommandText = "PR_LOC_City_SelectForDropDownByStateID";
            cmd5.Parameters.AddWithValue("@StateID", StateID);
            SqlDataReader objSDR5 = cmd5.ExecuteReader();
            dt5.Load(objSDR5);
            List<LOC_CityDropDownModel> list5 = new List<LOC_CityDropDownModel>();
            foreach (DataRow dr in dt5.Rows)
            {
                LOC_CityDropDownModel modelLOC_CityDropDown = new LOC_CityDropDownModel();
                modelLOC_CityDropDown.CityID = (Convert.ToInt32(dr["CityID"]));
                modelLOC_CityDropDown.CityName = (Convert.ToString(dr["CityName"]));
                list5.Add(modelLOC_CityDropDown);
            }
            var vmodel = list5;
            return Json(vmodel);


        }
    }
}
#endregion