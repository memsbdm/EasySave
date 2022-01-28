namespace EasySave1
{
    public class LogClass
    {
        public static string logPath = @"EasySaveLog.json";


        public string Name { get; set; }
        public string FileSource { get; set; }
        public string FileTarget { get; set; }
        public long FileSize { get; set; }
        public long FileTransferTime { get; set; }
        public DateTime Time { get; set; }

        public static void CreateLog()
        {
            using (var sw = new StreamWriter(LogClass.logPath, true));
        }
        
        
    }
}

