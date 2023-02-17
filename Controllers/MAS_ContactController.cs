using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Reflection;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class MAS_ContactController : Controller
    {
        private IConfiguration Configuration;

        public MAS_ContactController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public ActionResult Index()
        {
            string connectionString = this.Configuration.GetConnectionString("SQL_AddressBook");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_MAS_Contact_SelectAll";
            
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            return View("MAS_ContactList",dt);
        }


        public ActionResult Delete(int ContactID)
        {
            string connectionString = this.Configuration.GetConnectionString("SQL_AddressBook");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString); 
            
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_MAS_Contact_DeleteByPK";
            objCmd.Parameters.AddWithValue("@ContactID",ContactID);
            objCmd.ExecuteNonQuery();
            
            conn.Close();

            return RedirectToAction("Index");
        }


        public IActionResult Add(int? ContactID)
        {
            string connectionString = this.Configuration.GetConnectionString("SQL_AddressBook");

            if(ContactID!=null)
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_MAS_Contact_SelectByPK";
                objCmd.Parameters.Add("@ContactID",SqlDbType.Int).Value = ContactID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);
                MAS_ContactModel modelMAS_Contact = new MAS_ContactModel();

                foreach(DataRow dr in dt.Rows)
                {
                    modelMAS_Contact.ContactID = Convert.ToInt32(dr["ContactID"]);
                    modelMAS_Contact.ContactName = dr["ContactName"].ToString();
                    modelMAS_Contact.ContactAddress = dr["ContactAddress"].ToString();
                    modelMAS_Contact.ContactCountryID = dr["ContactCountryID"].ToString();
                    modelMAS_Contact.ContactStateID = dr["ContactStateID"].ToString();
                    modelMAS_Contact.ContactCityID = dr["ContactCityID"].ToString();
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
                }
                return View("MAS_ContactAddEdit",modelMAS_Contact);
            }
            return View("MAS_ContactAddEdit");
        }


        [HttpPost]
        public IActionResult Save(MAS_ContactModel modelMAS_Contact)
        {
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
            objCmd.Parameters.Add("@ContactCountryID",SqlDbType.VarChar).Value = modelMAS_Contact.ContactCountryID;
            objCmd.Parameters.Add("@ContactStateID",SqlDbType.VarChar).Value = modelMAS_Contact.ContactStateID;
            objCmd.Parameters.Add("@ContactCityID",SqlDbType.VarChar).Value = modelMAS_Contact.ContactCityID;
            objCmd.Parameters.Add("@ContactPincode",SqlDbType.VarChar).Value = modelMAS_Contact.ContactPincode;
            objCmd.Parameters.Add("@ContactMobile",SqlDbType.VarChar).Value = modelMAS_Contact.ContactMobile;
            objCmd.Parameters.Add("@ContactEmail",SqlDbType.VarChar).Value = modelMAS_Contact.ContactEmail;
            objCmd.Parameters.Add("@ContactDOB",SqlDbType.VarChar).Value = modelMAS_Contact.ContactDOB;
            objCmd.Parameters.Add("@ContactLinkedIN",SqlDbType.VarChar).Value = modelMAS_Contact.ContactLinkedIN;
            objCmd.Parameters.Add("@ContactGender",SqlDbType.VarChar).Value = modelMAS_Contact.ContactGender;
            objCmd.Parameters.Add("@ContactTypeOfProfession",SqlDbType.VarChar).Value = modelMAS_Contact.ContactTypeOfProfession;
            objCmd.Parameters.Add("@ContactCompanyName",SqlDbType.VarChar).Value = modelMAS_Contact.ContactCompanyName;
            objCmd.Parameters.Add("@ContactDesignation",SqlDbType.VarChar).Value = modelMAS_Contact.ContactDesignation;
            objCmd.Parameters.Add("@ModificationDate",SqlDbType.Date).Value = DBNull.Value;
            
             
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
    }
}