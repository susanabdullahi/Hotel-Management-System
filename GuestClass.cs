using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Hotel_Management_System
{
    internal class GuestClass
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\suzan\Desktop\visual project\login.accdb;Persist Security Info=False;";

        public bool InsertGuest(string GuestId, string GuestFirstName, string GuestLastName, string GuestPhone, string GuestCity)
        {
            bool isInserted = false;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO Guest (GuestId, GuestFirstName, GuestLastName, GuestPhone, GuestCity) VALUES (@GuestId, @GuestFirstName, @GuestLastName, @GuestPhone, @GuestCity)";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GuestId", GuestId);
                        command.Parameters.AddWithValue("@GuestFirstName", GuestFirstName);
                        command.Parameters.AddWithValue("@GuestLastName", GuestLastName);
                        command.Parameters.AddWithValue("@GuestPhone", GuestPhone);
                        command.Parameters.AddWithValue("@GuestCity", GuestCity);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            isInserted = true;
                        }
                    }
                }
                catch (OleDbException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return isInserted;
        }

        public DataTable GetGuests()
        {
            DataTable table = new DataTable();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM Guest";
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, connection))
                    {
                        adapter.Fill(table);
                    }
                }
                catch (OleDbException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return table;
        }
        public bool EditGuest(string GuestId, string GuestFirstName, string GuestLastName, string GuestPhone, string GuestCity)
        {
            bool isUpdated = false;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string editQuery = "UPDATE Guest SET GuestFirstName=?, GuestLastName=?, GuestPhone=?, GuestCity=? WHERE GuestId=?";

                    using (OleDbCommand command = new OleDbCommand(editQuery, connection))
                    {
                        command.Parameters.AddWithValue("?", GuestFirstName);
                        command.Parameters.AddWithValue("?", GuestLastName);
                        command.Parameters.AddWithValue("?", GuestPhone);
                        command.Parameters.AddWithValue("?", GuestCity);
                        command.Parameters.AddWithValue("?", GuestId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            isUpdated = true;
                        }
                    }
                }
                catch (OleDbException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return isUpdated;
        }
        public bool RemoveGuest(string GuestId)
        {
            bool isRemoved = false;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM Guest WHERE GuestId = @GuestId";

                    using (OleDbCommand command = new OleDbCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@GuestId", GuestId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            isRemoved = true;
                        }
                    }
                }
                catch (OleDbException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return isRemoved;
        }
        

    }
}
