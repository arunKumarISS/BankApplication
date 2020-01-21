using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Model
{
    public class Bank
    {

        string BankName;
        public string BankID;
        public List<Currency> Currencies = new List<Currency>();
        public Charges same = new Charges();
        public Charges different = new Charges();
        public List<Staff> StaffList = new List<Staff>();
        public List<Customer> CustomerList = new List<Customer>();

        public Bank(string bankName)
        {

            BankName = bankName;
            BankID = bankName.Substring(0, 3) + DateTime.Now.ToString("yyyymmdd");
            same.rtgs = 0;
            same.imps = 5;
            different.rtgs = 2;
            different.imps = 6;
            Currency Rupee = new Currency("Rupee", "INR", 1);
            Currencies.Add(Rupee);

            Currency Dollar = new Currency("Dollar", "DLR", 71.04);
            Currencies.Add(Dollar);

            Currency Yen = new Currency("Yen", "YEN", 0.64);
            Currencies.Add(Yen);
        }

        public Bank()
        {

        }

    }
}
