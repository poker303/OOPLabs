using System.Collections.Generic;

namespace Banks.TransmittedParameters
{
    public class BankParameters
    {
        public BankParameters(string name, AmountPercentPair table, int reliabilityAmount, int debitPercent, int debitCommission, int creditPercent, int creditCommission,  int depositCommission, int creditLimit)
        {
            Name = name;
            Table = table;
            ReliabilityAmount = reliabilityAmount;
            DebitPercent = debitPercent;
            DebitCommission = debitCommission;
            CreditPercent = creditPercent;
            CreditCommission = creditCommission;
            DepositCommission = depositCommission;
            CreditLimit = creditLimit;
        }

        public string Name { get; set; }

        public AmountPercentPair Table { get; set; }

        public int ReliabilityAmount { get; set; }

        public int DebitPercent { get; set; }

        public int DebitCommission { get; set; }

        public int CreditPercent { get; set; }

        public int CreditCommission { get; set; }

        public int DepositCommission { get; set; }

        public int CreditLimit { get; set; }
    }
}