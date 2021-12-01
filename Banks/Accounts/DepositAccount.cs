using System;
using System.Collections.Generic;
using System.Linq;

namespace Banks.Accounts
{
    public class DepositAccount : IAccount
    {
        public DepositAccount(int number, int amount, bool possibilityOfWithdrawal, int duration, List<AmountPercentPair> table)
            : base(amount, possibilityOfWithdrawal)
        {
            Duration = duration;
            foreach (AmountPercentPair pair in table.Where(pair => pair.Amount == amount))
            {
                Name = "Dep" + number;
                Percent = pair.Percent;
            }
        }

        public string Name { get; set; }

        public int Percent { get; set; }
        public int Duration { get; set; }

        public new int Withdrawal(int withdrawalAmount)
        {
            if (withdrawalAmount < 0)
            {
                throw new Exception();
            }

            if (DateTime.Now < OpeningDate.AddMonths(Duration))
            {
                throw new Exception("Can't do this operation.");
            }

            PossibilityOfWithdrawal = true;

            if (Amount < withdrawalAmount)
            {
                throw new Exception("Operation is not possible, going over the limit.");
            }

            return Amount - withdrawalAmount;
        }

        public new int Replenishment(int replenishmentAmount)
        {
            if (replenishmentAmount < 0)
            {
                throw new Exception();
            }

            return Amount + replenishmentAmount;
        }
    }
}