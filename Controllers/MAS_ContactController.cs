using AddressBook.DAL;
using AddressBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AddressBook.Controllers
{
    public class MAS_ContactController : Controller
    {
        private IConfiguration Configuration;

        public MAS_ContactController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        CON_DAL dalCON = new CON_DAL();

        public ActionResult Index()
        {
            string connectionString = this.Configuration.GetConnectionString("SQL_AddressBook");
            DataTable dt = dalCON.PR_MAS_Contact_SelectAll(connectionString);
            return View("MAS_ContactList",dt);
        }


        public ActionResult Delete(int ContactID)
        {
            String connectionString = this.Configuration.GetConnectionString("SQL_AddressBook");
            if (Convert.ToBoolean(dalCON.PR_MAS_Contact_Delete(connectionString, ContactID)))
                return RedirectToAction("Index");
            return View("Index");
        }


        public IActionResult Add(int? ContactID)
        {
            String connectionstr7 = this.Configuration.GetConnectionString("SQL_AddressBook");
            DataTable dt7 = dalCON.PR_ContactCategory_SelectByDropdownList(connectionstr7);
            
            List<MST_ContactCategoryDropDownModel> list7 = new List<MST_ContactCategoryDropDownModel>();
            foreach (DataRow dr in dt7.Rows)
            {
                MST_ContactCategoryDropDownModel modelMST_ContactCategory = new MST_ContactCategoryDropDownModel();
                modelMST_ContactCategory.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                modelMST_ContactCategory.ContactCategoryName = (string)dr["ContactCategoryName"];
                list7.Add(modelMST_ContactCategory);
            }
            ViewBag.ContactCategoryList = list7;

            String str6 = this.Configuration.GetConnectionString("SQL_AddressBook");
            DataTable dt6 = new DataTable();
            SqlConnection conn6 = new SqlConnection(str6);
            conn6.Open();
            SqlCommand cmd6 = conn6.CreateCommand();
            cmd6.CommandType = CommandType.StoredProcedure;
            cmd6.CommandText = "PR_LOC_Country_SelectForDropDown";
            SqlDataReader objSDR6 = cmd6.ExecuteReader();
            dt6.Load(objSDR6);
            conn6.Close();

            List<Areas.Models.LOC_CountryDropDownModel> list6 = new List<Areas.Models.LOC_CountryDropDownModel>();
            foreach (DataRow dr in dt6.Rows)
            {
                Areas.Models.LOC_CountryDropDownModel modelLOC_CountryDropDown = new Areas.Models.LOC_CountryDropDownModel();
                modelLOC_CountryDropDown.CountryID = Convert.ToInt32(dr["CountryID"]);
                modelLOC_CountryDropDown.CountryName = (string)dr["CountryName"];
                list6.Add(modelLOC_CountryDropDown);
            }
            ViewBag.CountryList = list6;

            List<Areas.Models.LOC_StateDropDownModel> list4 = new List<Areas.Models.LOC_StateDropDownModel>();
            ViewBag.StateList = list4;

            List<LOC_CityDropDownModel> list5 = new List<LOC_CityDropDownModel>();
            ViewBag.CityList = list5;

            
            if(ContactID!=null)
            {
                String str5 = this.Configuration.GetConnectionString("SQL_AddressBook");
                DataTable dt = dalCON.PR_MAS_Contact_SelectByPK(str5, ContactID);
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
                }
                return View("MAS_ContactAddEdit",modelMAS_Contact);
            }
            return View("MAS_ContactAddEdit");
        }


        [HttpPost]
        public IActionResult Save(MAS_ContactModel modelMAS_Contact)
        {
            if (modelMAS_Contact.File != null)
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
                    TempData["ContactInsetMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["ContactInsetMsg"] = "Record Updated Successfully";
                }
            }
            conn.Close();
            
            return View("MAS_ContactAddEdit");
        }

        public ActionResult DropDownByState(int StateID)
        {
            String connectionstr = this.Configuration.GetConnectionString("SQL_AddressBook");
           
           
            SqlConnection conn1 = new SqlConnection(connectionstr);
            conn1.Open();
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_City_SelectForDropDown";
            cmd1.Parameters.AddWithValue("@StateID", StateID);
            DataTable dt1 = new DataTable();
            SqlDataReader objSDR1 = cmd1.ExecuteReader();
            dt1.Load(objSDR1);
           
            List<LOC_CityDropDownModel> list = new List<LOC_CityDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                LOC_CityDropDownModel vl = new LOC_CityDropDownModel();
                vl.CityID = (Convert.ToInt32(dr["CityID"]));
                vl.CityName = (Convert.ToString(dr["CityName"]));
                list.Add(vl);
            }
           
            var vmodel = list;
            return Json(vmodel);
        }
    }
}