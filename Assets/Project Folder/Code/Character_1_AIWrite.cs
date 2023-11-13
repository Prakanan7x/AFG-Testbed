using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;


public class Character_1_AIWrite : MonoBehaviour {

    public bool is_Player;
    public Character_Stat characterStat;
    public Character_1_AIControl characterAIControl;
    public Character_1_Data characterData;
    public Character_1_DataWrite characterDataWrite;
    public Dynamic_Script characterDynamicScript;
    public int[,] Last_Combo_Code_List;
    public List<string> DName = new List<string>();
    public List<string> DValue = new List<string>();
    public string lastSave_LoadAIString;

    public int folderNum = 0;
	
    
    // Use this for initialization
    void Start () {
        string filePath = getPath() +"/DataBase/AIWrite/"  + folderNum +"/"+ (is_Player?"Player":"Enemy") ;
        string filename = (is_Player?"/P_":"/E_") +  "PatSet_L" 
		+characterDataWrite.logNumber
		+"_R"
		+characterDataWrite.roundNumber
		+"_T"
		+ System.DateTime.UtcNow.ToLocalTime().ToString("yyyy_MM_dd_HH_mm_ss")
        + ".csv";

      

        

    if (!Directory.Exists (filePath)) {
 
         Directory.CreateDirectory (filePath);
     }
        
        //File.WriteAllText(filePath+ filename, DataToCSV());
        //CSVToData(filePath+ filename);
       
	}

    public void LoadAISampleFile(){
        string filePathWTest = getPath() +"/DataBase/AIWrite/"  + folderNum +"/"+ (is_Player?"Player/":"Enemy/") ;
        string filenameWTest = "TestEnemyPatternSet.csv";
         CSVToData(filePathWTest+ filenameWTest);
    }

    public void UpdateData () {
        print("WriteAI");
        string filePath = getPath() +"/DataBase/AIWrite/" + (is_Player?"Player":"Enemy") ;
        string filename = (is_Player?"/P_":"/E_") +  "PatSet_L" 
		+characterDataWrite.logNumber
		+"_R"
		+characterDataWrite.roundNumber
		+"_T"
		+ System.DateTime.UtcNow.ToLocalTime().ToString("yyyy_MM_dd_HH_mm_ss")
        + ".csv";
        if (!Directory.Exists (filePath)) {
 
         Directory.CreateDirectory (filePath);
     }
        print("WriteAI Path = " + filePath );
                print("WriteAI Name = " + filename );
        

        File.WriteAllText(filePath+ filename, DataToCSV());
        print("Write AI Done");


            
 /*
        StreamWriter writer = new StreamWriter(filePath+ "Saved_Data.csv");
 
        writer.WriteLine(DataToCSV());

 

        writer.Flush();
        writer.Close();
*/

         
    }

    private void increaseRoundCount(){
        
    }

    public string DataToCSV_Combo(int ComboType){ /// 0 = Standard, 1 = React, 2 = RV
        var sbCombo = new StringBuilder("");
         int ComboCount = 0;
        //sbCombo.Append(System.Environment.NewLine);
        foreach (var frame in 
        (ComboType == 2)? (characterAIControl.CurrentAI.RevengeCombo_Libery):
        ((ComboType == 1)? (characterAIControl.CurrentAI.ReactCombo_Libery): 
        characterAIControl.CurrentAI.Combo_Libery) 
        ){
                if(frame == null) break;
            // print("Combo Count = " + ComboCount);
                sbCombo.Append(System.Environment.NewLine)
                .Append(ComboCount)
                .Append(',');
                int ActionCodeCount = 0;
                for(int i = 0; i<  frame.Get_ComboPattern().Length ; i++){
                    if((frame.Get_ComboPattern())[i]
                    != null){
                        sbCombo.Append((frame.Get_ComboPattern())[i])
                        .Append(',');
                    }
                }
                sbCombo.Append("X");
                ComboCount++;         
        sbCombo.Append(System.Environment.NewLine)
        .Append("Z");    
        }
        return sbCombo.ToString();
    }

