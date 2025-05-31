using Model;
using System.Data;
using ViewModel;


namespace WcfServiceLibrary1
{
    public class Service1 : IService1
    {
        CityDB cdb = new CityDB();
        Mailboxdb mdb = new Mailboxdb();
        UsersDB udb = new UsersDB();
        ItemDB idb = new ItemDB();
        OrderDB odb = new OrderDB();
        VisaDB vdb = new VisaDB();

        public CityList SelectAllCities()
        {
            return cdb.SelectAllCities();

        }
        public int SelectCityIDByName(string cityName)
        {
            return cdb.SelectCityIDByName(cityName);
        }
        public Cities SelectCityByName(string cityName)
        {
            return cdb.SelectCityByName(cityName);
        }
        public int ContactUs(MailBox m)
        {
            return mdb.ContactUs(m);
        }
        public MailBoxList SelectMessagEmail(string email)
        {
            return mdb.SelectMessagEmail(email);
        }
        public MailBoxList SelectAllMsg()
        {
            return mdb.SelectAllMsg();
        }
        public bool CheckAdminExist(string Upassword, string Uemail)
        {
            return udb.CheckAdminExist(Upassword, Uemail);
        }
        public bool CheckUserExist(string Upassword, string Uemail)
        {
            return udb.CheckUserExist(Upassword, Uemail);
        }
        public string SelectUserIdByEmail(string Uemail)
        {
            return udb.SelectUserIdByEmail(Uemail);
        }
        public Users SelectUserByEmail(string Uemail)
        {
            return udb.SelectUserByEmail(Uemail);
        }
        public int DeleteUserDataByEmail(string uEmail, string upassword)
        {
            return udb.DeleteUserDataByEmail(uEmail, upassword);
        }
        public int AddUser(Users user)
        {
            return udb.AddUser(user);
        }
        public int UpdateUserProfile(Users user)
        {
            return udb.UpdateUserProfile(user);
        }
        public UsersList Select()
        {
            return udb.Select();

        }

        public bool CheckUserExistByEmail(string Uemail)
        {
            return udb.CheckUserExistByEmail(Uemail);
        }

        public string GetQuestion(string Uemail)
        {
            return udb.GetQuestion(Uemail);
        }

        public string PassRecovery(string Uemail, string PassAns)

        {
            return udb.PassRecovery(Uemail, PassAns);
        }



        // --------------------  Items
        public ItemList SelectAllItems()
        {
            return idb.SelectAllItems();
        }
        public int DeleteItem(Item item)
        {
            return idb.DeleteItem(item);
        }
        public int AddItem(Item item)
        {
            return idb.AddItem(item);
        }
        public DataTable GetItems()
        {
            return idb.GetItems();
        }
        public int getnextItemID()
        {
            return idb.getnextItemID();
        }


        // -------------------- Orders
        public int AddOrder(Order order)
        {
            return odb.AddOrder(order);
        }
        public int UpdateOrder(Order order)
        {
            return odb.UpdateOrder(order);
        }
        public int DeleteOrder(Order order)
        {
            return odb.DeleteOrder(order);
        }
        public OrderList SelectUserOrder(string UserEmail)
        {
            return odb.SelectUserOrder(UserEmail);
        }

        // -------------------- Payments / Visa
        public int TotalOrderToPay(string uEmail)
        {
            return odb.TotalOrderToPay(uEmail);
        }
        public VisaList SelectAllAccounts()
        {
            return vdb.SelectAllAccounts();
        }
        public int UpdateVisaAccount(double amount, Visa visa)
        {
            return vdb.UpdateVisaAccount(amount, visa);
        }
        public Visa SelectAccountById(string uEmail)
        {
            return vdb.SelectAccountById(uEmail);
        }
    }
}
