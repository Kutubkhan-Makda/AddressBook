using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Multi_AddressBook.Areas.Models
{
    public class LOC_CityModel
    {
        public int CityID { get; set; }
        [Required(ErrorMessage = "City Name is Required")]
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int StateID { get; set; }
        [Required(ErrorMessage = "Select State")]
        public string StateName { get; set; }
        public int CountryID { get; set; }
        [Required(ErrorMessage = "Select Country")]
        public string CountryName { get; set; }
    }
    public class LOC_CityDropDownModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
    }
}
