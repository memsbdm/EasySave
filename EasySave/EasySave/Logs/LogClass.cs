using EasySave.ViewModel;
using EasySave.Model;
namespace EasySave.Logs;
using System.Xml;
public class LogClass
{
     public static void WriteLog(int workNb)
        {
            var easySaveLogJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "EasySaveLog.json");
            var logPath = easySaveLogJsonPath.Replace(@"bin\Debug\net6.0", "Logs");
            using (StreamWriter sw = new StreamWriter(logPath, true))
            {

                sw.WriteLine("{");
                sw.WriteLine($"\"Name\": \"{Data.WorkList[workNb - 1].Name}\", ");
                sw.WriteLine($"\"FileSource\": \"{WorkAction.FileSrcPath}\", ");
                sw.WriteLine($"\"FileTarget\": \"{WorkAction.FileDestPath}\", ");
                sw.WriteLine($"\"FileSize\": {WorkAction.FileSize}, ");
                sw.WriteLine($"\"FileTransferTime\": {WorkAction.FileTransferTime} ");
                sw.WriteLine($"\"time\": {GetTimestamp(DateTime.Now)} ");
                sw.WriteLine("}");
            }
        }

        public static void WriteLogXML(int workNb)
        {
            var easySaveLogXmlPath = Path.Combine(Directory.GetCurrentDirectory(), "EasySaveLog.xml");
            var logPath = easySaveLogXmlPath.Replace(@"bin\Debug\net6.0", "Logs");
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true
            };

            using (var sw = new StreamWriter(logPath, true))
            {
                using (XmlWriter writer = XmlWriter.Create(sw, settings))
                {
                    writer.WriteStartElement("Save");
                    writer.WriteAttributeString("Name", Data.WorkList[workNb - 1].Name);
                    writer.WriteElementString("FileSource", WorkAction.FileSrcPath);
                    writer.WriteElementString("FileTarget", WorkAction.FileDestPath);
                    writer.WriteElementString("FileSize", XmlConvert.ToString(WorkAction.FileSize));
                    writer.WriteElementString("FileTransferTime", XmlConvert.ToString(WorkAction.FileTransferTime));
                    writer.WriteElementString("time", GetTimestamp(DateTime.Now));
                    writer.WriteEndElement();
                }
                sw.WriteLine("\n");
            }

        }

        public static void WriteStateLog(int workNb)
        {
            var easySaveStateLogJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "EasySaveStateLog.json");
            var logPath = easySaveStateLogJsonPath.Replace(@"bin\Debug\net6.0", "Logs");
            using (StreamWriter sw = new StreamWriter(logPath, true))
            {

                sw.WriteLine("{");
                sw.WriteLine($"\"Name\": \"{Data.WorkList[workNb - 1].Name}\", ");
                sw.WriteLine($"\"SourceDir\": \"{Data.WorkList[workNb - 1].SrcPath}\", ");
                sw.WriteLine($"\"TargetDir\": \"{Data.WorkList[workNb - 1].DestPath}\", ");
                sw.WriteLine($"\"State\": \"{Data.WorkList[workNb - 1].WorkState}\", ");
                sw.WriteLine($"\"TotalFilesToCopy\": {Data.WorkList[workNb - 1].TotalFilesToCopy}, ");
                sw.WriteLine($"\"TotalFilesSize\": {Data.WorkList[workNb - 1].TotalDirSize}, ");
                sw.WriteLine($"\"NbFilesLeftToDo\": {Data.WorkList[workNb - 1].NbFilesLeftToDo} ");
                sw.WriteLine($"\"Progression\": {Data.WorkList[workNb - 1].Progression} ");
                sw.WriteLine("}");
            }
        }

        public static void WriteStateLogXML(int workNb)
        {
            var easySaveStateLogXmlPath = Path.Combine(Directory.GetCurrentDirectory(), "EasySaveStateLog.xml");
            var logPath = easySaveStateLogXmlPath.Replace(@"bin\Debug\net6.0", "Logs");
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true
            };
            using (var sw = new StreamWriter(logPath, true))
            {

                using (XmlWriter writer = XmlWriter.Create(sw, settings))
                {
                    writer.WriteStartElement("Save");
                    writer.WriteAttributeString("Name", Data.WorkList[workNb - 1].Name);
                    writer.WriteElementString("SourceDir", Data.WorkList[workNb - 1].SrcPath);
                    writer.WriteElementString("TargetDir", Data.WorkList[workNb - 1].DestPath);
                    writer.WriteElementString("State", Data.WorkList[workNb - 1].WorkState.ToString());
                    writer.WriteElementString("TotalFilesToCopy", XmlConvert.ToString(Data.WorkList[workNb - 1].TotalFilesToCopy));
                    writer.WriteElementString("TotalFilesSize", XmlConvert.ToString(Data.WorkList[workNb - 1].TotalDirSize));
                    writer.WriteElementString("NbFilesLeftToDo", XmlConvert.ToString(Data.WorkList[workNb - 1].NbFilesLeftToDo));
                    writer.WriteElementString("Progression", XmlConvert.ToString(Data.WorkList[workNb - 1].Progression));
                    writer.WriteEndElement();
                }
                sw.WriteLine("\n");
            }

        }

        public static void WriteLogger(int workNb)
        {
            WriteLog(workNb);
            WriteLogXML(workNb);
        }

        public static void WriteStateLogger(int workNb)
        {
            WriteStateLog(workNb);
            WriteStateLogXML(workNb);
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("dd/MM/yyyy HH:mm:ss");
        }
}