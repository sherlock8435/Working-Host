using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class VisaDB : DBFunctions
    {
        private VisaList list = new VisaList();
        DBFunctions dbf = new DBFunctions();
        private Visa  CreateModel(Visa visa)
        {
            visa.Uemail  = reader["uEmail"].ToString ();
            visa.visano = reader["visano"].ToString ();
            visa.Accountno = reader["Accountno"].ToString();
            visa.VisaType  = reader["VisaType"].ToString();
            visa.ExpDate = reader["ExpDate"].ToString();
            visa.Cash  = (double ) reader["Cash"];
            visa.credit  =(double) reader["credit"];
            return visa;
        }

        public Visa SelectAccountById(string uEmail)
        {
            list = SelectAllAccounts();
            Visa c = list.Find(item => item.Uemail == uEmail );// deligate
            return c;
        }

        public int UpdateVisaAccount(double amount, Visa visa)
        {
            Visa v = SelectAccountById(visa.Uemail);
        
            if (v.visano == visa.visano)
            {
                double balance = v.Cash;
                double credit = v.credit;
                double newBalance =   (balance + credit) -amount;
                if (newBalance >= 0)
                {
                    string updateAccountSql = string.Format("update visaTbl SET  Cash=" + (v.Cash - amount) + 
                        " where visano='" + visa.visano + "' and uEmail='"+ visa.Uemail +"'");
                    return (dbf.ChangeTable(updateAccountSql, "DB.accdb"));
                }
            }
            return 0; 
        }
        public VisaList SelectAllAccounts()
        {
            string sqlStr = "SELECT * FROM VisaTbl ";
            return SelectAccounts(sqlStr);
        }
        private VisaList SelectAccounts(string sqlStr)
        {
            try
            {
                cmd=GenerateOleDBCommand(sqlStr, "DB.accdb");
                conObj.Open();
                reader=cmd.ExecuteReader();
                while(reader.Read())
                {
                     Visa  v= new Visa  ();
                    list.Add(CreateModel(v));
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if(reader!=null)
                    reader.Close();
                if(this.conObj.State== System.Data.ConnectionState.Open)
                    this.conObj.Close();
            }
            return list;
        }
    }
}
