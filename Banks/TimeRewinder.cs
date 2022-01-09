using System;
using System.Collections.Generic;
using Banks.Accounts;

namespace Banks
{
    public class TimeRewinder
    {
        public TimeRewinder(string name)
        {
            Name = name;
        }

        private string Name { get; }

        public DateTime RewindTime(DateTime startDate, TimeSpan time)
        {
            startDate += time;
            return startDate;
        }

        public double AccountAfterTime(IAccount account, DateTime date)
        {
            double tempPercent = account.Percent / 36500d;
            int numberOfElapsedDays = (date - account.OpeningDate).Duration().Days;
            for (int day = 0; day < numberOfElapsedDays; day++)
            {
                account.Amount += account.Amount * tempPercent;
                if (account.Amount < 0)
                {
                    account.Amount -= account.Commission;
                }
            }

            return account.Amount;
        }
    }
}