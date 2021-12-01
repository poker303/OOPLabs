using System.Collections.Generic;
using Banks.Accounts;

namespace Banks
{
    public class Client
    {
        public Client(string name, string surname, string address, string passportNumber, int money)
        {
            Name = name;
            Surname = surname;
            Address = address;
            PassportNumber = passportNumber;
            Money = money;
            CreditAccounts = new List<CreditAccount>();
            DepositAccounts = new List<DepositAccount>();
            DebitAccounts = new List<DebitAccount>();
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string PassportNumber { get; set; }
        public int Money { get; set; }

        public List<CreditAccount> CreditAccounts { get; set; }
        public List<DepositAccount> DepositAccounts { get; set; }
        public List<DebitAccount> DebitAccounts { get; set; }
    }
}