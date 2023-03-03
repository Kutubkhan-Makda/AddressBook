//using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace AddressBook.Models
{
    public class LOC_StateModel
    {
        [Required]
        public int? StateID { get; set; }
        [Required(ErrorMessage = "State Name is Required")]
        public string StateName { get; set; }
        public string StateCode { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public int CountryID { get; set; }
        public DateTime ModificationDate { get; set; }  
        [Required(ErrorMessage = "Select Country")] 
        public string CountryName { get; set; }
    }
    public class LOC_StateDropDownModel
    {
        public int StateID { get; set; }
        public string? StateName { get; set; }
    }
}
