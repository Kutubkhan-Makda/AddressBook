using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Models
{
    public class MAS_ContactModel
    {
        public int ContactID {set;get;}
        [Required(ErrorMessage = "Name is Required")]
        public string ContactName {set;get;}
        [Required(ErrorMessage = "Address is Required")]
        public string ContactAddress {set;get;}
        [Required(ErrorMessage = "Select Country")]
        public String CountryName {set;get;}
        [Required(ErrorMessage = "Select State")]
        public String StateName {set;get;}
        [Required(ErrorMessage = "Select City")]
        public String CityName {set;get;}
        public int CountryID {set;get;}
        public int StateID {set;get;}
        public int CityID {set;get;}
        public int ContactPincode {set;get;}
        [Required(ErrorMessage = "Phone Number is Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})[-. ]?([0-9]{4})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Not a valid Phone number")]
        public string ContactMobile {set;get;}
        [Required(ErrorMessage = "Email ID is Required")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        public string ContactEmail {set;get;}
        public DateTime ContactDOB {set;get;}
        public string? ContactLinkedIN {set;get;}
        public string ContactGender {set;get;}
        public string ContactTypeOfProfession {set;get;}
        public string ContactCompanyName {set;get;}
        public string ContactDesignation {set;get;}
        public DateTime CreationDate {set;get;}
        public DateTime ModificationDate {set;get;}
        public int ContactCategoryID { get; set; }
        public string ContactCategoryName { get; set; }
        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }
    }

    public class MAS_ContactCategoryDropDownModel
    {
        public int ContactCategoryID { get; set; }
        public string ContactCategoryName { get; set; }
    }
}