using System;
using System.Data;
using System.Data.OleDb;

namespace Hotel_Management_System
{
    internal class ReservationClass1
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\suzan\Desktop\visual project\login.accdb;Persist Security Info=False";
        private OleDbConnection connection;

        public ReservationClass1()
        {
            connection = new OleDbConnection(connectionString);
        }

        public DataTable roomByType(int type)
        {
            string selectQuery = "SELECT * FROM Room WHERE RoomType=@type AND RoomStatus ='Free'";
            OleDbCommand command = new OleDbCommand(selectQuery, connection);
            command.Parameters.AddWithValue("@type", type);
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public int typeByRoomNo(string rno)
        {
            string selectQuery = "SELECT RoomType FROM Room WHERE RoomId=@rno";
            OleDbCommand command = new OleDbCommand(selectQuery, connection);
            command.Parameters.AddWithValue("@rno", rno);
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                return Convert.ToInt32(table.Rows[0]["RoomType"]);
            }
            return -1; // Return a default value or handle appropriately if no data found
        }

        public DataTable getReserv()
        {
            string selectQuery = "SELECT * FROM Reservation";
            OleDbCommand command = new OleDbCommand(selectQuery, connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public bool setReservRoom(string rno, string sts)
        {
            string updateQuery = "UPDATE Room SET RoomStatus=@sts WHERE RoomId=@rno";
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand(updateQuery, connection);

            command.Parameters.AddWithValue("@sts", sts);
            command.Parameters.AddWithValue("@rno", rno);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (OleDbException ex)
            {
                Console.WriteLine("OleDbException in setReservRoom: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }


        public bool addReserv(string guestId, string roomNo, DateTime dateIn, DateTime dateOut)
        {
            string insertQuery = "INSERT INTO `Reservation`(`GuestId`, `RoomNo`, `DateIn`, `DateOut`) VALUES (@Gid,@Rno,@Din,@Dout)";
            OleDbCommand command = new OleDbCommand(insertQuery, connection);

            command.Parameters.AddWithValue("@Gid", guestId);
            command.Parameters.AddWithValue("@Rno", roomNo);
            command.Parameters.AddWithValue("@Din", dateIn.ToString("MM/dd/yyyy ")); // Format the date
            command.Parameters.AddWithValue("@Dout", dateOut.ToString("MM/dd/yyyy ")); // Format the date

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
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


        public DataTable getRoomsByTypeAndStatus(int type, string status)
        {
            string selectQuery = "SELECT * FROM Room WHERE RoomType=@type AND RoomStatus=@status";
            OleDbCommand command = new OleDbCommand(selectQuery, connection);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@status", status);
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public bool removeReserv(int id)
        {
            try
            {
                string deleteQuery = "DELETE FROM Reservation WHERE ReservId = @id";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (OleDbException ex)
            {
                // Log or display the specific OleDbException message
                Console.WriteLine("OleDbException: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                // Log or display any other general exception
                Console.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }
        public bool editReserv(int reserId, string guestId, string roomNo, DateTime dateIn, DateTime dateOut)
        {
            string updateQuery = "UPDATE Reservation SET GuestId=@Gid, RoomNo=@Rno, DateIn=@Din, DateOut=@Dout WHERE ReservId=@rid";
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand(updateQuery, connection);

            command.Parameters.AddWithValue("@rid", reserId);
            command.Parameters.AddWithValue("@Gid", guestId);
            command.Parameters.AddWithValue("@Rno", roomNo);
            command.Parameters.AddWithValue("@Din", dateIn);
            command.Parameters.AddWithValue("@Dout", dateOut);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
            catch (OleDbException ex)
            {
                Console.WriteLine("OleDbException in editReserv: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }


    }
}
