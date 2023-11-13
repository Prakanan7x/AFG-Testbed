using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class Dynamic_Script : MonoBehaviour {


    #region Important Variable 

    //////////////////////// 
    public bool is_Player_AI; /// Is AI-Controlled character?
    public bool Use_EmptyAI; // For Enemy, Use Preset AI at the start?
    public bool Use_SampleFile; // For Enemy, Load Sample file for initial AI?
    public bool Is_Fixed_AI; // For Enemy, Not undating weight and progress for each round?

    //////////// For Player AI /////////////
    public int[] player_AI_currentWeightList = {0};
    public int[] player_AI_currentWeightTrainNumber = {1};
    ////////////////////////////////////////////////

    //////////// For Enemy-Fixed AI /////////////
    public int[] fixed_AI_currentWeightList = {0};
    public int[] fixed_AI_currentWeightTrainNumber = {1};
    ////////////////////////////////////////////////

    public Character_1 character;
    public Character_Stat charStat;
    public Character_1_Data charData;
    public Character_1_DataWrite playerCharDataWrite;
    public Character_1_Data oppoData;
    public Character_1_DataWrite oppoDataWrite;
    public Character_1_AIControl charAI;
    public Character_1_AIWrite charAIWrite;
    public bool PatternOrTime;
    public float[] charDataList;
    public float[] oppoDataList;


    public List<WeightGoal> WScodePriorityList = new List<WeightGoal>();
    public List<int> currentWeightList = new List<int>();
    public List<int> currentWeightTrainNumber = new List<int>() {3,2,1};
    public float levelWeightMulDecay = 0.15f;
    public float noncurlevelWeightMulDecay = 0.5f;
    public float[] currentWeightMulList;

    public AILogic_Code CurrentAILogic_Code;

    #endregion

    #region Classes

    public class WeightSet
    {
        public int code { get; set; }
        public string name { get; set; }
        public float weight { get; set; }
        public int calPriority { get; set; }
        public float[] weightMultiplier  {get; set;}
        public WeightSet(int code, string name, int calPriority, float weight )
        {
            this.code = code;
            this.name = name;
            this.calPriority = calPriority;
            this.weight = weight;
            this.weightMultiplier = new float[0];
        }
        public WeightSet(int code, string name, int calPriority, float weight , float[] weightMul)
        :this(code,name,calPriority,weight)
        {
            
            this.weightMultiplier = weightMul;
        }


    }

    public class Data
    {
        public int code { get; set; }
        public float value { get; set; }
        public Data(int code, float value)
        {
            this.code = code;
            this.value = value;
        }
    }

    public class WeightGoal{
         public int code { get; set; }
         public int level { get; set; }
         public float passingScore {get; set;} 
         public bool pass {get; set;}
         public WeightGoal(int code, int level, float passingScore){
            this.code = code;
            this.level = level;
            this.passingScore = passingScore;
            pass = false;
         }
         public void checkPass(){
            pass = true;
         }
    }

    public class AIlogic_String{
        public List<String> CCombo_Libery = new List<String>();
        public List<String> CPattern_Libery = new List<String>();
        public List<String> CReact_Command_Libery = new List<String>();
        public List<String> CRevenge_Command_Libery = new List<String>();
        public String CPatternSet;
        public float RV;

        public AIlogic_String(){}
    }

    public class AILogic_Code{
        public List<int[]> Combo_Libery_Code = new List<int[]>();
        public List<List<float[]>> Pattern_Libery_Code = new List<List<float[]>>();

        public List<int[]> ReactCombo_Libery_Code = new List<int[]>();
        public List<List<float[]>> React_Command_Libery_Code = new List<List<float[]>>();
        public List<float> React_Command_Libery_Time = new List<float>();
        public List<int[]> React_Command_Libery_Action_Code = new List<int[]>();

        public List<int[]> RevengeCombo_Libery_Code = new List<int[]>();
        public float revengeValue;
        public List<float[]> Revenge_Command_Libery_Code = new List<float[]>();
        public List<int> CurrentPatternSet_Code = new List<int>();
        
        /*
        
        */
        public AILogic_Code(){}  
        public AILogic_Code(AILogic_Code AIString)
        {
           Combo_Libery_Code = AIString.Combo_Libery_Code;
           Pattern_Libery_Code = AIString.Pattern_Libery_Code;

           ReactCombo_Libery_Code = AIString.ReactCombo_Libery_Code;
           React_Command_Libery_Code = AIString.React_Command_Libery_Code;
           React_Command_Libery_Time = AIString.React_Command_Libery_Time;
           React_Command_Libery_Action_Code = AIString.React_Command_Libery_Action_Code;

           RevengeCombo_Libery_Code = AIString.RevengeCombo_Libery_Code;
           revengeValue = AIString.revengeValue;
           Revenge_Command_Libery_Code = AIString.Revenge_Command_Libery_Code;
           CurrentPatternSet_Code = AIString.CurrentPatternSet_Code;
        }
        public AILogic_Code(List<int[]> Combo_Libery_Code,
            List<List<float[]>> Pattern_Libery_Code,

            List<int[]> ReactCombo_Libery_Code,
            List<List<float[]>> React_Command_Libery_Code,
            List<float> React_Command_Libery_Time,
            List<int[]> React_Command_Libery_Action_Code,

            List<int[]> RevengeCombo_Libery_Code,
            float revengeValue,
            List<float[]> Revenge_Command_Libery_Code,
            List<int> CurrentPatternSet_Code)
        {
           this.Combo_Libery_Code = Combo_Libery_Code;
           this.Pattern_Libery_Code = Pattern_Libery_Code;

           this.ReactCombo_Libery_Code = ReactCombo_Libery_Code;
           this.React_Command_Libery_Code = React_Command_Libery_Code;
           this.React_Command_Libery_Time = React_Command_Libery_Time;
           this.React_Command_Libery_Action_Code = React_Command_Libery_Action_Code;

           this.RevengeCombo_Libery_Code = RevengeCombo_Libery_Code;
           this.revengeValue = revengeValue;
           this.Revenge_Command_Libery_Code = Revenge_Command_Libery_Code;
           this.CurrentPatternSet_Code = CurrentPatternSet_Code;
        }
       

    
        
    }
    

    public int AbrActCode_To_FullActCode(string AbrActCode){
        switch (AbrActCode)
        {
            // Attack
            case "101": return 200110;
            case "102": return 200120;
            case "103": return 200130;
            case "104": return 200140;
            
            case "201": return 201110;
            case "202": return 201120;
            case "203": return 201130;
            case "204": return 201140;

            case "301": return 220110;
            case "302": return 220120;
            case "303": return 220130;

            case "401": return 221110;
            case "402": return 221120;
            case "403": return 221130;

            case "501": return 240110;
            case "502": return 240120;
            case "503": return 240130;
            case "504": return 240140;
            case "505": return 240150;

            case "601": return 241110;
            case "602": return 241120;
            case "603": return 241130;
            case "604": return 241140;
            case "605": return 241150;

            case "901": return 40201;
            case "902": return 40202;
            case "903": return 40203;
            case "904": return 40204;
            case "905": return 40205;
            case "906": return 40206;
            case "907": return 40207;
            case "908": return 40208;
            
            default:    return 0;
        }
    }


    #endregion

    #region Weight Set Value

    public List<WeightSet> mainWeightSetList = new List<WeightSet>();
    public List<WeightSet> subWeightSetList = new List<WeightSet>();

    public Dictionary<int, WeightSet> mainWeightSetDict = new Dictionary<int, WeightSet>();
    public Dictionary<int, WeightSet> subWeightSetDict = new Dictionary<int, WeightSet>();

    
    // Attack Mode Weight

    ///// Main Attack Mode Weight
    public WeightSet W_CRagAtkUse = new WeightSet(00000, "Close Range Attack Usage",0, 0,
        new float[] {1.0f,0.2f,1.0f,0.2f,0.5f,0.4f,0.4f,0.4f,0.4f,0.4f,0.4f});
    public WeightSet W_CRagAtkAcc= new WeightSet(01000, "Close Range Attack Accuracy", 0, 0,
        new float[] {1.0f,0.2f,1.0f,0.2f,0.5f,0.4f,0.4f,0.4f,0.4f,0.4f,0.4f});
    public WeightSet W_FRagAtkUse = new WeightSet(02000, "Far Range Attack Usage", 0, 0,
        new float[] {0.2f,1.0f,0.2f,1.0f,0.5f,0.4f,0.4f,0.4f,0.4f,0.4f,0.4f});
    public WeightSet W_FRagAtkAcc = new WeightSet(03000, "Far Range Attack Accuracy", 0, 0,
        new float[] {0.2f,1.0f,0.2f,1.0f,0.5f,0.4f,0.4f,0.4f,0.4f,0.4f,0.4f});
    public WeightSet W_UnderSCombo = new WeightSet(04000, "Understanding Combo", 0, 0,
        new float[] {0.5f,0.5f,0.5f,0.5f,1.0f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f});
    public WeightSet W_BModAtkUse = new WeightSet(05000, "Balance Mode Attack Usage", 0, 0,
        new float[] {0.4f,0.4f,0.4f,0.4f,0.5f,1.0f,1.0f,0.1f,0.1f,0.1f,0.1f});
    public WeightSet W_BModAtkAcc = new WeightSet(06000, "Balance Mode Attack Accuracy", 0, 0,
        new float[] {0.4f,0.4f,0.4f,0.4f,0.5f,1.0f,1.0f,0.1f,0.1f,0.1f,0.1f});
    public WeightSet W_HModAtkUse = new WeightSet(07000, "Heavy Mode Attack Usage", 0, 0,
        new float[] {0.4f,0.4f,0.4f,0.4f,0.5f,0.1f,0.1f,1.0f,1.0f,0.1f,0.1f});
    public WeightSet W_HModAtkAcc = new WeightSet(08000, "Heavy Mode Attack Accuracy", 0, 0,
        new float[] {0.4f,0.4f,0.4f,0.4f,0.5f,0.1f,0.1f,1.0f,1.0f,0.1f,0.1f});
    public WeightSet W_QModAtkUse = new WeightSet(09000, "Quick Mode Attack Usage", 0, 0,
        new float[] {0.4f,0.4f,0.4f,0.4f,0.5f,0.1f,0.1f,0.1f,0.1f,1.0f,1.0f});
    public WeightSet W_QModAtkAcc = new WeightSet(10000, "Quick Mode Attack Accuracy", 0, 0,
        new float[] {0.4f,0.4f,0.4f,0.4f,0.5f,0.1f,0.1f,0.1f,0.1f,1.0f,1.0f});

    ///// Sub Attack Mode Weight
    ///////// Usage
    public WeightSet SW_BModCRagAtkUse = new WeightSet(00100, "Balance Mode Close Range Attack Usage", 1, 0);
    public WeightSet SW_BN1AtkUse = new WeightSet(00101, "BN1 Usage", 0, 0);
    public WeightSet SW_BN2AtkUse = new WeightSet(00102, "BN2 Usage", 0, 0);
    public WeightSet SW_BN3AtkUse = new WeightSet(00103, "BN3 Usage", 0, 0);
    public WeightSet SW_BN4AtkUse = new WeightSet(00104, "BN4 Usage", 0, 0);
    public WeightSet SW_BModFRagAtkUse = new WeightSet(00200, "Balance Mode Far Range Attack Usage", 1, 0);
    public WeightSet SW_BF1AtkUse = new WeightSet(00201, "BF1 Usage", 0, 0);
    public WeightSet SW_BF2AtkUse = new WeightSet(00202, "BF2 Usage", 0, 0);
    public WeightSet SW_BF3AtkUse = new WeightSet(00203, "BF3 Usage", 0, 0);
    public WeightSet SW_BF4AtkUse = new WeightSet(00204, "BF4 Usage", 0, 0);
    public WeightSet SW_HModCRagAtkUse = new WeightSet(00300, "Heavy Mode Close Range Attack Usage", 1, 0);
    public WeightSet SW_HN1AtkUse = new WeightSet(00301, "HN1 Usage", 0, 0);
    public WeightSet SW_HN2AtkUse = new WeightSet(00302, "HN2 Usage", 0, 0);
    public WeightSet SW_HN3AtkUse = new WeightSet(00303, "HN3 Usage", 0, 0);
    public WeightSet SW_HModFRagAtkUse = new WeightSet(00400, "Heavy Mode Far Range Attack Usage", 1, 0);
    public WeightSet SW_HF1AtkUse = new WeightSet(00401, "HF1 Usage", 0, 0);
    public WeightSet SW_HF2AtkUse = new WeightSet(00402, "HF2 Usage", 0, 0);
    public WeightSet SW_HF3AtkUse = new WeightSet(00403, "HF3 Usage", 0, 0);
    public WeightSet SW_QModCRagAtkUse = new WeightSet(00500, "Quick Mode Close Range Attack Usage", 1, 0);
    public WeightSet SW_QN1AtkUse = new WeightSet(00501, "QN1 Usage", 0, 0);
    public WeightSet SW_QN2AtkUse = new WeightSet(00502, "QN2 Usage", 0, 0);
    public WeightSet SW_QN3AtkUse = new WeightSet(00503, "QN3 Usage", 0, 0);
    public WeightSet SW_QN4AtkUse = new WeightSet(00504, "QN4 Usage", 0, 0);
    public WeightSet SW_QN5AtkUse = new WeightSet(00505, "QN5 Usage", 0, 0);
    public WeightSet SW_QModFRagAtkUse = new WeightSet(00600, "Quick Mode Far Range Attack Usage", 1, 0);
    public WeightSet SW_QF1AtkUse = new WeightSet(00601, "QF1 Usage", 0, 0);
    public WeightSet SW_QF2AtkUse = new WeightSet(00602, "QF2 Usage", 0, 0);
    public WeightSet SW_QF3AtkUse = new WeightSet(00603, "QF3 Usage", 0, 0);
    public WeightSet SW_QF4AtkUse = new WeightSet(00604, "QF4 Usage", 0, 0);
    public WeightSet SW_QF5AtkUse = new WeightSet(00605, "QF5 Usage", 0, 0);
    ///////// Accuarcy
    public WeightSet SW_BModCRagAtkAcc = new WeightSet(00110, "Balance Mode Close Range Attack Accuracy", 1, 0);
    public WeightSet SW_BN1AtkAcc = new WeightSet(00111, "BN1 Accuracy", 0, 0);
    public WeightSet SW_BN2AtkAcc = new WeightSet(00112, "BN2 Accuracy", 0, 0);
    public WeightSet SW_BN3AtkAcc = new WeightSet(00113, "BN3 Accuracy", 0, 0);
    public WeightSet SW_BN4AtkAcc = new WeightSet(00114, "BN4 Accuracy", 0, 0);
    public WeightSet SW_BModFRagAtkAcc = new WeightSet(00210, "Balance Mode Far Range Attack Accuracy", 1, 0);
    public WeightSet SW_BF1AtkAcc = new WeightSet(00211, "BF1 Accuracy", 0, 0);
    public WeightSet SW_BF2AtkAcc = new WeightSet(00212, "BF2 Accuracy", 0, 0);
    public WeightSet SW_BF3AtkAcc = new WeightSet(00213, "BF3 Accuracy", 0, 0);
    public WeightSet SW_BF4AtkAcc = new WeightSet(00214, "BF4 Accuracy", 0, 0);
    public WeightSet SW_HModCRagAtkAcc = new WeightSet(00310, "Heavy Mode Close Range Attack Accuracy", 1, 0);
    public WeightSet SW_HN1AtkAcc = new WeightSet(00311, "HN1 Accuracy", 0, 0);
    public WeightSet SW_HN2AtkAcc = new WeightSet(00312, "HN2 Accuracy", 0, 0);
    public WeightSet SW_HN3AtkAcc = new WeightSet(00313, "HN3 Accuracy", 0, 0);
    public WeightSet SW_HModFRagAtkAcc = new WeightSet(00410, "Heavy Mode Far Range Attack Accuracy", 1, 0);
    public WeightSet SW_HF1AtkAcc = new WeightSet(00411, "HF1 Accuracy", 0, 0);
    public WeightSet SW_HF2AtkAcc = new WeightSet(00412, "HF2 Accuracy", 0, 0);
    public WeightSet SW_HF3AtkAcc = new WeightSet(00413, "HF3 Accuracy", 0, 0);
    public WeightSet SW_QModCRagAtkAcc = new WeightSet(00510, "Quick Mode Close Range Attack Accuracy", 1, 0);
    public WeightSet SW_QN1AtkAcc = new WeightSet(00511, "QN1 Accuracy", 0, 0);
    public WeightSet SW_QN2AtkAcc = new WeightSet(00512, "QN2 Accuracy", 0, 0);
    public WeightSet SW_QN3AtkAcc = new WeightSet(00513, "QN3 Accuracy", 0, 0);
    public WeightSet SW_QN4AtkAcc = new WeightSet(00514, "QN4 Accuracy", 0, 0);
    public WeightSet SW_QN5AtkAcc = new WeightSet(00515, "QN5 Accuracy", 0, 0);
    public WeightSet SW_QModFRagAtkAcc = new WeightSet(00610, "Quick Mode Far Range Attack Accuracy", 1, 0);
    public WeightSet SW_QF1AtkAcc = new WeightSet(00611, "QF1 Accuracy", 0, 0);
    public WeightSet SW_QF2AtkAcc = new WeightSet(00612, "QF2 Accuracy", 0, 0);
    public WeightSet SW_QF3AtkAcc = new WeightSet(00613, "QF3 Accuracy", 0, 0);
    public WeightSet SW_QF4AtkAcc = new WeightSet(00614, "QF4 Accuracy", 0, 0);
    public WeightSet SW_QF5AtkAcc = new WeightSet(00615, "QF5 Accuracy", 0, 0);

    #endregion

    // Use this for initialization

    #region Start
    void Start () {
        DS_initializeALL();
        if(!character.Is_Player_or_Enemy || character.Is_P_Enemy){
                if(is_Player_AI){
                DS_initializeALL_Player();
                DS_generateAiALL_Player();
            }
            else{
                //SetCurrentAILogic_Code_To_AIControl();
                if(Is_Fixed_AI){
                    initialize_currentWeightList_Fixed();
                    DS_generateAiALL();
                } 
                else if(Use_EmptyAI){
                    SetCurrentAILogic_Code_To_AIControl();
                    print("SetCurrentAILogic_Code_To_AIControl");
                }
                else if(Use_SampleFile){
                    charAIWrite.LoadAISampleFile();
                    print("SetCurrentAIfromWrite");   
                }
            }
        }
        
        
            
    }
    #endregion
	
	#region Update
	void Update () {
        

    }
    #endregion

    #region Dynamic Script generate Call Method


    public void DS_generateAiALL(){
        
        if(Is_Fixed_AI){
            CurrentAILogic_Code = GenerateAI(CurrentAILogic_Code,currentWeightList);
        }
        else{
            ImportDataToList();
            CalculateWeightEndRound();
            ChooseWeightPriority();
            SetCharacterStat();
            CurrentAILogic_Code = GenerateAI(CurrentAILogic_Code,currentWeightList);
        }
        
        
        //Set New Enemy AI
        SetCurrentAILogic_Code_To_AIControl();
         
    }

    public void DS_generateAiALL_Player(){
        

        CurrentAILogic_Code = GenerateAI_Player(CurrentAILogic_Code,currentWeightList);
        //Set New Enemy AI
        SetCurrentAILogic_Code_To_AIControl();
         
    }


    void SetCurrentAILogic_Code_To_AIControl(){
            charAI.SetCurrentAIfromWrite(CurrentAILogic_Code.Combo_Libery_Code,
                         CurrentAILogic_Code.Pattern_Libery_Code,

                         CurrentAILogic_Code.ReactCombo_Libery_Code,
                         CurrentAILogic_Code.React_Command_Libery_Code,
                         CurrentAILogic_Code.React_Command_Libery_Time,
                         CurrentAILogic_Code.React_Command_Libery_Action_Code,

                         CurrentAILogic_Code.RevengeCombo_Libery_Code,
                         CurrentAILogic_Code.revengeValue,
                         CurrentAILogic_Code.Revenge_Command_Libery_Code,
                        CurrentAILogic_Code.CurrentPatternSet_Code);
    }

    #endregion

    #region ImportData

    
    public void ImportDataToList(){
        charDataList = new float[charData.DataValueList.Length];
        for(int i = 0; i < charDataList.Length ; i++){
            charDataList[i] = (charData.DataValueList[i]).Second;
        }
        oppoDataList = new float[oppoData.DataValueList.Length];
        for(int i = 0; i < oppoDataList.Length ; i++){
            oppoDataList[i] = (oppoData.DataValueList[i]).Second;
        }
        //// TO DO NEXT
    }


    #endregion

    #region Initialize

    public void DS_initializeALL(){
        initialize_AddWScodePriorityList();
        initialize_AddMainWeightSetList();
        initialize_AddSubWeightSetList();

        initialize_currentWeightList();
        initialize_level0AI();
        
    }

    public void DS_initializeALL_Player(){
        
        initialize_currentWeightList_Player();
        CurrentAILogic_Code = GenerateAI_Player(CurrentAILogic_Code,currentWeightList);
        //Set New Enemy AI
        print("Set Player Dynamic Script Complete");
        SetCurrentAILogic_Code_To_AIControl();
         
    }
    public void initialize_AddWScodePriorityList(){

        List<WeightGoal> WScodePriorityL = new List<WeightGoal>{
            new WeightGoal (0000, 0, 10),
            new WeightGoal(1000, 0, 10),
            new WeightGoal(2000, 0, 10),
            new WeightGoal(3000, 0, 10),
            new WeightGoal(0000, 1, 25),
            new WeightGoal(1000, 1, 25),
            new WeightGoal(2000, 1, 25),
            new WeightGoal(3000, 1, 25),
            new WeightGoal(4000, 0, 10),
            new WeightGoal(5000, 0, 10),

            new WeightGoal(6000, 0, 10),
            new WeightGoal(7000, 0, 10),
            new WeightGoal(8000, 0, 10),
            new WeightGoal(9000, 0, 10),
            new WeightGoal(10000, 0, 10),
            new WeightGoal(4000, 1, 25),
            new WeightGoal(5000, 1, 25),
            new WeightGoal(6000, 1, 25),
            new WeightGoal(7000, 1, 25),
            new WeightGoal(8000, 1, 25),

            new WeightGoal(9000, 1, 25),
            new WeightGoal(10000, 1, 25),
            new WeightGoal(0000, 2, 45),
            new WeightGoal(1000, 2, 45),
            new WeightGoal(2000, 2, 45),
            new WeightGoal(3000, 2, 45),
            new WeightGoal(4000, 2, 45),
            new WeightGoal(5000, 2, 45),
            new WeightGoal(6000, 2, 45),
            new WeightGoal(7000, 2, 45),

            new WeightGoal(8000, 2, 45),
            new WeightGoal(9000, 2, 45),
            new WeightGoal(10000, 2, 45),
            new WeightGoal(0000, 3, 70),
            new WeightGoal(1000, 3, 70),
            new WeightGoal(2000, 3, 70),
            new WeightGoal(3000, 3, 70),
            new WeightGoal(4000, 3, 70),
            new WeightGoal(5000, 3, 70),
            new WeightGoal(6000, 3, 70),

            new WeightGoal(7000, 3, 70),
            new WeightGoal(8000, 3, 70),
            new WeightGoal(9000, 3, 70),
            new WeightGoal(10000, 3, 70)
        };
    WScodePriorityList = WScodePriorityL;
        /*
        WScodePriorityList = {
            CreatePair(000, 1),
            CreatePair(000, 1),
        };
        {000, 100, 200, 300, 
         000, 100, 200, 300,
                             400, 500, 600, 700, 800, 900, 1000};
                             */
    }
    public void initialize_AddMainWeightSetList()
    {
        mainWeightSetDict.Add(W_CRagAtkUse.code, W_CRagAtkUse);
        mainWeightSetDict.Add(W_CRagAtkAcc.code, W_CRagAtkAcc);
        mainWeightSetDict.Add(W_FRagAtkUse.code, W_FRagAtkUse);
        mainWeightSetDict.Add(W_FRagAtkAcc.code, W_FRagAtkAcc);
        mainWeightSetDict.Add(W_UnderSCombo.code, W_UnderSCombo);
        mainWeightSetDict.Add(W_BModAtkUse.code, W_BModAtkUse);
        mainWeightSetDict.Add(W_BModAtkAcc.code, W_BModAtkAcc);
        mainWeightSetDict.Add(W_HModAtkUse.code, W_HModAtkUse);
        mainWeightSetDict.Add(W_HModAtkAcc.code, W_HModAtkAcc);
        mainWeightSetDict.Add(W_QModAtkUse.code, W_QModAtkUse);
        mainWeightSetDict.Add(W_QModAtkAcc.code, W_QModAtkAcc);
    }
    public void initialize_AddSubWeightSetList()
    {
        ///////// Usage
        subWeightSetDict.Add(SW_BModCRagAtkUse.code, SW_BModCRagAtkUse);
        subWeightSetDict.Add(SW_BN1AtkUse.code, SW_BN1AtkUse);
        subWeightSetDict.Add(SW_BN2AtkUse.code,SW_BN2AtkUse);
        subWeightSetDict.Add(SW_BN3AtkUse.code,SW_BN3AtkUse);
        subWeightSetDict.Add(SW_BN4AtkUse.code,SW_BN4AtkUse);
        subWeightSetDict.Add(SW_BModFRagAtkUse.code,SW_BModFRagAtkUse);
        subWeightSetDict.Add(SW_BF1AtkUse.code,SW_BF1AtkUse);
        subWeightSetDict.Add(SW_BF2AtkUse.code,SW_BF2AtkUse);
        subWeightSetDict.Add(SW_BF3AtkUse.code,SW_BF3AtkUse);
        subWeightSetDict.Add(SW_BF4AtkUse.code,SW_BF4AtkUse);

        subWeightSetDict.Add(SW_HModCRagAtkUse.code,SW_HModCRagAtkUse);
        subWeightSetDict.Add(SW_HN1AtkUse.code,SW_HN1AtkUse);
        subWeightSetDict.Add(SW_HN2AtkUse.code,SW_HN2AtkUse);
        subWeightSetDict.Add(SW_HN3AtkUse.code,SW_HN3AtkUse);
        subWeightSetDict.Add(SW_HModFRagAtkUse.code,SW_HModFRagAtkUse);
        subWeightSetDict.Add(SW_HF1AtkUse.code,SW_HF1AtkUse);
        subWeightSetDict.Add(SW_HF2AtkUse.code,SW_HF2AtkUse);
        subWeightSetDict.Add(SW_HF3AtkUse.code,SW_HF3AtkUse);

        subWeightSetDict.Add(SW_QModCRagAtkUse.code,SW_QModCRagAtkUse);
        subWeightSetDict.Add(SW_QN1AtkUse.code,SW_QN1AtkUse);
        subWeightSetDict.Add(SW_QN2AtkUse.code,SW_QN2AtkUse);
        subWeightSetDict.Add(SW_QN3AtkUse.code,SW_QN3AtkUse);
        subWeightSetDict.Add(SW_QN4AtkUse.code,SW_QN4AtkUse);
        subWeightSetDict.Add(SW_QN5AtkUse.code,SW_QN5AtkUse);
        subWeightSetDict.Add(SW_QModFRagAtkUse.code,SW_QModFRagAtkUse);
        subWeightSetDict.Add(SW_QF1AtkUse.code,SW_QF1AtkUse);
        subWeightSetDict.Add(SW_QF2AtkUse.code,SW_QF2AtkUse);
        subWeightSetDict.Add(SW_QF3AtkUse.code,SW_QF3AtkUse);
        subWeightSetDict.Add(SW_QF4AtkUse.code,SW_QF4AtkUse);
        subWeightSetDict.Add(SW_QF5AtkUse.code,SW_QF5AtkUse);

        ///////// Accuarcy
        subWeightSetDict.Add(SW_BModCRagAtkAcc.code,SW_BModCRagAtkAcc);
        subWeightSetDict.Add(SW_BN1AtkAcc.code,SW_BN1AtkAcc);
        subWeightSetDict.Add(SW_BN2AtkAcc.code,SW_BN2AtkAcc);
        subWeightSetDict.Add(SW_BN3AtkAcc.code,SW_BN3AtkAcc);
        subWeightSetDict.Add(SW_BN4AtkAcc.code,SW_BN4AtkAcc);
        subWeightSetDict.Add(SW_BModFRagAtkAcc.code,SW_BModFRagAtkAcc);
        subWeightSetDict.Add(SW_BF1AtkAcc.code,SW_BF1AtkAcc);
        subWeightSetDict.Add(SW_BF2AtkAcc.code,SW_BF2AtkAcc);
        subWeightSetDict.Add(SW_BF3AtkAcc.code,SW_BF3AtkAcc);
        subWeightSetDict.Add(SW_BF4AtkAcc.code,SW_BF4AtkAcc);

        subWeightSetDict.Add(SW_HModCRagAtkAcc.code,SW_HModCRagAtkAcc);
        subWeightSetDict.Add(SW_HN1AtkAcc.code,SW_HN1AtkAcc);
        subWeightSetDict.Add(SW_HN2AtkAcc.code,SW_HN2AtkAcc);
        subWeightSetDict.Add(SW_HN3AtkAcc.code,SW_HN3AtkAcc);
        subWeightSetDict.Add(SW_HModFRagAtkAcc.code,SW_HModFRagAtkAcc);
        subWeightSetDict.Add(SW_HF1AtkAcc.code,SW_HF1AtkAcc);
        subWeightSetDict.Add(SW_HF2AtkAcc.code,SW_HF2AtkAcc);
        subWeightSetDict.Add(SW_HF3AtkAcc.code,SW_HF3AtkAcc);

        subWeightSetDict.Add(SW_QModCRagAtkAcc.code,SW_QModCRagAtkAcc);
        subWeightSetDict.Add(SW_QN1AtkAcc.code,SW_QN1AtkAcc);
        subWeightSetDict.Add(SW_QN2AtkAcc.code,SW_QN2AtkAcc);
        subWeightSetDict.Add(SW_QN3AtkAcc.code,SW_QN3AtkAcc);
        subWeightSetDict.Add(SW_QN4AtkAcc.code,SW_QN4AtkAcc);
        subWeightSetDict.Add(SW_QN5AtkAcc.code,SW_QN5AtkAcc);
        subWeightSetDict.Add(SW_QModFRagAtkAcc.code,SW_QModFRagAtkAcc);
        subWeightSetDict.Add(SW_QF1AtkAcc.code,SW_QF1AtkAcc);
        subWeightSetDict.Add(SW_QF2AtkAcc.code,SW_QF2AtkAcc);
        subWeightSetDict.Add(SW_QF3AtkAcc.code,SW_QF3AtkAcc);
        subWeightSetDict.Add(SW_QF4AtkAcc.code,SW_QF4AtkAcc);
        subWeightSetDict.Add(SW_QF5AtkAcc.code,SW_QF5AtkAcc);
    }

    public void initialize_currentWeightList(){
        currentWeightList = new List<int>();
        currentWeightMulList = new float[currentWeightTrainNumber.Count];
        for(int i = 0; i<currentWeightTrainNumber.Count ; i++ ){
            currentWeightList.Add(i);
            float x = 1 + (levelWeightMulDecay * i);
            currentWeightMulList[i] = x<0?0:x;
        }
    }
    public void initialize_currentWeightList_Fixed(){
        currentWeightTrainNumber = new List<int>();
        currentWeightTrainNumber.AddRange(fixed_AI_currentWeightTrainNumber);
        currentWeightList = new List<int>();
        currentWeightMulList = new float[currentWeightTrainNumber.Count];
        for(int i = 0; i<currentWeightTrainNumber.Count ; i++ ){
            currentWeightList.Add(fixed_AI_currentWeightList[i]);
            float x = 1 + (levelWeightMulDecay * i);
            currentWeightMulList[i] = x<0?0:x;
        }
    }
    public void initialize_currentWeightList_Player(){
        currentWeightTrainNumber = new List<int>();
        currentWeightTrainNumber.AddRange(player_AI_currentWeightTrainNumber);
        currentWeightList = new List<int>();
        currentWeightMulList = new float[currentWeightTrainNumber.Count];
        for(int i = 0; i<currentWeightTrainNumber.Count ; i++ ){
            currentWeightList.Add(fixed_AI_currentWeightList[i]);
            float x = 1 + (levelWeightMulDecay * i);
            currentWeightMulList[i] = x<0?0:x;
        }
    }
    public void initialize_level0AI(){
        // Combo
        List<int[]> combo_L = new List<int[]>();
        combo_L.Insert(0,new int[] {905});
        // ReactCombo
        List<int[]> react_combo_L = new List<int[]>();
        combo_L.Insert(0,new int[] {905});
        // RevengeCombo
        List<int[]> rv_combo_L = new List<int[]>();
        combo_L.Insert(0,new int[] {905});
        // Movement
        List<float[]> movement_L = new List<float[]>();
        movement_L.Insert(0,new float[] { 901,0, DirCodeToRan(0) , -15    ,2      ,0     });
        // Action
        List<float[]> action_L = new List<float[]>();
        action_L.Insert(0,new float[] { 101,0, 000 , 0f    ,0      ,0     });
        // Pattern
        List<List<float[]>> pattern_L = new List<List<float[]>>();
        pattern_L.Insert(0,new List<float[]> {  movement_L[0] });
        // React Pattern
        List<List<float[]>> reactPat_L = new List<List<float[]>>();
        // React Time
        List<float> reactTime_L = new List<float>();
        // React Command
        List<int[]> reactActCode_L = new List<int[]>();
        // RV (Revenge Value)
        float rv_Value = 5;
        // Revenge Pattern
        List<float[]> revenge_L = new List<float[]>();
        revenge_L.Insert(0, action_L[0] );
        // PatternSet
        List<int> patSet_L = new List<int>();
        patSet_L.Insert(0,0);
        CurrentAILogic_Code = new AILogic_Code(combo_L,pattern_L,
            react_combo_L,reactPat_L,reactTime_L,reactActCode_L,
            rv_combo_L,rv_Value,revenge_L,patSet_L);
        
    }

    public int DirCodeToRan(int i){
        System.Random rnd = new System.Random();
        int num = rnd.Next(0,2);
        return (i==1)?1:
                ((i==2)?((num == 0)?2:8):
                 ((i==3)?((num == 0)?3:7):
                  ((i==4)?((num == 0)?4:6):
                    ((i==5)?5:0
                    )
                  )
                 )
                );
    }

    #endregion

    #region CalculateWeight 
    public void CalculateWeightEndRound ()
    {
        CalculateSubWeight();
        CalculateNUpdateMainWeight();
    }

    public void CalculateSubWeight(){
        Boolean foundPriority = false;
        for (int i = 0; i < 10; i++) {
            for (int j = 0; i < subWeightSetList.Count ; i++){
                if(subWeightSetList[j].calPriority == i)
                {
                    subWeightSetList[j].weight = CalculateWeightCase(subWeightSetList[j].code);
                    foundPriority = true;
                }
            }
            if (!foundPriority) break;
            foundPriority = false;
        }
    }

    public void CalculateNUpdateMainWeight(){
        Boolean foundPriority = false;
        for (int i = 0; i < 10; i++) {
            for (int j = 0; i < mainWeightSetList.Count ; i++){
                if(mainWeightSetList[j].calPriority == i)
                {
                    mainWeightSetList[j].weight += 
                    ChooseMaxMultiplier(mainWeightSetList[j]) * 
                    CalculateWeightCase(mainWeightSetList[j].code);
                    
                    foundPriority = true;
                }
            }
            if (!foundPriority) break;
            foundPriority = false;
        }
    }

    public float ChooseMaxMultiplier(WeightSet ws){
        float inPriorityWeight = 0f;
        float nonPriorityWeight = 0f;

        float curNonPriorityWeight = 0f;
        for(int i = 0 ; i < currentWeightTrainNumber.Count ; i++){
            if(  WScodePriorityList[currentWeightList[i]].code == ws.code)
                inPriorityWeight = currentWeightMulList[i];
            curNonPriorityWeight 
            = mainWeightSetDict[(WScodePriorityList[currentWeightList[i]].code)].weightMultiplier[ws.code/1000]
            * (float)(Math.Pow(0.5, WScodePriorityList[currentWeightList[i]].level));
            if(nonPriorityWeight < curNonPriorityWeight)
                nonPriorityWeight = curNonPriorityWeight;

        } 
        
        return inPriorityWeight > nonPriorityWeight ? inPriorityWeight  :nonPriorityWeight;
    } 

    public float CalculateWeightCase (int code)
    {
        switch (code)
        {
            // Attack
            //// Main   
            case 0:
                return (GetWfList(100, false) + GetWfList(300, false) + GetWfList(500, false))/3;
                break;
            case 1000:
                return (GetWfList(200, false) + GetWfList(400, false) + GetWfList(600, false)) / 3;
                break;
            case 2000:
                return (GetWfList(110, false) + GetWfList(310, false) + GetWfList(510, false)) / 3;
                break;
            case 3000:
                return (GetWfList(210, false) + GetWfList(410, false) + GetWfList(610, false)) / 3;
                break;
            case 4000:
                return 0; //WIP
                break;
            case 5000:
                return (GetWfList(100, false) + GetWfList(200, false)) / 2;
                break;
            case 6000:
                return (GetWfList(110, false) + GetWfList(210, false)) / 2;
                break;
            case 7000:
                return (GetWfList(300, false) + GetWfList(400, false)) / 2;
                break;
            case 8000:
                return (GetWfList(310, false) + GetWfList(410, false)) / 2;
                break;
            case 9000:
                return (GetWfList(500, false) + GetWfList(600, false)) / 2;
                break;
            case 10000:
                return (GetWfList(510, false) + GetWfList(610, false)) / 2;
                break;


            //// Sub 
                case 100:
                return GetWfList(101, false) + (GetWfList(102, false)*0.5f) + (GetWfList(103, false) * 0.35f) + (GetWfList(104, false) * 0.15f);
                break;
                case 101:
                return CScaleFormula(GetPlayerDfList(200110)); 
                break;
                case 102:
                return CScaleFormula(GetPlayerDfList(200120)); 
                break;
                case 103:
                return CScaleFormula(GetPlayerDfList(200130)); 
                break;
                case 104:
                return CScaleFormula(GetPlayerDfList(200140)); 
                break;
                
                case 200:
                return GetWfList(201, false) + (GetWfList(202, false)*0.5f) + (GetWfList(203, false) * 0.35f) + (GetWfList(204, false) * 0.15f);
                break;
                case 201:
                return CScaleFormula(GetPlayerDfList(201110)); 
                break;
                case 202:
                return CScaleFormula(GetPlayerDfList(201120)); 
                break;
                case 203:
                return CScaleFormula(GetPlayerDfList(201130)); 
                break;
                case 204:
                return CScaleFormula(GetPlayerDfList(201140)); 
                break;

                case 300:
                return GetWfList(301, false) + (GetWfList(302, false)*0.65f) + (GetWfList(303, false) * 0.35f);
                break;
                case 301:
                return CScaleFormula(GetPlayerDfList(220110)); 
                break;
                case 302:
                return CScaleFormula(GetPlayerDfList(220120)); 
                break;
                case 303:
                return CScaleFormula(GetPlayerDfList(220130)); 
                break;

                case 400:
                return GetWfList(401, false) + (GetWfList(402, false)*0.65f) + (GetWfList(403, false) * 0.35f);
                break;
                case 401:
                return CScaleFormula(GetPlayerDfList(221110)); 
                break;
                case 402:
                return CScaleFormula(GetPlayerDfList(221120)); 
                break;
                case 403:
                return CScaleFormula(GetPlayerDfList(221130)); 
                break;

                case 500:
                return GetWfList(501, false) + (GetWfList(502, false)*0.4f) + (GetWfList(503, false) * 0.3f)  + (GetWfList(504, false)*0.2f) + (GetWfList(505, false) * 0.1f);
                break;
                case 501:
                return CScaleFormula(GetPlayerDfList(240110)); 
                break;
                case 502:
                return CScaleFormula(GetPlayerDfList(240120)); 
                break;
                case 503:
                return CScaleFormula(GetPlayerDfList(240130)); 
                break;
                case 504:
                return CScaleFormula(GetPlayerDfList(240140)); 
                break;
                case 505:
                return CScaleFormula(GetPlayerDfList(240150)); 
                break;

                case 600:
                return GetWfList(601, false) + (GetWfList(602, false)*0.4f) + (GetWfList(603, false) * 0.3f)  + (GetWfList(604, false)*0.2f) + (GetWfList(605, false) * 0.1f);
                break;
                case 601:
                return CScaleFormula(GetPlayerDfList(241110)); 
                break;
                case 602:
                return CScaleFormula(GetPlayerDfList(241120)); 
                break;
                case 603:
                return CScaleFormula(GetPlayerDfList(241130)); 
                break;
                case 604:
                return CScaleFormula(GetPlayerDfList(241140)); 
                break;
                case 605:
                return CScaleFormula(GetPlayerDfList(241150)); 
                break;

        }


        return 0;
    }

    public float GetWfList(int code, bool isMain)
    {
        if(isMain)
            return (mainWeightSetDict[code]).weight;
        else
            return (subWeightSetDict[code]).weight;
    }
    public float GetPlayerDfList(int code)
    {
        return charDataList[code];
    }
    public float GetEnemyDfList(int code)
    {
        return oppoDataList[code];
    }

    public float CScaleFormula (float x)
    {
        return PatternOrTime ? PatternScaleFormula(x, (int)GetEnemyDfList(14)) : TimeScaleFormula(x, GetPlayerDfList(1));
    }

    public float PatternScaleFormula(float x, int patternCount)
    {
        return (x <= (patternCount / 5)) ?
                (x * x) / (patternCount / 5) :
                x;
    }
    public float TimeScaleFormula(float x, float timeCount)
    {
        return (x <= (timeCount / 60)) ?
                (x * x) / (timeCount / 60) :
                x;
    }
    #endregion

    #region ChoosePriority


    public void ChooseWeightPriority()
    {
        WeightGoal[] aa = new WeightGoal[currentWeightTrainNumber.Count];
        List<int> newWeightList = currentWeightList;
        int curWeightListPos = 0;
        for(int i = 0; i< WScodePriorityList.Count ; i++){
            //Case 1: Already Pass
            if(WScodePriorityList[i].pass){

            }
            //Case 2: Currently Train
            else if ( (curWeightListPos<currentWeightList.Count)?(currentWeightList[curWeightListPos] == i):false ){
                //Case 2.1 Pass
                if(WScodePriorityList[i].passingScore 
                <= (mainWeightSetDict[(WScodePriorityList[i].code)].weight)){
                    int cot = i;
                    while(WScodePriorityList.Count > cot){
                        cot++;
                        bool checkIfInList = false;
                        foreach( int a in currentWeightList){
                            if(a == cot) checkIfInList = true;
                        }
                        if(!WScodePriorityList[cot].pass 
                        && !checkIfInList){
                            newWeightList[curWeightListPos] = cot;
                            print("newWeightList["+curWeightListPos+"] = " + cot);
                            break;
                        }                            
                    }                   
                }
                //Case 2.2 Not pass yet
                else{
                    print("curWeightListPos = " + curWeightListPos);
                    newWeightList[curWeightListPos] = i;
                }
                curWeightListPos++;
                
            }

        }
        newWeightList.Sort();
        currentWeightList = newWeightList;
        print("currentWeightList.Count = " + currentWeightList.Count);
////////////////////////////// WIP /////////////////////////////

    }


    #endregion

    #region Generate AI

        #region Enemy Dynamic Script
    public void SetCharacterStat(){
        charStat.HP = 200;
        print("SetHP = 200");
    }

    public AILogic_Code GenerateAI (AILogic_Code OgAi, List<int> currentWeightList){
        AILogic_Code NewAi = OgAi;
        print("GenerateAIGenerateAIGenerateAIGenerateAIGenerateAIGenerateAI");

        ///// Normal Pattern
        // Combo
        List<int[]> combo_L = new List<int[]>();
        int combo_L_LastCount = 0;
        // Movement
        List<float[]> movement_L = new List<float[]>();
        int movement_L_LastCount = 0;
        // Action
        List<float[]> action_L = new List<float[]>();
        int action_L_LastCount = 0;
        // Pattern
        List<List<float[]>> pattern_L= new List<List<float[]>>();
        int pattern_L_LastCount = 0;

        ///// React Pattern
        // React Combo
        List<int[]> react_combo_L = new List<int[]>();
        // React Pattern, Time, Command
        List<List<float[]>> reactPat_L = new List<List<float[]>>();
        List<float> reactTime_L = new List<float>();
        List<int[]> reactActCode_L = new List<int[]>();
        int reactPat_L_LastCount = 0;

        ///// RV Pattern
        // RV Combo
        List<int[]> rv_combo_L = new List<int[]>();
        // RV (Revenge Value)
        float rv_Value = 5;
        // Revenge Pattern
        List<float[]> revenge_L = new List<float[]>();
        int revenge_L_LastCount = 0;
        // PatternSet
        List<int> patSet_L = new List<int>();
        int patSet_L_LastCount = 0;
             


        for(int i = currentWeightList.Count-1 ; i >= 0 ; i--){
            for(int k = 0; k <= currentWeightTrainNumber[i] ; k++){
                int Lv = WScodePriorityList[currentWeightList[i]].level;
                ///// Normal Pattern
                // Combo
                int ComboCount = (int)GetDynamicParameter(4,Lv);
                combo_L = GenerateAI_Combo(currentWeightList, i, combo_L, ComboCount);
                // Movement
                movement_L = GenerateAI_Movement(currentWeightList, i, movement_L);
                // Action
                action_L = GenerateAI_Action(currentWeightList, i, action_L,ComboCount);
                // Pattern
                pattern_L = GenerateAI_Pattern(currentWeightList,movement_L,i, action_L,pattern_L,ComboCount);

                ///// Reactive Pattern
                // React Pattern, Time, Command
                GenerateAI_ReactPatternTimeActCode(currentWeightList
                , i, reactPat_L, reactTime_L, reactActCode_L,movement_L_LastCount,action_L_LastCount
                ,out react_combo_L, out  reactPat_L, out reactTime_L,out reactActCode_L);
                // Revenge Pattern
                //revenge_L = GenerateAI_RevengePattern(currentWeightList,i,movement_L,action_L,revenge_L, out rv_combo_L);
                
                GenerateAI_RevengePattern(currentWeightList
                ,i,revenge_L, rv_Value,
                 out rv_combo_L, out revenge_L, out rv_Value);
                //Count Update
                combo_L_LastCount = combo_L.Count;
                movement_L_LastCount = movement_L.Count;
                action_L_LastCount = action_L.Count;
                pattern_L_LastCount = pattern_L.Count;
                reactPat_L_LastCount = reactPat_L.Count;
            }
        }
        /*
        // React Pattern
        List<List<float[]>> reactPat_L = GenerateAI_ReactPattern(currentWeightList,movement_L,action_L);
        // React Time
        List<float> reactTime_L = new List<float>(30); // don't have it yet
        for (int i=0;i<30;i++) reactTime_L.Add(0);
        reactTime_L.RemoveAll(item => item == null);
        // React Command
        List<int[]> reactActCode_L = new List<int[]>(30); // don't have it yet
        for (int i=0;i<30;i++) reactActCode_L.Add(null);
        reactActCode_L.RemoveAll(item => item == null);
        */
        // PatternSet
        patSet_L = GenerateAI_PatSet(currentWeightList,pattern_L,patSet_L);
        //patSet_L = GenerateAI_PatSet(currentWeightList,movement_L,action_L,patSet_L);

        
        print("Pass All Dynamic Scirpt");
        NewAi = new AILogic_Code(combo_L,pattern_L,
        react_combo_L,reactPat_L,reactTime_L,reactActCode_L,
        rv_combo_L,rv_Value,revenge_L,patSet_L);
        return NewAi;
    }

    private float GetDynamicParameter(int code, int level){
        /* Code
        0/1 = Forward Distance for close range/Long range
        2 = Forward Time
        3 = Combo Length
        4 = Action Count
        5 = Action Placement  (Before Run-in/After Run-in/After Retreat)
        6 = Attack/Dodge/Block/Ratio
        7 = Combo Delay
        8 = Retreat Distance
        9 = Retreat Time
        10 = Wait/Bait Ratio
        11 = Pre Attack Wait Time
        12 = Post Attack Wait Time 
        13 = Pre Attack Bait Time
        14 = Post Attack Bait Time 
        15 = Wait & Bait Count
        16 = Wait & Bait Placement (Before Run-in/Before Attack/After Attack/After Run-in)
        
        */
        
        switch (code){
            case 0: // 0/1 = Forward Distance for close range/Long range
            switch(level){
                case 0: return RandomFloat(2.5f,5.5f);break;
                case 1: return RandomFloat(3.5f,6.5f);break;
                case 2: return RandomFloat(4.2f,7.2f);break;   
            }break;
            case 1: //
            switch(level){
                case 0: return RandomFloat(4.5f,5.5f);break;
                case 1: return RandomFloat(5.5f,8.5f);break;
                case 2: return RandomFloat(6.2f,9.2f);break;
            } break;
            case 2: // Forward Time
            switch(level){
                case 0: return RandomFloat(3.5f,6.5f);break;
                case 1: return RandomFloat(3.2f,6.2f);break;
                case 2: return RandomFloat(3.0f,6.0f);break;
            } break;
            case 3: // 3 = Combo Length
            switch(level){
                case 0: return RandomIntwithRatio(new int[] {0,1},new double[] {0.4,0.6});break;
                case 1: return RandomIntwithRatio(new int[] {0,1,2},new double[] {0.2,0.4,0.4});break;
                case 2: return RandomIntwithRatio(new int[] {0,1,2,3},new double[] {0,0.3,0.6,0.1});break;
            } break;
            case 4: // 4 = Action Count
            switch(level){
                case 0: return RandomIntwithRatio(new int[] {0,1},new double[] {0.4,0.6});break;
                case 1: return RandomIntwithRatio(new int[] {0,1,2},new double[] {0.2,0.4,0.4});break;
                case 2: return RandomIntwithRatio(new int[] {0,1,2,3},new double[] {0,0.3,0.6,0.1});break;
            } break;
            case 5: // 5 = Action Placement  (Before Run-in/After Run-in/After Retreat)
            switch(level){
                case 0: return RandomIntwithRatio(new int[] {0,1,2},new double[] {0.15,0.7,0.15});break;
                case 1: return RandomIntwithRatio(new int[] {0,1,2},new double[] {0.15,0.6,0.25});break;
                case 2: return RandomIntwithRatio(new int[] {0,1,2},new double[] {0.2,0.5,0.3});break;
            } break;
            case 6: // 6 = Attack/Dodge/Block/Ratio
            switch(level){
                case 0: return RandomIntwithRatio(new int[] {0,1},new double[] {1.0,0.0});break;
                case 1: return RandomIntwithRatio(new int[] {0,1},new double[] {0.75,0.25});break;
                case 2: return RandomIntwithRatio(new int[] {0,1},new double[] {0.6,0.4});break;
            } break;
            case 7: // 7 = Combo Delay
            switch(level){
                case 0: return RandomFloat(0.25f,0.5f);break;
                case 1: return RandomFloat(0.15f,0.38f);break;
                case 2: return RandomFloat(0.08f,0.3f);break;
            } break;
            case 8: // 8 = Retreat Distance
            switch(level){
                case 0: return RandomFloat(18f,24f);break;
                case 1: return RandomFloat(14f,20f);break;
                case 2: return RandomFloat(10f,18f);break;
            } break;
            case 9: // 9 = Retreat Time
            switch(level){
                case 0: return RandomFloat(1.5f,3f);break;
                case 1: return RandomFloat(1.2f,2.5f);break;
                case 2: return RandomFloat(1.0f,2.3f);break;
            } break;
            case 10: // 10 = Wait/Bait Ratio
            switch(level){
                case 0: return RandomIntwithRatio(new int[] {0,1},new double[] {1.0,0});break;
                case 1: return RandomIntwithRatio(new int[] {0,1},new double[] {0.7,0.3});break;
                case 2: return RandomIntwithRatio(new int[] {0,1},new double[] {0.5,0.5});break;
            } break;
            case 11: // 11 = Pre Attack wait Time
            switch(level){
                case 0: return RandomFloat(0.5f,1.0f);break;
                case 1: return RandomFloat(0.35f,0.8f);break;
                case 2: return RandomFloat(0.1f,0.5f);break;
            } break;
            case 12: // 12 = Post Attack wait Time
            switch(level){
                case 0: return RandomFloat(0.8f,1.6f);break;
                case 1: return RandomFloat(0.5f,1.2f);break;
                case 2: return RandomFloat(0.1f,0.7f);break;
            } break;
            case 13: // 13 = Pre Attack Bait Time
            switch(level){
                case 0: return RandomFloat(0.5f,1.2f);break;
                case 1: return RandomFloat(0.25f,0.95f);break;
                case 2: return RandomFloat(0.12f,0.75f);break;
            } break;
            case 14: // 14 = Post Attack Bait Time
            switch(level){
                case 0: return RandomFloat(0.5f,1.2f);break;
                case 1: return RandomFloat(0.35f,1.05f);break;
                case 2: return RandomFloat(0.2f,0.8f);break;
            } break;
            case 15: // 15 = Wait & Bait Count
            switch(level){
                case 0: return RandomIntwithRatio(new int[] {0,1,2,3},new double[] {0,0.3,0.7,0});break;
                case 1: return RandomIntwithRatio(new int[] {0,1,2,3},new double[] {0.1,0.4,0.4,0.1});break;
                case 2: return RandomIntwithRatio(new int[] {0,1,2,3},new double[] {0.15,0.3,0.3,0.25});break;
            } break;
            case 16: // 16 = Wait & Bait Placement  (Before Run-in/Before Attack/After Attack/After Run-in)
            switch(level){
                case 0: return RandomIntwithRatio(new int[] {0,1,2,3},new double[] {0.1,0.5,0.4,0});break;
                case 1: return RandomIntwithRatio(new int[] {0,1,2,3},new double[] {0.15,0.35,0.35,0.15});break;
                case 2: return RandomIntwithRatio(new int[] {0,1,2,3},new double[] {0.1,0.25,0.35,0.30});break;
            } break;
        }
        return 0;
    }
    private static readonly System.Random random = new System.Random();
    private float RandomFloat(float minValue, float maxValue){
        // Perform arithmetic in double type to avoid overflowing
        //System.Random random = new System.Random();
        //rnd = new System.Random();
        double val;
        val = (GenerateUniqueRandom() * (maxValue - minValue) + minValue);
        val = ((double)((int)(val * 100)))/100;
        return (float)val;
    }
    private int RandomIntwithRatio(int[] valueList, double[] ratioList){
        // Perform arithmetic in double type to avoid overflowing
        //System.Random random = new System.Random();
        //rnd = new System.Random();
        double val = GenerateUniqueRandom();
        double currentRatio = 0;
        for(int i = 0 ; i < valueList.Length ; i++){
            currentRatio += ratioList[i];
            if(val <= currentRatio) return valueList[i];
        }
        return valueList[0];
    }
    private static double lastDoubleNumber = 0;
    private double GenerateUniqueRandom(){
        double rng = random.NextDouble();
        //print("First Random = " + rng);
        while(rng == lastDoubleNumber){
            rng = random.NextDouble();
            print("Random = " + rng);
        }
        lastDoubleNumber = rng;
        return rng;
    }

    private List<int[]> GenerateAI_Combo(List<int> currentWeightList, int i, List<int[]> combo_L, int ComboCount){
        //WScodePriorityList[currentWeightList[i]]
        List<int[]> ComboListToAdd = new List<int[]>();
        int Lv = WScodePriorityList[currentWeightList[i]].level;
        int[] AttackPool1;
        int[] AttackPool2;
        int[] AttackPool3;
        int[] AttackPool4;
        int[] AttackPool5;
        switch (WScodePriorityList[currentWeightList[i]].code)
            { 
                case 0000: case 1000: // Close Range Attack Usage
                    AttackPool1 = new int[] {101,301,501};
                    AttackPool2 = new int[] {102,302,502};
                    AttackPool3 = new int[] {103,303,503};
                    AttackPool4 = new int[] {104,303,504};
                    AttackPool5 = new int[] {104,303,505};
                    break;
                case 2000: case 3000: // Far Range Attack Usage
                    AttackPool1 = new int[] {201,401,601};
                    AttackPool2 = new int[] {202,402,602};
                    AttackPool3 = new int[] {203,403,603};
                    AttackPool4 = new int[] {204,403,604};
                    AttackPool5 = new int[] {204,403,605};
                    break;
                case 5000: case 6000: // Balance Attack Usage (Use Heavy Mode Attack)
                    AttackPool1 = new int[] {301,401};
                    AttackPool2 = new int[] {302,402};
                    AttackPool3 = new int[] {303,403};
                    AttackPool4 = new int[] {303,403};
                    AttackPool5 = new int[] {303,403};
                    break;
                case 7000: case 8000: // Heavy Attack Usage (Use Quick Mode Attack)
                    AttackPool1 = new int[] {501,601};
                    AttackPool2 = new int[] {502,602};
                    AttackPool3 = new int[] {503,603};
                    AttackPool4 = new int[] {504,604};
                    AttackPool5 = new int[] {505,605};
                    break;
                case 9000: case 10000: // Quick Attack Usage (Use Balance Mode Attack)
                    AttackPool1 = new int[] {101,201};
                    AttackPool2 = new int[] {102,202};
                    AttackPool3 = new int[] {103,203};
                    AttackPool4 = new int[] {104,204};
                    AttackPool5 = new int[] {104,204};
                    break;
                default: 
                    AttackPool1 = new int[] {101};
                    AttackPool2 = new int[] {102};
                    AttackPool3 = new int[] {103};
                    AttackPool4 = new int[] {104};
                    AttackPool5 = new int[] {505};
                    break;
        }
        int[] DodgePool = new int[] {901,902,903,904,905,906,907,908};

        for(int k = 0; k < ComboCount;k++){
            int ComboLength = (int)GetDynamicParameter(3,Lv);
            int[] combo_Add = new int[ComboLength];
            System.Random random = new System.Random();
            int curComboCount = 1; 
            int[] curAttackPool = AttackPool1;
            for(int j = 0; j < ComboLength; j++){
                int AtkDogBokChoose = (int)GetDynamicParameter(6,Lv);
                
                if(AtkDogBokChoose == 1){
                    int choose = random.Next(0, DodgePool.Length);
                    combo_Add[j] = DodgePool[choose];
                }
                else if(AtkDogBokChoose == 2){
                    combo_Add[j] = 800; // Block
                }
                else {
                    int choose = random.Next(0, curAttackPool.Length);
                    combo_Add[j] = curAttackPool[choose];

                    if(curComboCount < 5) curComboCount++;

                    if(curComboCount == 2) curAttackPool = AttackPool2;
                    else if(curComboCount == 3) curAttackPool = AttackPool3;
                    else if(curComboCount == 4) curAttackPool = AttackPool4;
                    else if(curComboCount == 5) curAttackPool = AttackPool5;
                }
            }
            ComboListToAdd.Add(combo_Add);
        }
        
        //combo_L.Add(ComboListToAdd);    
        //combo_L.RemoveAll(item => item == null);
       // return combo_L;
       return ComboListToAdd;

    }

    private List<float[]> GenerateAI_Movement(List<int> currentWeightList, int i, List<float[]> movement_L){
        //WScodePriorityList[currentWeightList[i]]
        List<float[]> movement_LAdd = new List<float[]>();
        int Lv = WScodePriorityList[currentWeightList[i]].level;
        float ForwardDis_Close = GetDynamicParameter(0,Lv);
        //print("ForwardDis_Close = " + ForwardDis_Close);
        float ForwardDis_Far = GetDynamicParameter(1,Lv);
        float ForwardTime = GetDynamicParameter(2,Lv);
        float RetreatDis = GetDynamicParameter(8,Lv);
        float RetreatTime = GetDynamicParameter(9,Lv);
        float PreAtkWaitTime = GetDynamicParameter(11,Lv);
        float PostAtkWaitTime = GetDynamicParameter(12,Lv);
        float PreAtkBaitTime = GetDynamicParameter(13,Lv);
        float PostAtkBaitTime = GetDynamicParameter(14,Lv);
        System.Random random = new System.Random();
        int ForDir = random.Next(1, 2+1);
        int RetDir = random.Next(4, 5+1);
        int BaitCloseDir = random.Next(4, 5+1);
        int BaitAllDir = random.Next(3, 5+1);
        switch (WScodePriorityList[currentWeightList[i]].code)
            { 
                case 0000: case 1000: // Close Range Attack Usage
                    movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(0) ,0      ,2f   ,0     });
                    movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(0) ,0      ,PreAtkWaitTime     ,0     });
                    movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(0) ,0      ,PostAtkWaitTime    ,0     });
                    movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(BaitCloseDir) ,0      ,PreAtkBaitTime     ,0     });
                    movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(BaitCloseDir) ,0      ,PostAtkBaitTime    ,0     });
                    movement_LAdd.Add(new float[] { 901,0, DirCodeToRan(ForDir)    ,ForwardDis_Close   ,ForwardTime     ,0     });
                    movement_LAdd.Add(new float[] { 901,0, DirCodeToRan(RetDir)    ,-RetreatDis         ,RetreatTime    ,0     });
                    break;
                case 2000: case 3000: // Far Range Attack Usage
                    movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(0) ,0      ,2f   ,0     });
                    movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(0) ,0      ,PreAtkWaitTime     ,0     });
                    movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(0) ,0      ,PostAtkWaitTime    ,0     });
                    movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(3) ,0      ,PreAtkBaitTime     ,0     });
                    movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(3) ,0      ,PostAtkBaitTime    ,0     });
                    movement_LAdd.Add(new float[] { 901,0, DirCodeToRan(ForDir)    ,ForwardDis_Close   ,ForwardTime     ,0     });
                    movement_LAdd.Add(new float[] { 901,0, DirCodeToRan(RetDir)    ,-RetreatDis         ,RetreatTime    ,0     });
                    break;
                default: 
                    movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(0) ,0      ,2f   ,0     });
                    movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(0) ,0      ,PreAtkWaitTime     ,0     });
                    movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(0) ,0      ,PostAtkWaitTime    ,0     });
                    movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(BaitAllDir) ,0      ,PreAtkBaitTime     ,0     });
                    movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(BaitAllDir) ,0      ,PostAtkBaitTime    ,0     });
                    movement_LAdd.Add(new float[] { 901,0, DirCodeToRan(ForDir)    ,ForwardDis_Close   ,ForwardTime     ,0     });
                    movement_LAdd.Add(new float[] { 901,0, DirCodeToRan(RetDir)    ,-RetreatDis         ,RetreatTime    ,0     });
                    break;
            }
        //movement_L.AddRange(movement_LAdd);
        return movement_LAdd;
    
    }

    private List<float[]> GenerateAI_Action(List<int> currentWeightList, int i, List<float[]> action_L, int ComboCount){
        //WIP
        List<float[]> action_LAdd = new List<float[]>();
        int Lv = WScodePriorityList[currentWeightList[i]].level;
        float ComboDelay = GetDynamicParameter(7,Lv);
        for(int j = 0; j < ComboCount ; j++){
            action_LAdd.Add(new float[] { 101,0, j ,  ComboDelay   ,0      ,0     });
        }
        action_L.AddRange(action_LAdd);
        return action_L;
    }

    private List<List<float[]>> GenerateAI_Pattern(List<int> currentWeightList, List<float[]> M, int i, List<float[]> A,List<List<float[]>> pattern_L,int ComboCount){

        //List<List<float[]>> pattern_LAdd = new List<List<float[]>>();
        int Lv = WScodePriorityList[currentWeightList[i]].level;
        float WaitBaitRatio = GetDynamicParameter(10,Lv);
        int WaitBaitCount = (int)GetDynamicParameter(15,Lv);
        List<float[]> pattern_new = new List<float[]>();

        List<int> ActionPlace = new List<int>();
        List<int> WaitBaitPlace = new List<int>();
        for(int j = 0 ; j < ComboCount ; j++){
            ActionPlace.Add( (int)GetDynamicParameter(5,Lv) );
            
        }
        ActionPlace.Sort();
        bool isDuplicate = false;
        int loopRound = 0;
        for(int j = 0 ; j < WaitBaitCount ; j++){
            if(j >=4 || loopRound >=20) break;
            int placement = (int)GetDynamicParameter(16,Lv) ;
            for(int k = 0; k < j; k++){
                if(placement == WaitBaitPlace[k])
                isDuplicate = true;
            }
            if(!isDuplicate) WaitBaitPlace.Add(placement);
            else j--;
            isDuplicate = false;
            loopRound++;
        }
        WaitBaitPlace.Sort();

        //Assemable Pattern Part
        int curActionPlaceNum = 0;
        int curWaitBaitPlaceNum = 0;
        int waitBaitRatio;
        // Pre Walk in Wait Bait
        while( (WaitBaitPlace.Count > curWaitBaitPlaceNum) 
                && WaitBaitPlace[curWaitBaitPlaceNum] == 0){
            waitBaitRatio = ( (int)GetDynamicParameter(10,Lv) );
            pattern_new.Add( (waitBaitRatio==0)? M[1] : M[3] );
            curWaitBaitPlaceNum++;
        }
        // Pre Walk in Attack
        while( (ActionPlace.Count > curActionPlaceNum) 
                && ActionPlace[curActionPlaceNum] == 0){
            pattern_new.Add( A[curActionPlaceNum]);
            curActionPlaceNum++;
        }
        // Walk in
        pattern_new.Add( M[5]);        
        // In range Pre Attack Wait Bait
        while( (WaitBaitPlace.Count > curWaitBaitPlaceNum) 
                && WaitBaitPlace[curWaitBaitPlaceNum] == 1){
            waitBaitRatio = ( (int)GetDynamicParameter(10,Lv) );
            pattern_new.Add( (waitBaitRatio==0)? M[1] : M[3] );
            curWaitBaitPlaceNum++;
        }
        // In range Attack
        while( (ActionPlace.Count > curActionPlaceNum) 
                && ActionPlace[curActionPlaceNum] == 1){
            pattern_new.Add( A[curActionPlaceNum]);
            curActionPlaceNum++;
        }
        // In range Post Attack Wait Bait
        while( (WaitBaitPlace.Count > curWaitBaitPlaceNum) 
                && WaitBaitPlace[curWaitBaitPlaceNum] == 2){
            waitBaitRatio = ( (int)GetDynamicParameter(10,Lv) );
            pattern_new.Add( (waitBaitRatio==0)? M[2] : M[4] );
            curWaitBaitPlaceNum++;
        }
        // Retreat
        pattern_new.Add( M[6]);   
        // Post Retreat Wait Bait
        while( (WaitBaitPlace.Count > curWaitBaitPlaceNum) 
                && WaitBaitPlace[curWaitBaitPlaceNum] == 3){
            waitBaitRatio = ( (int)GetDynamicParameter(10,Lv) );
            pattern_new.Add( (waitBaitRatio==0)? M[1] : M[3] );
            curWaitBaitPlaceNum++;
        }
        // Post Retreat Attack
        while( (ActionPlace.Count > curActionPlaceNum) 
                && ActionPlace[curActionPlaceNum] == 2){
            pattern_new.Add( A[curActionPlaceNum]);
            curActionPlaceNum++;
        }
        
        print(pattern_new.Count);
        pattern_L.Add(pattern_new);
        return pattern_L;
    }

    //Tuple< (List<List<float[]>>) ,(List<float>) ,(List<int[]>) >
    private  void GenerateAI_ReactPatternTimeActCode (List<int> currentWeightList,
        int i, List<List<float[]>> reactPat_L,  List<float> reactTime_L,        List<int[]> reactActCode_L          , int movement_L_LastCount, int action_L_LastCount,
        out List<int[]> react_combo_L ,out List<List<float[]>> reactPat_L_O,   out List<float> reactTime_L_O,  out List<int[]> reactActCode_L_O){

        List<int[]> ComboListToAdd = new List<int[]>
        {
            new int[] { 905,104 }
        };
        List<float[]> action_LAdd = new List<float[]>{
            new float[] { 101,0, 0 ,  0.01f   ,0      ,0     }
        };
        react_combo_L =  ComboListToAdd ;
        reactPat_L_O = new List<List<float[]>> { new List<float[]> {  action_LAdd[0] } };
        reactTime_L_O = new List<float> {1.0f};  
        reactActCode_L_O = new List<int[]> {  new int[]{0} }; 
    }

    private void GenerateAI_RevengePattern(List<int> currentWeightList,
        int i,  List<float[]> revenge_L, float rv_Value,
        out List<int[]> rv_combo_L, out List<float[]> revenge_L_O, out float rv_Value_O){
        
        int Lv = WScodePriorityList[currentWeightList[i]].level;

        List<int[]> ComboListToAdd = new List<int[]>
        {
            new int[] { 905,104 }
        };

        List<float[]> action_LAdd = new List<float[]>{
            new float[] { 101,0, 0 ,  0.01f   ,0      ,0     }
        };

        switch (WScodePriorityList[currentWeightList[i]].code)
        { 
            default: 
                switch (WScodePriorityList[currentWeightList[i]].level){                   
                    case 1: default:
                        revenge_L_O = new List<float[]> {  action_LAdd[0]};  
                     //WIP
                    break;
                }
                break;
        }
        rv_combo_L = ComboListToAdd ;
        rv_Value_O = 4f;
    }
    
    private List<int> GenerateAI_PatSet(List<int> currentWeightList,List<List<float[]>> pattern_L, List<int> patSet_L){

        //List<int>[] randPoolList = new List<int>[currentWeightList.Count];
        List<int> randPool = new List<int>();    

        for(int i = 0; i < pattern_L.Count ; i++){
             randPool.Add(i);   
        }
        randPool = Shuffle(randPool);         
        return randPool;

    }


    /*
    private List<int> GenerateAI_PatSet(List<int> currentWeightList, List<float[]> M,List<float[]> A, List<int> patSet_L){
         
         //WScodePriorityList[currentWeightList[i]]
        //List<int> patSet_L = new List<int>();
        
        /*
        int fact = 1;
        for(int x=1;x<=currentWeightList.Count;x++){      
            fact=fact*x;      
        } 
        List<int>[] randPoolList = new List<int>[currentWeightList.Count];
        List<int> randPool = new List<int>();        
        for(int i = currentWeightList.Count-1 ; i >= 0 ; i--){
            switch (WScodePriorityList[currentWeightList[i]].code)
            { 
                case 0000: case 1000: // Close Range Attack Usage
                    switch (WScodePriorityList[currentWeightList[i]].level){
                        case 0: // level 0
                            randPool = new List<int> {1,2,3,4};                            
                        break;
                        case 1: default:
                            randPool = new List<int> {1,2,3,4,9,10};                            
                        break;
                    }
                    break;
                case 2000: case 3000: // Far Range Attack Usage
                    switch (WScodePriorityList[currentWeightList[i]].level){
                        case 0: // level 0
                            randPool = new List<int> {5,6,7,8};                            
                        break;
                        case 1: default:
                            randPool = new List<int> {5,6,7,8,11,12};                            
                        break;
                    }
                    break;
                case 5000: case 6000: // Far Range Attack Usage
                    switch (WScodePriorityList[currentWeightList[i]].level){
                        case 0: // level 0
                            randPool = new List<int> {2,6};                          
                        break;
                        case 1: default:
                            randPool = new List<int> {2,6};                            
                        break;
                    }
                    break;
                case 7000: case 8000: // Far Range Attack Usage
                    switch (WScodePriorityList[currentWeightList[i]].level){
                        case 0: // level 0
                            randPool = new List<int> {3,7};                          
                        break;
                        case 1: default:
                            randPool = new List<int> {3,7};                            
                        break;
                    }
                    break;
                case 9000: case 10000: // Far Range Attack Usage
                    switch (WScodePriorityList[currentWeightList[i]].level){
                        case 0: // level 0
                            randPool = new List<int> {1,5};                          
                        break;
                        case 1: default:
                            randPool = new List<int> {1,5};                            
                        break;
                    }
                    break;
                default: 
                    switch (WScodePriorityList[currentWeightList[i]].level){
                        case 0: // level 0
                            randPool = new List<int> {0};  
                        break;
                        case 1: default:
                            randPool = new List<int> {0};  
                        
                         //WIP
                        break;
                    }
                    break;
            }

            randPool = Shuffle(randPool);
            print(randPool[0] + "   " + randPoolList.Length);
            print(i);
            randPoolList[i] = randPool;           
        }

        for(int j = 0 ; j < currentWeightList.Count ; j++){
            for(int k = 0; k < currentWeightList.Count-j ; k++){
                patSet_L.Add(randPoolList[j][(k<randPoolList[j].Count)?k:0]);
            }                
        }
        patSet_L.RemoveAll(item => item == null);
        return patSet_L;

    }
        */
    #endregion

        #region Player Fixed Script
    public AILogic_Code GenerateAI_Player (AILogic_Code OgAi, List<int> currentWeightList){
           AILogic_Code NewAi = OgAi;

        ///// Normal Pattern
        // Combo
        List<int[]> combo_L = new List<int[]>();
        // Movement
        List<float[]> movement_L = new List<float[]>();
        // Action
        List<float[]> action_L = new List<float[]>();
        // Pattern
        List<List<float[]>> pattern_L= new List<List<float[]>>();
        ///// React Pattern
        // React Combo
        List<int[]> react_combo_L = new List<int[]>();
        // React Pattern, Time, Command
        List<List<float[]>> reactPat_L = new List<List<float[]>>();
        List<float> reactTime_L = new List<float>();
        List<int[]> reactActCode_L = new List<int[]>();

        ///// RV Pattern
        // RV Combo
        List<int[]> rv_combo_L = new List<int[]>();
        // RV (Revenge Value)
        float rv_Value = 5;
        // Revenge Pattern
        List<float[]> revenge_L = new List<float[]>();
        int revenge_L_LastCount = 0;
        // PatternSet
        List<int> patSet_L = new List<int>();
        int patSet_L_LastCount = 0;

        int firstPriorityLevel = WScodePriorityList[currentWeightList[0]].level;
        int firstPriorityCode = WScodePriorityList[currentWeightList[0]].code;

        combo_L = GenerateAI_Combo_Player(firstPriorityLevel, firstPriorityCode);
        movement_L = GenerateAI_Movement_Player(firstPriorityLevel, firstPriorityCode);
        action_L = GenerateAI_Action_Player(firstPriorityLevel, firstPriorityCode);
        pattern_L = GenerateAI_Pattern_Player(firstPriorityLevel, firstPriorityCode, movement_L,action_L);
        patSet_L = GenerateAI_PatSet_Player(firstPriorityLevel, firstPriorityCode, pattern_L);

        GenerateAI_ReactPatternTimeActCode_Player(firstPriorityLevel, firstPriorityCode
                ,movement_L,action_L
                ,out react_combo_L, out  reactPat_L, out reactTime_L,out reactActCode_L);
        GenerateAI_RevengePattern_Player(firstPriorityLevel, firstPriorityCode
                 ,out rv_combo_L, out revenge_L, out rv_Value);
        
        print("Pass All Dynamic Scirpt Player");
        NewAi = new AILogic_Code(combo_L,pattern_L,
        react_combo_L,reactPat_L,reactTime_L,reactActCode_L,
        rv_combo_L,rv_Value,revenge_L,patSet_L);
        return NewAi;
    }

    private float GetReactiveAIParameter_Player(int code, int level){
          switch (code){
            case 0: // 0 = Range Predict off set
            switch(level){
                case 0: return RandomFloat(0.8f,1.6f);break;
                case 1: return RandomFloat(0.85f,1.4f);break;
                case 2: return RandomFloat(0.9f,1.22f);break;   
            }break;
            case 1: 
            switch(level){
                case 0: return 1.5f;break;
                case 1: return 1.5f;break;
                case 2: return 1.5f;break;
            } break;
            case 2: // Forward Time
            switch(level){
                case 0: return 2f;break;
                case 1: return 3f;break;
                case 2: return 4f;break;
            } break;
            case 3: // Forward Time
            switch(level){
                case 0: return 0.15f;break;
                case 1: return 0.1f;break;
                case 2: return 0.0f;break;
            } break;
            case 4: case 5: case 6: //
            switch(level){
                case 0: return 0.4f;break;
                case 1: return 0.3f;break;
                case 2: return 0.24f;break;
            } break;
            
        }
        return 0;
    }

     private List<int[]> GenerateAI_Combo_Player( int level, int code){
        //WScodePriorityList[currentWeightList[i]]
        List<int[]> ComboListToAdd = new List<int[]>();
        switch (level)
            { 
                case 0:
                    ComboListToAdd.Add(new int[] {101,102});
                    ComboListToAdd.Add(new int[] {201,202});
                    ComboListToAdd.Add(new int[] {301,302});
                    ComboListToAdd.Add(new int[] {401,402});
                    ComboListToAdd.Add(new int[] {501,502});
                    ComboListToAdd.Add(new int[] {601,602});
                    break;
                case 1:
                    ComboListToAdd.Add(new int[] {101,102,103});
                    ComboListToAdd.Add(new int[] {201,202,203});
                    ComboListToAdd.Add(new int[] {301,302,303});
                    ComboListToAdd.Add(new int[] {401,402,403});
                    ComboListToAdd.Add(new int[] {501,502,503});
                    ComboListToAdd.Add(new int[] {601,602,603});
                    break;
                case 2:
                    ComboListToAdd.Add(new int[] {101,102,103,104});
                    ComboListToAdd.Add(new int[] {201,202,203,204});
                    ComboListToAdd.Add(new int[] {301,302,303});
                    ComboListToAdd.Add(new int[] {401,402,403});
                    ComboListToAdd.Add(new int[] {501,502,503,504});
                    ComboListToAdd.Add(new int[] {601,602,603,604});
                    break;
                default: 
                    ComboListToAdd.Add(new int[] {101,102,103,104});
                    ComboListToAdd.Add(new int[] {201,202,203,204});
                    ComboListToAdd.Add(new int[] {301,302,303});
                    ComboListToAdd.Add(new int[] {401,402,403});
                    ComboListToAdd.Add(new int[] {501,502,503,504,505});
                    ComboListToAdd.Add(new int[] {601,602,603,604,605});
                    break;
        } 
       return ComboListToAdd;
    }


    private List<float[]> GenerateAI_Movement_Player(int level, int code){
        //WScodePriorityList[currentWeightList[i]]
        List<float[]> movement_LAdd = new List<float[]>();
        float DistanceBN = GetReactiveAIParameter_Player(0,level);
        float GetInRangeTime = GetReactiveAIParameter_Player(1,level);
        float WaitBeforeAtkTime = GetReactiveAIParameter_Player(3,level);
        // Balance Close
        movement_LAdd.Add(new float[] { 901,0, DirCodeToRan(1)    ,DistanceBN * 5.5f   ,GetInRangeTime    ,0     });
        // Balance Far
        movement_LAdd.Add(new float[] { 901,0, DirCodeToRan(1)    ,DistanceBN * 7f   ,GetInRangeTime    ,0     });
        // Heavy Close
        movement_LAdd.Add(new float[] { 901,0, DirCodeToRan(1)    ,DistanceBN * 9f   ,GetInRangeTime    ,0     });
        // Heavy Far
        movement_LAdd.Add(new float[] { 901,0, DirCodeToRan(1)    ,DistanceBN * 11f   ,GetInRangeTime    ,0     });
        // Quick Close
        movement_LAdd.Add(new float[] { 901,0, DirCodeToRan(1)    ,DistanceBN * 3f   ,GetInRangeTime    ,0     });
        // Quick Far
        movement_LAdd.Add(new float[] { 901,0, DirCodeToRan(1)    ,DistanceBN * 4.0f   ,GetInRangeTime   ,0     });
        // Wait Reaction Time
        movement_LAdd.Add(new float[] { 900,0, DirCodeToRan(0) ,0      ,WaitBeforeAtkTime   ,0     });

        return movement_LAdd;
    }

    private List<float[]> GenerateAI_Action_Player(int level, int code){
        //WIP
        List<float[]> action_LAdd = new List<float[]>();
        float ComboDelay = 0.20f;
        for(int j = 0; j < 6 ; j++){
            action_LAdd.Add(new float[] { 101,0, j ,  ComboDelay   ,0      ,0     });
        }
        return action_LAdd;
    }

    private List<List<float[]>> GenerateAI_Pattern_Player(int level, int code, List<float[]> M,  List<float[]> A){
        List<List<float[]>> pattern_LAdd = new List<List<float[]>>();       
        for(int j = 0; j < 6 ; j++){
            List<float[]> pattern_new
            = new List<float[]> { M[j] , M[6] , A[0]} ;
            pattern_LAdd.Add(pattern_new);
        }
        return pattern_LAdd;
    }

    private List<int> GenerateAI_PatSet_Player(int level, int code,List<List<float[]>> pattern_L){
        List<int> patSet = new List<int>();    
         switch (code)
            { 
                case 0000: case 1000:  case 5000: case 6000:
                    patSet.Add(0);
                    break;
                case 2000: case 3000:
                    patSet.Add(1);
                    break;
                case 7000: case 8000:
                    patSet.Add(2);
                    break;
                case 9000: case 10000:
                    patSet.Add(4);
                    break;
                default: 
                    patSet.Add(0);
                    break;
            }        
        return patSet;
    }
    private  void GenerateAI_ReactPatternTimeActCode_Player (int level, int code,
        List<float[]> M,  List<float[]> A,
        out List<int[]> react_combo_L ,out List<List<float[]>> reactPat_L_O,   out List<float> reactTime_L_O,  out List<int[]> reactActCode_L_O){
        
        react_combo_L = new List<int[]>
        {
            new int[] { 905}
        };
        List<int[]> rACPre = new List<int[]>();    
        List<List<float[]>> patPre = new List<List<float[]>>();
        
        //R0 if (Oppo in Balance Mode) Switch to Quick, P4
        rACPre.Add(new int[] {1001});
        patPre.Add(new List<float[]> { 
            new float[] {1000   ,0  ,4  ,0  ,0  ,0},
            new float[] {991    ,0  ,3  ,0  ,0  ,0}
        });
        //R1 if (Oppo in Balance Mode) Switch to Quick, P5
        rACPre.Add(new int[] {1001});
        patPre.Add(new List<float[]> { 
            new float[] {1000   ,0  ,5  ,0  ,0  ,0},
            new float[] {991    ,0  ,3  ,0  ,0  ,0} 
        });
        //R2 if (Oppo in Heavy Mode) Switch to Balance, P0
        rACPre.Add(new int[] {1002});
        patPre.Add(new List<float[]> { 
            new float[] {1000   ,0  ,0  ,0  ,0  ,0},
            new float[] {991    ,0  ,1  ,0  ,0  ,0} 
        });
        //R3 if (Oppo in Heavy Mode) Switch to Balance, P1
        rACPre.Add(new int[] {1002});
        patPre.Add(new List<float[]> { 
            new float[] {1000   ,0  ,1  ,0  ,0  ,0},
            new float[] {991    ,0  ,1  ,0  ,0  ,0} 
        });
        //R4 if (Oppo in Quick Mode) Switch to Heavy, P2
        rACPre.Add(new int[] {1003});
        patPre.Add(new List<float[]> { 
            new float[] {1000   ,0  ,2  ,0  ,0  ,0},
            new float[] {991    ,0  ,2  ,0  ,0  ,0} 
        });
        //R5 if (Oppo in Quick Mode) Switch to Heavy. P3
        rACPre.Add(new int[] {1003});
        patPre.Add(new List<float[]> { 
            new float[] {1000   ,0  ,3  ,0  ,0  ,0},
            new float[] {991    ,0  ,2  ,0  ,0  ,0} 
        });
        //R6 if (Oppo Attack) Dodge left
        rACPre.Add(new int[] {101,102,103,104,201,202,203,204
        ,301,302,303,401,402,403,501,502,503,504,505,601,602,603,604,605});
        patPre.Add(new List<float[]> { 
            new float[] {100    ,0  ,0  ,0.01f  ,0  ,0} 
        });
        //R7 if (Oppo Dodge/Block) Wait 0.7 sec
        rACPre.Add(new int[] {800,
        901,902,903,904,905,906,907,908});
        patPre.Add(new List<float[]> { 
            new float[] {900    ,0  ,0  ,5f  ,0  ,0} 
        });
        //R8 if (Oppo walk Side) Switch to Balance P0
        rACPre.Add(new int[] {3,7});
        patPre.Add(new List<float[]> { 
            new float[] {1000   ,0  ,0  ,0  ,0  ,0},
            new float[] {991    ,0  ,1  ,0  ,0  ,0} 
        });
        //R9 if (Oppo not walk Side) Switch to Balance  P1
        rACPre.Add(new int[] {0,1,2,4,5,6,8});
        patPre.Add(new List<float[]> { 
            new float[] {1000   ,0  ,1  ,0  ,0  ,0},
            new float[] {991    ,0  ,1  ,0  ,0  ,0} 
        });
        //R10 if (Oppo walk Side) Switch to Heavy P2
        rACPre.Add(new int[] {3,7});
        patPre.Add(new List<float[]> { 
            new float[] {1000   ,0  ,2  ,0  ,0  ,0},
            new float[] {991    ,0  ,2  ,0  ,0  ,0} 
        });
        //R11 if (Oppo not walk Side) Switch to Heavy P3
        rACPre.Add(new int[] {0,1,2,4,5,6,8});
        patPre.Add(new List<float[]> { 
            new float[] {1000   ,0  ,3  ,0  ,0  ,0},
            new float[] {991    ,0  ,2  ,0  ,0  ,0} 
        });
        //R12 if (Oppo walk Side) Switch to Quick P4
        rACPre.Add(new int[] {3,7});
        patPre.Add(new List<float[]> { 
            new float[] {1000   ,0  ,4  ,0  ,0  ,0},
            new float[] {991    ,0  ,3  ,0  ,0  ,0} 
        });
        //R13 if (Oppo not walk Side) Switch to Quick P5
        rACPre.Add(new int[] {0,1,2,4,5,6,8});
        patPre.Add(new List<float[]> { 
            new float[] {1000   ,0  ,5  ,0  ,0  ,0},
            new float[] {991    ,0  ,3  ,0  ,0  ,0} 
        });

        reactActCode_L_O = new List<int[]>();
        reactPat_L_O  =  new List<List<float[]>>();
        reactTime_L_O = new List<float>();
        float RT = GetReactiveAIParameter_Player(3,level);  
        switch (code)
            { 
                case 0000: case 1000:  
                    reactActCode_L_O = new List<int[]>{rACPre[0],rACPre[2],rACPre[4],rACPre[6],rACPre[7]};
                    reactPat_L_O =  new List<List<float[]>>{patPre[0],patPre[2],patPre[4],patPre[6],patPre[7]};
                    reactTime_L_O = new List<float>(){RT,RT,RT,RT,RT};
                    break;
                case 2000: case 3000:
                    reactActCode_L_O = new List<int[]>{rACPre[1],rACPre[3],rACPre[5],rACPre[6],rACPre[7]};
                    reactPat_L_O =  new List<List<float[]>>{patPre[1],patPre[3],patPre[5],patPre[6],patPre[7]};
                    reactTime_L_O = new List<float>(){RT,RT,RT,RT,RT};
                    break;
                case 5000: case 6000:
                    reactActCode_L_O = new List<int[]>{rACPre[6],rACPre[7],rACPre[8],rACPre[9]};
                    reactPat_L_O =  new List<List<float[]>>{patPre[6],patPre[7],patPre[8],patPre[9]};
                    reactTime_L_O = new List<float>(){RT,RT,RT,RT};
                    break;
                case 7000: case 8000:
                    reactActCode_L_O = new List<int[]>{rACPre[6],rACPre[7],rACPre[10],rACPre[11]};
                    reactPat_L_O =  new List<List<float[]>>{patPre[6],patPre[7],patPre[10],patPre[11]};
                    reactTime_L_O = new List<float>(){RT,RT,RT,RT};
                    break;
                case 9000: case 10000:
                    reactActCode_L_O = new List<int[]>{rACPre[6],rACPre[7],rACPre[12],rACPre[13]};
                    reactPat_L_O =  new List<List<float[]>>{patPre[6],patPre[7],patPre[12],patPre[13]};
                    reactTime_L_O = new List<float>(){RT,RT,RT,RT};
                    break;
                default: 
                    reactActCode_L_O = new List<int[]>{rACPre[0],rACPre[2],rACPre[4],rACPre[6],rACPre[7]};
                    reactPat_L_O =  new List<List<float[]>>{patPre[0],patPre[2],patPre[4],patPre[6],patPre[7]};
                    reactTime_L_O = new List<float>(){RT,RT,RT,RT,RT};
                    break;
            }    
        print("reactActCode_L_O.Count = " + reactActCode_L_O.Count + "   " + "reactPat_L_O.Count = " + reactPat_L_O.Count + "   " + "reactTime_L_O.Count = " + reactTime_L_O.Count);
    }

     private void GenerateAI_RevengePattern_Player(int level, int code,
        out List<int[]> rv_combo_L, out List<float[]> revenge_L_O, out float rv_Value_O){
        List<int[]> ComboListToAdd = new List<int[]>
        {
            new int[] { 905}
        };
        List<float[]> action_LAdd = new List<float[]>{
            new float[] { 101,0, 0 ,  0.01f   ,0      ,0     }
        };

        switch (code)
        { 
            default: 
                switch (level){                   
                    case 1: default:
                        revenge_L_O = new List<float[]> {  action_LAdd[0]};  
                     //WIP
                    break;
                }
                break;
        }
        rv_combo_L = ComboListToAdd ;
        rv_Value_O = 100f;
    }


    public List<int> Shuffle(List<int> list)  
    {  
        System.Random rng = new System.Random();  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            int value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
        return list;
    }
    #endregion

    #endregion

    #region File Import/Export part

    public void importWeightSetListfromFile ()
    {

    }


    public List<WeightSet> CalculateAllWeightinList()
    {
        return new List<WeightSet>();
    }


    public List<Data> ComboCSVToWeightSetList(string filename)
    {
        string fileN = filename;
        string[] lines = File.ReadAllLines(fileN);
        print(String.Join(Environment.NewLine, lines));
        //float[] dataList = new float[lines.Length - 1];
        List<Data> dataList = new List<Data>();
        for (int i = 1; i < (lines.Length - 1); i++)
        {
            string[] ComboCodeS = lines[i].Split(',');
            //print(ComboCodeS[i]);
            dataList.Add(new Data(stringToInt(ComboCodeS[1]), stringToFloat(ComboCodeS[2])));
            //print(stringToInt(ComboCodeS[i]));
        }
        return dataList;
    }
    #endregion
    
    #region Misc.
    private int stringToInt(string a)
    {
        return int.Parse(a,
      System.Globalization.CultureInfo.InvariantCulture);
    }

    private float stringToFloat(string a)
    {
        return float.Parse(a,
      System.Globalization.CultureInfo.InvariantCulture);
    }
    #endregion
}

