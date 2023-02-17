namespace AddressBook.Models
{
    public class LOC_CityModel
    {
        public int CityID {set;get;}
        public string CityName {set;get;}
        public string? CityCode {set;get;}
        public int StateID {set;get;}
        public int CountryID {set;get;}
        public DateTime CreationDate {set;get;}
        public DateTime ModificationDate {set;get;}
    }

    public class LOC_CityDropDownModel
    {
        public int CityID {set;get;}
        public string CityName {set;get;}
    }
}