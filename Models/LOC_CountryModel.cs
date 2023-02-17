namespace AddressBook.Models
{
    public class LOC_CountryModel
    {
        public int CountryID {set;get;}
        public string CountryName {set;get;}
        public string? CountryCode {set;get;}
        public DateTime CreationDate {set;get;}
        public DateTime ModificationDate {set;get;}
    }

    public class LOC_CountryDropDownModel
    {
        public int CountryID {set;get;}
        public string CountryName {set;get;}
    }
}
