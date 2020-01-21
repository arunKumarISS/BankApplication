using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Model;

namespace BankApplication.Services
{
    public class UserServices
    {
        public bool GetStaff(Bank bank, string username, string password,out Staff staffMember)
        {

            //foreach (var staff in bank.StaffList)
            //{
            //    //if (staff.Username.Equals(username) && staff.Password.Equals(password))
            //    //{
            //    //return staff;
            //    staffMember = staff;
            //        return staff.Username.Equals(username) && staff.Password.Equals(password)
            //    //}
            //}
            ////return null;
            ///
            staffMember = bank.StaffList.FirstOrDefault(_ => string.Equals(_.Username, username) && string.Equals(_.Password, password));
            return staffMember != null;

        }

        public Customer GetCustomer(Bank bank, string username, string password)
        {

            foreach (var customer in bank.CustomerList)
            {
                if (customer.Username.Equals(username) && customer.Password.Equals(password))
                {
                    return customer;
                }
            }
            return null;
        }
    }
}
