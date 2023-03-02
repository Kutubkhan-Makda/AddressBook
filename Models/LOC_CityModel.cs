namespace AddressBook.Models
{
    public class LOC_CityModel
    {
        public int? CityID { get; set; }
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }
    }
    public class LOC_CityDropDownModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
    }
}
