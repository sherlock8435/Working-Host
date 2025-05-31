
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using System.Data.OleDb;



namespace ViewModel
{
    public class UsersDB : DBFunctions
    {
        Users user = null;
        private UsersList list = new UsersList();
        DBFunctions dbf = new DBFunctions();
        public UsersDB() : base() { }

        private Users CreateModel(Users user)
        {
            CityDB c = new CityDB();
            user.Fname = reader["Fname"].ToString();
            user.Lname = reader["Lname"].ToString();
            user.Upassword = reader["UPassword"].ToString();
            user.Utelnum = reader["Utelnum"].ToString();
            int cld = (int)reader["cityID"];
            user.City = c.SelectCityById(cld);
            user.Uemail = reader["Uemail"].ToString();
            user.Ubirthday = reader["Ubirthday"].ToString();
            user.Ugender = reader["Ugender"].ToString();
            user.Answer = reader["Uanswer"].ToString();
            user.Question = reader["Uquestion"].ToString();
            return user;
        }
        public int DeleteUserDataByEmail(string uEmail, string upassword)
        {
            string delSql = string.Format("Delete from UsersTbl "  +
                "  where Uemail='" + uEmail + "'and UPassword='" + upassword + "'");
            return dbf.ChangeTable(delSql, "DB.accdb");
        }

        public UsersList Select()
        {
            UsersList list = new UsersList();
            try
            {
                string sqlStr="SELECT * FROM UsersTbl";
                cmd=GenerateOleDBCommand(sqlStr,"DB.accdb");
                conObj.Open();
                reader=cmd.ExecuteReader();

                while (reader.Read()) 
                {
                 user = new Users();   
                    list.Add(CreateModel(user));    // add new user to users list 9999(sql, con,dt,tbl)
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
        public string SelectUserIdByEmail(string Uemail)
        {
            DataTable dt = null; 
            string sqlStr="SELECT Upassword From UsersTbl where Uemail='"+Uemail+"'";
                dt=dbf.Select(sqlStr,"DB.accdb");
            if(dt==null) return "user not found";
            return dt.Rows[0][0].ToString();
        }
        public Users SelectUserByEmail(string Uemail)
        {
            list = Select();
            return list.Find(item => item.Uemail == Uemail);
        }
        public bool CheckUserExist(string Upassword, string Uemail)
        {
            DataTable dt= null;
            string sqlStr="SELECT * From UsersTbl where Uemail='"+ Uemail+"' and UPassword='"+Upassword +"'";
            dt=dbf.Select(sqlStr,"GardeningDB.accdb");
            if(dt==null) return false;
            return(dt.Rows.Count>0);
        }

        public bool CheckUserExistByEmail( string Uemail)
        {
            DataTable dt = null;
            string sqlStr = "SELECT * From UsersTbl where Uemail='" + Uemail  + "'";
            dt = dbf.Select(sqlStr, "DB.accdb");
            if (dt == null) return false;
            return (dt.Rows.Count > 0);
        }

       public string GetQuestion(string Uemail) 
        {
            DataTable dt = null;
            string sqlStr = "SELECT Uquestion From UsersTbl where Uemail='" + Uemail + "'";
            dt = dbf.Select(sqlStr, "DB.accdb");
            if (dt == null) return "user not found";
            return dt.Rows[0][0].ToString();
        }

        public string PassRecovery(string Uemail,string PassAns)
        {
            DataTable dt = null;
            string sqlStr = "";
            sqlStr = "select Upassword from UsersTbl where Uemail='" + Uemail + "' and Uanswer='" + PassAns + "'";
            dt = dbf.Select(sqlStr, "DB.accdb");
        

            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            return "";
        }

        public bool CheckAdminExist(string Upassword,string Uemail)
        {
            return dbf.CheckAdmin(Uemail, Upassword);
        }
        public int AddUser(Users user )
        {
            string insertSql=string.Format("insert into Userstbl " +
                " (Uemail,Upassword,Fname,Lname,Utelnum,Ubirthday,Cityid,Ugender,Uquestion,Uanswer) " +
                "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                user.Uemail,user.Upassword,user.Fname,user.Lname,user.Utelnum,user.Ubirthday,user.City.CityID,user.Ugender,user.Question,user.Answer );
            return dbf.ChangeTable(insertSql,"DB.accdb");
        }
        public int UpdateUserProfile(Users user )
        {
            string updateSql=string.Format("update UsersTbl SET  Upassword='"+user.Upassword+"', Fname='"+user.Fname+"'," +
                "Lname='"+user.Lname+ "',Utelnum='" + user.Utelnum+"' where Uemail='"+user.Uemail+"'");
            return dbf.ChangeTable(updateSql, "DB.accdb");
        }
    }
}
