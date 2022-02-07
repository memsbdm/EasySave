using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EasySave1
{
    public class Model
    {
        private static int maxWorksNb = 5;
        private static string saveDataFilePath = @"savedata.json";
        private static Work[] workList;

        public static Work[] WorkList 
        { 
            get { return workList; } 
            set { workList = value; } 
        }

        public static int MaxWorksNb
        {
            get { return maxWorksNb; }
        }
        public static string SaveDataFilePath
        {
            get { return saveDataFilePath; }
        }

        public static void DisplayWorkList()
        {   if(workList == null)
            {
                Console.WriteLine("--- Work file is empty. ---");
            }
        else
            {
                for (int i = 0; i < WorkList.Length; i++)
                {
                    if(workList[i].Name == "")
                    {
                        Console.WriteLine("{0}.Empty",i+1);
                    }
                    else
                    {
                        Console.WriteLine("{0}.{1}",i+1,WorkList[i].Name);
                    }
                }
            }
        }

        public static void InitializeWorkList()
        {   
            if(WorkList == null)
            {
                WorkList = new Work[maxWorksNb];
                for (int i = 0; i < MaxWorksNb; i++)
                    WorkList[i] = new Work("", "", "", WorkType.undefined);
            }

        }

        public static void CreateDataFile()
        {
            using (var sw = new StreamWriter(saveDataFilePath, true));
        }


        public static void SaveDataFile()
        {
            CreateDataFile();
            InitializeWorkList();
            string workData = JsonConvert.SerializeObject(WorkList, Formatting.Indented, new JsonSerializerSettings { });
            File.WriteAllText(SaveDataFilePath, workData);

        }



        public static void ReadDataList()
        {
            string workData = File.ReadAllText(SaveDataFilePath);
            var listData = JsonConvert.DeserializeObject<List<Work>>(workData);
            WorkList = listData.ToArray();
        }


        public static void GetSave()
        {
            if (File.Exists(SaveDataFilePath))
            {
                ReadDataList();
            }
            else
            {
                SaveDataFile();
            }
        }
    }
}