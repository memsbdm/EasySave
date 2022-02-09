namespace EasySave.Model;
using Vue;
public class Work
{
    public string Name { get; set; }
    public string SrcPath { get; set; }
    public string DestPath { get; set; }
    public string CreationTime { get; set; }
    public WorkStateEnum WorkState { get; set; }
    public WorkType Type { get; set; }
    public int NbFilesLeftToDo { get; set; }
    public double Progression { get; set; }
    public int TotalFilesToCopy { get; set; }
    public long TotalDirSize { get; set; }
    
    public enum WorkType
    {
        undefined,
        complete,
        differential
    }

    public enum WorkStateEnum
    {
        undefined,
        inactive,
        active,
        ended
    }
    public Work(string _name, string _srcPath, string _destPath, WorkType _type)
    {
        Name = _name;
        SrcPath = _srcPath;
        DestPath = _destPath;
        CreationTime = "";
        WorkState = WorkStateEnum.undefined;
        Type = _type;
        TotalFilesToCopy = 0;
        TotalDirSize = 0;
    }
    
    public void SetWorkName()
    {
        var name = "";
        WorkInput.NameInput();
        while (name == "")
            name = Console.ReadLine()!;
        this.Name = name;
    }
    
    public void SetWorkSrc()
    {
        WorkInput.SrcInput();
        var path = Console.ReadLine()!;
        while (!Directory.Exists(path))
        {
            WorkInput.PathInputError();
            path = Console.ReadLine()!;
        }
        this.SrcPath = path;
    }
    
    public void SetWorkTarget()
    {
        WorkInput.TargetInput();
        var path = Console.ReadLine()!;
        try
        {
            while ((!Directory.Exists(path)))
                Directory.CreateDirectory(path);
        }
        catch (Exception e)
        {
            WorkInput.PathInputError();
            path = Console.ReadLine()!;
        }
        this.DestPath = path;
    }
    
    public void SetWorkType()
    {
        WorkInput.WorkTypeInput();
        var type = Console.ReadLine()!;
        WorkType t = WorkType.undefined;
        while (type != "c" && type != "d")
        {
            WorkInput.WrongInput();
            type = Console.ReadLine()!;
        }
        if(type == "c")
            t = WorkType.complete;
        else
            t = WorkType.differential;
        this.Type = t;
    }
    
    public static int FilesToCopy(DirectoryInfo _directory_srcPath)
    {
        int TotaFilesNb = 0;
        foreach (FileInfo ffinfo in _directory_srcPath.GetFiles())
            TotaFilesNb++;
        foreach (DirectoryInfo subDir in _directory_srcPath.GetDirectories())
            TotaFilesNb += FilesToCopy(subDir);

        return TotaFilesNb;
    }

    public static long DirectorySize(DirectoryInfo _directory_srcPath)
    {
        long TotalDirSize= 0 ;
        foreach (FileInfo finfo in _directory_srcPath.GetFiles())
            TotalDirSize += finfo.Length;
        foreach (DirectoryInfo subDir in _directory_srcPath.GetDirectories())
            TotalDirSize += DirectorySize(subDir);

        return TotalDirSize;
    }
    
    public static void GetProgression(int workNb)
    {
        Data.WorkList[workNb - 1].Progression = (double)( 100 - (((double)(Data.WorkList[workNb - 1].NbFilesLeftToDo) / (Data.WorkList[workNb - 1].TotalFilesToCopy)) * 100));
    }
    
    public static int GetDifferentFilesNb(int workNb)
    {
        int nbFiles = 0;
        foreach (var filePath in Directory.GetFiles(Data.WorkList[workNb - 1].SrcPath, "*.*", SearchOption.AllDirectories))
        {
            if (!File.Exists(filePath.Replace(Data.WorkList[workNb - 1].SrcPath, Data.WorkList[workNb - 1].DestPath)))
                nbFiles++;
            else
            {
                if (File.GetLastWriteTime(filePath) != File.GetLastWriteTime(filePath.Replace(Data.WorkList[workNb - 1].SrcPath, Data.WorkList[workNb - 1].DestPath)))
                    nbFiles++;
            }
        }
        return nbFiles;
    }
    
    
    public static void GetNbLeftToDo(int workNb)
    {
        if (Data.WorkList[workNb - 1].Type == Work.WorkType.complete)
            Data.WorkList[workNb - 1].NbFilesLeftToDo = Data.WorkList[workNb - 1].TotalFilesToCopy;
        
        else if(Data.WorkList[workNb -1].Type == Work.WorkType.differential)
            Data.WorkList[workNb - 1].NbFilesLeftToDo = GetDifferentFilesNb(workNb);
    }
}