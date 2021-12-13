using System;
using System.Collections.Generic;

namespace Banks
{
    public class AmountPercentPair
    {
        public AmountPercentPair(List<int> boundarySums, List<int> percents)
        {
            Percents = percents;
            Amounts = boundarySums;
        }

        public List<int> Percents { get; }
        public List<int> Amounts { get; }
    }
}