    public string DataToCSV()
    {
        /*
        List<Combo> Combo_Libery = characterAIControl.CurrentAI.Combo_Libery;
        List<Pattern> Pattern_Libery = CurrentAI.Pattern_Libery;
        List<React_Command> React_Command_Libery = CurrentAI.React_Command_Libery;
        List<Pattern> Revenge_Command_Libery = CurrentAI.Revenge_Command_Libery;
        PatternSet curPatternSet = CurrentAI.curPatternSet;;
        float RV = CurrentAI.RV;
        */

        var sb = new StringBuilder("");
        int lineCount = 0;

        /////////////// Weight Part/////////////////
        sb.Append("Weight");
        ///// WORK HERE
        for(int i = 0; i < characterDynamicScript.currentWeightList.Count ; i++){
            sb.Append(System.Environment.NewLine)
            .Append(i)
            .Append(',')
            .Append(characterDynamicScript.WScodePriorityList[(characterDynamicScript.currentWeightList)[i]].code);

        }

        sb.Append(System.Environment.NewLine)
        .Append("Z");
        /////////////////////////////// Standard Pattern Part ////////////////////////////////
        /////////////// Combo Part////////////////////////
        sb.Append(System.Environment.NewLine);
        sb.Append("Combo");
        sb.Append(DataToCSV_Combo(0));

        ////////////////////////////////////////////////////

        ///////////////// Pattern Part////////////////////
        sb.Append(System.Environment.NewLine);
        sb.Append("Pattern");
        //sb.Append(System.Environment.NewLine);
        int PatternCount = 0;
         foreach (var frame in characterAIControl.CurrentAI.Pattern_Libery)
        {
            if(frame == null) break;
            sb.Append(System.Environment.NewLine)
            .Append(PatternCount)
            .Append(',')
            .Append("P");
            int PatternL1Count = 0;
            foreach(var code1 in ((characterAIControl.CurrentAI.Pattern_Libery)[PatternCount]).Get_movePattern()){
                int PatternL2Count = 0;
                sb.Append(System.Environment.NewLine)
                .Append(PatternL1Count);
                foreach(var code2 in (((characterAIControl.CurrentAI.Pattern_Libery)[PatternCount]).Get_movePattern())[PatternL1Count]  ){
                    sb.Append(',')
                    .Append(code2);
                    PatternL2Count++;
                }
                PatternL1Count++;
            }
            sb.Append(System.Environment.NewLine);
            sb.Append("X");
            PatternCount++;
        }
        sb.Append(System.Environment.NewLine)
        .Append("Z");
        ////////////////////////////////////////////////////
        //////
        ///
        
        /////////////// ReactCombo Part////////////////////////
        sb.Append(System.Environment.NewLine);
        sb.Append("ReactCombo");
        sb.Append(DataToCSV_Combo(1));
        ////////////////////////////////////////////////////

        ///////////////// ReactPattern Part////////////////////
        sb.Append(System.Environment.NewLine);
        sb.Append("ReactPattern");
        //sb.Append(System.Environment.NewLine);
        int ReactPatternCount = 0;
         foreach (var frame in characterAIControl.CurrentAI.React_Command_Libery)
        {
            if(frame == null) break;
            sb.Append(System.Environment.NewLine)
            .Append(ReactPatternCount)
            .Append(',')
            .Append("RP");

            sb.Append(System.Environment.NewLine)
            .Append("C")
            .Append(',')
            .Append((characterAIControl.CurrentAI.React_Command_Libery)[ReactPatternCount].Get_reaction_Speed());
            int ReactPatternEActionCount = 0;
            foreach(var code1 in ((characterAIControl.CurrentAI.React_Command_Libery)[ReactPatternCount]).Get_oppo_Action()){
                sb.Append(code1)
                .Append(',');

                ReactPatternEActionCount++;
            }
            sb.Append("C");
            //sb.Append(System.Environment.NewLine);
            int PatternL1Count = 0;
            print((((characterAIControl.CurrentAI.React_Command_Libery)[ReactPatternCount]).Get_patternList()));
            foreach(var code1 in (((characterAIControl.CurrentAI.React_Command_Libery)[ReactPatternCount]).Get_patternList())
            [ReactPatternCount].Get_movePattern()  ){

                if(code1 == null) break;
                int PatternL2Count = 0;
                sb.Append(System.Environment.NewLine)
                .Append(PatternL1Count);
                foreach(var code2 in ((((characterAIControl.CurrentAI.React_Command_Libery)[ReactPatternCount]).Get_patternList())[ReactPatternCount].Get_movePattern())[PatternL1Count]  ){
                    if(code2 == null) break;
                    sb.Append(',')
                    .Append(code2);
                    PatternL2Count++;
                }
                ReactPatternCount++;
            }
            sb.Append(System.Environment.NewLine);
            sb.Append("X");
            PatternCount++;
        }
        sb.Append(System.Environment.NewLine)
        .Append("Z");
        ////////////////////////////////////////////////////
        //////
        
        /////////////// RevengeCombo Part////////////////////////
        sb.Append(System.Environment.NewLine);
        sb.Append("RevengeCombo");
        sb.Append(DataToCSV_Combo(1));
        ////////////////////////////////////////////////////

        ///////////////// Revenge Part////////////////////
        sb.Append(System.Environment.NewLine);
        sb.Append("RevengePattern");
        //sb.Append(System.Environment.NewLine);
        int RevengePatternCount = 0;
         foreach (var frame in characterAIControl.CurrentAI.Revenge_Command_Libery)
        {
            if(frame == null) break;
            sb.Append(System.Environment.NewLine)
            .Append(RevengePatternCount)
            .Append(',')
            .Append("RVP");

            sb.Append(System.Environment.NewLine)
            .Append("V")
            .Append(',')
            .Append(characterStat.RevengeValue);
            int PatternL1Count = 0;
           foreach(var code1 in ((characterAIControl.CurrentAI.Revenge_Command_Libery)[RevengePatternCount]).Get_movePattern()){
            if(code1 == null) break;
                int PatternL2Count = 0;
                sb.Append(System.Environment.NewLine)
                .Append(PatternL1Count);
                foreach(var code2 in (((characterAIControl.CurrentAI.Revenge_Command_Libery)[RevengePatternCount]).Get_movePattern())[PatternL1Count]  ){
                    if(code2 == null) break;
                    sb.Append(',')
                    .Append(code2);
                    PatternL2Count++;
                }
                PatternL1Count++;
            }
            sb.Append(System.Environment.NewLine);
            sb.Append("X");
            RevengePatternCount++;
        }
        sb.Append(System.Environment.NewLine)
        .Append("Z");
        ////////////////////////////////////////////////////

        ////////////////////Pattern Set /////////////////////////////
        sb.Append(System.Environment.NewLine);
        sb.Append("PatternSet");
        sb.Append(System.Environment.NewLine);
        // WIP ERROR NEED FIXING VVVV
        if(characterAIControl.CurrentAI.curPatternSet.Get_patternListCode() != null){
            foreach (var frame in characterAIControl.CurrentAI.curPatternSet.Get_patternListCode())
            {
                if(frame == null) break;
                    sb.Append(frame)
                    .Append(',');
            }
        }
        sb.Append("X");
        sb.Append(System.Environment.NewLine)
        .Append("Z");
        sb.Append(System.Environment.NewLine);


        ////////////////////////////////////////////////
        lastSave_LoadAIString = "Save\n" + sb.ToString();

        return lastSave_LoadAIString;
    }
    
