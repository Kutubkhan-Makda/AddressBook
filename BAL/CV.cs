using Microsoft.AspNetCore.Http;

namespace AddressBook.BAL
{
    public static class CV
    {
        public static IHttpContextAccessor _HttpContextAccessor;

        static CV()
        {
            _HttpContextAccessor = new HttpContextAccessor();
        }

        public static string? UserName()
        {
            string? UserName = null;

            if(_HttpContextAccessor.HttpContext.Session.GetString("UserName") != null)
            {
                UserName = _HttpContextAccessor.HttpContext.Session.GetString("UserName").ToString();
            }
            return UserName;
        }

        public static int? UserID()
        {
            int? UserID = null;

            if(_HttpContextAccessor.HttpContext.Session.GetString("UserID") != null)
            {
                UserID = Convert.ToInt32(_HttpContextAccessor.HttpContext.Session.GetString("UserID"));
            }
            return UserID;
        }
    }
}