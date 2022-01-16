using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BackupsExtra
{
    public class JsonService
    {
        public JsonService(ILoggerService logger)
        {
            BackupJobs = new List<ImprovedBackupJob>();
            Logger = logger;
        }

        private List<ImprovedBackupJob> BackupJobs { get; set; }
        private ILoggerService Logger { get; }

        public void AddBackupJob(ImprovedBackupJob backupJob)
        {
            BackupJobs.Add(backupJob);
        }

        public void SaveJson()
        {
            File.WriteAllText(
                "C:/Users/Алексей/OneDrive/Документы/GitHub/poker303/BackupsExtra/Output.json", JsonConvert.SerializeObject(Logger));
        }

        public void ShowJson()
        {
            BackupJobs = JsonConvert.DeserializeObject<List<ImprovedBackupJob>>(
                File.ReadAllText("C:/Users/Алексей/OneDrive/Документы/GitHub/poker303/BackupsExtra/Output.json"));
        }
    }
}