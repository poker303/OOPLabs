using System.Collections.Generic;
using System.Linq;
using Banks.TransmittedParameters;

namespace Banks.ConsoleSweeps
{
    public class CreateSubtypes
    {
        public CentralBank CreateCentralBank(List<CentralBank> centralBanks, string centralBankName, string centralBankCountry)
        {
            var centralBank = new CentralBank(centralBankName, centralBankCountry);
            centralBanks.Add(centralBank);
            return centralBank;
        }

        public void CreateBank(List<BankParameters> bankParameters, List<CentralBank> centralBanks, string centralBankName, int bankTemplateNumber)
        {
            foreach (CentralBank cB in centralBanks.Where(cB => cB.Name == centralBankName))
            {
                cB.CreateBank(bankParameters[bankTemplateNumber]);
            }
        }
    }
}