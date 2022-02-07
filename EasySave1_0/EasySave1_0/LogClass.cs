using System;
using System.Xml;

namespace EasySave1
{
    public class LogClass
    {

        public static void WriteLog(int workNb)
        {
            var easySaveLogJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "EasySaveLog.json");
            var logPath = easySaveLogJsonPath.Replace(@"bin\Debug\net6.0", "Logs");
            using (StreamWriter sw = new StreamWriter(logPath, true))
            {

                sw.WriteLine("{");
                sw.WriteLine($"\"Name\": \"{Model.WorkList[workNb - 1].Name}\", ");
                sw.WriteLine($"\"FileSource\": \"{SaveAction.FileSrcPath}\", ");
                sw.WriteLine($"\"FileTarget\": \"{SaveAction.FileDestPath}\", ");
                sw.WriteLine($"\"FileSize\": {SaveAction.FileSize}, ");
                sw.WriteLine($"\"FileTransferTime\": {SaveAction.FileTransferTime} ");
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
                    writer.WriteAttributeString("Name", Model.WorkList[workNb - 1].Name);
                    writer.WriteElementString("FileSource", SaveAction.FileSrcPath);
                    writer.WriteElementString("FileTarget", SaveAction.FileDestPath);
                    writer.WriteElementString("FileSize", XmlConvert.ToString(SaveAction.FileSize));
                    writer.WriteElementString("FileTransferTime", XmlConvert.ToString(SaveAction.FileTransferTime));
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
                sw.WriteLine($"\"Name\": \"{Model.WorkList[workNb - 1].Name}\", ");
                sw.WriteLine($"\"SourceDir\": \"{Model.WorkList[workNb - 1].SrcPath}\", ");
                sw.WriteLine($"\"TargetDir\": \"{Model.WorkList[workNb - 1].DestPath}\", ");
                sw.WriteLine($"\"State\": \"{Model.WorkList[workNb - 1].WorkState}\", ");
                sw.WriteLine($"\"TotalFilesToCopy\": {Model.WorkList[workNb - 1].TotalFilesToCopy}, ");
                sw.WriteLine($"\"TotalFilesSize\": {Model.WorkList[workNb - 1].TotalDirSize}, ");
                sw.WriteLine($"\"NbFilesLeftToDo\": {Model.WorkList[workNb - 1].NbFilesLeftToDo} ");
                sw.WriteLine($"\"Progression\": {Model.WorkList[workNb - 1].Progression} ");
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
                    writer.WriteAttributeString("Name", Model.WorkList[workNb - 1].Name);
                    writer.WriteElementString("SourceDir", Model.WorkList[workNb - 1].SrcPath);
                    writer.WriteElementString("TargetDir", Model.WorkList[workNb - 1].DestPath);
                    writer.WriteElementString("State", Model.WorkList[workNb - 1].WorkState.ToString());
                    writer.WriteElementString("TotalFilesToCopy", XmlConvert.ToString(Model.WorkList[workNb - 1].TotalFilesToCopy));
                    writer.WriteElementString("TotalFilesSize", XmlConvert.ToString(Model.WorkList[workNb - 1].TotalDirSize));
                    writer.WriteElementString("NbFilesLeftToDo", XmlConvert.ToString(Model.WorkList[workNb - 1].NbFilesLeftToDo));
                    writer.WriteElementString("Progression", XmlConvert.ToString(Model.WorkList[workNb - 1].Progression));
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
}
