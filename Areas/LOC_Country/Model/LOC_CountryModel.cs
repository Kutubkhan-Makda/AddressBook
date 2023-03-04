using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Areas.Models
{
    public class LOC_CountryModel
    {
        [Required]
        public int? CountryID { get; set; }
       [Required(ErrorMessage = "Country Name is Required")]
        public string CountryName {get;set;}
        
        public string CountryCode { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

    }
    public class LOC_CountryDropDownModel
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }
}
