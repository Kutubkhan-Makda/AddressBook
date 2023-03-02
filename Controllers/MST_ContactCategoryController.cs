using AddressBook.DAL;
using AddressBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AddressBook.Controllers
{
    public class MST_ContactCategoryController : Controller
    {
        private IConfiguration Configuration;
        public MST_ContactCategoryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public ActionResult Index()
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            CON_DAL dalCON = new CON_DAL();
            DataTable dt = dalCON.dbo_PR_MST_ContactCategory_SelectAll(str);
            return View("MST_ContactCategoryList", dt);
        }
        public ActionResult Delete(int ContactCategoryID)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_ContactCategory_DeleteByPK";
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
                String str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_MST_ContactCategory_SelectByPK";
                cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = ContactCategoryID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);
                MST_ContactCategoryModel modelMST_ContactCategory = new MST_ContactCategoryModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelMST_ContactCategory.ContactCategoryID = (Convert.ToInt32(dr["ContactCategoryID"]));
                    modelMST_ContactCategory.ContactCategory = (Convert.ToString(dr["ContactCategory"]));
                    modelMST_ContactCategory.CreationDate = (Convert.ToDateTime(dr["CreationDate"]));
                    modelMST_ContactCategory.ModificationDate = (Convert.ToDateTime(dr["ModificationDate"]));
                    modelMST_ContactCategory.PhotoPath = (Convert.ToString(dr["PhotoPath"]));
                }
                return View("MST_ContactCategoryAddEdit", modelMST_ContactCategory);
            }
            return View("MST_ContactCategoryAddEdit");
        }
    
        [HttpPost]
        public ActionResult Save(MST_ContactCategoryModel modelMST_ContactCategory)
        {
            if (modelMST_ContactCategory.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, modelMST_ContactCategory.File.FileName);
                modelMST_ContactCategory.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelMST_ContactCategory.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))

                {
                    modelMST_ContactCategory.File.CopyTo(stream);
                }

            }

            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelMST_ContactCategory.ContactCategoryID == null)
            {
                cmd.CommandText = "PR_MST_ContactCategory_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = DBNull.Value;
                cmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelMST_ContactCategory.PhotoPath;
            }
            else
            {
                cmd.CommandText = "PR_MST_ContactCategory_UpdateByPK";
                cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelMST_ContactCategory.ContactCategoryID;
                cmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelMST_ContactCategory.PhotoPath;
            } 
            cmd.Parameters.Add("@ContactCategory", SqlDbType.NVarChar).Value = modelMST_ContactCategory.ContactCategory;
            cmd.Parameters.Add("@ModificationDate", SqlDbType.DateTime).Value = DBNull.Value;

            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
        }
    }
}
