using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.econtactClasses
{
    class contactClass
    {
        public int contactID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string contactNo{ get; set; }
        public string address { get; set; }
        public string gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //selecting data from db
        public DataTable Select()
        {
            //DB connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //writing sql query
                string sql = "SELECT * FROM tbl_contact";

                // creating command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                //creating SQL dataadapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }


        //Inseting Data into Database
        public bool Insert(contactClass c)
        {
            //creating a default return type and setting its value to false
            bool isSuccess = false;

            // connect dn
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "INSERT INTO tbl_contact (firstName, lastName, contactNo, address, gender) VALUES(@firstName, @lastName, @contactNo, @address, @gender)";
                // creating sql command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create parameter in add_data
                cmd.Parameters.AddWithValue("@firstName", c.firstName);
                cmd.Parameters.AddWithValue("@lastName", c.lastName);
                cmd.Parameters.AddWithValue("@contactNo", c.contactNo);
                cmd.Parameters.AddWithValue("@address", c.address);
                cmd.Parameters.AddWithValue("@gender", c.gender);

                //connection open here

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if the query runs successfully then the values of rows will be greater than zero else its values will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }


        //method to update data in db from our app
        public bool Update(contactClass c)
        {
            //a defaulf return type with false
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql to update data
                string sql = "UPDATE tbl_contact SET firstName=@firstName, lastName=@lastName, contactNo=@contactNo, address=@address, gender=@gender WHERE contactID=@contactID";

                //creating sql command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //ccreating parameters
                cmd.Parameters.AddWithValue("@firstName", c.firstName);
                cmd.Parameters.AddWithValue("@lastName", c.lastName);
                cmd.Parameters.AddWithValue("@contactNo", c.contactNo);
                cmd.Parameters.AddWithValue("@address", c.address);
                cmd.Parameters.AddWithValue("@gender", c.gender);
                cmd.Parameters.AddWithValue("@contactID", c.contactID);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //if the query runs successfully then the values of rows will be greater than zero else its values will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        //Delete from db
        public bool Delete(contactClass c)
        {
            //a defaulf return type with false
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql to delete data
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";
                //creating sql command
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@contactID", c.contactID);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //if the query runs successfully then the values of rows will be greater than zero else its values will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
    }
}
