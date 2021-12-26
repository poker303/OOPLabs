using System;

namespace Banks.TransmittedParameters
{
    public class AccountParameters
    {
        public AccountParameters(int number, double amount, int commission, bool possibilityOfWithdrawal, DateTime openingDate, int duration, bool reliability, int reliabilityAmount)
        {
            Numder = number;
            Amount = amount;
            Commission = commission;
            PossibilityOfWithdrawal = possibilityOfWithdrawal;
            OpeningDate = openingDate;
            Duration = duration;
            Reliability = reliability;
            ReliabilityAmount = reliabilityAmount;
        }

        public int ReliabilityAmount { get; set; }

        public int Numder { get; set; }

        public double Amount { get; set; }

        public int Commission { get; set; }

        public bool PossibilityOfWithdrawal { get; set; }

        public DateTime OpeningDate { get; set; }

        public int Duration { get; set; }

        public bool Reliability { get; set; }
    }
}