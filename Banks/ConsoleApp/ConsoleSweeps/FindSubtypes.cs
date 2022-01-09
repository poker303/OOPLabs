using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;

namespace Banks.ConsoleSweeps
{
    public class FindSubtypes
    {
        public List<Client> FindClientByNameSurname(List<Client> clients, string clientName, string clientSurName)
        {
            var foundClients = clients.Where(client => client.GetName() == clientName && client.GetSurName() == clientSurName).ToList();

            return foundClients;
        }

        public Bank FindBankByName(List<CentralBank> centralBanks, string nameBank)
        {
            var foundBanks = centralBanks[0].Banks.Where(bank => bank.GetName() == nameBank).ToList();

            return foundBanks[0];
        }

        public IAccount FindAccountByNumber(int number, Bank bank)
        {
            IAccount foundAccount = null;
            foreach (IAccount account in bank.CreditAccounts.Concat(bank.DebitAccounts).Concat(bank.DepositAccounts))
            {
                if (number == account.Number)
                {
                    foundAccount = account;
                }
            }

            return foundAccount;
        }
    }
}