using System;
using System.Data;
using System.Data.OleDb;

namespace Hotel_Management_System
{
    internal class RoomClass
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\suzan\Desktop\visual project\login.accdb;Persist Security Info=False;";

        public DataTable GetRoomType()
        {
            DataTable table = new DataTable();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM category"; // Adjust 'category' to your table name in Access

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, connection))
                    {
                        adapter.Fill(table);
                    }
                }
                catch (OleDbException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    // Handle exceptions according to your application's requirements
                }
            }

            return table;
        }
        public bool AddRoom(string no, int type, string phone, string status)
        {
            string insertQuery = "INSERT INTO room (RoomId, RoomType, RoomPhone, RoomStatus) VALUES (@no, @type, @ph, @sts)";
            OleDbConnection connection = new OleDbConnection(connectionString); // Your connection string here
            OleDbCommand command = new OleDbCommand(insertQuery, connection);

            command.Parameters.AddWithValue("@no", no);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@ph", phone);
            command.Parameters.AddWithValue("@sts", status);

            try
            {
                connection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (OleDbException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Handle exceptions according to your application's requirements
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        public DataTable GetRoomList()
        {
            string selectQuery = "SELECT * FROM Room";
            OleDbConnection connection = new OleDbConnection(connectionString); // Your connection string here
            OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, connection);
            DataTable table = new DataTable();

            try
            {
                connection.Open();
                adapter.Fill(table);
            }
            catch (OleDbException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Handle exceptions according to your application's requirements
            }
            finally
            {
                connection.Close();
            }

            return table;
        }
        public bool EditRoom(int roomId, int type, string phone, string status)
        {
            string editQuery = "UPDATE Room SET RoomType = @type, RoomPhone = @phone, RoomStatus = @status WHERE RoomId = @roomId";
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand(editQuery, connection);

            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@status", status);
            command.Parameters.AddWithValue("@roomId", roomId);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
            catch (OleDbException ex)
            {
                Console.WriteLine("OleDbException in EditRoom: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool RemoveRoom(int id)
        {
            string deleteQuery = "DELETE FROM room WHERE RoomId = @id";
            OleDbConnection connection = new OleDbConnection(connectionString); // Your connection string here
            OleDbCommand command = new OleDbCommand(deleteQuery, connection);

            command.Parameters.AddWithValue("@id", id);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected == 1; // Check if exactly one row was affected (deleted)
            }
            catch (OleDbException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }


    }
}
