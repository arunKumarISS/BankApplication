using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Services;
using BankApplication.Model;
using BankApplication.Database;

namespace ConsoleApp1
{
    public class Program
    {
        public enum TransactionMode
        {
            RTGS, IMPS
        };
        public static void Main(string[] args)
        {
            BankServices Admin = new BankServices();
            StaffServices NewStaffService = new StaffServices();

            Bank FirstBank = Admin.CreateBank("STATE BANK INDIA");
            Console.WriteLine(FirstBank.BankID);
            Staff NewStaff = NewStaffService.CreateStaffMember(FirstBank, "arun");
            Console.WriteLine("Name:" + NewStaff.Name);
            Console.WriteLine("Username:" + NewStaff.Username);
            Console.WriteLine("Password:" + NewStaff.Password);

            Bank SecondBank = Admin.CreateBank("HDFC");
            Console.WriteLine(SecondBank.BankID);
            NewStaff = NewStaffService.CreateStaffMember(SecondBank, "arun");
            Console.WriteLine("Name:" + NewStaff.Name);
            Console.WriteLine("Username:" + NewStaff.Username);
            Console.WriteLine("Password:" + NewStaff.Password);

            Bank ThirdBank = Admin.CreateBank("TECHNOVERT");
            Console.WriteLine(ThirdBank.BankID);
            NewStaff = NewStaffService.CreateStaffMember(ThirdBank, "arun");
            Console.WriteLine("Name:" + NewStaff.Name);
            Console.WriteLine("Username:" + NewStaff.Username);
            Console.WriteLine("Password:" + NewStaff.Password);
            UserServices NewService = new UserServices();
            while (true)
            {
            begin:
                Console.WriteLine("1 -> STATE BANK OF INDIA\n2 -> HDFC\n3 -> TECHNOVERT\n0 -> EXIT");
                string BankName = Console.ReadLine();
                if (Convert.ToInt32(BankName) == 0)
                {
                    break;
                }
                Bank Bank = BankList.ListOfBanks.ElementAt(Convert.ToInt32(BankName) - 1);
                while (true)
                {
                    string Username, Password;
                    string userType;
                login:
                    Console.WriteLine("1 -> staff\n2 -> customer\n0 -> back");
                    userType = Console.ReadLine();
                    CustomerServices NewCustomerService = new CustomerServices(Bank);
                    switch (Convert.ToInt32(userType))
                    {
                        case 1:
                            {
                                Console.WriteLine("enter username: ");
                                Username = Console.ReadLine();
                                Console.WriteLine("enter password: ");
                                Password = Console.ReadLine();
                                Staff StaffMember;
                                if (NewService.GetStaff(Bank, Username, Password, out StaffMember))
                                {
                                    
                                    while (true)
                                    {
                                        Console.WriteLine("1 -> create account\n2 -> remove account\n3 -> revert transaction\n4 -> transaction history\n5 -> set charges\n6 -> update account\n7 -> Add Currency\n0 -> logout");
                                        string staffAction;
                                        staffAction = Console.ReadLine();
                                        switch (Convert.ToInt32(staffAction))
                                        {
                                            case 1:
                                                {
                                                    string Name;
                                                    Console.WriteLine("enter name:");
                                                    Name = Console.ReadLine();
                                                    
                                                    Customer NewCustomer = NewCustomerService.CreateAccount( Name.ToUpper());
                                                    Console.WriteLine("Account number: " + NewCustomer.AccountID);
                                                    Console.WriteLine("Balance: " + NewCustomer.Balance);
                                                    Console.WriteLine(NewCustomer.Name + "\n" + NewCustomer.Username + "\n" + NewCustomer.Password);
                                                    break;
                                                }

                                            case 2:
                                                {
                                                    string AccountID;
                                                    Console.WriteLine("enter accountID:");
                                                    AccountID = Console.ReadLine();
                                                    if (NewCustomerService.RemoveAccount( AccountID.ToUpper()))
                                                        Console.WriteLine("Removed successfully!!");
                                                    else
                                                        Console.WriteLine("accountID not found");

                                                    break;
                                                }

                                            case 3:
                                                {
                                                    string AccountID, TransactionID;
                                                    Console.WriteLine("enter Account number: ");
                                                    AccountID = Console.ReadLine();
                                                    Console.WriteLine("enter TransactionID: ");
                                                    TransactionID = Console.ReadLine();
                                                    AccountServices NewAccountService = new AccountServices(Bank);
                                                    BankList bankList = new BankList();
                                                    if (NewAccountService.RevertTransaction(bankList, AccountID.ToUpper(), TransactionID.ToUpper()))
                                                        Console.WriteLine("Reverted successfully!!");
                                                    else
                                                        Console.WriteLine("failed to Revert the Transaction");
                                                    break;
                                                }

                                            case 4:
                                                {
                                                    string AccountID;
                                                    Console.WriteLine("enter account number");
                                                    AccountID = Console.ReadLine();
                                                    TransactionServices NewTransactionService = new TransactionServices(NewService.GetCustomer(Bank, Username, Password));
                                                    NewTransactionService.DisplayAllTransactions();
                                                    break;
                                                }

                                            case 5:
                                                {
                                                    double srtgs, simps, drtgs, dimps;
                                                    Console.WriteLine("enter RTGS for same bank account: ");
                                                    srtgs = Convert.ToDouble(Console.ReadLine());
                                                    Console.WriteLine("enter IMPS for same bank account: ");
                                                    simps = Convert.ToDouble(Console.ReadLine());
                                                    Console.WriteLine("enter RTGS for different bank account: ");
                                                    drtgs = Convert.ToDouble(Console.ReadLine());
                                                    Console.WriteLine("enter IMPS for different bank account: ");
                                                    dimps = Convert.ToDouble(Console.ReadLine());
                                                    Admin.SetCharges(Bank, srtgs, simps, drtgs, dimps);
                                                    break;
                                                }

                                            case 6:
                                                {
                                                    Console.WriteLine("enter accountID");
                                                    string AccountID = Console.ReadLine();
                                                    Console.WriteLine("enter login details to make changes\nusername:");
                                                    string CustomerUsername = Console.ReadLine();
                                                    Console.WriteLine("password:");
                                                    string CustomerPassword = Console.ReadLine();
                                                    Console.WriteLine("enter new password:");
                                                    string NewPassword = Console.ReadLine();
                                                    if (NewCustomerService.UpdateAccount(AccountID.ToUpper(), CustomerUsername, CustomerPassword, NewPassword))
                                                        Console.WriteLine("Updated successfully!!");
                                                    else
                                                        Console.WriteLine("check the detials you have entered");
                                                    break;
                                                }

                                            case 7:
                                                {
                                                    Console.WriteLine("enter currency name:");
                                                    string Name = Console.ReadLine();

                                                    Console.WriteLine("enter currency Id:");
                                                    string Id = Console.ReadLine();

                                                    Console.WriteLine("enter exchangerate:");
                                                    double ExchangeRate = Convert.ToDouble(Console.ReadLine());

                                                    if (Admin.AddCurrency(Bank, Name, Id.ToUpper(), ExchangeRate))
                                                        Console.WriteLine("Currency added successfully");
                                                    else
                                                        Console.WriteLine("Currency already exists");
                                                    break;
                                                }

                                            case 0:
                                                goto login;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("incorrect username or password");
                                    goto login;
                                }

                            }
                        case 2:
                            {

                                Console.WriteLine("enter username: ");
                                Username = Console.ReadLine();
                                Console.WriteLine("enter password: ");
                                Password = Console.ReadLine();
                                Customer Customer = NewService.GetCustomer(Bank, Username, Password);
                                if (Customer != null)
                                {
                                    AccountServices NewAccountService = new AccountServices(Bank);
                                    TransactionServices NewTransactionService = new TransactionServices(Customer);
                                    while (true)
                                    {
                                        Console.WriteLine("1 -> withdraw\n2 -> deposit\n3 -> transfer\n4 -> balance check\n5 -> transaction history\n6 -> display transactions by date\n0 -> logout");
                                        string customerAction = Console.ReadLine();
                                        switch (Convert.ToInt32(customerAction))
                                        {
                                             
                                            case 1:
                                                {
                                                    string Amount;
                                                    Console.WriteLine("enter amount:");

                                                    Amount = Console.ReadLine();
                                                    if (NewAccountService.Withdraw( Convert.ToDouble(Amount), Customer))
                                                        Console.WriteLine("Withdrawn Successfully!!");
                                                    else
                                                        Console.WriteLine("Transaction Failed");
                                                    break;
                                                }

                                            case 2:
                                                {
                                                    string Amount;

                                                    Console.WriteLine("enter amount:");
                                                    Amount = Console.ReadLine();
                                                    if (NewAccountService.Deposit( Convert.ToDouble(Amount), Customer) != null)
                                                        Console.WriteLine("Deposited Successfully!!");
                                                    else
                                                        Console.WriteLine("Transaction Failed");
                                                    break;
                                                }

                                            case 3:
                                                {
                                                    Console.WriteLine("enter account number:");
                                                    string ToAccountID = Console.ReadLine();
                                                    Console.WriteLine("enter BankID");
                                                    string BankID = Console.ReadLine();
                                                    Console.WriteLine("enter amount:");
                                                    string Amount = Console.ReadLine();
                                                    Console.WriteLine("enter currency Id");
                                                    string CurrencyId = Console.ReadLine();
                                                    Console.WriteLine("1 -> RTGS\n2 -> IMPS");
                                                    int TypeOfTransfer = Console.Read();
                                                    BankList bankList = new BankList();
                                                    Bank ToBank = Admin.GetBank(bankList, BankID);
                                                    if (NewAccountService.Transfer( Convert.ToDouble(Amount), ToAccountID.ToUpper(), ToBank, CurrencyId.ToUpper(), Customer, TypeOfTransfer))
                                                        Console.WriteLine("Transfered Successfullly!!");
                                                    else
                                                        Console.WriteLine("Transaction Failed");
                                                    break;
                                                }

                                            case 4:
                                                {
                                                    Console.WriteLine(Customer.Balance);
                                                    break;
                                                }

                                            case 5:
                                                {
                                                    NewTransactionService.DisplayAllTransactions();
                                                    break;
                                                }

                                            case 6:
                                                {
                                                    Console.WriteLine("enter date:");
                                                    string Date = Console.ReadLine();
                                                    NewTransactionService.DisplayTransaction( Date);
                                                    break;
                                                }

                                            case 0:
                                                goto login;
                                        }

                                    }
                                }
                                else
                                {
                                    Console.WriteLine("incorrect username or password");
                                    goto login;
                                }
                            }

                        case 0:
                            {
                                goto begin;

                            }
                    }
                }
            }
        }
    }
}
