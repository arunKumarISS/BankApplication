using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Model
{
    public class Transaction
    {
        public string FromAccountNo;
        public string DestinationAccountNo;
        public double Amount;
        public string TypeOfTransaction;
        public string Status;
        public string Date;
        public string TransactionID;
        public Transaction(string fromAccountID, string destinationAccountID, double amount, string transactionID, string typeOfTransaction, string status, string date)
        {
            FromAccountNo = fromAccountID;
            DestinationAccountNo = destinationAccountID;
            Amount = amount;
            TransactionID = transactionID;
            TypeOfTransaction = typeOfTransaction;
            Status = status;
            Date = date;
        }

    }
}
