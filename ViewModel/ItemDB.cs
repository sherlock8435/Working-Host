using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ItemDB : DBFunctions
    {
        Item Itm = null;
        private ItemList list = new ItemList();
        DBFunctions dbf = new DBFunctions();
        public ItemDB() : base() { }

        private Item CreateModel(Item Itm)
        {
            Itm.ItemID = int.Parse(reader["ItemCode"].ToString());
            Itm.Name = reader["Name"].ToString();
            Itm.price = double.Parse(reader["Price"].ToString());
            Itm.ItemImg = reader["ItemImg"].ToString();
            Itm.Description = reader["Description"].ToString();
            Itm.Quantity = int.Parse(reader["Quantity"].ToString());

            return Itm;
        }

        private ItemList SelectItems(string sqlStr)
        {
            ItemList list = new ItemList();
            try
            {

                cmd = GenerateOleDBCommand(sqlStr, "DB.accdb");
                conObj.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Itm = new Item();
                    list.Add(CreateModel(Itm));
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

        public ItemList SelectAllItems()
        {
            string sqlStr = "Select * From Itemstbl";
            return SelectItems(sqlStr);
        }
        public int AddItem(Item item)
        {
            string insertSql =
            $"INSERT INTO Itemstbl (ItemCode ,Name, Price, Description, Quantity, ItemImg) " +
            $"VALUES ({item.ItemID},'{item.Name}', {item.price}, '{item.Description}', {item.Quantity}, '{item.ItemImg}')";
            return dbf.ChangeTable(insertSql, "DB.accdb");
        }
        public DataTable GetItems()
        {
            string sqlStr = "Select * From Itemstbl";
            DataTable dt = dbf.Select(sqlStr, "DB.accdb");
            return dt;
        }
        public int DeleteItem(Item item)
        {
            string delSql = $"Delete from Itemstbl where ItemCode = {item.ItemID}";
            return dbf.ChangeTable(delSql, "DB.accdb");
        }
        public int getnextItemID()
        {
            string sqlStr = "Select ItemCode From Itemstbl";
            int i = 1;
            DataTable dt = dbf.Select(sqlStr, "DB.accdb");
            while (dt.Rows.Count >= i && (int)dt.Rows[i - 1][0] == i)
            {
                i++;
            }
            return i;
        }
    }
}
