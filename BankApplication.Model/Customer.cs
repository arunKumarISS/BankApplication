using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Model
{
    public class Customer : User
    {
        public string AccountID;
        public double Balance;
        public List<Transaction> TransactionHistory = new List<Transaction>();

        public Customer(string name)
        {
            Name = name.ToUpper();
            Username = name.ToUpper() + ".cus";
            Password = Name.Substring(0, 4) + "@I23";
            AccountID = Name.ToUpper().Substring(0, 3) + DateTime.UtcNow.ToString("hhmmss");
            Balance = 0;
        }

        public Customer()
        {

        }
    }
}
