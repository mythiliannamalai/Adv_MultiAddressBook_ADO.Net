using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
namespace Avd_MultiAddressBook
{
    public class AddressBook
    {
        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = Multi_Address_Book; Integrated Security=SSPI;";
        static SqlConnection connection = new SqlConnection(connectionString);
        // Check connection
        public static void EstablishConnection()
        {
            if (connection != null && connection.State.Equals(ConnectionState.Closed))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception)
                {
                    throw new ContactException(ContactException.ExceptionType.Connection_Failed, "Connection failed..");
                }
            }
            if (connection != null && connection.State.Equals(ConnectionState.Open))
            {
                try
                {
                    connection.Close();
                }
                catch (Exception)
                {
                    throw new ContactException(ContactException.ExceptionType.Connection_Failed, "Connection failed..");
                }
            }
        }
        //UC-1 Create contact
        public static List<Contact> CreateContact()
        {
            List<Contact> contacts = new List<Contact>();
            Contact contactDetails = new Contact();
            SqlConnection connection = new SqlConnection(connectionString);
            string spname = "dbo.Create_Contact";
            using (connection)
            {
                SqlCommand sqlCommand = new SqlCommand(spname, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                connection.Close();
                return contacts;
            }
        }
        static void Main(string[]args)
        {
            AddressBook.EstablishConnection();
            AddressBook.CreateContact();
        }
    }
}