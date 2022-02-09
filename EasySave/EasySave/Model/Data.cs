namespace EasySave.Model;
using Newtonsoft.Json;

public class Data
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
    
    
    
    public static void InitializeWorkList()
    {   
        if(WorkList == null)
        {
            WorkList = new Work[maxWorksNb];
            for (int i = 0; i < MaxWorksNb; i++)
                WorkList[i] = new Work("", "", "", Work.WorkType.undefined);
        }
    }
    
    
    public static void CreateDataFile()
    {
        // Trick to create if not exist (to avoid create() function problems)
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
            ReadDataList();
        else
            SaveDataFile();
    }
}