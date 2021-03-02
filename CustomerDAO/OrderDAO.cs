using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ProductOrder.Model;
namespace ProductOrder.DAL
{
    public class OrderDAO
    {
        SqlConnection con = new SqlConnection(@"...........");
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        DataTable dt = null;
        string Query = null;
        public bool AddtoCart(Order order)
        {
            try
            {
                Query = "Insert into ShoppingCart values(@OrderId,@OrderName,@OrderDate)";
                cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@OrderId", order.Order_Id);
                cmd.Parameters.AddWithValue("@OrderName", order.Order_Name);
                cmd.Parameters.AddWithValue("@OrderDate", order.Order_Date);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        public bool PlaceOrder(Order order)
        {
            try
            {
                Query = "Insert into Orders values(@OrderId,@OrderName,@OrderDate)";
                cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@OrderId", order.Order_Id);
                cmd.Parameters.AddWithValue("@OrderName", order.Order_Name);
                cmd.Parameters.AddWithValue("@OrderDate", order.Order_Date);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataRow SearchOrderById(int Order_Id)
        {
            try
            {
                Query = "Select * from Orders where Order_Id=" + Order_Id;
                da = new SqlDataAdapter(Query, con);
                dt = new DataTable("Orders");
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                    return dt.Rows[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
         public void ConfirmOrder(int OrderId, bool paymentStatus)
        {
            try
            {
                if (paymentStatus == true)
                {
                    Console.WriteLine("\nORDER CONFIRMED");
                    TrackShipping(OrderId);

                }
                else
                {
                    Console.WriteLine("\nPAYMENT INCOMPLETE");
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void TrackOrder(int OrderId)
        {
            try
            {
                Qry = "SELECT ORDERSTATUS FROM ORDERDETAILS WHERE ORDERID=" + OrderId.ToString();
                con.Open();
                cmd = new SqlCommand(Qry, con);
                drr = cmd.ExecuteReader();
                con.Close();
                while (drr.HasRows)
                {
                    drr.Read();
                    Console.WriteLine("Order Id:{0} \t OrderStatus Status:{1}", drr["OrderId"], drr["OderStatus"]);
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void TrackShipping(int OrderId)
        {
            try
            {

                Qry = "SELECT * FROM SHIPPINGDETAILS WHERE ORDERID=" + OrderId.ToString();
                con.Open();
                cmd = new SqlCommand(Qry, con);
                drr = cmd.ExecuteReader();
                while (drr.HasRows)
                {
                    drr.Read();
                    Console.WriteLine("Order Id:{0} \t Shipping Status:{1}", drr["OrderId"], drr["ShippingStatus"]);
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
