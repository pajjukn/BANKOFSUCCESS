namespace BankOfSuccess.UILayer
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Creating instance of AccountForm
            AccountForm form = new AccountForm();
            Console.WriteLine("********Welcome to Bank Of Success Pvt Ltd*******\n\n");
            bool test = true;
            while (test)
            {
                Console.WriteLine("Select  one operation:");
                Console.WriteLine("1.  Open Account");
                Console.WriteLine("2.  Close Account");
                Console.WriteLine("3.  Withdraw Money");
                Console.WriteLine("4.  Deposit Money");
                Console.WriteLine("5.  Transfer Money");
                Console.WriteLine("6.  Exit");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        form.OpenAccount();
                        break;
                    case 2:
                        form.CloseAccount();
                        break;
                    case 3:
                        form.Withdraw();
                        break;
                    case 4:
                        form.Deposit();
                        break;
                    case 5:
                        form.Transfer();
                        break;
                    case 6:
                        Console.WriteLine("\n\n**********Thankyou!**********");
                        test = false;
                        break;
                    default: break;
                }

            }

            Console.Read();

        }
    }
}