using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Hotel_Management_System
{
    public partial class GuestForm : Form
    {
        GuestClass guest = new GuestClass();

        public GuestForm()
        {
            InitializeComponent();
        }

        private void button_clean_Click(object sender, EventArgs e)
        {
            textBox_id.Clear();
            textBox_fName.Clear();
            textBox_lName.Clear();
            textBox_phone.Clear();
            textBox_city.Clear();
        }



        // If you don't need these event handlers, you can remove them
        private void label6_Click(object sender, EventArgs e)
        {
            // Code for label6 click event
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Code for flowLayoutPanel1 paint event
        }

        // Make sure the DataGridView control is named dataGridView1 in your form design
        private void DisplayGuests()
        {
            dataGridView_guest.DataSource = guest.GetGuests();
        }

        private void GuestForm_Load(object sender, EventArgs e)
        {
            dataGridView_guest.DefaultCellStyle.ForeColor = Color.Black;
            DisplayGuests();
        }

        private void button_save_Click_1(object sender, EventArgs e)
        {
            if (textBox_id.Text == "" || textBox_fName.Text == "" || textBox_phone.Text == "")
            {
                MessageBox.Show("Required Field - Id no, first name and phone no:", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string GuestId = textBox_id.Text;
                    string GuestFirstName = textBox_fName.Text;
                    string GuestLastName = textBox_lName.Text;
                    string GuestPhone = textBox_phone.Text;
                    string GuestCity = textBox_city.Text;

                    bool insertGuest = guest.InsertGuest(GuestId, GuestFirstName, GuestLastName, GuestPhone, GuestCity);
                    if (insertGuest)
                    {
                        MessageBox.Show("New guest saved successfully", "Guest Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button_clean.PerformClick(); // Clears all textboxes
                    }
                    else
                    {
                        MessageBox.Show("ERROR - guest not inserted", "Error Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }



        private void button_update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_id.Text))
            {
                MessageBox.Show("Please enter the Guest ID", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (string.IsNullOrWhiteSpace(textBox_fName.Text))
            {
                MessageBox.Show("Please enter the Guest's First Name", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (string.IsNullOrWhiteSpace(textBox_phone.Text))
            {
                MessageBox.Show("Please enter the Guest's Phone Number", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string GuestId = textBox_id.Text;
                    string GuestFirstName = textBox_fName.Text;
                    string GuestLastName = textBox_lName.Text;
                    string GuestPhone = textBox_phone.Text;
                    string GuestCity = textBox_city.Text;

                    bool editGuest = guest.EditGuest(GuestId, GuestFirstName, GuestLastName, GuestPhone, GuestCity);
                    if (editGuest)
                    {
                        MessageBox.Show("Guest data updated successfully", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DisplayGuests(); // Assuming DisplayGuests() method shows the updated data in the DataGridView
                        button_clean.PerformClick(); // Clears all textboxes
                    }
                    else
                    {
                        MessageBox.Show("ERROR - guest data not updated", "Error Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (OleDbException ex)
                {
                    Console.WriteLine("OleDb Error: " + ex.Message);
                    MessageBox.Show("Database Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General Error: " + ex.Message);
                    MessageBox.Show("General Error: " + ex.Message);
                }
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_id.Text == "")
            {
                MessageBox.Show("Required Field - Id no", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string GuestId = textBox_id.Text;
                    bool deleteGuest = guest.RemoveGuest(GuestId);

                    if (deleteGuest)
                    {
                        MessageBox.Show("Guest data removed successfully", "Guest Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Refresh the table or perform any other action to update the display
                        DisplayGuests(); // Assuming DisplayGuests() updates the DataGridView
                                         // Clear all text after the delete action
                        button_clean.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("ERROR - Guest not removed", "Error delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button_clean_Click_1(object sender, EventArgs e)
        {
            textBox_id.Clear();
            textBox_fName.Clear();
            textBox_lName.Clear();
            textBox_phone.Clear();
            textBox_city.Clear();
        }



        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void label_exit_MouseLeave_1(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.White;
        }
        private void dataGridView_guest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView_guest_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            textBox_id.Text = dataGridView_guest.CurrentRow.Cells[0].Value.ToString();
            textBox_fName.Text = dataGridView_guest.CurrentRow.Cells[1].Value.ToString();
            textBox_lName.Text = dataGridView_guest.CurrentRow.Cells[2].Value.ToString();
            textBox_phone.Text = dataGridView_guest.CurrentRow.Cells[3].Value.ToString();
            textBox_city.Text = dataGridView_guest.CurrentRow.Cells[4].Value.ToString();
        }

       

          private float r1, r2; // Declare these variables at the form level to retain values

        private void button1_Click(object sender, EventArgs e)
        {
            if (!float.TryParse(textBox1.Text, out r1) || !float.TryParse(textBox2.Text, out r2))
            {
                MessageBox.Show("Please enter valid numeric values in textboxes.");
                return;
            }

            // Trigger a repaint of the form to update the pie chart
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw the background
            base.OnPaintBackground(e);

            // Your calculations and drawing operations here based on r1 and r2
            float total = r1 + r2;
            float pr1 = (r1 / total) * 360;
            float pr2 = (r2 / total) * 360;

            Pen p = new Pen(Color.Black);
            p.Width = 4;

            Rectangle rec = new Rectangle(210, 70, 150, 150); // Adjust the position and size as needed
            Brush b1 = new SolidBrush(Color.Turquoise);
            Brush b2 = new SolidBrush(Color.HotPink);

            // Draw pie chart directly on the form's graphics
            e.Graphics.DrawPie(p, rec, 0, pr1);
            e.Graphics.FillPie(b1, rec, 0, pr1);
            e.Graphics.DrawPie(p, rec, pr1, pr2);
            e.Graphics.FillPie(b2, rec, pr1, pr2);
        }
    


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button1_Click(sender, e);

        }
    }

}
