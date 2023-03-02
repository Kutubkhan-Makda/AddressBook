using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Models
{
    public class MST_ContactCategoryModel
    {
        [Required]
        public int? ContactCategoryID { get; set; }
        public string ContactCategory { get; set; }
        

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }


    }

    public class MST_ContactCategoryDropDownModel
    {
        public int ContactCategoryID { get; set; }
        public string ContactCategory { get; set; }
    }
}
