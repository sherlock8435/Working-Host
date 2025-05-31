using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class OrderDB : DBFunctions
    {
        Order o = null;
        private OrderList list = new OrderList();
        DBFunctions dbf = new DBFunctions();
        public OrderDB() : base() { }

        private Order CreateModel(Order o)
        {
            o.ItemId = (int)reader["ItemId"];
            o.Uemail = reader["Uemail"].ToString();
            o.VisaNumber = reader["VisaNumber"].ToString();
            o.Qnty = (int)reader["Qnty"];
            o.Price = (int)reader["Price"];
            o.OrderDate = reader["OrderDate"].ToString();
            o.OrderStatus = reader["OrderStatus"].ToString();

            return o;
        }

        public OrderList SelectAllOrder(string uEmail)
        {
            string sqlStr = "SELECT * FROM Ordertbl  where  Uemail = '" + uEmail + "'";
            return SelectOrders(sqlStr);
        }
        public OrderList SelectUserOrder(string uEmail)
        {
            string sqlStr = "SELECT * FROM Ordertbl  where  Uemail = '" + uEmail + "'" +
                " and orderStatus='order'";
            return SelectOrders(sqlStr);
        }

        private OrderList SelectOrders(string sqlStr)   // select in General
        {
            OrderList list = new OrderList();
            try
            {
                cmd = GenerateOleDBCommand(sqlStr, "DB.accdb");
                conObj.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    o = new Order();
                    list.Add(CreateModel(o));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (this.conObj.State == System.Data.ConnectionState.Open)
                    this.conObj.Close();
            }
            return list;
        }
        public int TotalOrderToPay(string uEmail)
        {
            int n = 0;
            string sumSql = string.Format("select sum (Price*Qnty) from Ordertbl   " +
                "where   OrderStatus ='order' and  Uemail = '" + uEmail + "'");

            System.Data.DataTable dt = dbf.Select(sumSql, "DB.accdb");
            if (dt != null)
                if (dt.Rows.Count > 0 &&  
                    dt.Rows[0][0].ToString() != null &&
                    dt.Rows[0][0].ToString().Length > 0)
                    n = int.Parse(dt.Rows[0][0].ToString());
            return n;
        }
        public int AddOrder(Order order)
        {
            string insertSql = string.Format("insert into Ordertbl " +
                " (ItemId,Uemail,VisaNumber,Qnty,Price,OrderDate,OrderStatus) " +
                "values({0},'{1}','{2}',{3},{4},'{5}','{6}')",
                order.ItemId, order.Uemail, order.VisaNumber, order.Qnty, order.Price, order.OrderDate, order.OrderStatus);
            return dbf.ChangeTable(insertSql, "DB.accdb");
        }
        public int UpdateOrder(Order order)
        {
            string updateSql = string.Format("update Ordertbl SET  VisaNumber='" + order.VisaNumber + "', " +
                "[orderStatus]='paid'  where OrderDate='" + order.OrderDate + "' " +
                "and  Uemail='" + order.Uemail + "' and orderStatus='order'");
            return dbf.ChangeTable(updateSql, "DB.accdb");
        }
        public int DeleteOrder(Order order)
        {
            string delSql = string.Format("Delete from Ordertbl  where ItemId=" + order.ItemId +" and Uemail='"+order.Uemail+"' and OrderDate='"+order.OrderDate+"'");
            return dbf.ChangeTable(delSql, "DB.accdb");
        }
    }
}

    
