using System;

namespace EasySave1
{
    public class LogClass
    {

        public static void WriteLog(int workNb)
        {   
            using(StreamWriter sw = new StreamWriter("EasySaveLog.json", true))
            {

                    sw.WriteLine("{");
                    sw.WriteLine($"\"Name\": \"{Model.WorkList[workNb-1].Name}\", ");
                    sw.WriteLine($"\"FileSource\": \"{SaveAction.FileSrcPath}\", ");
                    sw.WriteLine($"\"FileTarget\": \"{SaveAction.FileDestPath}\", ");
                    sw.WriteLine($"\"FileSize\": {SaveAction.FileSize}, ");
                    sw.WriteLine($"\"FileTransferTime\": {SaveAction.FileTransferTime} ");
                    sw.WriteLine($"\"time\": {GetTimestamp(DateTime.Now)} ");
                    sw.WriteLine("}");
            }
        }

        public static void WriteStateLog(int workNb)
        {
            using (StreamWriter sw = new StreamWriter("EasySaveStateLog.json", true))
            {

                sw.WriteLine("{");
                sw.WriteLine($"\"Name\": \"{Model.WorkList[workNb - 1].Name}\", ");
                sw.WriteLine($"\"SourceDir\": \"{Model.WorkList[workNb - 1].SrcPath}\", ");
                sw.WriteLine($"\"TargetDir\": \"{Model.WorkList[workNb - 1].DestPath}\", ");
                sw.WriteLine($"\"State\": \"{Model.WorkList[workNb - 1].WorkState}\", ");
                sw.WriteLine($"\"TotalFilesToCopy\": {Model.WorkList[workNb - 1].TotalFilesToCopy}, ");
                sw.WriteLine($"\"TotalFilesSize\": {Model.WorkList[workNb - 1].TotalDirSize}, ");
                sw.WriteLine($"\"NbFilesLeftToDo\": {Model.WorkList[workNb-1].NbFilesLeftToDo} ");
                sw.WriteLine($"\"Progression\": {Model.WorkList[workNb - 1].Progression} ");
                sw.WriteLine("}");
            }
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("dd/MM/yyyy HH:mm:ss");
        }


    }
}
