using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace final
{
    public partial class Registration : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jhayr\Documents\LoginData.mdf;Integrated Security=True;Connect Timeout=30");

        public Registration()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            hide_btn lForm = new hide_btn();
            lForm.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void register_Click(object sender, EventArgs e)
        {

            if (reg_email.Text == "" || reg_Uname.Text == "" || reg_pass.Text == "" || reg_name.Text == "")
            {
                MessageBox.Show("Please Fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                if (string.IsNullOrWhiteSpace(reg_email.Text) || string.IsNullOrWhiteSpace(reg_Uname.Text) || string.IsNullOrWhiteSpace(reg_pass.Text) || string.IsNullOrWhiteSpace(reg_bday.Text) || string.IsNullOrWhiteSpace(reg_name.Text))
                {
                    MessageBox.Show("Please fill in all required fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();
                        String checkUsername = "SELECT * FROM admin WHERE username = @username";
                        using (SqlCommand checkUser = new SqlCommand(checkUsername, connect))
                        {
                            checkUser.Parameters.AddWithValue("@username", reg_Uname.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(checkUser);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count >= 1)
                            {
                                MessageBox.Show(reg_Uname.Text + " is already exist", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO admin (email, username, password, date_created, birthdate)" +
                                                    "VALUES (@email, @username, @password, @date, @birthdate)";

                                string currentDate = DateTime.Today.ToString("yyyy-MM-dd");

                                using (SqlCommand cmd = new SqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@email", reg_email.Text.Trim());
                                    cmd.Parameters.AddWithValue("@username", reg_Uname.Text.Trim());
                                    cmd.Parameters.AddWithValue("@password", reg_pass.Text.Trim());
                                    cmd.Parameters.AddWithValue("@date", currentDate);
                                    cmd.Parameters.AddWithValue("@birthdate", reg_bday.Text.Trim());

                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Register Successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    hide_btn lForm = new hide_btn();
                                    lForm.Show();
                                    this.Hide();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR connecting Database: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }

        }

            

        private void show_Click(object sender, EventArgs e)
        {
            if (reg_pass.PasswordChar == '*')
            {
                button1.BringToFront();
                reg_pass.PasswordChar = '\0';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (reg_pass.PasswordChar == '\0')
            {
                show.BringToFront();
                reg_pass.PasswordChar = '*';
            }
        }
    }
}
