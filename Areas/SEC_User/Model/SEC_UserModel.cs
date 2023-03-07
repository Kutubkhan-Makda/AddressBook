using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Areas.Models
{
    public class SEC_UserModel
    {
        [Required]
        public int? UserID { get; set; }
        [Required(ErrorMessage = "User Name is Required")]
        public string UserName {get;set;}
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}