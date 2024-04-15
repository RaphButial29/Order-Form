using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace RJB_2A
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LoadRecord();
            }
        }

        void LoadRecord()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\srcadmin\Documents\Visual Studio 2012\Projects\RJB 2A\RJB 2A\App_Data\Database1.mdf;Integrated Security=True");

            con.Open();
            SqlCommand cmd;

            cmd = new SqlCommand("Select * from Orders", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\srcadmin\Documents\Visual Studio 2012\Projects\RJB 2A\RJB 2A\App_Data\Database1.mdf;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO Orders(MyOrder,Price) VALUES (@MyOrder,@Price)", con);
            cmd.Parameters.AddWithValue("@MyOrder", txtMyOrder.Text);
            cmd.Parameters.AddWithValue("@Price", txtPrice.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            Label1.Text = "Save SuccessFully";
            LoadRecord();

        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\srcadmin\Documents\Visual Studio 2012\Projects\RJB 2A\RJB 2A\App_Data\Database1.mdf;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Orders Set Price = @Price WHERE MyOrder = @MyOrder", con);
            cmd.Parameters.AddWithValue("@MyOrder", txtMyOrder.Text);
            cmd.Parameters.AddWithValue("@Price", txtPrice.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            Label1.Text = "Update SuccessFully";
            LoadRecord();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\srcadmin\Documents\Visual Studio 2012\Projects\RJB 2A\RJB 2A\App_Data\Database1.mdf;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM Orders WHERE MyOrder = @MyOrder AND Price = @Price", con);
            cmd.Parameters.AddWithValue("@MyOrder", txtMyOrder.Text);
            cmd.Parameters.AddWithValue("@Price", txtPrice.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            Label1.Text = "Delete Successfully";
            LoadRecord();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string searchTerm = SearchBox.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                SearchRecords(searchTerm);
            }
            else
            {
                LoadRecord();
            }
        }

        void SearchRecords(string searchTerm)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\srcadmin\Documents\Visual Studio 2012\Projects\RJB 2A\RJB 2A\App_Data\Database1.mdf;Integrated Security=True"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM ORDERS WHERE MyOrder LIKE @SearchTerm OR Price LIKE @SearchTerm", con);
                cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                Label1.Text = "Item Not Found!";

            }

        }
    }
}