namespace AddressBook.Models
{
    public class LOC_StateModel
    {
        public int StateID {set;get;}
        public string StateName {set;get;}
        public string? StateCode {set;get;}
        public int CountryID {set;get;}
        public DateTime CreationDate {set;get;}
        public DateTime ModificationDate {set;get;}
    }

    public class LOC_StateDropDownModel
    {
        public int StateID {set;get;}
        public string StateName {set;get;}
    }
}
