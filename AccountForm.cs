using BankOfSuccess.BuisnessLogicLayer;
using BankOfSuccess.EntityLayer;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BankOfSuccess.UILayer
{/// <summary>
/// Boundary class - used to represent and manage the interactions with the user
/// </summary>
///
    class AccountForm
    {
        //creating object of AccountManager class
        AccountManager manager = new AccountManager();
        //Taking data from user to open account
        public void OpenAccount()
        {
            bool checkAccountOpened = false;
            
            Console.WriteLine("Account Opening");
            Console.WriteLine("Do you want to open Savings or Current:");
            string accountType = Console.ReadLine();

            if (accountType.Equals("Savings"))
            {
                Savings savings = new Savings();
                bool isValid = false;

                Console.Write("Enter Your Name:");
                savings.Name = Console.ReadLine();

                do
                {
                    Console.Write("Pin number: ");
                    savings.PinNumber = int.Parse(Console.ReadLine());
                    isValid = CheckPinNumberLength(savings.PinNumber);
                    if (isValid == false) { Console.WriteLine(" Pin number should contain four digits"); }
                } while (isValid == false);


                do
                {
                    Console.Write("Enter Your Date of Birth:");
                    savings.DateOfBirth = DateTime.Parse(Console.ReadLine());
                    isValid = CheckEligibleAge(savings.DateOfBirth);
                    if (isValid == false) { Console.WriteLine("Age is Less than 18 years, not eligible to open account"); }
                } while (isValid == false);


                Console.Write("Enter Your Gender(M/F/T):");
                savings.Gender = char.Parse(Console.ReadLine());

                do {
                    Console.Write("Enter Your Phone Number:");
                    savings.PhoneNo = Console.ReadLine();

                    isValid = CheckMobileNumberLength(savings.PhoneNo);
                    if (isValid == false) { Console.WriteLine("Phone Number should Contain 10 digits"); }
                } while (isValid == false);


                Console.WriteLine("Select the Privilge: 1.PRIMIUM \t 2.GOLD \t 3.SILVER");
                string privilge = Convert.ToString(Console.ReadLine());
                if (privilge.Equals("1"))
                    savings.Privilge = Privilge.PREMIUM;
                else if (privilge.Equals("2"))
                    savings.Privilge = Privilge.GOLD;
                else if (privilge.Equals("3"))
                    savings.Privilge = Privilge.SILVER;

                try
                {
                    //Calling account manager by passing savings object
                    checkAccountOpened = manager.OpenAccount(savings, accountType);
                    if (checkAccountOpened)
                    {
                        Console.WriteLine("Account Opened Successfully.");
                        Console.WriteLine($"Account type:{accountType}");
                        Console.WriteLine($"Account number: {savings.AccountNumber}");
                        Console.WriteLine($"Account holder name: {savings.Name}");
                        Console.WriteLine($"Balance: {savings.Balance}");
                        Console.WriteLine($"Date of birth: {savings.DateOfBirth}");
                        Console.WriteLine($"Gender: {savings.Gender}\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            else if (accountType.Equals("Current"))
            {
                Current current = new Current();
                bool isValid = false;

                Console.Write("Name:");
                current.Name = Console.ReadLine();

                do { 
                Console.Write("Pin number: ");
                current.PinNumber = int.Parse(Console.ReadLine());
                isValid =CheckPinNumberLength(current.PinNumber);
                if (isValid == false) { Console.WriteLine(" Pin number should contain four digits"); }
            }while(isValid ==false);


                do
                {
                    Console.Write("Company Name: ");
                    current.CompanyName = Console.ReadLine();
                    isValid = CheckCompanyName(current.CompanyName);
                    if (isValid==false) { Console.WriteLine("Company name should not be empty"); }
                }while(isValid==false); 

                do
                {
                    Console.Write("Website: ");
                    current.Website = Console.ReadLine();
                    isValid = CheckWebsiteUrl(current.Website);
                    if (isValid==false) { Console.WriteLine(" information invaid,Website Name should start from https:/ or http:/"); }
                } while(isValid==false);

                do
                {
                    Console.Write(" Enter Registration Number: ");
                    current.RegistrationNo = Console.ReadLine();
                    isValid = CheckRegistrationNo(current.RegistrationNo);
                    if (isValid==false) { Console.WriteLine("Registration number should not be empty"); }
                } while (isValid==false);

                Console.WriteLine("Select the Privilge: 1.PRIMIUM /t 2.GOLD /t 3.SILVER");
                string privilge = Convert.ToString(Console.ReadLine());
                if (privilge.Equals("1"))
                    current.Privilge = Privilge.PREMIUM;
                else if (privilge.Equals("2"))
                    current.Privilge = Privilge.GOLD;
                else if (privilge.Equals("3"))
                    current.Privilge = Privilge.SILVER;

                try
                {
                    //Calling account manager by passing Current object
                    checkAccountOpened = manager.OpenAccount(current, accountType);
                    if (checkAccountOpened)
                    {
                        Console.WriteLine("Account Opened Successfully.");
                        Console.WriteLine($"Account type:{accountType}");
                        Console.WriteLine($"Account number: {current.AccountNumber}");
                        Console.WriteLine($"Account holder name: {current.Name}");
                        Console.WriteLine($"Registration number: {current.RegistrationNo}");
                        Console.WriteLine($"Company name: {current.CompanyName}");
                        Console.WriteLine($"Website: {current.Website}");
                        Console.WriteLine($"Balance: {current.Balance}\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        //Method for taking inputs from user to close opened Account
        public void CloseAccount()
        {
            bool isClosed = false;
            Console.Write("Do you want to close your account: (Y/N)");
            char choice = char.Parse(Console.ReadLine());

            try
            {
                if (choice == 'Y')
                {
                    Console.Write("Account number:");
                    int accNo = int.Parse(Console.ReadLine());


                    Console.Write("Pin number:");
                    int pinNo = int.Parse(Console.ReadLine());

                    //Get account from accountRepository by passing account number as parameter
                    Account accountTobeClosed = AccountRepository.GetAccount(accNo);

                    //Calling account manager by passing account object to validate details inserted by user
                    isClosed = manager.CloseAccount(accountTobeClosed);
                    if (isClosed)
                        Console.WriteLine($"Account closed successfully.\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }

        }
        //Method for taking input form user to withdraw money
        public void Withdraw()
        {
            double accountBalance = 0;

            Console.Write("Account Number: ");
            int accNo = int.Parse(Console.ReadLine());

            Console.Write("Account Pin:");
            int pin = int.Parse(Console.ReadLine());

            Console.Write("Amount which you want to withdraw:");
            double withdrawAmt = double.Parse(Console.ReadLine());

            try
            {
                //Get account from accountRepository by passing account number as parameter
                Account accountWithdrawn = AccountRepository.GetAccount(accNo);

                //Calling account manager by passing account object,pin, withdraw amount
                accountBalance = manager.Withdraw(pin, withdrawAmt, accountWithdrawn);

                Console.WriteLine($"{withdrawAmt} is debited from account:{accNo}.Total balance is {accountBalance}.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }
        }
        //Method for taking inputs from user to deposit money account
        public void Deposit()
        {
            double accountBalance = 0;

            Console.Write("Account Number: ");
            int accNo = int.Parse(Console.ReadLine());
            Console.Write("Amount which you want to deposit:");
            double depositAmt = double.Parse(Console.ReadLine());

            try
            {
                //Get account from accountRepository by passing account number as parameter
                Account accountDeposit = AccountRepository.GetAccount(accNo);

                //Calling account manager by passing account object,amount
                accountBalance = manager.Deposit(accountDeposit, depositAmt);
                Console.WriteLine($"{depositAmt} is deposited in account:{accNo}.Total balance:{accountBalance}.\n");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }
        }
        //Method for taking inputs from user to transfer money from one account to another
        public void Transfer()
        {
            double accountBalance = 0;
            Transfer transfer = new Transfer();

            Console.Write("Enter Account Number from which you Want to transfer:");
            int fromAccNo = int.Parse(Console.ReadLine());

            Console.Write("Enter pin:");
            transfer.PinNumber = int.Parse(Console.ReadLine());

            Console.Write("Enter Account Number in which you Want to transfer:");
            int toAccNo = int.Parse(Console.ReadLine());

            //Asking for transfer mode to user
            Console.WriteLine("Transfer Mode: 1.IMPS\t       2.NEFT\t        3.RTGS");
            int mode = int.Parse(Console.ReadLine());
            if (mode.Equals("1"))
                transfer.Mode = TransferMode.IMPS;
            else if (mode.Equals("2"))
                transfer.Mode = TransferMode.NEFT;
            else if (mode.Equals("3"))
                transfer.Mode = TransferMode.RTGS;

            Console.WriteLine("Enter amount:");
            transfer.Amount = Convert.ToDouble(Console.ReadLine());
            //transfer.PinNumber= pin;
            try
            {
                //Get account from accountRepository by passing account number as parameter
                transfer.FromAccount = AccountRepository.GetAccount(fromAccNo);
                transfer.ToAccount = AccountRepository.GetAccount(toAccNo);
                //Calling account manager by passing transfer object
                accountBalance = manager.Transfer(transfer);
                Console.WriteLine($"{transfer.Amount} is debited from account:{transfer.FromAccount.AccountNumber}.Total balance is {accountBalance}.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }
        }
       
        public void TransactionDetail()
        {
            Transaction transaction = new Transaction();
            Console.Write("Enter Account Number:");
            transaction.AccountNo = int.Parse(Console.ReadLine());

            manager.GetTransactionDetail(transaction);


        }




        protected bool CheckMobileNumberLength(string mobileNumber)
        {
            bool isValid = false;

            isValid = (mobileNumber.Length == 10);

            return isValid;
        }

        protected bool CheckPinNumberLength(int pinNo)
        {
            bool isValid = false;

            isValid = (pinNo.ToString().Length == 4);

            return isValid;
        }

        protected bool CheckEligibleAge(DateTime dateOfBirth)
        {
            bool isValid = false;

            int age = 0;

            age = DateTime.Now.Year - dateOfBirth.Year;

            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            {
                age = age - 1;
            }

            if (age >= 18) { isValid = true; }

            return isValid;
        }

        // Validating Current account Information

        protected bool CheckCompanyName(string companyName)
        {
            bool isValid = false;

            //Here you validate the number of digits in the Pin No
            isValid = (!String.IsNullOrEmpty(companyName));

            return isValid;

        }

        protected bool CheckWebsiteUrl(string WebsiteURL)
        {
            bool isValid = false;

            isValid = isValid = (WebsiteURL.Contains("https") || WebsiteURL.Contains("http")); ;

            return isValid;

        }

        protected bool CheckRegistrationNo(string registrationNo)
        {
            bool isValid = false;

            isValid = (!String.IsNullOrEmpty(registrationNo));

            return isValid;
        }
    }

}