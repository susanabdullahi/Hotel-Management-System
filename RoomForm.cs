using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management_System
{
    public partial class RoomForm : Form
    {
        RoomClass room = new RoomClass();
        public RoomForm()
        {
            InitializeComponent();
        }

        private void RoomForm_Load(object sender, EventArgs e)
        {

            comboBox_roomType.DataSource = room.GetRoomType();
            comboBox_roomType.DisplayMember = "Label";
            comboBox_roomType.ValueMember = "CategoryId";


            GetRoomList();
            dataGridView_room.DefaultCellStyle.ForeColor = Color.Black;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            string no = textBox_id.Text;
            int type = Convert.ToInt32(comboBox_roomType.SelectedValue.ToString());
            string ph = textBox_phone.Text;
            string status = radioButton_free.Checked ? "Free" : "Busy";

            try
            {
                RoomClass room = new RoomClass(); // Assuming RoomClass is instantiated here

                if (room.AddRoom(no, type, ph, status))
                {
                    MessageBox.Show("Room Added Successfully", "Add Room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Call getRoomList() method to update room list display or perform related actions
                    // button_clean.PerformClick(); // If you have a clean/reset function
                }
                else
                {
                    MessageBox.Show("Room Not Added", "Add Room", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_clean_Click(object sender, EventArgs e)
        {
            textBox_id.Clear();
            comboBox_roomType.SelectedIndex = 0;
            textBox_phone.Clear();
        }
        private void GetRoomList()
        {
            dataGridView_room.DataSource = room.GetRoomList();
        }

        private void button_update_Click(object sender, EventArgs e)
        {

            int no = 0; // Default value in case of conversion failure
            int type = 0; // Default value in case of conversion failure
            string ph = textBox_phone.Text;
            string status = radioButton_free.Checked ? "Free" : "Busy";

            // Attempt to parse the room ID from textBox_id
            if (int.TryParse(textBox_id.Text, out int parsedNo))
            {
                no = parsedNo; // If successful, assign the parsed value to 'no'
            }
            else
            {
                MessageBox.Show("Invalid room ID entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method early if the conversion fails
            }

            // Attempt to parse the selected value from the comboBox_roomType
            if (comboBox_roomType.SelectedItem != null && int.TryParse(comboBox_roomType.SelectedValue.ToString(), out int selectedValue))
            {
                type = selectedValue; // If successful, assign the parsed value to 'type'
            }
            else
            {
                MessageBox.Show("Invalid room type selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method early if the conversion fails
            }

            try
            {
                RoomClass room = new RoomClass(); // Ensure RoomClass is instantiated correctly

                bool updateResult = room.EditRoom(no, type, ph, status);

                if (updateResult)
                {
                    MessageBox.Show("Room Updated Successfully", "Update Room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Perform actions like refreshing room list display or resetting form elements
                    // Example: getRoomList(); 
                    // Example: button_clean.PerformClick(); // If you have a clean/reset function
                }
                else
                {
                    MessageBox.Show("Room Not Updated", "Update Room", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating room: " + ex.Message, "Update Room", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
      


       
        private void button_delete_Click_1(object sender, EventArgs e)
        {
            string idText = textBox_id.Text;
            if (int.TryParse(idText, out int id))
            {
                try
                {
                    RoomClass roomInstance = new RoomClass();
                    bool deleteRoom = roomInstance.RemoveRoom(id);

                    if (deleteRoom)
                    {
                        MessageBox.Show("Room removed successfully", "Room Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Perform necessary actions after successful deletion
                    }
                    else
                    {
                        MessageBox.Show("Error - Room not Removed", "Error Deleting Room", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Invalid Room ID entered", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_room_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_room.CurrentRow != null && e.RowIndex >= 0)
            {
                textBox_id.Text = dataGridView_room.CurrentRow.Cells[0].Value?.ToString() ?? string.Empty;
                comboBox_roomType.SelectedValue = dataGridView_room.CurrentRow.Cells[1].Value?.ToString();

                // Check for null values before conversion
                string phoneValue = dataGridView_room.CurrentRow.Cells[2].Value?.ToString();
                if (!string.IsNullOrEmpty(phoneValue))
                {
                    textBox_phone.Text = phoneValue;
                }
                else
                {
                    textBox_phone.Text = string.Empty; // or set to default value
                }

                // Check for null values before accessing and parsing
                if (dataGridView_room.CurrentRow.Cells[3].Value != null)
                {
                    string rButton = dataGridView_room.CurrentRow.Cells[3].Value.ToString();
                    if (rButton.Equals("Free", StringComparison.OrdinalIgnoreCase)) // Use case-insensitive comparison
                    {
                        radioButton_free.Checked = true;
                    }
                    else
                    {
                        radioButton_busy.Checked = true;
                    }
                }
                else
                {
                    // Handle the case where the cell value is null
                    radioButton_free.Checked = false;
                    radioButton_busy.Checked = false;
                }
            }
        }

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.White;
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }



}
