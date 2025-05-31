using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ViewModel;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        CityList SelectAllCities();
        [OperationContract]
        int SelectCityIDByName(string cityName);

        [OperationContract]
        Cities SelectCityByName(string cityName);

        [OperationContract]
        MailBoxList SelectAllMsg();

        [OperationContract]
        int ContactUs(MailBox m);
        [OperationContract]
        MailBoxList SelectMessagEmail(string email);
        [OperationContract]
        string PassRecovery(string Uemail, string PassAns);

        [OperationContract]
        string GetQuestion(string Uemail);

        [OperationContract]
        bool CheckUserExistByEmail(string Uemail);

        [OperationContract]
        bool CheckAdminExist(string Upassword, string Uemail);

        [OperationContract]
        bool CheckUserExist(string Upassword, string Uemail);

        [OperationContract]
        string SelectUserIdByEmail(string Uemail);
        [OperationContract]
        Users SelectUserByEmail(string Uemail);

        [OperationContract]
        int DeleteUserDataByEmail(string uEmail, string upassword);

        [OperationContract]
        int AddUser(Users user);

        [OperationContract]
        int UpdateUserProfile(Users user);

        [OperationContract]
        UsersList Select();

        [OperationContract]
        ItemList SelectAllItems();

        [OperationContract]
        int DeleteItem(Item item);
        [OperationContract]

        DataTable GetItems();

        [OperationContract]
        int AddItem(Item item);
        
        [OperationContract]
        int getnextItemID();


        [OperationContract]
        int AddOrder(Order order);

        [OperationContract]
        int UpdateOrder(Order order);
        [OperationContract]
        int DeleteOrder(Order order);
        [OperationContract]
        OrderList SelectUserOrder(string UserEmail);
        [OperationContract]
        int TotalOrderToPay(string uEmail);
        // -------------------- Payments / Visa
        [OperationContract]
        VisaList SelectAllAccounts();
        [OperationContract]
        int UpdateVisaAccount(double amount, Visa visa);
        [OperationContract]
        Visa SelectAccountById(string uEmail);

    }
}