    public void CSVToData(string filename){
        string fileN = filename;
        lastSave_LoadAIString = "Load/n" + File.ReadAllText(fileN)  ;
        string[] lines = File.ReadAllLines(fileN);
        print(String.Join(Environment.NewLine, lines));
        int readMode = 0; //1 = Combo, 2 = Pattern, 3 = ReactCombo, 4 = ReactPattern, 5 = RevengeCombo, 6 = RevengePattern, 7 = PatternSet
        // Regular Pattern
        List<int[]> Combo_Libery_Code = new List<int[]>();
        int Current_Combo_Number = 0;

        List<List<float[]>> Pattern_Libery_Code = new List<List<float[]>>();
        int Current_Pattern_Number = 0;

        // React Pattern
        List<int[]> ReactCombo_Libery_Code = new List<int[]>();
        int Current_ReactCombo_Number = 0;

        List<List<float[]>> React_Command_Libery_Code = new List<List<float[]>>();
        int Current_ReactPattern_Number = 0;
        List<float> React_Command_Libery_Time = new List<float>();
        List<int[]> React_Command_Libery_Action_Code = new List<int[]>();

        // Revenge Pattern
        List<int[]> RevengeCombo_Libery_Code = new List<int[]>();
        int Current_RevengeCombo_Number = 0;

        float revengeValue = 5;
        int Current_RevPattern_Number = 0;
        List<float[]> Revenge_Command_Libery_Code = new List<float[]>();
        
        List<int> CurrentPatternSet_Code = new List<int>();

        foreach(var frame in lines){
            string[] TextList = frame.Split(',');

            if(TextList[0] == "Combo"){
                readMode = 1;
            }
            else if(TextList[0] == "Pattern"){
                readMode = 2;
            }
            else if(TextList[0] == "ReactCombo"){
                readMode = 3;
            }
            else if(TextList[0] == "ReactPattern"){
                readMode = 4;
            }
            else if(TextList[0] == "RevengeCombo"){
                readMode = 5;
            }
            else if(TextList[0] == "RevengePattern"){
                readMode = 6;
            }
            else if(TextList[0] == "PatternSet"){
                readMode = 7;
            }
            else if (TextList[0] == "Z" || TextList[0] == "Weight"){
                readMode = 0;
            }
            else{
                if(readMode == 1 || readMode == 3 || readMode == 5){
                    /*
                    string firstCharacter = TextList[0];
                    string secondCharacter = TextList[1];
                    
                    if(firstCharacter == "X"){
                        Current_Combo_Number = 0;
                    }
                    else if(secondCharacter == "C"){
                        string[] collection = frame.Split(',');
                        Current_Combo_Number = stringToInt(collection[0]);
                    }
                    else{ */
                        
                        string comboList = frame;
                        comboList = comboList.Remove(comboList.Length -1,1);

                        string[] ComboCodeS = comboList.Split(',');
                        int[] ComboCodeI = new int[TextList.Length-2];
                        for(int i = 1 ; i < (TextList.Length-1) ; i++){
                            if (ComboCodeS[i] == "X" || ComboCodeS[i] == "" ) break;
                            //print(ComboCodeS[i]);
                            ComboCodeI[i-1] = stringToInt(ComboCodeS[i]);
                            //print(stringToInt(ComboCodeS[i]));
                       /* }
                       */
                    //int[] ComboCodeI = Array.ConvertAll(ComboCodeS, s => stringToInt(s));
                        if(readMode == 1)           Combo_Libery_Code.Add(ComboCodeI);
                        else if (readMode == 3)     ReactCombo_Libery_Code.Add(ComboCodeI);
                        else if (readMode == 5)     RevengeCombo_Libery_Code.Add(ComboCodeI);
                    }
                    
                }
                else if (readMode == 2){
                    
                    string firstCharacter = TextList[0];
                    string secondCharacter = "";
                    if(TextList.Length > 1) secondCharacter = TextList[1];
                    //print("LastCharacter = " + lastCharacter);
                    if(firstCharacter == "X"){
                        Current_Pattern_Number = 0;
                    }
                    else if(secondCharacter == "P"){
                        string[] collection = frame.Split(',');
                        Current_Pattern_Number = stringToInt(collection[0]);
                    }
                    else{
                        string[] collection = frame.Split(',');
                        /*
                        (Pattern_Libery_Code[Current_Pattern_Number])[stringToInt(collection[0])] 
                        = new float[] {stringToFloat(collection[1]),stringToFloat(collection[2]),stringToFloat(collection[3]),
                        stringToFloat(collection[4]),stringToFloat(collection[5]),stringToFloat(collection[6])};
                        */
                        if(Pattern_Libery_Code.Count <= Current_Pattern_Number){
                            Pattern_Libery_Code.Add(new List<float[]>());
                        }
                        /*
                        print(collection);
                        print(collection[1]);
                        print(collection[1] + " " + collection[2] + " " + collection[3] + " " + 
                        collection[4] + " " + collection[5] + " " + collection[6]);
                        */
                        
                        (Pattern_Libery_Code[Current_Pattern_Number]).Insert(stringToInt(collection[0]),
                        new float[] {stringToFloat(collection[1]),stringToFloat(collection[2]),stringToFloat(collection[3]),
                        stringToFloat(collection[4]),stringToFloat(collection[5]),stringToFloat(collection[6])}
                        );
                    }
                    //if(frame )
                }
                else if (readMode == 4){
                    
                    string firstCharacter = TextList[0];
                    string secondCharacter = "";
                    if(TextList.Length > 1) secondCharacter = TextList[1];
                    /*
                    string last2Character = frame.Substring(Math.Max(0, frame.Length - 2));
                    string firstCharacter = frame.Substring(0, 1);
                    */
                    if(firstCharacter == "X"){
                        Current_ReactPattern_Number = 0;
                    }
                    else if(secondCharacter == "RP"){
                        string[] collection = frame.Split(',');
                        Current_ReactPattern_Number = stringToInt(collection[0]);
                    }
                    else if(firstCharacter == "C"){
                        string[] collection = frame.Split(',');
                        //React_Command_Libery_Time[Current_ReactPattern_Number] = stringToFloat(collection[1]);
                        React_Command_Libery_Time.Insert(Current_ReactPattern_Number, stringToFloat(collection[1]));
                        int[] commandCodeArray = new int[collection.Length-3];
                        for(int i = 2 ; i < (collection.Length-1) ; i++){
                            if (TextList[i] == "C" || TextList[i] == "" ) break;
                            commandCodeArray[i-2] = stringToInt(collection[i]);
                        }
                        //React_Command_Libery_Action_Code[Current_ReactPattern_Number] = commandCodeArray;
                        React_Command_Libery_Action_Code.Insert(Current_ReactPattern_Number, commandCodeArray);
                    }
                    else{
                        string[] collection = frame.Split(',');

                        if(React_Command_Libery_Code.Count <= Current_ReactPattern_Number){
                            React_Command_Libery_Code.Add(new List<float[]>());
                        }
                        (React_Command_Libery_Code[Current_ReactPattern_Number]).Insert(stringToInt(collection[0]),
                        new float[] {stringToFloat(collection[1]),stringToFloat(collection[2]),stringToFloat(collection[3]),
                        stringToFloat(collection[4]),stringToFloat(collection[5]),stringToFloat(collection[6])}
                        );
                    }
                }
                else if (readMode == 6){
                    string firstCharacter = TextList[0];
                     string secondCharacter = "";
                    if(TextList.Length > 1) secondCharacter = TextList[1];

                    /*
                    string last3Character = frame.Substring(Math.Max(0, frame.Length - 3));
                    string firstCharacter = frame.Substring(0, 1);
                    */
                    print("secondCharacter = " + secondCharacter);
                    if(firstCharacter == "X"){
                        Current_RevPattern_Number = 0;
                    }
                    else if(secondCharacter == "RVP"){
                        string[] collection = frame.Split(',');
                        Current_RevPattern_Number = stringToInt(collection[0]);
                    }
                    else if(firstCharacter == "V"){
                        //string lastCharacter = frame.Substring(Math.Max(0, frame.Length - 1));
                        revengeValue = stringToFloat(secondCharacter);

                    }
                    else{
                        string[] collection = frame.Split(',');
                        (Revenge_Command_Libery_Code).Insert(stringToInt(collection[0]),
                        new float[] {stringToFloat(collection[1]),stringToFloat(collection[2]),stringToFloat(collection[3]),
                        stringToFloat(collection[4]),stringToFloat(collection[5]),stringToFloat(collection[6])}
                        );
                    }
                }
                else if(readMode == 7){
                    print("NO number 7");
                    string[] collection = frame.Split(',');
                    for(int i = 0 ; i < (collection.Length-1) ; i++){
                        if (collection[i] == "X" || collection[i] == "" ) break;
                        CurrentPatternSet_Code.Add(stringToInt(collection[i]));
                        //print(stringToInt(collection[i]));
                    }
                    
                }

            }
        }
        /*
          print("-----------Check Read Data Start----------");
        
        print(Combo_Libery_Code.Count);
        print(Pattern_Libery_Code.Count);
        //print(Current_Pattern_Number.Count);
        print(React_Command_Libery_Code.Count);
        //print(Current_ReactPattern_Number);
        print(React_Command_Libery_Time.Count);
        print(React_Command_Libery_Action_Code.Count);
        print(revengeValue);
        print(Current_RevPattern_Number);
        print(Revenge_Command_Libery_Code.Count);
        print(CurrentPatternSet_Code.Count);
        
                print("-----------Check Read Data End----------");
      */
        /////////// Write on AI ///////////////
        characterAIControl.SetCurrentAIfromWrite(Combo_Libery_Code,
                         Pattern_Libery_Code,
                         ReactCombo_Libery_Code,
                         React_Command_Libery_Code,
                         React_Command_Libery_Time,
                         React_Command_Libery_Action_Code,
                         RevengeCombo_Libery_Code,
                         revengeValue,
                         Revenge_Command_Libery_Code,
                        CurrentPatternSet_Code);
    }

    private int stringToInt(string a){
        return int.Parse(a);
    }


    private float stringToFloat(string a){
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
