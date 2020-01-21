using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Model;
using BankApplication.Database;

namespace BankApplication.Services
{
    public class CustomerServices
    {
        public Bank Bank { get; set; }
        public CustomerServices(Bank bank)
        {
            Bank = bank;
        }

        public Customer CreateAccount( string name)
        {
            Customer NewCustomer = new Customer(name);
            Bank.CustomerList.Add(NewCustomer);
            return NewCustomer;
        }

        public bool RemoveAccount( string accountID)
        {
            foreach (var customer in Bank.CustomerList)
            {
                if (customer.AccountID.Equals(accountID))
                {
                    Bank.CustomerList.Remove(customer);
                    return true;
                }
            }
            return false;
        }

        public bool UpdateAccount( string accountID,string username, string password, string newPassword)
        {
            foreach (var customer in Bank.CustomerList)
            {
                if (customer.AccountID.Equals(accountID))
                {
                    
                    if (customer.Username.Equals(username) && customer.Password.Equals(password))
                    {
                        
                            customer.Password = newPassword;
                            return true;
                        
                    }
                    
                }
            }
            return false;
        }

        public Customer GetCustomer(Bank bank, string accountID)
        {
            foreach (var customer in bank.CustomerList)
            {
                if (customer.AccountID.Equals(accountID))
                {
                    return customer;
                }
            }
            return null;
        }
    }
}
