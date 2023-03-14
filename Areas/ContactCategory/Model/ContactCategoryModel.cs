using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Multi_AddressBook.Areas.Models
{
    public class ContactCategoryModel
    {
        [Required]
        public int? ContactCategoryID { get; set; }
        public string? ContactCategoryName { get; set; }
        
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

    }

    public class ContactCategoryDropDownModel
    {
        public int? ContactCategoryID { get; set; }
        public string? ContactCategoryName { get; set; }
    }
}
