using System;
using Banks.TransmittedParameters;

namespace Banks.Accounts
{
    public class IAccount
    {
        protected IAccount(AccountParameters parameters)
        {
            Number = parameters.Numder;
            if (parameters.Amount < 0)
            {
                throw new Exception();
            }

            Duration = parameters.Duration;
            ReliabilityAmount = parameters.ReliabilityAmount;
            CloseDate = parameters.OpeningDate.AddDays(parameters.Duration);
            Reliability = parameters.Reliability;
            Commission = parameters.Commission;
            Amount = parameters.Amount;
            OpeningDate = parameters.OpeningDate;
            PossibilityOfWithdrawal = parameters.PossibilityOfWithdrawal;
        }

        public int ReliabilityAmount { get; set; }

        public DateTime CloseDate { get; }

        public float Percent { get; set; }
        public int Commission { get; set; }

        public bool Reliability { get; set; }
        public int Number { get; }
        public double Amount { get; set; }
        public DateTime OpeningDate { get; }
        public bool PossibilityOfWithdrawal { get; set; }

        private int Duration { get; }

        public double Withdrawal(int withdrawalAmount)
        {
            if (withdrawalAmount < 0)
            {
                throw new Exception();
            }

            Amount -= withdrawalAmount;
            return Amount;
        }

        public double Replenishment(int replenishmentAmount)
        {
            if (replenishmentAmount < 0)
            {
                throw new Exception();
            }

            Amount += replenishmentAmount;
            return Amount;
        }

        protected float SetPercents(float newPercent)
        {
            Percent = newPercent;
            return Percent;
        }
    }
}