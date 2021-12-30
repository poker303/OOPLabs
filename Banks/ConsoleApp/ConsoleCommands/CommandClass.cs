using System.Collections.Generic;
using Banks.ConsoleSweeps;
using Banks.TransmittedParameters;

namespace Banks.ConsoleCommands
{
    public abstract class CommandClass
    {
        public CommandClass()
        {
            ConsoleApp = new ConsoleAppRecordingMethods();
            CentralBanks = new List<CentralBank>();
            TemplateParametersForCreatingBank = new List<BankParameters>();
            Clients = new List<Client>();

            CreateSubtypes = new CreateSubtypes();
            FindSubtypes = new FindSubtypes();
            OutputSubtypes = new OutputSubtypes();
            ShowSubtypes = new ShowSubtypes();
        }

        protected ConsoleAppRecordingMethods ConsoleApp { get; }
        protected List<CentralBank> CentralBanks { get; }
        protected List<BankParameters> TemplateParametersForCreatingBank { get; }
        protected List<Client> Clients { get; }

        protected CreateSubtypes CreateSubtypes { get; }
        protected FindSubtypes FindSubtypes { get; }
        protected OutputSubtypes OutputSubtypes { get; }
        protected ShowSubtypes ShowSubtypes { get; }
        public virtual void Command()
        {
        }
    }
}