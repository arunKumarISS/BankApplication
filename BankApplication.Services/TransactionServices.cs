using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Model;

namespace BankApplication.Services
{
    public class TransactionServices
    {
        public Customer Customer { get; set; }

        public TransactionServices()
        {

        }
        public TransactionServices(Customer customer)
        {
            Customer = customer;
        }

        public void AddTransaction(double amount, string TransactionID, string TypeOfTransaction, string Status, string Date)
        {
            Transaction NewTransaction = new Transaction(Customer.AccountID, Customer.AccountID, amount, TransactionID, "Deposit", "success", DateTime.Now.ToString("yyyymmdd"));
            Customer.TransactionHistory.Add(NewTransaction);
        }

        public void DisplayAllTransactions()
        {
            
            foreach (var transaction in Customer.TransactionHistory)
            {
                Console.WriteLine(transaction.FromAccountNo + " to " + transaction.DestinationAccountNo + " amount: " + transaction.Amount + " transactionID: " + transaction.TransactionID + " type of Transaction: " + transaction.TypeOfTransaction + " status: " + transaction.Status + " date: " + transaction.Date + "\n");
                
            }
            
        }

        public void DisplayTransaction(string date)
        {
            foreach (var transaction in Customer.TransactionHistory)
            {
                if (transaction.Date.Equals(date))
                {
                    Console.WriteLine(transaction.FromAccountNo + " to " + transaction.DestinationAccountNo + " amount: " + transaction.Amount + " transactionID: " + transaction.TransactionID + " type of Transaction: " + transaction.TypeOfTransaction + " status: " + transaction.Status + " date: " + transaction.Date + "\n");
                }
            }
        }

        public string GenerateTransactionID(Bank bank)
        {
            return "TXN" + bank.BankID + Customer.AccountID + DateTime.UtcNow.ToString("hhmmss");
        }
    }
}
