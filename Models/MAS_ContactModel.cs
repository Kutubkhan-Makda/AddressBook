using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Models
{
    public class MAS_ContactModel
    {
        public int ContactID {set;get;}
        public string ContactName {set;get;}
        public string ContactAddress {set;get;}
        public string ContactCountryID {set;get;}
        public string ContactStateID {set;get;}
        public string ContactCityID {set;get;}
        public int ContactPincode {set;get;}
        public string ContactMobile {set;get;}
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
}