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
        //UC-2 Add contact        
        public static void Add_Contact()
        {
            List<Contact> contacts = new List<Contact>();
            Contact contactDetails = new Contact();
            Console.WriteLine("First Name :");
            contactDetails.FirstName = Console.ReadLine();
            Console.WriteLine("Last Name :");
            contactDetails.LastName = Console.ReadLine();
            Console.WriteLine("Address :");
            contactDetails.Address = Console.ReadLine();
            Console.WriteLine("City :");
            contactDetails.City = Console.ReadLine();
            Console.WriteLine("State :");
            contactDetails.State = Console.ReadLine();
            Console.WriteLine("Zip code :");
            contactDetails.Zipcode = Console.ReadLine();
            Console.WriteLine("Phone Number :");
            contactDetails.PhoneNumber = Console.ReadLine();
            Console.WriteLine("Email Id :");
            contactDetails.EmailId = Console.ReadLine();
            SqlConnection connection = new SqlConnection(connectionString);
            string spname = "dbo.Add_Contact";
            using (connection)
            {
                SqlCommand sqlCommand = new SqlCommand(spname, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@FirstName", contactDetails.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", contactDetails.LastName);
                sqlCommand.Parameters.AddWithValue("@Address", contactDetails.Address);
                sqlCommand.Parameters.AddWithValue("@City", contactDetails.City);
                sqlCommand.Parameters.AddWithValue("@State", contactDetails.State);
                sqlCommand.Parameters.AddWithValue("@Zipcode", contactDetails.Zipcode);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", contactDetails.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@EmailId", contactDetails.EmailId);
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                Console.WriteLine(contactDetails.FirstName + "," + contactDetails.LastName + "," + contactDetails.Address + ","
                    + contactDetails.City + "," + contactDetails.State + "," + contactDetails.Zipcode, ","
                    + contactDetails.PhoneNumber + "," + contactDetails.EmailId);
                contacts.Add(contactDetails);
                connection.Close();
            }
        }
        static void Main(string[]args)
        {
            AddressBook.EstablishConnection();
            AddressBook.CreateContact();
            AddressBook.Add_Contact();
        }
    }
}