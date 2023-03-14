using Multi_AddressBook.DAL;
using Multi_AddressBook.BAL;
using Multi_AddressBook.Areas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Multi_AddressBook.Areas.MST_ContactCategory.Controllers
{
    [CheckAccess]
    [Area("MST_ContactCategory")]
    [Route("MST_ContactCategory/[Controller]/[action]")]
    public class MST_ContactCategoryController : Controller
    {
        private IConfiguration Configuration;
        public MST_ContactCategoryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        MAS_DAL dalCON = new MAS_DAL();
        public ActionResult Index()
        {
            DataTable dt = dalCON.PR_ContactCategory_SelectAll();
            return View("MST_ContactCategoryList", dt);
        }
        public ActionResult Delete(int ContactCategoryID)
        {
            String str = this.Configuration.GetConnectionString("SQL_AddressBook");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_ContactCategory_DeleteByPK";
            cmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID);
            DataTable dt = new DataTable();
            cmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("Index");
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
                MST_ContactCategoryModel modelMST_ContactCategory = new MST_ContactCategoryModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelMST_ContactCategory.ContactCategoryID = (Convert.ToInt32(dr["ContactCategoryID"]));
                    modelMST_ContactCategory.ContactCategoryName = (Convert.ToString(dr["ContactCategoryName"]));
                    modelMST_ContactCategory.CreationDate = (Convert.ToDateTime(dr["CreationDate"]));
                    modelMST_ContactCategory.ModificationDate = (Convert.ToDateTime(dr["ModificationDate"]));
                }
                return View("MST_ContactCategoryAddEdit", modelMST_ContactCategory);
            }
            return View("MST_ContactCategoryAddEdit");
        }
    
        [HttpPost]
        public ActionResult Save(MST_ContactCategoryModel modelMST_ContactCategory)
        {
            String str = this.Configuration.GetConnectionString("SQL_AddressBook");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelMST_ContactCategory.ContactCategoryID == null)
            {
                cmd.CommandText = "PR_ContactCategory_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = DBNull.Value;
            }
            else
            {
                cmd.CommandText = "PR_ContactCategory_UpdateByPK";
                cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelMST_ContactCategory.ContactCategoryID;
            } 
            cmd.Parameters.Add("@ContactCategoryName", SqlDbType.NVarChar).Value = modelMST_ContactCategory.ContactCategoryName;
            cmd.Parameters.Add("@ModificationDate", SqlDbType.DateTime).Value = DBNull.Value;

            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
        }
    }
}
