using System;

namespace Banks.Accounts
{
    public class IAccount
    {
        // DateTime openingDate
        public IAccount(int amount, bool possibilityOfWithdrawal)
        {
            // if (number < 1)
            // {
            //     throw new Exception();
            // }
            //
            // Number = number;
            if (amount < 0)
            {
                throw new Exception();
            }

            Amount = amount;
            OpeningDate = DateTime.Now;
            PossibilityOfWithdrawal = possibilityOfWithdrawal;
        }

        public int Number { get; set; }
        public int Amount { get; set; }
        public DateTime OpeningDate { get; set; }
        public bool PossibilityOfWithdrawal { get; set; }

        public int Withdrawal(int withdrawalAmount)
        {
            if (withdrawalAmount < 0)
            {
                throw new Exception();
            }

            return Amount;
        }

        public int Replenishment(int replenishmentAmount)
        {
            if (replenishmentAmount < 0)
            {
                throw new Exception();
            }

            return Amount;
        }
    }
}