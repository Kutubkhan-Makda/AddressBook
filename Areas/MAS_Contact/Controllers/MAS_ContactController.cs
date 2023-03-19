using Multi_AddressBook.DAL;
using Multi_AddressBook.BAL;
using Multi_AddressBook.Areas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Multi_AddressBook.Areas.MAS_Contact.Controllers
{
    [CheckAccess]
    [Area("MAS_Contact")]
    [Route("MAS_Contact/[Controller]/[action]")]
    public class MAS_ContactController : Controller
    {
        private IConfiguration Configuration;

        public MAS_ContactController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public ActionResult Index()
        {
            MAS_DAL dalCON = new MAS_DAL();
            DataTable dt = dalCON.PR_MAS_Contact_SelectAll();
            return View("MAS_ContactList",dt);
        }


        public ActionResult Delete(int ContactID)
        {
            MAS_DAL dalCON = new MAS_DAL();
            if (Convert.ToBoolean(dalCON.PR_MAS_Contact_Delete(ContactID)))
                return RedirectToAction("Index");
            return View("Index");
        }


        public IActionResult Add(int? ContactID)
        {
            LOC_DAL dalLOC = new LOC_DAL();
            MAS_DAL dalCON = new MAS_DAL();
            DataTable dtDropdownContactCategory = dalCON.PR_ContactCategory_SelectByDropdownList();
            
            List<Areas.Models.ContactCategoryDropDownModel> list7 = new List<Areas.Models.ContactCategoryDropDownModel>();
            foreach (DataRow dr in dtDropdownContactCategory.Rows)
            {
                Areas.Models.ContactCategoryDropDownModel modelContactCategory = new Areas.Models.ContactCategoryDropDownModel();
                modelContactCategory.ContactCategoryID = (Convert.ToInt32(dr["ContactCategoryID"]));
                modelContactCategory.ContactCategoryName = (Convert.ToString(dr["ContactCategoryName"]));
                list7.Add(modelContactCategory);
            }
            ViewBag.ContactCategoryList = list7;

            DataTable dt2 = dalLOC.PR_LOC_Country_SelectByDropdownList();
            
            List<Areas.Models.LOC_CountryDropDownModel> listCountry = new List<Areas.Models.LOC_CountryDropDownModel>();
            foreach (DataRow dr in dt2.Rows)
            {
                Areas.Models.LOC_CountryDropDownModel modelLOC_CountryDropDown = new Areas.Models.LOC_CountryDropDownModel();
                modelLOC_CountryDropDown.CountryID = (Convert.ToInt32(dr["CountryID"]));
                modelLOC_CountryDropDown.CountryName = (Convert.ToString(dr["CountryName"]));
                listCountry.Add(modelLOC_CountryDropDown);
            }
            ViewBag.CountryList = listCountry;

            List<Areas.Models.LOC_StateDropDownModel> listState = new List<Areas.Models.LOC_StateDropDownModel>();
            ViewBag.StateList = listState;

            List<Areas.Models.LOC_CityDropDownModel> listCity = new List<Areas.Models.LOC_CityDropDownModel>();
            ViewBag.CityList = listCity;

            
            if(ContactID!=null)
            {
                DataTable dt = dalCON.PR_MAS_Contact_SelectByPK(ContactID);
                MAS_ContactModel modelMAS_Contact = new MAS_ContactModel();

                foreach(DataRow dr in dt.Rows)
                {
                    modelMAS_Contact.ContactID = Convert.ToInt32(dr["ContactID"]);
                    modelMAS_Contact.ContactName = dr["ContactName"].ToString();
                    modelMAS_Contact.ContactAddress = dr["ContactAddress"].ToString();
                    modelMAS_Contact.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelMAS_Contact.StateID = Convert.ToInt32(dr["StateID"]);
                    modelMAS_Contact.CityID = Convert.ToInt32(dr["CityID"]);
                    modelMAS_Contact.ContactPincode = Convert.ToInt32(dr["ContactPincode"]);
                    modelMAS_Contact.ContactMobile = dr["ContactMobile"].ToString();
                    modelMAS_Contact.ContactEmail = dr["ContactEmail"].ToString();
                    modelMAS_Contact.ContactDOB = Convert.ToDateTime(dr["ContactDOB"]);
                    modelMAS_Contact.ContactLinkedIN = dr["ContactLinkedIN"].ToString();
                    modelMAS_Contact.ContactGender = dr["ContactGender"].ToString();
                    modelMAS_Contact.ContactTypeOfProfession = dr["ContactTypeOfProfession"].ToString();
                    modelMAS_Contact.ContactCompanyName = dr["ContactCompanyName"].ToString();
                    modelMAS_Contact.ContactDesignation = dr["ContactDesignation"].ToString();
                    modelMAS_Contact.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                    modelMAS_Contact.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelMAS_Contact.PhotoPath = dr["PhotoPath"].ToString();
                }

                //String connectionstr = this.Configuration.GetConnectionString("SQL_AddressBook");
                //SqlConnection conn1 = new SqlConnection(connectionstr);
                //conn1.Open();
                //SqlCommand cmd1 = conn1.CreateCommand();
                //cmd1.CommandType = CommandType.StoredProcedure;
                //cmd1.CommandText = "PR_LOC_State_SelectForDropDownByCountryID";
                //cmd1.Parameters.AddWithValue("@CountryID", modelMAS_Contact.CountryID);
                //DataTable dt1 = new DataTable();
                //SqlDataReader objSDR1 = cmd1.ExecuteReader();
                //dt1.Load(objSDR1);

                DataTable dtDropdownState = dalLOC.PR_LOC_State_SelectByDropdownList(modelMAS_Contact.CountryID);

                List<Areas.Models.LOC_StateDropDownModel> listState1 = new List<Areas.Models.LOC_StateDropDownModel>();
                foreach (DataRow dr in dtDropdownState.Rows)
                {
                    Areas.Models.LOC_StateDropDownModel vl = new Areas.Models.LOC_StateDropDownModel();
                    vl.StateID = (Convert.ToInt32(dr["StateID"]));
                    vl.StateName = (Convert.ToString(dr["StateName"]));
                    listState1.Add(vl);
                }
                ViewBag.StateList = listState1;


                //SqlConnection conn2 = new SqlConnection(connectionstr);
                //conn2.Open();
                //SqlCommand cmd2 = conn2.CreateCommand();
                //cmd2.CommandType = CommandType.StoredProcedure;
                //cmd2.CommandText = "PR_LOC_City_SelectForDropDown";
                //cmd2.Parameters.AddWithValue("@StateID", modelMAS_Contact.StateID);
                //DataTable dt3 = new DataTable();
                //SqlDataReader objSDR2 = cmd2.ExecuteReader();
                //dt3.Load(objSDR2);

                DataTable dtDropdownCity = dalLOC.PR_LOC_City_SelectByDropdownList(modelMAS_Contact.StateID);

                List<Models.LOC_CityDropDownModel> listCity1 = new List<Models.LOC_CityDropDownModel>();
                foreach (DataRow dr in dtDropdownCity.Rows)
                {
                    Models.LOC_CityDropDownModel vl = new Models.LOC_CityDropDownModel();
                    vl.CityID = (Convert.ToInt32(dr["CityID"]));
                    vl.CityName = (Convert.ToString(dr["CityName"]));
                    listCity1.Add(vl);
                }
                ViewBag.CityList = listCity1;

                return View("MAS_ContactAddEdit",modelMAS_Contact);
            }
            return View("MAS_ContactAddEdit");
        }

        public ActionResult DropDownByCountry(int CountryID)
        {
            LOC_DAL dalLOC = new LOC_DAL();
            //String connectionstr = this.Configuration.GetConnectionString("SQL_AddressBook");
           //
            //SqlConnection conn1 = new SqlConnection(connectionstr);
            //conn1.Open();
            //SqlCommand cmd1 = conn1.CreateCommand();
            //cmd1.CommandType = CommandType.StoredProcedure;
            //cmd1.CommandText = "PR_LOC_State_SelectForDropDownByCountryID";
            //cmd1.Parameters.AddWithValue("@CountryID", CountryID);
            //DataTable dt1 = new DataTable();
            //SqlDataReader objSDR1 = cmd1.ExecuteReader();
            //dt1.Load(objSDR1);

            DataTable dtDropdownState = dalLOC.PR_LOC_State_SelectByDropdownList(CountryID);
           
            List<Models.LOC_StateDropDownModel> listState = new List<Models.LOC_StateDropDownModel>();
            foreach (DataRow dr in dtDropdownState.Rows)
            {
                Models.LOC_StateDropDownModel vl = new Models.LOC_StateDropDownModel();
                vl.StateID = (Convert.ToInt32(dr["StateID"]));
                vl.StateName = (Convert.ToString(dr["StateName"]));
                listState.Add(vl);
            }
           
            var vmodel = listState;
            return Json(vmodel);
        }

        [HttpPost]
        public IActionResult Save(MAS_ContactModel modelMAS_Contact)
        {
            LOC_DAL dalLOC = new LOC_DAL();
            if (modelMAS_Contact.PhotoPath != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, modelMAS_Contact.File.FileName);
                modelMAS_Contact.PhotoPath = "" + FilePath.Replace("wwwroot\\", "/") + "/" + modelMAS_Contact.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))

                {
                    modelMAS_Contact.File.CopyTo(stream);
                }

            }

            string connectionString = this.Configuration.GetConnectionString("SQL_AddressBook");
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            if(modelMAS_Contact.ContactID == null)
            {
                objCmd.CommandText = "PR_MAS_Contact_Insert";
                objCmd.Parameters.Add("@CreationDate",SqlDbType.Date).Value = DBNull.Value;
            }
            else
            {
                objCmd.CommandText = "PR_MAS_Contact_UpdateByPK";
                objCmd.Parameters.Add("@ContactID",SqlDbType.Int).Value = modelMAS_Contact.ContactID;
            }
            objCmd.Parameters.Add("@ContactName",SqlDbType.VarChar).Value = modelMAS_Contact.ContactName;
            objCmd.Parameters.Add("@ContactAddress",SqlDbType.VarChar).Value = modelMAS_Contact.ContactAddress;
            objCmd.Parameters.Add("@ContactCategoryID",SqlDbType.Int).Value = modelMAS_Contact.ContactCategoryID;
            objCmd.Parameters.Add("@CountryID",SqlDbType.Int).Value = modelMAS_Contact.CountryID;
            objCmd.Parameters.Add("@StateID",SqlDbType.Int).Value = modelMAS_Contact.StateID;
            objCmd.Parameters.Add("@CityID",SqlDbType.Int).Value = modelMAS_Contact.CityID;
            objCmd.Parameters.Add("@ContactPincode",SqlDbType.VarChar).Value = modelMAS_Contact.ContactPincode;
            objCmd.Parameters.Add("@ContactMobile",SqlDbType.VarChar).Value = modelMAS_Contact.ContactMobile;
            objCmd.Parameters.Add("@ContactEmail",SqlDbType.VarChar).Value = modelMAS_Contact.ContactEmail;
            objCmd.Parameters.Add("@ContactDOB",SqlDbType.Date).Value = modelMAS_Contact.ContactDOB;
            objCmd.Parameters.Add("@ContactLinkedIN",SqlDbType.VarChar).Value = modelMAS_Contact.ContactLinkedIN;
            objCmd.Parameters.Add("@ContactGender",SqlDbType.VarChar).Value = modelMAS_Contact.ContactGender;
            objCmd.Parameters.Add("@ContactTypeOfProfession",SqlDbType.VarChar).Value = modelMAS_Contact.ContactTypeOfProfession;
            objCmd.Parameters.Add("@ContactCompanyName",SqlDbType.VarChar).Value = modelMAS_Contact.ContactCompanyName;
            objCmd.Parameters.Add("@ContactDesignation",SqlDbType.VarChar).Value = modelMAS_Contact.ContactDesignation;
            objCmd.Parameters.Add("@ModificationDate",SqlDbType.Date).Value = DBNull.Value;
            objCmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelMAS_Contact.PhotoPath;
            
             
            if(Convert.ToBoolean(objCmd.ExecuteNonQuery()))
            {
                if(modelMAS_Contact.ContactID == null)
                {
                    TempData["ContactInsertMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["ContactInsertMsg"] = "Record Updated Successfully";
                }
            }
            conn.Close();
            
            return RedirectToAction("Index");
        }

        public ActionResult DropDownByState(int StateID)
        {
            LOC_DAL dalLOC = new LOC_DAL();
            //String connectionstr = this.Configuration.GetConnectionString("SQL_AddressBook");
            //SqlConnection conn1 = new SqlConnection(connectionstr);
            //conn1.Open();
            //SqlCommand cmd1 = conn1.CreateCommand();
            //cmd1.CommandType = CommandType.StoredProcedure;
            //cmd1.CommandText = "PR_LOC_City_SelectForDropDown";
            //cmd1.Parameters.AddWithValue("@StateID", StateID);
            //DataTable dt1 = new DataTable();
            //SqlDataReader objSDR1 = cmd1.ExecuteReader();
            //dt1.Load(objSDR1);

            DataTable dtDropdownCity = dalLOC.PR_LOC_City_SelectByDropdownList(StateID);
           
            List<Models.LOC_CityDropDownModel> listCity = new List<Models.LOC_CityDropDownModel>();
            foreach (DataRow dr in dtDropdownCity.Rows)
            {
                Models.LOC_CityDropDownModel vl = new Models.LOC_CityDropDownModel();
                vl.CityID = (Convert.ToInt32(dr["CityID"]));
                vl.CityName = (Convert.ToString(dr["CityName"]));
                listCity.Add(vl);
            }
           
            var vmodel = listCity;
            return Json(vmodel);
        }
    }
}