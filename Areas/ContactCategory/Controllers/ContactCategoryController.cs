using Multi_AddressBook.DAL;
using Multi_AddressBook.BAL;
using Multi_AddressBook.Areas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Multi_AddressBook.Areas.ContactCategory.Controllers
{
    [CheckAccess]
    [Area("ContactCategory")]
    [Route("ContactCategory/[Controller]/[action]")]
    public class ContactCategoryController : Controller
    {
        private IConfiguration Configuration;
        public ContactCategoryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public ActionResult Index()
        {
            MAS_DAL dalCON = new MAS_DAL();
            DataTable dt = dalCON.PR_ContactCategory_SelectAll();
            return View("ContactCategoryList", dt);
        }
        public ActionResult Delete(int ContactCategoryID)
        {
            MAS_DAL dalCON = new MAS_DAL();
            if (Convert.ToBoolean(dalCON.PR_ContactCategory_Delete(ContactCategoryID)))
                return RedirectToAction("Index");
            return View("Index");
        }
        public IActionResult Add(int? ContactCategoryID)
        {
            if (ContactCategoryID != null)
            {
                String str = this.Configuration.GetConnectionString("SQL_AddressBook");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_ContactCategory_SelectByPK";
                cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = ContactCategoryID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);
                ContactCategoryModel modelContactCategory = new ContactCategoryModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelContactCategory.ContactCategoryID = (Convert.ToInt32(dr["ContactCategoryID"]));
                    modelContactCategory.ContactCategoryName = (Convert.ToString(dr["ContactCategoryName"]));
                    modelContactCategory.CreationDate = (Convert.ToDateTime(dr["CreationDate"]));
                    modelContactCategory.ModificationDate = (Convert.ToDateTime(dr["ModificationDate"]));
                }
                return View("ContactCategoryAddEdit", modelContactCategory);
            }
            return View("ContactCategoryAddEdit");
        }
    
        [HttpPost]
        public ActionResult Save(ContactCategoryModel modelContactCategory)
        {
            String str = this.Configuration.GetConnectionString("SQL_AddressBook");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelContactCategory.ContactCategoryID == null)
            {
                cmd.CommandText = "PR_ContactCategory_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = DBNull.Value;
            }
            else
            {
                cmd.CommandText = "PR_ContactCategory_UpdateByPK";
                cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelContactCategory.ContactCategoryID;
            } 
            cmd.Parameters.Add("@ContactCategoryName", SqlDbType.NVarChar).Value = modelContactCategory.ContactCategoryName;
            cmd.Parameters.Add("@ModificationDate", SqlDbType.DateTime).Value = DBNull.Value;

            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
        }
    }
}
