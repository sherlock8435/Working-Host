using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class Mailboxdb :DBFunctions
    {
        private MailBoxList  list = new MailBoxList ();
       public MailBoxList SelectMessagEmail(string email)
        {
            string sqlStr = "SELECT *FROM MailBoxtbl where SenderEmail =' " + email + "'";
            return SelectAll(sqlStr);

        }
        private MailBox CreateModel(MailBox m)
        {
            m .SenderEmail  = reader["SenderEmail"].ToString ();
            m .msgDate = reader["msgDate"].ToString();
            m.msgRead = (bool)reader["msgRead"];
            m.msgSubject = reader["msgSubject"].ToString();
            m.RecieverEmail = reader["RecieverEmail"].ToString();
            m.SenderFName = reader["SenderFName"].ToString();
            m.SenderLName = reader["SenderLName"].ToString();
            m.msgBody = reader["msgBody"].ToString();
            return m;

        }
        public MailBoxList SelectAllMsg()
        {
            string sqlStr = "SELECT * FROM MailBoxtbl";
            return SelectAll(sqlStr);
        }


        public int ContactUs(MailBox m)
        {
            DBFunctions dt = new DBFunctions();

            string insertSql = string.Format("insert into MailBoxtbl" +
           " (msgDate, SenderEmail,SenderLname,SenderFname,msgSubject,msgBody,RecieverEmail)" +
            "values('{0}', '{1}','{2}', '{3}','{4}','{57},'{6}')",
            m.msgDate, m.SenderEmail, m.SenderLName, m.SenderFName, m.msgSubject, m.msgBody, m.RecieverEmail);
            return dt.ChangeTable(insertSql, "DB1.accdb");
        }
        private MailBoxList  SelectAll(string sqlStr)
        {
            try
            {
                
                cmd = GenerateOleDBCommand(sqlStr, "DB.accdb");
                conObj.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MailBox  m = new MailBox ();
                    list.Add(CreateModel(m));
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
    }
}
