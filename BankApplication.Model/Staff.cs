using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Model
{
    public class Staff : User
    {
        public Staff(string name)
        {
            Name = name.ToUpper();
            Username = Name.Substring(0, 4) + ".staff";
            Password = Name.Substring(0, 4) + "@I23";
        }

        public Staff()
        {

        }
    }
}
