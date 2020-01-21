using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Model;
using BankApplication.Database;

namespace BankApplication.Services
{
    public class BankServices
    {
        public Bank CreateBank(string name)
        {
            Bank NewBank = new Bank(name);
            BankList.ListOfBanks.Add(NewBank);
            return NewBank;
        }

        public Bank GetBank(BankList admin, string bankID)
        {
            foreach (var bank in BankList.ListOfBanks)
            {
                if (bank.BankID.Equals(bankID))
                {
                    return bank;
                }
            }
            return null;
        }

        public void SetCharges(Bank bank, double srtgs, double simps, double drtgs, double dimps)
        {
            bank.same.rtgs = srtgs;
            bank.same.imps = simps;
            bank.different.rtgs = drtgs;
            bank.different.imps = dimps;
        }

        public bool AddCurrency(Bank bank, string name, string id, double exchangeRate)
        {
            foreach(var currency in bank.Currencies)
            {
                if (currency.Id.Equals(id))
                    return false;
            }
            Currency NewCurrency = new Currency(name, id, exchangeRate);
            bank.Currencies.Add(NewCurrency);
            return true;
        }

    }
}
