
namespace AddressBook.Models
{
    public class CON_ContactModel
    {
        public int? ContactID { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public DateTime DateofBirth { get; set; }
        public int Pincode { get; set; }
        public string Gender { get; set; }
        public string Linkedin { get; set; }
        public int ContactCategoryID { get; set; }
        public string ContactCategory { get; set; }

        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }
        // public int CreationDate { get; set; }
        // public int ModificationDate { get; set; }








    }
}
