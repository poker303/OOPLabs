using System;
using Banks.Exceptions;
using Banks.TransmittedParameters;

namespace Banks.Accounts
{
    public class DebitAccount : IAccount
    {
        public DebitAccount(AccountParameters parameters, int percent)
            : base(parameters)
        {
            SetPercents(percent);
        }

        public new double Withdrawal(int withdrawalAmount)
        {
            if (withdrawalAmount < 0)
            {
                throw new AmountException("The amount specified is incorrect, please change it.");
            }

            if (Amount < withdrawalAmount)
            {
                throw new LimitException("Operation is not possible, going over the limit.");
            }

            Amount -= withdrawalAmount;
            return Amount;
        }

        public new double Replenishment(int replenishmentAmount)
        {
            if (replenishmentAmount < 0)
            {
                throw new AmountException("The amount specified is incorrect, please change it.");
            }

            Amount += replenishmentAmount;
            return Amount;
        }
    }
}