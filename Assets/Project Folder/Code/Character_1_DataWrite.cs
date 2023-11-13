using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class Character_1_DataWrite : MonoBehaviour {

    public bool is_Player;
    public Character_1_Data characterData;
    public List<List<int>> Last_Combo_Code_List;
    public List<string> DName = new List<string>();
    public List<string> DValue = new List<string>();
	public int logNumber = 0;
	public int roundNumber = 0;
    public string timeCode;
    public string filePath;
    public string filename;
    public string recordFilePath;
    public string recordFilename;
    public string allInOneFilePath;
    public string allInOneFileName;
    

    private string recordFileaddress;
    private string allInOneFileaddress;

    public int folderNum = 0;

    // Use this for initialization
    void Start () {

         timeCode = System.DateTime.UtcNow.ToLocalTime().ToString("yyyy_MM_dd_HH_mm_ss");
         filePath = getPath() +"/DataBase/DataWrite/" + folderNum +"/" + (is_Player?"Player/":"Enemy/") ;
         filename =  "Saved_Data_" + timeCode+ ".csv";
        if (!Directory.Exists (filePath)) {
            Directory.CreateDirectory (filePath);
        }
         print("Fileaddress = " + filePath+ filename);
        File.WriteAllText(filePath+ filename, DataToCSV());

        // Log File that record Log and Round Number List
        recordFilePath = getPath() +"/DataBase/" ;
        recordFilename =  "Log" + (is_Player?"Player":"Enemy")+ ".csv";
        if (!Directory.Exists (recordFilePath)) {
            Directory.CreateDirectory (recordFilePath);
        }
        recordFileaddress = recordFilePath+ recordFilename;
        print("recordFileaddress = " + recordFileaddress);
        File.WriteAllText(recordFileaddress, UpdateLog_LogStart(recordFileaddress));

        // All in File that record all Data in each round in one file.
        allInOneFilePath = getPath() +"/DataBase/" ;
        allInOneFileName =  "AllInOne_" + (is_Player?"Player":"Enemy")+ ".csv";
        if (!Directory.Exists (allInOneFilePath)) {
            Directory.CreateDirectory (allInOneFilePath);
        }
        allInOneFileaddress = recordFilePath+ allInOneFileName;
        print("allInOneFileaddress = " + recordFileaddress);
        File.WriteAllText(allInOneFileaddress, UpdateAllInOne(allInOneFileaddress,filename));
        
	}

    public void UpdateData () {

        //Update Data
        characterData.SaveDataToList(false);
        print("UPDATE DATA");
        this.Last_Combo_Code_List = characterData.Last_Combo_Code_List;
        timeCode = System.DateTime.UtcNow.ToLocalTime().ToString("yyyy_MM_dd_HH_mm_ss");
        filePath = getPath() +"/DataBase/DataWrite/" + folderNum +"/" + (is_Player?"Player/":"Enemy/") ;
        filename =  "Saved_Data_" + timeCode
        + ".csv";
        string comboFileName = "Saved_Data_" + timeCode + "_Combo"
        + ".csv";
        if (!Directory.Exists (filePath)) {
 
         Directory.CreateDirectory (filePath);
     }
     
        if(File.Exists(filePath+ filename))
        {
            File.WriteAllText(filePath + filename, DataToCSV());
            File.WriteAllText(filePath + comboFileName, ComboDataToCSV());
        }

        else{
            File.WriteAllText(filePath+ filename, DataToCSV());
            File.WriteAllText(filePath + comboFileName, ComboDataToCSV());
        }

        // Log File that record Log and Round Number List
        File.WriteAllText(recordFileaddress, UpdateLog_RoundStart(recordFileaddress,filename));
        
        // All in File that record all Data in each round in one file.
        File.WriteAllText(allInOneFileaddress, UpdateAllInOne(allInOneFileaddress,filename));

        characterData.ResetALLData();
         
    }
    
    public string DataToCSV()
    {

        var sb = new StringBuilder("Num,DataCode,Value");
        print("DataToCSV         dsafasd ");
        int lineCount = 0;
        foreach (var frame in characterData.DataValueList)
        {
            sb.Append(System.Environment.NewLine)
            .Append(lineCount)
            .Append(',')
            .Append(frame.First.ToString())
            .Append(',')
            .Append(frame.Second.ToString())
            .Append(',');
            lineCount++;
        }

        

        return sb.ToString();
    }
    public string ComboDataToCSV()
    {

        var sb = new StringBuilder("ComboList");
        print("ComboList");
        foreach (var frame in characterData.Last_Combo_Code_List)
        {
            sb.Append(System.Environment.NewLine);
            foreach (var frame2 in frame)
            {
                sb
                .Append(frame2)
                .Append(',');
            }
        }

        return sb.ToString();
    }


    public float[] CSVToDataList(string filename)
    {
        string fileN = filename;
        string[] lines = File.ReadAllLines(fileN);
        print(String.Join(Environment.NewLine, lines));
        float[] dataList = new float[lines.Length-1];
        for (int i = 1; i < (lines.Length - 1); i++)
        {
            string[] ComboCodeS = lines[i].Split(',');
            //print(ComboCodeS[i]);
            dataList[i - 1] = stringToFloat(ComboCodeS[2]);
            //print(stringToInt(ComboCodeS[i]));
        }
        return dataList;
    }


    public float[] ComboCSVToDataList(string filename)
    {
        string fileN = filename;
        string[] lines = File.ReadAllLines(fileN);
        print(String.Join(Environment.NewLine, lines));
        float[] dataList = new float[lines.Length - 1];
        for (int i = 1; i < (lines.Length - 1); i++)
        {
            string[] ComboCodeS = lines[i].Split(',');
            //print(ComboCodeS[i]);
            dataList[i - 1] = stringToFloat(ComboCodeS[i]);
            //print(stringToInt(ComboCodeS[i]));
        }
        return dataList;
    }
    public string UpdateLog_LogStart(string fileaddress)
    {
        /// Get Original File
        var sb = new StringBuilder("");
        if(!File.Exists(fileaddress))
            File.WriteAllText(fileaddress, "");
        if(  File.ReadAllText(fileaddress) == "")
            sb.Append("Log,RoundList");

        string[] lines = File.ReadAllLines(fileaddress);
        int lineNumber = lines.Length;
        int roundNumber = 0;
        logNumber = (lineNumber == 0)?1:lineNumber;
        sb.Append(System.Environment.NewLine)
        .Append(logNumber)
        .Append(',');

        return File.ReadAllText(fileaddress) + sb.ToString();
    }

    public string UpdateLog_RoundStart(string fileaddress, string datafilename){
        roundNumber++;
        var sb = new StringBuilder("");
         sb.Append(datafilename)
        .Append(',');
        return File.ReadAllText(fileaddress) + sb.ToString();
    }

    public string UpdateAllInOne(string fileaddress, string datafilename)
    {
        /// Get Original File
        var sb = new StringBuilder("");
        int numCount = 0;
        if(!File.Exists(fileaddress))
            File.WriteAllText(fileaddress, "");
        if(File.ReadAllText(fileaddress) == ""){
            sb.Append("Log,Round,FileName,");
            foreach (var frame in characterData.DataValueList)
            {
                sb.Append(numCount)
                .Append(',');
                numCount++;
            }
            sb.Append(System.Environment.NewLine);
            sb.Append(" , , ,");
            foreach (var frame in characterData.DataValueList)
            {
                sb.Append(frame.First.ToString())
                .Append(',');
            }
        }
        sb.Append(System.Environment.NewLine);
        sb.Append( logNumber)
        .Append(',')
        .Append(roundNumber)
        .Append(',')
        .Append(datafilename)
        .Append(',');
        foreach (var frame in characterData.DataValueList)
        {
            sb.Append(frame.Second.ToString())
            .Append(',');
        }

        return File.ReadAllText(fileaddress) + sb.ToString();


    }

    

    private float stringToFloat(string a)
    {
        return float.Parse(a,
      System.Globalization.CultureInfo.InvariantCulture);
    }

    private string getPath()
    {
    #if UNITY_EDITOR
        return Application.dataPath + "/Data";
        //"Participant " + "   " + DateTime.Now.ToString("dd-MM-yy   hh-mm-ss") + ".csv";
    #elif UNITY_ANDROID
        return Application.persistentDataPath;
    #elif UNITY_IPHONE
        return Application.persistentDataPath+"";
    #else
        return Application.dataPath +"";
    #endif
    }
}
