using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Model;
using BankApplication.Database;

namespace BankApplication.Services
{
    

    public class AccountServices
    {
        public Bank Bank { get; set; }
        public AccountServices(Bank bank)
        {
            Bank = bank;
        }
        public enum TransactionMode
        {
            RTGS = 1,IMPS = 2
        };

        public bool Withdraw( double amount, Customer customer)
        {
            if (customer.Balance > amount)
            {
                customer.Balance -= amount;
                TransactionServices NewTransaction = new TransactionServices(customer);
                string TransactionID = NewTransaction.GenerateTransactionID(Bank);
                NewTransaction.AddTransaction( amount, TransactionID, "Withdraw", "success", DateTime.Now.ToString("yyyymmdd"));
                return true;
            }
            else
            {
                TransactionServices NewTransaction = new TransactionServices(customer);
                string TransactionID = NewTransaction.GenerateTransactionID(Bank);
                NewTransaction.AddTransaction( amount, TransactionID, "Withdraw", "Failed", DateTime.Now.ToString("yyyymmdd"));
                return false;
            }

        }

        public string Deposit( double amount, Customer customer)
        {
            customer.Balance += amount;
            TransactionServices NewTransaction = new TransactionServices(customer);
            string TransactionID = NewTransaction.GenerateTransactionID(Bank);
            NewTransaction.AddTransaction( amount, TransactionID, "deposit", "success", DateTime.Now.ToString("yyyymmdd"));
            return TransactionID;
        }

        public bool Transfer( double amount, string toAccountID, Bank toBank, string currencyId, Customer customer,int modeOfTransacation)
        {
            double RateOfExchange = 0;
            TransactionMode ModeOfTransaction = (TransactionMode)modeOfTransacation;
            foreach(var currency in Bank.Currencies)
            {
                if (currency.Id.Equals(currencyId))
                    RateOfExchange = currency.ExchangeRate;
            }
            if (RateOfExchange == 0)
                return false;
            double ConvertedAmount = amount * RateOfExchange;
            double ServiceCharges = 0;

            if (toBank != null)
            {
                CustomerServices NewService = new CustomerServices(Bank);
                Customer ToCustomer = NewService.GetCustomer(toBank, toAccountID);
                if (ToCustomer != null)
                {
                    switch (ModeOfTransaction)
                    {
                        case TransactionMode.RTGS:
                            {
                                if (toBank.BankID.Equals(Bank.BankID))
                                     ServiceCharges = amount * (Bank.same.rtgs* 0.01);
                                else
                                    ServiceCharges = amount * (Bank.different.rtgs *0.01);
                                break;
                            }
                        case TransactionMode.IMPS:
                            {
                                if (toBank.BankID.Equals(Bank.BankID))
                                    ServiceCharges = amount * (Bank.same.imps*0.01);
                                else
                                    ServiceCharges = amount * (Bank.different.imps* 0.01);
                                break;
                            }
                    }
                    if (customer.Balance > amount + ServiceCharges)
                    {
                        customer.Balance -= (amount + ServiceCharges);
                        ToCustomer.Balance += ConvertedAmount;
                        TransactionServices NewTransaction = new TransactionServices(customer);
                        string TransactionID = NewTransaction.GenerateTransactionID(Bank);
                        NewTransaction.AddTransaction( amount, TransactionID, "Transfer", "success", DateTime.Now.ToString("yyyymmdd"));
                        return true;
                    }
                    else
                    {
                        TransactionServices NewTransaction = new TransactionServices(customer);
                        string TransactionID = NewTransaction.GenerateTransactionID(Bank);
                        NewTransaction.AddTransaction( amount, TransactionID, "Transfer", "Failed", DateTime.Now.ToString("yyyymmdd"));
                        return false;
                    }
                }
            }
            return false;
        }

        public bool RevertTransaction(BankList admin, string accountNo, string transactionID)
        {
           foreach (var customer in Bank.CustomerList)
            {
                if (customer.AccountID.Equals(accountNo))
                {
                    foreach (var transaction in customer.TransactionHistory)
                    {
                        if (transaction.TransactionID.Equals(transactionID))
                        {
                            BankServices NewBankService = new BankServices();
                            Bank toBank = NewBankService.GetBank(admin, transaction.TransactionID.Substring(3, 11));
                            {
                                CustomerServices NewService = new CustomerServices(Bank);
                                if (NewService.GetCustomer(toBank, transaction.DestinationAccountNo).Balance > transaction.Amount)
                                {
                                    if(Transfer( transaction.Amount, customer.AccountID, Bank, "INR", customer, 1))
                                    return true;
                                }
                            }

                        }
                    }
                }
            }
            return false;
        }
    }
}
