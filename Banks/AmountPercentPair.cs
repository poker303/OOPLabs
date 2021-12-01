using System;
using System.Collections.Generic;

namespace Banks
{
    public class AmountPercentPair
    {
        public AmountPercentPair(int percent, int amount)
        {
            Percent = percent;
            Amount = amount;

            // Pair = new KeyValuePair<int, int>(amount, percent);
        }

        public int Percent { get; set; }
        public int Amount { get; set; }

        // public KeyValuePair<int, int> Pair { get; set; }
    }
}