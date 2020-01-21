using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Model;
using BankApplication.Database;

namespace BankApplication.Services
{
    public class StaffServices
    {
        
        public Staff CreateStaffMember(Bank bank, string name)
        {
            Staff StaffMember = new Staff(name);
            bank.StaffList.Add(StaffMember);
            return StaffMember;
        }

        public void DisplayStaffDetails(Staff staffMember)
        {
            
        }
    }
}
