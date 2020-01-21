using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Model
{
    public class Currency
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double ExchangeRate { get; set; }

        public Currency(string name, string id, double exchangeRate)
        {
            Id = id;
            Name = name;
            ExchangeRate = exchangeRate;
        }
    }
}
