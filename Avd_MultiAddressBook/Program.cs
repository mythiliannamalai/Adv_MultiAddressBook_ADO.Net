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
        //UC-2 && 5 Add contact        
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
        //UC-3 Edit contact
        public static void Edit_Contact()
        {
            List<Contact> contacts = new List<Contact>();
            Contact contactDetails = new Contact();

            SqlConnection connection = new SqlConnection(connectionString);
            string spname = "dbo.Edit_Contact";
            using (connection)
            {
                SqlCommand sqlCommand = new SqlCommand(spname, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                Console.WriteLine("Edit Contact");
                Console.WriteLine("--------------");
                Console.WriteLine("First Name :");
                contactDetails.FirstName = Console.ReadLine();
                sqlCommand.Parameters.AddWithValue("@FirstName", contactDetails.FirstName);
                Console.WriteLine("Last Name :");
                contactDetails.LastName = Console.ReadLine();
                sqlCommand.Parameters.AddWithValue("@LastName", contactDetails.LastName);
                Console.WriteLine("Address :");
                contactDetails.Address = Console.ReadLine();
                sqlCommand.Parameters.AddWithValue("@Address", contactDetails.Address);
                Console.WriteLine("City :");
                contactDetails.City = Console.ReadLine();
                sqlCommand.Parameters.AddWithValue("@City", contactDetails.City);
                Console.WriteLine("State :");
                contactDetails.State = Console.ReadLine();
                sqlCommand.Parameters.AddWithValue("@State", contactDetails.State);
                Console.WriteLine("Zip code :");
                contactDetails.Zipcode = Console.ReadLine();
                sqlCommand.Parameters.AddWithValue("@Zipcode", contactDetails.Zipcode);
                Console.WriteLine("Phone Number :");
                contactDetails.PhoneNumber = Console.ReadLine();
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", contactDetails.PhoneNumber);
                Console.WriteLine("Email Id :");
                contactDetails.EmailId = Console.ReadLine();
                sqlCommand.Parameters.AddWithValue("@EmailId", contactDetails.EmailId);
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            contactDetails.FirstName = (string)reader["FirstName"];
                            contactDetails.LastName = (string)reader["LastName"];
                            contactDetails.Address = (string)reader["Address"];
                            contactDetails.City = (string)reader["City"];
                            contactDetails.State = (string)reader["State"];
                            contactDetails.Zipcode = (string)reader["Zipcode"];
                            contactDetails.PhoneNumber = (string)reader["PhoneNumber"];
                            contactDetails.EmailId = (string)reader["EmailId"];
                            contacts.Add(contactDetails);
                            Console.WriteLine(contactDetails.FirstName + "," + contactDetails.LastName + "," + contactDetails.Address + ","
                                + contactDetails.City + "," + contactDetails.State + "," + contactDetails.Zipcode, ","
                               + contactDetails.PhoneNumber + "," + contactDetails.EmailId);
                        }
                        connection.Close();
                    }
                }
            }
        }
        //UC-4 delete contact
        public static void Delete_Contact()
        {
            List<Contact> contacts = new List<Contact>();
            Contact contactDetails = new Contact();

            SqlConnection connection = new SqlConnection(connectionString);
            string spname = "dbo.Delete_Contact";
            using (connection)
            {
                SqlCommand sqlCommand = new SqlCommand(spname, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                Console.WriteLine("Delete Contact");
                Console.WriteLine("--------------");
                Console.WriteLine("First Name :");
                contactDetails.FirstName = Console.ReadLine();
                sqlCommand.Parameters.AddWithValue("@FirstName", contactDetails.FirstName);
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            contactDetails.FirstName = (string)reader["FirstName"];
                            contactDetails.LastName = (string)reader["LastName"];
                            contactDetails.Address = (string)reader["Address"];
                            contactDetails.City = (string)reader["City"];
                            contactDetails.State = (string)reader["State"];
                            contactDetails.Zipcode = (string)reader["Zipcode"];
                            contactDetails.PhoneNumber = (string)reader["PhoneNumber"];
                            contactDetails.EmailId = (string)reader["EmailId"];
                            contacts.Add(contactDetails);
                            Console.WriteLine(contactDetails.FirstName + "," + contactDetails.LastName + "," + contactDetails.Address + ","
                                + contactDetails.City + "," + contactDetails.State + "," + contactDetails.Zipcode, ","
                               + contactDetails.PhoneNumber + "," + contactDetails.EmailId);
                        }
                        connection.Close();
                    }
                }
            }
        }
        public static List<Contact> Create_MultiAddressBook()
        {
            List<Contact> contacts = new List<Contact>();
            Contact contactDetails = new Contact();

            SqlConnection connection = new SqlConnection(connectionString);
            string spname = "dbo.Create_MultiAddressBook";
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
                AddressBook.Create_MultiAddressBook();
                int val;
              do
              {
                Console.WriteLine("1.Add contact");
                Console.WriteLine("2.Edit contact");
                Console.WriteLine("3.Delete contact");
                Console.WriteLine("0.Exit");
                Console.WriteLine("Enter your choice");
                val = int.Parse(Console.ReadLine());

                switch (val)
                {
                    case 1:
                        AddressBook.Add_Contact();
                        break;
                    case 2:
                        AddressBook.Edit_Contact();
                        break;
                    case 3:
                        AddressBook.Delete_Contact();
                        break;
                    case 0:
                        Console.WriteLine("****EXIT****");
                        break;
                }

              } while (val != 0);
            }
        
    }
}