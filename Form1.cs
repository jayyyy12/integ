using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;


namespace final
{
    public partial class hide_btn : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jhayr\Documents\LoginData.mdf;Integrated Security=True;Connect Timeout=30");
        public hide_btn()
        {
            InitializeComponent();
        }

        private void LogINBTN_Click(object sender, EventArgs e)
        {
            string user = " ";
            string pass = "";                                                                                                                                                                                                                                                                                                                                                                                       
            string conString = ConfigurationManager.ConnectionStrings["final.Properties.Settings.ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from usertbl where username= " + login_Uname.Text.ToString() + " and password= "+ login_pass.Text.ToString(), conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read()) 
            {
                user= reader.GetString(0);
                user = reader.GetString(1);
            }
            MessageBox .Show(user);
                
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration rForm = new Registration();
            rForm.Show();
            this.Hide();
        }

        private void login_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void show_Click(object sender, EventArgs e)
        {
            if (login_pass.PasswordChar == '*')
            {
                button1.BringToFront();
                login_pass.PasswordChar = '\0';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (login_pass.PasswordChar == '\0')
            {
                show.BringToFront();
                login_pass.PasswordChar = '*';
            }
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            if (login_Uname.Text == "" || login_pass.Text == "")
            {
                MessageBox.Show(" PLease Fill  All Blank Fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open) 
                {
                    try
                    {
                        connect.Open();
                        String selectData = "SELECT * FROM admin WHERE username = @username AND password = @password";
                        using (SqlCommand cmd = new SqlCommand(selectData, connect  )) 
                        {
                            cmd.Parameters.AddWithValue("@username", login_Uname.Text);
                            cmd.Parameters.AddWithValue("@password", login_pass.Text);
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count >= 1)
                            {
                                MessageBox.Show(" Logged In Successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                CsHome Home= new CsHome();
                                Home.Show();
                                this.Hide();
                            }
                            else 
                            {
                                MessageBox.Show("Incorrect: Username/Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                                
                        }   
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Connecting: "+ ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);     
                    }
                    finally {
                        connect.Close();
                    }
                }
            }
            
        }
    }
}
