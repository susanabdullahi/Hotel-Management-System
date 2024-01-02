using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Hotel_Management_System
{
    public partial class ReservationForm : Form
    {
        RoomClass room = new RoomClass();
        ReservationClass1 reservation = new ReservationClass1();

        public ReservationForm()
        {
            InitializeComponent();
        }

        private void ReservationForm_Load(object sender, EventArgs e)
        {
            comboBox_roomType.DataSource = room.GetRoomType();
            comboBox_roomType.DisplayMember = "Label";
            comboBox_roomType.ValueMember = "CategoryId";


            int type = Convert.ToInt32(comboBox_roomType.SelectedValue.ToString());
            comboBox_roomNo.DataSource = reservation.roomByType(type);
            comboBox_roomNo.DisplayMember = "RoomId";
            comboBox_roomNo.ValueMember = "RoomId";

            dataGridView_reserv.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView_reserv.DataSource = reservation.getReserv();


            getReservTable();


        }


        public void getReservTable()
        {
            //dataGridView_reserv.DataSource = reservation.getReserv();
        }

        private void comboBox_roomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int type = Convert.ToInt32(comboBox_roomType.SelectedValue.ToString());
                comboBox_roomNo.DataSource = reservation.roomByType(type);
                comboBox_roomNo.DisplayMember = "RoomId";
                comboBox_roomNo.ValueMember = "RoomId";
            }
            catch (Exception)
            {
                // Handle exceptions accordingly
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            try
            {
                string guestId = textBox_guestId.Text;
                string roomNo = comboBox_roomNo.SelectedValue.ToString();
                DateTime dIn = dateTimePicker_dateIn.Value;
                DateTime dOut = dateTimePicker_dateOut.Value;

                if (dIn < DateTime.Today)
                {
                    MessageBox.Show("Reservation Date In must be Today Or After", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dOut < dIn)
                {
                    MessageBox.Show("Reservation Date Out must be same Date In and After", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (reservation.addReserv(guestId, roomNo, dIn, dOut) && reservation.setReservRoom(roomNo, "Busy"))
                    {
                        DataTable updatedReservations = reservation.getReserv();
                        dataGridView_reserv.DataSource = updatedReservations;
                        getReservTable();
                        MessageBox.Show("New Reservation added Successfully", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Reservation Not added Successfully", "Error Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Reservation Add Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox_roomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int type = Convert.ToInt32(comboBox_roomType.SelectedValue.ToString());
                comboBox_roomNo.DataSource = reservation.roomByType(type);
                comboBox_roomNo.DisplayMember = "RoomId";
                comboBox_roomNo.ValueMember = "RoomId";
            }
            catch (Exception)
            {
                //Nothing
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox__reservId.Text) && !string.IsNullOrEmpty(comboBox_roomNo.Text))
                {
                    int reserId;
                    if (int.TryParse(textBox__reservId.Text, out reserId))
                    {
                        string rno = comboBox_roomNo.Text;

                        if (reservation.removeReserv(reserId))
                        {
                            if (reservation.setReservRoom(rno, "Free"))
                            {
                                getReservTable();
                                MessageBox.Show("Delete Reservation Successfully", "Deleted Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Unable to update room status.", "Error Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Unable to delete reservation.", "Error Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Reservation ID.", "Error Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Reservation ID or Room Number is empty.", "Error Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_reserv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Fetching data from the DataGridView to populate fields
            textBox__reservId.Text = dataGridView_reserv.CurrentRow.Cells[0].Value.ToString();
            textBox_guestId.Text = dataGridView_reserv.CurrentRow.Cells[1].Value.ToString();

            string rno = dataGridView_reserv.CurrentRow.Cells[2].Value.ToString();
            comboBox_roomNo.Text = rno;
            dateTimePicker_dateIn.Text = dataGridView_reserv.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker_dateOut.Text = dataGridView_reserv.CurrentRow.Cells[4].Value.ToString();

            // Get the room type for the selected room number
            int roomType = reservation.typeByRoomNo(rno);

            // Loop through the comboBox_roomType items to find and select the matching room type
            for (int i = 0; i < comboBox_roomType.Items.Count; i++)
            {
                DataRowView drv = (DataRowView)comboBox_roomType.Items[i];
                if ((int)drv["CategoryId"] == roomType)
                {
                    comboBox_roomType.SelectedIndex = i;
                    break;
                }
            }
        }

        private void button_clean_Click(object sender, EventArgs e)
        {
            textBox__reservId.Clear();
            textBox_guestId.Clear();
            comboBox_roomType.SelectedValue = 1;
            dateTimePicker_dateIn.Value = DateTime.Now;
            dateTimePicker_dateOut.Value = DateTime.Now;
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                int reservId;
                if (!int.TryParse(textBox__reservId.Text, out reservId))
                {
                    MessageBox.Show("Invalid reservation ID format.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string guestId = textBox_guestId.Text;
                string roomNo = comboBox_roomNo.SelectedValue?.ToString(); // Using null-conditional operator '?'
                DateTime dIn = dateTimePicker_dateIn.Value;
                DateTime dOut = dateTimePicker_dateOut.Value;

                // Validate reservId
                if (reservId <= 0)
                {
                    MessageBox.Show("Invalid reservation ID.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate guestId length or format, assuming a maximum length of 50 characters
                if (guestId.Length > 50)
                {
                    MessageBox.Show("Guest ID exceeds maximum length.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Assuming room numbers are alphanumeric and have a specific length
                const int expectedRoomNoLength = 5; // Update this with your expected room number length
                if (roomNo.Length != expectedRoomNoLength || !IsRoomNumberValid(roomNo))
                {
                    MessageBox.Show("Invalid room number.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate date inputs
                if (dIn < DateTime.Today)
                {
                    MessageBox.Show("Reservation Date In must be Today Or After", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (dOut < dIn)
                {
                    MessageBox.Show("Reservation Date Out must be same Date In and After", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Call editReserv if all validations pass
                if (reservation.editReserv(reservId, guestId, roomNo, dIn, dOut) && reservation.setReservRoom(roomNo, "Busy"))
                {
                    getReservTable();
                    MessageBox.Show("Reservation edited successfully", "Update Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Reservation not edited successfully", "Error Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Reservation Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsRoomNumberValid(string roomNumber)
        {
            // Implement your room number validation logic here
            // For example, check if the room number follows a specific pattern/format
            // Return true if the room number is valid, otherwise return false
            return true; // Replace this with your validation logic
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.White;
        }

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }
    }
}
