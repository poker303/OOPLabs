using System;

namespace Banks.Accounts
{
    public class CreditAccount : IAccount
    {
        public CreditAccount(int number, int amount, int limit, int commission, bool possibilityOfWithdrawal)
            : base(amount, possibilityOfWithdrawal)
        {
            Name = "Cre" + number;
            Limit = limit;
            Commission = commission;
        }

        public string Name { get; set; }

        public int Limit { get; set; }
        public int Commission { get; set; }

        public new int Withdrawal(int withdrawalAmount)
        {
            if (withdrawalAmount < 0)
            {
                throw new Exception();
            }

            if (Math.Abs(Amount - withdrawalAmount) > Limit)
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