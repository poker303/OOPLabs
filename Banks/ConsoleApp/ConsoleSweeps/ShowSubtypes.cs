using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;

namespace Banks.ConsoleSweeps
{
    public class ShowSubtypes
    {
        public List<string> ShowBanks(List<CentralBank> centralBanks)
        {
            return centralBanks[0].Banks.Select(bank => bank.GetName()).ToList();
        }

        public List<Client> ShowBankClients(Bank bank)
        {
            return bank.Clients;
        }

        public double ShowAccountAfterTime(IAccount account, DateTime date)
        {
            var timeRewinder = new TimeRewinder("first");
            return timeRewinder.AccountAfterTime(account, date);
        }
    }
}