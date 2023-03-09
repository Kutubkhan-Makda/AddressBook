using System.Data;
using System.Reflection;

namespace Multi_AddressBook.DAL
{
    public class DALConnection
    {
        public static string SQL_Connection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("SQL_AddressBook");
    }
}