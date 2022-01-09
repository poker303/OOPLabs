using System;
using Banks.TransmittedParameters;

namespace Banks.Accounts
{
    public class CreditAccount : IAccount
    {
        public CreditAccount(AccountParameters parameters, int limit, int percent)
            : base(parameters)
        {
            SetPercents(percent);
            Limit = limit;
        }

        public int Limit { get; set; }

        public new double Withdrawal(int withdrawalAmount)
        {
            if (withdrawalAmount < 0)
            {
                throw new Exception();
            }

            if (Math.Abs(Amount - withdrawalAmount) > Limit)
            {
                throw new Exception("Operation is not possible, going over the limit.");
            }

            Amount -= withdrawalAmount;
            return Amount;
        }

        public new double Replenishment(int replenishmentAmount)
        {
            if (replenishmentAmount < 0)
            {
                throw new Exception();
            }

            Amount += replenishmentAmount;
            return Amount;
        }
    }
}