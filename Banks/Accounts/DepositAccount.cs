using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Exceptions;
using Banks.TransmittedParameters;

namespace Banks.Accounts
{
    public class DepositAccount : IAccount
    {
        public DepositAccount(AccountParameters parameters, AmountPercentPair table)
            : base(parameters)
        {
            for (int i = 0; i < table.Percents.Count; i++)
            {
                if (table.Amounts[i] <= parameters.Amount && parameters.Amount < table.Amounts[i + 1])
                {
                    SetPercents(table.Percents[i]);
                }
            }
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

            PossibilityOfWithdrawal = true;
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