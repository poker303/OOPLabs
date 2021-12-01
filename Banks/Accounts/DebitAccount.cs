using System;

namespace Banks.Accounts
{
    public class DebitAccount : IAccount
    {
        public DebitAccount(int number, int amount, int percent, bool possibilityOfWithdrawal)
            : base(amount, possibilityOfWithdrawal)
        {
            Name = "Deb" + number;
            Percent = percent;
        }

        public string Name { get; set; }

        public int Percent { get; set; }

        public new int Withdrawal(int withdrawalAmount)
        {
            if (withdrawalAmount < 0)
            {
                throw new Exception();
            }

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