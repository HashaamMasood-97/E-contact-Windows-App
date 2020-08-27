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
using WindowsFormsApp1.econtactClasses;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        contactClass c = new contactClass();

    

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //add
        private void button1_Click(object sender, EventArgs e)
        {
            //get the value from input field
            c.firstName = textBox5.Text;
            c.lastName = textBox4.Text;
            c.contactNo = textBox3.Text;
            c.address = textBox2.Text;
            c.gender = comboBox1.Text;

            bool success = c.Insert(c);
            if (success == true)
            {
                MessageBox.Show("New Contact Successfully Inserted");
                //clear method
                clear();
            }
            else
            {
                MessageBox.Show("Failed");
            }

            //Load data on grid
            DataTable dt = c.Select();
            dataGridView1.DataSource = dt;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //Load data on grid
            DataTable dt = c.Select();
            dataGridView1.DataSource = dt;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        //clear data from fields
        public void clear()
        {
            textBox5.Text ="";
            textBox4.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            textBox1.Text = "";
        }


        //update button
        private void button2_Click(object sender, EventArgs e)
        {
            c.firstName = textBox5.Text;
            c.lastName = textBox4.Text;
            c.contactNo = textBox3.Text;
            c.address = textBox2.Text;
            c.gender = comboBox1.Text;
            c.contactID = int.Parse(textBox1.Text);

            bool success = c.Update(c);
            if (success == true)
            {
                MessageBox.Show("Contact Updated Successfully");
                //Load data on grid
                DataTable dt = c.Select();
                dataGridView1.DataSource = dt;

                //clear method
                clear();
            }
            else
            {
                MessageBox.Show("Failed");
            }

        }


        //onclick row header and gets the value into textboxes
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get data from text fields 
            // identify the row on which the mouse is clicked
            int rowIndex = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            textBox5.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[rowIndex].Cells[5].Value.ToString();
        }


        //clear button
        private void button4_Click(object sender, EventArgs e)
        {
            //call clear method here on button clin
            clear();
        }


        //delete button
        private void button3_Click(object sender, EventArgs e)
        {
            c.contactID = Convert.ToInt32(textBox1.Text);
            bool success = c.Delete(c);
            if (success == true)
            {
                MessageBox.Show("Contact Deleted Successfully");
              
                //Load data on grid
                DataTable dt = c.Select();
                dataGridView1.DataSource = dt;

                //clear method
                clear();


            }
            else
            {
                MessageBox.Show("Failed");
            }

        }



        //to establish db connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        //search textbox
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //get the value from text box
            string keyword = textBox7.Text;
            SqlConnection conn = new SqlConnection(myconnstrng);
            SqlDataAdapter sda= new SqlDataAdapter("SELECT * FROM tbl_contact WHERE firstName LIKE '%"+keyword+ "%' OR lastName LIKE '%" + keyword + "%' OR contactNo LIKE '%" + keyword + "%' OR address LIKE '%" + keyword +"%'", conn );
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
