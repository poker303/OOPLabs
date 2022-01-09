using System.Collections.Generic;
using Banks.Accounts;

namespace Banks
{
    public class Client
    {
        private List<string> _notifications;
        public Client(string name, string surname, int money)
        {
            Name = name;
            Surname = surname;
            Money = money;
            _notifications = new List<string>();
            Address = null;
            PassportNumber = null;
            SubscriptionDesire = false;
            CreditAccounts = new List<IAccount>();
            DepositAccounts = new List<IAccount>();
            DebitAccounts = new List<IAccount>();
        }

        public string Address { get; set; }
        public string PassportNumber { get; set; }
        public int Money { get; set; }

        public List<IAccount> CreditAccounts { get; }
        public List<IAccount> DepositAccounts { get; }
        public List<IAccount> DebitAccounts { get; }

        private string Name { get; }
        private string Surname { get; }
        private bool SubscriptionDesire { get; set; }

        public string GetName()
        {
            return Name;
        }

        public string GetSurName()
        {
            return Surname;
        }

        public string AddAddress(string address)
        {
            Address = address;
            return Address;
        }

        public string AddPassportNumber(string passportNumber)
        {
            PassportNumber = passportNumber;
            return PassportNumber;
        }

        public bool ChangeSubscriptionDesire(bool decision)
        {
            SubscriptionDesire = decision;
            return SubscriptionDesire;
        }

        public bool GetSubscriptionDesire()
        {
            return SubscriptionDesire;
        }

        public void Update(IAccount account)
        {
            switch (account)
            {
                case CreditAccount _:
                    _notifications.Add("Credit account terms updated");
                    break;
                case DepositAccount _:
                    _notifications.Add("Deposit account terms updated");
                    break;
                case DebitAccount _:
                    _notifications.Add("Debit account terms updated");
                    break;
            }
        }

        public List<string> GetNotifications()
        {
            return _notifications;
        }
    }
}