using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Abeczar_Inventory_System
{
    public partial class Form_Login : Form
    {
        //SQLConnections
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Abeczar Inventory System 1.0\Abeczar Inventory System\database_Abeczar.mdf"";Integrated Security=True;Connect Timeout=30");
        SqlCommand sqlCommand;
        SqlDataAdapter sqlDataAdapter;
        SqlDataReader sqlDataReader;
        DataTable sqlDataTable;

        public Form_Login()
        {
            InitializeComponent();
            timer1.Start();
            checkBox_Password.CheckedChanged += new EventHandler(checkBox_Password_CheckedChanged);
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            String Name, Username, Password;
            Username = text_Username.Text;
            Password = text_Password.Text;

            try
            {
                String query = "SELECT * FROM Users WHERE Username = '" + text_Username.Text + "'AND Password = '" + text_Password.Text + "'";
                sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
                sqlDataTable = new DataTable();
                sqlDataAdapter.Fill(sqlDataTable);

                if (sqlDataTable.Rows.Count > 0)
                {
                    
                    //directs to the Main Menu Form
                    Form_MainMenu MainMenu = new Form_MainMenu();
                    MainMenu.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Invalid login details", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    Clear();
                    text_Username.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                this.Show();
            }
            else
            {
                Application.Exit();
            }
        }

        private void checkBox_Password_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Password.Checked)
            {
                text_Password.UseSystemPasswordChar = false;
            }
            else
            {
                text_Password.UseSystemPasswordChar = true;
            }
        }

        public void Clear()
        {
            text_Username.Clear();
            text_Password.Clear();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label_Time.Text = DateTime.Now.ToShortTimeString();
            label_Date.Text = DateTime.Now.ToShortDateString();
        }

        
    }
}
