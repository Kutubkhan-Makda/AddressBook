//using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace AddressBook.Models
{
    public class LOC_StateModel
    {
        [Required]
        public int? StateID { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public int CountryID { get; set; }
        public DateTime ModificationDate { get; set; }   
        public string CountryName { get; set; }
        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }
    }
    public class LOC_StateDropDownModel
    {
        public int StateID { get; set; }
        public string? StateName { get; set; }
    }
}
