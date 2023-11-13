using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Character_1_Data : MonoBehaviour {

    #region Data Stroage

    //public List<string> DataValueList = new List<string>();
    //public float[] DataValueList = new float[250];

    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
    }
    public Pair<int, float>[] DataValueList = new Pair<int, float>[250];
    #endregion

    #region Unused Data Part

    public Character_Stat characterStat;

    public float DATA_RemainHP_Percent = 0;
    public float DATA_PlayTime = 0;
    public float DATA_RunTime = 0;
    public float DATA_RunTime_Percent = 0;
    public float DATA_Total_Attack_Exeute = 0;
    public float DATA_Total_Attack_Hit = 0;
    public float DATA_Total_Attack_Percent = 0f;
    public float DATA_Total_Dodge_Execute = 0;
    public float DATA_Total_Dodge_Sucessful = 0;
    public float DATA_Total_Dodge_Percent = 0f;
    public float DATA_Total_Block_Execute = 0;
    public float DATA_Total_Block_Sucessful = 0;
    public float DATA_Total_Block_Percent = 0f;
    public float DATA_Total_Jump_Execute = 0;
    public float DATA_Total_Jump_Sucessful = 0;
    public float DATA_Total_Jump_Percent = 0f;
    public float DATA_Average_Distance_Between = 0;
    public float DATA_Pattern_Count = 0;

    #endregion

    #region Remove Later (Old attack)
    public int DATA_Total_ATK_Close_Execute = 0;
    public int DATA_Total_ATK_Close_Successful = 0;
    public float DATA_Total_ATK_Close_Percent = 0;
    public int DATA_Total_ATK_Mid_Execute = 0;
    public int DATA_Total_ATK_Mid_Successful = 0;
    public float DATA_Total_ATK_Mid_Percent = 0;
    public int DATA_Total_ATK_Cone_Execute = 0;
    public int DATA_Total_ATK_Cone_Successful = 0;
    public float DATA_Total_ATK_Cone_Percent = 0;

    public int DATA_Total_PATK_Close_Execute = 0;
    public int DATA_Total_PATK_Close_Successful = 0;
    public float DATA_Total_PATK_Close_Pecent = 0;
    public int DATA_Total_PATK_Far_Execute = 0;
    public int DATA_Total_PATK_Far_Successful = 0;
    public float DATA_Total_PATK_Far_Pecent = 0;

    public int DATA_Total_FIN_Close_Execute = 0;
    public int DATA_Total_FIN_Close_Successful = 0;
    public float DATA_Total_FIN_Close_Pecent = 0;
    public int DATA_Total_FIN_Far_Execute = 0;
    public int DATA_Total_FIN_Far_Successful = 0;
    public float DATA_Total_FIN_Far_Pecent = 0;
    #endregion

    #region Attack (Landing) Data
    //Balance Mode
    public float DATA_20011_BN1_Execute = 0;
    public float DATA_20011_BN1_Successful = 0;
    public float DATA_20011_BN1_Percent = 0;
    public float DATA_20012_BN2_Execute = 0;
    public float DATA_20012_BN2_Successful = 0;
    public float DATA_20012_BN2_Percent = 0;
    public float DATA_20013_BN3_Execute = 0;
    public float DATA_20013_BN3_Successful = 0;
    public float DATA_20013_BN3_Percent = 0;
    public float DATA_20014_BN4F_Execute = 0;
    public float DATA_20014_BN4F_Successful = 0;
    public float DATA_20014_BN4F_Percent = 0;

    public float DATA_20111_BF1_Execute = 0;
    public float DATA_20111_BF1_Successful = 0;
    public float DATA_20111_BF1_Percent = 0;
    public float DATA_20112_BF2_Execute = 0;
    public float DATA_20112_BF2_Successful = 0;
    public float DATA_20112_BF2_Percent = 0;
    public float DATA_20113_BF3_Execute = 0;
    public float DATA_20113_BF3_Successful = 0;
    public float DATA_20113_BF3_Percent = 0;
    public float DATA_20114_BF4F_Execute = 0;
    public float DATA_20114_BF4F_Successful = 0;
    public float DATA_20114_BF4F_Percent = 0;

    //Heavy Mode
    public float DATA_22011_HN1_Execute = 0;
    public float DATA_22011_HN1_Successful = 0;
    public float DATA_22011_HN1_Percent = 0;
    public float DATA_22012_HN2_Execute = 0;
    public float DATA_22012_HN2_Successful = 0;
    public float DATA_22012_HN2_Percent = 0;
    public float DATA_22013_HN3F_Execute = 0;
    public float DATA_22013_HN3F_Successful = 0;
    public float DATA_22013_HN3F_Percent = 0;

    public float DATA_22111_HF1_Execute = 0;
    public float DATA_22111_HF1_Successful = 0;
    public float DATA_22111_HF1_Percent = 0;
    public float DATA_22112_HF2_Execute = 0;
    public float DATA_22112_HF2_Successful = 0;
    public float DATA_22112_HF2_Percent = 0;
    public float DATA_22113_HF3F_Execute = 0;
    public float DATA_22113_HF3F_Successful = 0;
    public float DATA_22113_HF3F_Percent = 0;

    //Quick Mode
    public float DATA_24011_QN1_Execute = 0;
    public float DATA_24011_QN1_Successful = 0;
    public float DATA_24011_QN1_Percent = 0;
    public float DATA_24012_QN2_Execute = 0;
    public float DATA_24012_QN2_Successful = 0;
    public float DATA_24012_QN2_Percent = 0;
    public float DATA_24013_QN3_Execute = 0;
    public float DATA_24013_QN3_Successful = 0;
    public float DATA_24013_QN3_Percent = 0;
    public float DATA_24014_QN4_Execute = 0;
    public float DATA_24014_QN4_Successful = 0;
    public float DATA_24014_QN4_Percent = 0;
    public float DATA_24015_QN5F_Execute = 0;
    public float DATA_24015_QN5F_Successful = 0;
    public float DATA_24015_QN5F_Percent = 0;

    public float DATA_24111_QF1_Execute = 0;
    public float DATA_24111_QF1_Successful = 0;
    public float DATA_24111_QF1_Percent = 0;
    public float DATA_24112_QF2_Execute = 0;
    public float DATA_24112_QF2_Successful = 0;
    public float DATA_24112_QF2_Percent = 0;
    public float DATA_24113_QF3_Execute = 0;
    public float DATA_24113_QF3_Successful = 0;
    public float DATA_24113_QF3_Percent = 0;
    public float DATA_24114_QF4_Execute = 0;
    public float DATA_24114_QF4_Successful = 0;
    public float DATA_24114_QF4_Percent = 0;
    public float DATA_24115_QF5F_Execute = 0;
    public float DATA_24115_QF5F_Successful = 0;
    public float DATA_24115_QF5F_Percent = 0;




    #region Defense Data

    /////////////////////// Dodge Direction Data  //////////////////////////
    #region Dodge Direction Data

    public float DATA_4020X_Dodge_Total_Execute = 0;
    public float DATA_4020X_Dodge_Total_Successful = 0;
    public float DATA_4020X_Dodge_Total_Percent = 0;
    public float DATA_40201_Dodge_FOR_Execute = 0;
    public float DATA_40201_Dodge_FOR_Successful = 0;
    public float DATA_40201_Dodge_FOR_Percent = 0;
    public float DATA_40202_Dodge_FORRIG_Execute = 0;
    public float DATA_40202_Dodge_FORRIG_Successful = 0;
    public float DATA_40202_Dodge_FORRIG_Percent = 0;
    public float DATA_40203_Dodge_RIG_Execute = 0;
    public float DATA_40203_Dodge_RIG_Successful = 0;
    public float DATA_40203_Dodge_RIG_Percent = 0;
    public float DATA_40204_Dodge_BACRIG_Execute = 0;
    public float DATA_40204_Dodge_BACRIG_Successful = 0;
    public float DATA_40204_Dodge_BACRIG_Percent = 0;
    public float DATA_40205_Dodge_BAC_Execute = 0;
    public float DATA_40205_Dodge_BAC_Successful = 0;
    public float DATA_40205_Dodge_BAC_Percent = 0;
    public float DATA_40206_Dodge_BACLET_Execute = 0;
    public float DATA_40206_Dodge_BACLET_Successful = 0;
    public float DATA_40206_Dodge_BACLET_Percent = 0;
    public float DATA_40207_Dodge_LET_Execute = 0;
    public float DATA_40207_Dodge_LET_Successful = 0;
    public float DATA_40207_Dodge_LET_Percent = 0;
    public float DATA_40208_Dodge_FORLET_Execute = 0;
    public float DATA_40208_Dodge_FORLET_Successful = 0;
    public float DATA_40208_Dodge_FORLET_Percent = 0;

    public float DATA_4220X_CDodge_Total_Execute = 0;
    public float DATA_4220X_CDodge_Total_Successful = 0;
    public float DATA_4220X_CDodge_Total_Percent = 0;
    public float DATA_42201_CDodge_FOR_Execute = 0;
    public float DATA_42201_CDodge_FOR_Successful = 0;
    public float DATA_42201_CDodge_FOR_Percent = 0;
    public float DATA_42202_CDodge_FORRIG_Execute = 0;
    public float DATA_42202_CDodge_FORRIG_Successful = 0;
    public float DATA_42202_CDodge_FORRIG_Percent = 0;
    public float DATA_42203_CDodge_RIG_Execute = 0;
    public float DATA_42203_CDodge_RIG_Successful = 0;
    public float DATA_42203_CDodge_RIG_Percent = 0;
    public float DATA_42204_CDodge_BACRIG_Execute = 0;
    public float DATA_42204_CDodge_BACRIG_Successful = 0;
    public float DATA_42204_CDodge_BACRIG_Percent = 0;
    public float DATA_42205_CDodge_BAC_Execute = 0;
    public float DATA_42205_CDodge_BAC_Successful = 0;
    public float DATA_42205_CDodge_BAC_Percent = 0;
    public float DATA_42206_CDodge_BACLET_Execute = 0;
    public float DATA_42206_CDodge_BACLET_Successful = 0;
    public float DATA_42206_CDodge_BACLET_Percent = 0;
    public float DATA_42207_CDodge_LET_Execute = 0;
    public float DATA_42207_CDodge_LET_Successful = 0;
    public float DATA_42207_CDodge_LET_Percent = 0;
    public float DATA_42208_CDodge_FORLET_Execute = 0;
    public float DATA_42208_CDodge_FORLET_Successful = 0;
    public float DATA_42208_CDodge_FORLET_Percent = 0;

    #endregion

    /////////////////////// Dodge Attack Data  //////////////////////////
    #region Dodge Attack Data
    public float DATA_Dodge_20011_BN1_Successful = 0;
    public float DATA_Dodge_20012_BN2_Successful = 0;
    public float DATA_Dodge_20013_BN3_Successful = 0;
    public float DATA_Dodge_20014_BN4F_Successful = 0;
    public float DATA_Dodge_20111_BF1_Successful = 0;
    public float DATA_Dodge_20112_BF2_Successful = 0;
    public float DATA_Dodge_20113_BF3_Successful = 0;
    public float DATA_Dodge_20114_BF4F_Successful = 0;

    public float DATA_Dodge_22011_HN1_Successful = 0;
    public float DATA_Dodge_22012_HN2_Successful = 0;
    public float DATA_Dodge_22013_HN3F_Successful = 0;
    public float DATA_Dodge_22111_HF1_Successful = 0;
    public float DATA_Dodge_22112_HF2_Successful = 0;
    public float DATA_Dodge_22113_HF3F_Successful = 0;

    public float DATA_Dodge_24011_QN1_Successful = 0;
    public float DATA_Dodge_24012_QN2_Successful = 0;
    public float DATA_Dodge_24013_QN3_Successful = 0;
    public float DATA_Dodge_24014_QN4_Successful = 0;
    public float DATA_Dodge_24015_QN5F_Successful = 0;
    public float DATA_Dodge_24111_QF1_Successful = 0;
    public float DATA_Dodge_24112_QF2_Successful = 0;
    public float DATA_Dodge_24113_QF3_Successful = 0;
    public float DATA_Dodge_24114_QF4_Successful = 0;
    public float DATA_Dodge_24115_QF5F_Successful = 0;

    public float DATA_Dodge_4220X_CDODGE_Successful = 0;



    public float DATA_Dodge_20011_BN1_Successful_Point = 0;
    public float DATA_Dodge_20012_BN2_Successful_Point = 0;
    public float DATA_Dodge_20013_BN3_Successful_Point = 0;
    public float DATA_Dodge_20014_BN4F_Successful_Point = 0;
    public float DATA_Dodge_20111_BF1_Successful_Point = 0;
    public float DATA_Dodge_20112_BF2_Successful_Point = 0;
    public float DATA_Dodge_20113_BF3_Successful_Point = 0;
    public float DATA_Dodge_20114_BF4F_Successful_Point = 0;

    public float DATA_Dodge_22011_HN1_Successful_Point = 0;
    public float DATA_Dodge_22012_HN2_Successful_Point = 0;
    public float DATA_Dodge_22013_HN3F_Successful_Point = 0;
    public float DATA_Dodge_22111_HF1_Successful_Point = 0;
    public float DATA_Dodge_22112_HF2_Successful_Point = 0;
    public float DATA_Dodge_22113_HF3F_Successful_Point = 0;

    public float DATA_Dodge_24011_QN1_Successful_Point = 0;
    public float DATA_Dodge_24012_QN2_Successful_Point = 0;
    public float DATA_Dodge_24013_QN3_Successful_Point = 0;
    public float DATA_Dodge_24014_QN4_Successful_Point = 0;
    public float DATA_Dodge_24015_QN5F_Successful_Point = 0;
    public float DATA_Dodge_24111_QF1_Successful_Point = 0;
    public float DATA_Dodge_24112_QF2_Successful_Point = 0;
    public float DATA_Dodge_24113_QF3_Successful_Point = 0;
    public float DATA_Dodge_24114_QF4_Successful_Point = 0;
    public float DATA_Dodge_24115_QF5F_Successful_Point = 0;

    public float DATA_Dodge_4220X_CDODGE_Successful_Point = 0;


    #endregion

    #region Block Attack Data
    public float DATA_Block_20011_BN1_Successful = 0;
    public float DATA_Block_20012_BN2_Successful = 0;
    public float DATA_Block_20013_BN3_Successful = 0;
    public float DATA_Block_20014_BN4F_Successful = 0;
    public float DATA_Block_20111_BF1_Successful = 0;
    public float DATA_Block_20112_BF2_Successful = 0;
    public float DATA_Block_20113_BF3_Successful = 0;
    public float DATA_Block_20114_BF4F_Successful = 0;

    public float DATA_Block_22011_HN1_Successful = 0;
    public float DATA_Block_22012_HN2_Successful = 0;
    public float DATA_Block_22013_HN3F_Successful = 0;
    public float DATA_Block_22111_HF1_Successful = 0;
    public float DATA_Block_22112_HF2_Successful = 0;
    public float DATA_Block_22113_HF3F_Successful = 0;

    public float DATA_Block_24011_QN1_Successful = 0;
    public float DATA_Block_24012_QN2_Successful = 0;
    public float DATA_Block_24013_QN3_Successful = 0;
    public float DATA_Block_24014_QN4_Successful = 0;
    public float DATA_Block_24015_QN5F_Successful = 0;
    public float DATA_Block_24111_QF1_Successful = 0;
    public float DATA_Block_24112_QF2_Successful = 0;
    public float DATA_Block_24113_QF3_Successful = 0;
    public float DATA_Block_24114_QF4_Successful = 0;
    public float DATA_Block_24115_QF5F_Successful = 0;

    public float DATA_Block_4220X_CDODGE_Successful = 0;



    public float DATA_Block_20011_BN1_Successful_Point = 0;
    public float DATA_Block_20012_BN2_Successful_Point = 0;
    public float DATA_Block_20013_BN3_Successful_Point = 0;
    public float DATA_Block_20014_BN4F_Successful_Point = 0;
    public float DATA_Block_20111_BF1_Successful_Point = 0;
    public float DATA_Block_20112_BF2_Successful_Point = 0;
    public float DATA_Block_20113_BF3_Successful_Point = 0;
    public float DATA_Block_20114_BF4F_Successful_Point = 0;

    public float DATA_Block_22011_HN1_Successful_Point = 0;
    public float DATA_Block_22012_HN2_Successful_Point = 0;
    public float DATA_Block_22013_HN3F_Successful_Point = 0;
    public float DATA_Block_22111_HF1_Successful_Point = 0;
    public float DATA_Block_22112_HF2_Successful_Point = 0;
    public float DATA_Block_22113_HF3F_Successful_Point = 0;

    public float DATA_Block_24011_QN1_Successful_Point = 0;
    public float DATA_Block_24012_QN2_Successful_Point = 0;
    public float DATA_Block_24013_QN3_Successful_Point = 0;
    public float DATA_Block_24014_QN4_Successful_Point = 0;
    public float DATA_Block_24015_QN5F_Successful_Point = 0;
    public float DATA_Block_24111_QF1_Successful_Point = 0;
    public float DATA_Block_24112_QF2_Successful_Point = 0;
    public float DATA_Block_24113_QF3_Successful_Point = 0;
    public float DATA_Block_24114_QF4_Successful_Point = 0;
    public float DATA_Block_24115_QF5F_Successful_Point = 0;

    public float DATA_Block_4220X_CDODGE_Successful_Point = 0;


    #endregion

    public float DATA_Total_Dodge_Off_Execute = 0;
    public float DATA_Total_Dodge_Off_Successful = 0;
    public float DATA_Total_Dodge_Off_Percent = 0;
    public float DATA_Total_Dodge_DiaOff_Execute = 0;
    public float DATA_Total_Dodge_DiaOff_Successful = 0;
    public float DATA_Total_Dodge_DiaOff_Percent = 0;
    public float DATA_Total_Dodge_Side_Execute = 0;
    public float DATA_Total_Dodge_Side_Successful = 0;
    public float DATA_Total_Dodge_Side_Percent = 0;
    public float DATA_Total_Dodge_DiaDef_Execute = 0;
    public float DATA_Total_Dodge_DiaDef_Successful = 0;
    public float DATA_Total_Dodge_DiaDef_Percent = 0;
    public float DATA_Total_Dodge_Def_Execute = 0;
    public float DATA_Total_Dodge_Def_Successful = 0;
    public float DATA_Total_Dodge_Def_Percent = 0;

    #endregion
    /// <summary>
    /// //
    /// </summary>
    /// 
    /////////////////////// For Score_1_Attack //////////////////////////


    public float DATA_Total_ATK_Close_Successful_Point = 0;
    /// <summary>
    /// /////afwagwg
    /// </summary>
    public float DATA_Total_ATK_Mid_Successful_Point = 0;
    public float DATA_Total_ATK_Cone_Successful_Point = 0;
    public float DATA_Total_PATK_Close_Successful_Point = 0;
    public float DATA_Total_PATK_Far_Successful_Point = 0;
    public float DATA_Total_FIN_Close_Successful_Point = 0;
    public float DATA_Total_FIN_Far_Successful_Point = 0;



    public float DATA_20011_BN1_Successful_Point = 0;
    public float DATA_20012_BN2_Successful_Point = 0;
    public float DATA_20013_BN3_Successful_Point = 0;
    public float DATA_20014_BN4F_Successful_Point = 0;
    public float DATA_20111_BF1_Successful_Point = 0;
    public float DATA_20112_BF2_Successful_Point = 0;
    public float DATA_20113_BF3_Successful_Point = 0;
    public float DATA_20114_BF4F_Successful_Point = 0;

    public float DATA_22011_HN1_Successful_Point = 0;
    public float DATA_22012_HN2_Successful_Point = 0;
    public float DATA_22013_HN3F_Successful_Point = 0;
    public float DATA_22111_HF1_Successful_Point = 0;
    public float DATA_22112_HF2_Successful_Point = 0;
    public float DATA_22113_HF3F_Successful_Point = 0;

    public float DATA_24011_QN1_Successful_Point = 0;
    public float DATA_24012_QN2_Successful_Point = 0;
    public float DATA_24013_QN3_Successful_Point = 0;
    public float DATA_24014_QN4_Successful_Point = 0;
    public float DATA_24015_QN5F_Successful_Point = 0;
    public float DATA_24111_QF1_Successful_Point = 0;
    public float DATA_24112_QF2_Successful_Point = 0;
    public float DATA_24113_QF3_Successful_Point = 0;
    public float DATA_24114_QF4_Successful_Point = 0;
    public float DATA_24115_QF5F_Successful_Point = 0;
    /// /////////////////////////////////////////////////////////////////////


    

    /////////////////////// For Score_2_Dodge Old //////////////////////////
    public float DATA_Total_Dodge_Atk_Close_Successful = 0;
    public float DATA_Total_Dodge_Atk_Mid_Successful = 0;
    public float DATA_Total_Dodge_Atk_Cone_Successful = 0;
    public float DATA_Total_Dodge_Pre_Close_Successful = 0;
    public float DATA_Total_Dodge_Pre_Far_Successful = 0;
    public float DATA_Total_Dodge_Fin_Close_Successful = 0;
    public float DATA_Total_Dodge_Fin_Far_Successful = 0;

    public float DATA_Total_Dodge_Atk_Close_Successful_Point = 0;
    public float DATA_Total_Dodge_Atk_Mid_Successful_Point = 0;
    public float DATA_Total_Dodge_Atk_Cone_Successful_Point = 0;
    public float DATA_Total_Dodge_Pre_Close_Successful_Point = 0;
    public float DATA_Total_Dodge_Pre_Far_Successful_Point = 0;
    public float DATA_Total_Dodge_Fin_Close_Successful_Point = 0;
    public float DATA_Total_Dodge_Fin_Far_Successful_Point = 0;
    /// /////////////////////////////////////////////////////////////////////

    /// <summary>
    /// ////////////////////////////////
    /// </summary>
    private int Last_Dodge_Code = 0;
    //public int Last_Attack_Code_Hit_Check = 0;

    /*
    public Text DATA_PlayTime_text;
    public Text DATA_Total_Attack_Exeute_text;
    public Text DATA_Total_Attack_Hit_text;
    public Text DATA_Total_Dodge_Execute_text;
    public Text DATA_Total_Dodge_Sucessful_text;
    public Text DATA_Total_Block_Execute_text;
    public Text DATA_Total_Block_Sucessful_text;
    public Text DATA_Total_Jump_Execute_text;
    public Text DATA_Total_Jump_Sucessful_text;
    public Text DATA_Average_Distance_Between_text;
    */
    public float Dodge_Count_Down = 0f;

    public float Dodge_Check_Countdown = 0.65f;
    public float Dodge_Score_Time_Weight = 0.6f;
    #endregion

    #region Data Process


    public float StylePoint = 0;
    public int styleRank = 0; // D0 C1 B2 A3 S4 SS5 SSS6

    private static int Combo_List_Storage = 300;
    private static int Combo_Count_List_Storage = 20;
    public List<List<int>> Last_Combo_Code_List = new List<List<int>>();
    public List<List<int>>  Last_Combo_Code_List_2nd = new List<List<int>>();
    public int ComboCount = -1;
    public int NumberOfComboCount = 0;
    private int Last_Attack_Code_In_Combo = 0;

    public void AddComboListData(int code, bool newComboChain)
    {
        //print("newComboChain = " + newComboChain);
        if (newComboChain)
        {
             ComboCount= 0;
            NumberOfComboCount++;         
            if(ComboCount > Combo_List_Storage)
            {

                Last_Combo_Code_List_2nd = Last_Combo_Code_List;
                //Last_Combo_Code_List = new int[Combo_List_Storage, Combo_Count_List_Storage];
                ComboCount = 0;
            }
            while( (Last_Combo_Code_List.Count) <= NumberOfComboCount)
            {
                Last_Combo_Code_List.Add(new List<int>());
            }

            Last_Combo_Code_List[NumberOfComboCount].Add(code);
        }
        else
        {
            while ((Last_Combo_Code_List.Count) <= NumberOfComboCount)
            {
                Last_Combo_Code_List.Add(new List<int>());
            }
            Last_Combo_Code_List[NumberOfComboCount].Add(code);

        }
        
        //print("Last_Combo_Code_List " + ComboCount + " , " + NumberOfComboCount + "       " + code);
        if (NumberOfComboCount < Combo_Count_List_Storage)
        {
            
            //(Last_Combo_Code_List[ComboCount])[NumberOfComboCount] = code;
            
        }

        /*
        print((Last_Combo_Code_List[0])[0]  + " " + (Last_Combo_Code_List[0])[1] + " " + (Last_Combo_Code_List[0])[2] + " " + (Last_Combo_Code_List[0])[3] + " " + (Last_Combo_Code_List[0])[4]  );
        print((Last_Combo_Code_List[1])[0] + " " + (Last_Combo_Code_List[1])[1] + " " + (Last_Combo_Code_List[1])[2] + " " + (Last_Combo_Code_List[1])[3] + " " + (Last_Combo_Code_List[1])[4]  );
        print((Last_Combo_Code_List[2])[0] + " " + (Last_Combo_Code_List[2])[1] + " " + (Last_Combo_Code_List[2])[2] + " " + (Last_Combo_Code_List[2])[3] + " " + (Last_Combo_Code_List[2])[4]  );
        */
    }

    int LastAttack_InputType = 0; // 1 = Circle, 2 = Triangle
    int LastAttack_ModeType = 0; // 1 = Balance, 2 = Heavy, 3 = Quick

    private void printLast_Combo_Code_List()
    {
        for(int i = 0; i < Last_Combo_Code_List.Count; i++)
        {
            string a = "{";
            for (int j = 0; j < Last_Combo_Code_List[i].Count; j++)
            {
                a += (Last_Combo_Code_List[i])[j] + ", ";
            }
            a += "}";
            print(a);
        }
    }

    private float CalculateStylePointMulti(int code, float point)
    {
        float multiplier = 1;
        // Point Redection for redendent combo\



        for (int i = 0; i < NumberOfComboCount; i++)
        {
            print("Combo Count = " + ComboCount + "i = " + i);
            if (ComboCount >= 3)
            {
                
                if ((Last_Combo_Code_List[i])[ComboCount - 1] == code) multiplier -= 0.5f;
                if ((Last_Combo_Code_List[i])[ComboCount - 2] == code || (Last_Combo_Code_List[ComboCount - 3])[i] == code) multiplier -= 0.5f;
                else if ((Last_Combo_Code_List[i])[ComboCount - 2] == code) multiplier -= 0.25f;
            }
            else if(ComboCount == 2){
                if ((Last_Combo_Code_List[i])[ComboCount - 1] == code) multiplier -= 0.5f;
                
                if ((Last_Combo_Code_List[i])[ComboCount - 2] == code) multiplier -= 0.25f;
            }
            else if (ComboCount == 1)
            {
                if ((Last_Combo_Code_List[i])[ComboCount - 1] == code) multiplier -= 0.5f; ////////////ERROR HERE ///////////////////////////
            }
        }
        int Attack_InputType = 0; // 1 = Circle, 2 = Triangle
        int Attack_ModeType = 0; // 1 = Balance, 2 = Heavy, 3 = Quick
        
            if (code >= 20000 && code < 20100)
            {
                Attack_InputType = 1;
                Attack_ModeType = 1;
            }
            else if (code >= 20100 && code < 20200)
            {
                Attack_InputType = 2;
                Attack_ModeType = 1;
            }
            if (code >= 22000 && code < 22100)
            {
                Attack_InputType = 1;
                Attack_ModeType = 2;
            }
            else if (code >= 22100 && code < 22200)
            {
                Attack_InputType = 2;
                Attack_ModeType = 2;
            }
            if (code >= 24000 && code < 24100)
            {
                Attack_InputType = 1;
                Attack_ModeType = 3;
            }
            else if (code >= 24100 && code < 24200)
            {
                Attack_InputType = 2;
                Attack_ModeType = 3;
            }



        if (NumberOfComboCount > 0)
        {
            if (Attack_InputType != LastAttack_InputType) multiplier = multiplier * 1.2f;
            if (Attack_ModeType != LastAttack_ModeType) multiplier = multiplier * 1.5f;
        }
        print("StylePoint Multiplier = " + multiplier + "   " + Attack_InputType + "-" + LastAttack_InputType + "    " + Attack_ModeType + "-" + LastAttack_ModeType);
        Last_Attack_Code_In_Combo = code;
        LastAttack_InputType = Attack_InputType;
        LastAttack_ModeType = Attack_ModeType;

        printLast_Combo_Code_List();



        return multiplier;
    }

    public void AddData_AttackHit(int code, float point)
    {
        float Multiplier = CalculateStylePointMulti(code, point);
        int StyleP = 0;
        switch (code)
        {
            //Balance Mode
            case 20011:
                DATA_20011_BN1_Successful++;
                DATA_20011_BN1_Successful_Point += point;
                StyleP += characterStat.CommandList[7].stylePoint;
                break;
            case 20012:
                DATA_20012_BN2_Successful++;
                DATA_20012_BN2_Successful_Point += point;
                StyleP += characterStat.CommandList[8].stylePoint;
                break;
            case 20013:
                DATA_20013_BN3_Successful++;
                DATA_20013_BN3_Successful_Point += point;
                StyleP += characterStat.CommandList[9].stylePoint;
                break;
            case 20014:
                DATA_20014_BN4F_Successful++;
                DATA_20014_BN4F_Successful_Point += point;
                StyleP += characterStat.CommandList[10].stylePoint;
                break;

            case 20111:
                DATA_20111_BF1_Successful++;
                DATA_20111_BF1_Successful_Point += point;
                StyleP += characterStat.CommandList[11].stylePoint;
                break;
            case 20112:
                DATA_20112_BF2_Successful++;
                DATA_20112_BF2_Successful_Point += point;
                StyleP += characterStat.CommandList[12].stylePoint;
                break;
            case 20113:
                DATA_20113_BF3_Successful++;
                DATA_20113_BF3_Successful_Point += point;
                StyleP += characterStat.CommandList[13].stylePoint;
                break;
            case 20114:
                DATA_20114_BF4F_Successful++;
                DATA_20114_BF4F_Successful_Point += point;
                StyleP += characterStat.CommandList[14].stylePoint;
                break;

            //Heavy Mode
            case 22011:
                DATA_22011_HN1_Successful++;
                DATA_22011_HN1_Successful_Point += point;
                StyleP += characterStat.CommandList[15].stylePoint;
                break;
            case 22012:
                DATA_22012_HN2_Successful++;
                DATA_22012_HN2_Successful_Point += point;
                StyleP += characterStat.CommandList[16].stylePoint;
                break;
            case 22013:
                DATA_22013_HN3F_Successful++;
                DATA_22013_HN3F_Successful_Point += point;
                StyleP += characterStat.CommandList[17].stylePoint;
                break;

            case 22111:
                DATA_22111_HF1_Successful++;
                DATA_22111_HF1_Successful_Point += point;
                StyleP += characterStat.CommandList[18].stylePoint;
                break;
            case 22112:
                DATA_22112_HF2_Successful++;
                DATA_22112_HF2_Successful_Point += point;
                StyleP += characterStat.CommandList[19].stylePoint;
                break;
            case 22113:
                DATA_22113_HF3F_Successful++;
                DATA_22113_HF3F_Successful_Point += point;
                StyleP += characterStat.CommandList[20].stylePoint;
                break;

            //Quick Mode
            case 24011:
                DATA_24011_QN1_Successful++;
                DATA_24011_QN1_Successful_Point += point;
                StyleP += characterStat.CommandList[21].stylePoint;
                break;
            case 24012:
                DATA_24012_QN2_Successful++;
                DATA_24012_QN2_Successful_Point += point;
                StyleP += characterStat.CommandList[22].stylePoint;
                break;
            case 24013:
                DATA_24013_QN3_Successful++;
                DATA_24013_QN3_Successful_Point += point;
                StyleP += characterStat.CommandList[23].stylePoint;
                break;
            case 24014:
                DATA_24014_QN4_Successful++;
                DATA_24014_QN4_Successful_Point += point;
                StyleP += characterStat.CommandList[24].stylePoint;
                break;
            case 24015:
                DATA_24015_QN5F_Successful++;
                DATA_24015_QN5F_Successful_Point += point;
                StyleP += characterStat.CommandList[25].stylePoint;
                break;

            case 24111:
                DATA_24111_QF1_Successful++;
                DATA_24111_QF1_Successful_Point += point;
                StyleP += characterStat.CommandList[26].stylePoint;
                break;
            case 24112:
                DATA_24112_QF2_Successful++;
                DATA_24112_QF2_Successful_Point += point;
                StyleP += characterStat.CommandList[27].stylePoint;
                break;
            case 24113:
                DATA_24113_QF3_Successful++;
                DATA_24113_QF3_Successful_Point += point;
                StyleP += characterStat.CommandList[28].stylePoint;
                break;
            case 24114:
                DATA_24114_QF4_Successful++;
                DATA_24114_QF4_Successful_Point += point;
                StyleP += characterStat.CommandList[29].stylePoint;
                break;
            case 24115:
                DATA_24115_QF5F_Successful++;
                DATA_24115_QF5F_Successful_Point += point;
                StyleP += characterStat.CommandList[30].stylePoint;
                break;
        }
        
        StylePoint += StyleP * Multiplier;
        print("StylePointM = " + StylePoint);
    }

    public float stylePointReduce_D = 5;
    public float stylePointReduce_C = 5.5f;
    public float stylePointReduce_B = 6;
    public float stylePointReduce_A = 6.5f;
    public float stylePointReduce_S = 7.5f;
    public float stylePointReduce_SS = 9f;
    public float stylePointReduce_SSS = 12f;
    float stylePointReductionPerSec = 5;

    public void StylePointManage()
    {
        if (StylePoint < 100)
        {
            styleRank = 0;
            stylePointReductionPerSec = stylePointReduce_D;
        }
        else if (StylePoint < 220) { styleRank = 1; stylePointReductionPerSec = stylePointReduce_C * 1.2f; } // 120
        else if (StylePoint < 360) { styleRank = 2; stylePointReductionPerSec = stylePointReduce_B * 1.4f; } // 140
        else if (StylePoint < 520) { styleRank = 3; stylePointReductionPerSec = stylePointReduce_A * 1.6f; }  // 160
        else if (StylePoint < 700) { styleRank = 4; stylePointReductionPerSec = stylePointReduce_S * 1.8f; }  // 180
        else if (StylePoint < 920) { styleRank = 5; stylePointReductionPerSec = stylePointReduce_SS * 2.2f; }  // 220
        else if (StylePoint < 1220) { styleRank = 6; stylePointReductionPerSec = stylePointReduce_SSS * 3.0f; }   // 300
        else styleRank = -1;

        float TIME = Time.deltaTime;

        if (StylePoint > 0)
        {
            StylePoint -= stylePointReductionPerSec * TIME;
        }
        else StylePoint = 0;


     }

    public void DeStyleRank()
    {
        if (StylePoint < 100)
        {
            StylePoint = 0;
        }
        else if (StylePoint < 220) StylePoint -= 120; // 120
        else if (StylePoint < 360) StylePoint -= 140; // 140
        else if (StylePoint < 520) StylePoint -= 160;  // 160
        else if (StylePoint < 700) StylePoint -= 180;  // 180
        else if (StylePoint < 920) StylePoint -= 220;  // 220
        else if (StylePoint < 1220) StylePoint -= 300;   // 300
        else styleRank = -1;
    }



   public void AddData_DodgeAttack(int Last_Opponent_State, float Multiplier)
    {
        
        int StyleP = 30;
        switch (Last_Opponent_State)
        {
            //Balance
            case Battle_Controller.S_ATK_BAL_N1:
                DATA_Dodge_20011_BN1_Successful += 1;
                DATA_Dodge_20011_BN1_Successful_Point += Multiplier;                      
                break;
            case Battle_Controller.S_ATK_BAL_N2:
                DATA_Dodge_20012_BN2_Successful += 1;
                DATA_Dodge_20012_BN2_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_BAL_N3:
                DATA_Dodge_20013_BN3_Successful += 1;
                DATA_Dodge_20013_BN3_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_BAL_N4:
                DATA_Dodge_20014_BN4F_Successful += 1;
                DATA_Dodge_20014_BN4F_Successful_Point +=Multiplier;
                break;

            case Battle_Controller.S_ATK_BAL_F1:
                DATA_Dodge_20111_BF1_Successful += 1;
                DATA_Dodge_20111_BF1_Successful_Point +=Multiplier;
                break;
            case Battle_Controller.S_ATK_BAL_F2:
                DATA_Dodge_20112_BF2_Successful += 1;
                DATA_Dodge_20112_BF2_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_BAL_F3:
                DATA_Dodge_20113_BF3_Successful += 1;
                DATA_Dodge_20113_BF3_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_BAL_F4:
                DATA_Dodge_20114_BF4F_Successful += 1;
                DATA_Dodge_20114_BF4F_Successful_Point += Multiplier;
                break;

            //Heavy
            case Battle_Controller.S_ATK_HEV_N1:
                DATA_Dodge_22011_HN1_Successful += 1;
                DATA_Dodge_22011_HN1_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_HEV_N2:
                DATA_Dodge_22012_HN2_Successful += 1;
                DATA_Dodge_22012_HN2_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_HEV_N3:
                DATA_Dodge_22013_HN3F_Successful += 1;
                DATA_Dodge_22013_HN3F_Successful_Point += Multiplier;
                break;

            case Battle_Controller.S_ATK_HEV_F1:
                DATA_Dodge_22111_HF1_Successful += 1;
                DATA_Dodge_22111_HF1_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_HEV_F2:
                DATA_Dodge_22112_HF2_Successful += 1;
                DATA_Dodge_22112_HF2_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_HEV_F3:
                DATA_Dodge_22113_HF3F_Successful += 1;
                DATA_Dodge_22113_HF3F_Successful_Point += Multiplier;
                break;

            //Quick
            case Battle_Controller.S_ATK_QUK_N1:
                DATA_Dodge_24011_QN1_Successful += 1;
                DATA_Dodge_24011_QN1_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_N2:
                DATA_Dodge_24012_QN2_Successful += 1;
                DATA_Dodge_24012_QN2_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_N3:
                DATA_Dodge_24013_QN3_Successful += 1;
                DATA_Dodge_24013_QN3_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_N4:
                DATA_Dodge_24014_QN4_Successful += 1;
                DATA_Dodge_24014_QN4_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_N5:
                DATA_Dodge_24015_QN5F_Successful += 1;
                DATA_Dodge_24015_QN5F_Successful_Point += Multiplier;
                break;

            case Battle_Controller.S_ATK_QUK_F1:
                DATA_Dodge_24111_QF1_Successful += 1;
                DATA_Dodge_24111_QF1_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_F2:
                DATA_Dodge_24112_QF2_Successful += 1;
                DATA_Dodge_24112_QF2_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_F3:
                DATA_Dodge_24113_QF3_Successful += 1;
                DATA_Dodge_24113_QF3_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_F4:
                DATA_Dodge_24114_QF4_Successful += 1;
                DATA_Dodge_24114_QF4_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_F5:
                DATA_Dodge_24115_QF5F_Successful += 1;
                DATA_Dodge_24115_QF5F_Successful_Point += Multiplier;
                break;

        }
        if(Last_Opponent_State >= Battle_Controller.S_CHAGE_DODGE &&
            Last_Opponent_State <= Battle_Controller.S_CDODGE_FOR_LEFT)
        {
            DATA_Dodge_4220X_CDODGE_Successful += 1;
            DATA_Dodge_4220X_CDODGE_Successful_Point += Multiplier;
        }
        //StylePoint += StyleP;
        //StylePoint += StyleP * Multiplier;
        //print("StylePointM = " + StylePoint);
    }

    public void AddData_DodgeDirection(bool Is_Dodge, bool Is_ChargeDodge, int Last_Dodge_Dir_Code)
    {
        int StyleP = 30;
        //DATA_Total_Dodge_Sucessful += 1;
        if (Is_Dodge)
        {


            DATA_4020X_Dodge_Total_Successful += 1;
            switch (Last_Dodge_Code)
            {
                case 1:
                    DATA_40201_Dodge_FOR_Successful += 1;
                    break;
                case 2:
                    DATA_40202_Dodge_FORRIG_Successful += 1;
                    break;
                case 3:
                    DATA_40203_Dodge_RIG_Successful += 1;
                    break;
                case 4:
                    DATA_40204_Dodge_BACRIG_Successful += 1;
                    break;
                case 5:
                    DATA_40205_Dodge_BAC_Successful += 1;
                    break;
                case 6:
                    DATA_40206_Dodge_BACLET_Successful += 1;
                    break;
                case 7:
                    DATA_40207_Dodge_LET_Successful += 1;
                    break;
                case 8:
                    DATA_40208_Dodge_FORLET_Successful += 1;
                    break;
            }
        }
        else if (Is_ChargeDodge)
        {
            DATA_Dodge_4220X_CDODGE_Successful += 1;
            switch (Last_Dodge_Code)
            {
                case 1:
                    DATA_42201_CDodge_FOR_Successful += 1;
                    break;
                case 2:
                    DATA_42202_CDodge_FORRIG_Successful += 1;
                    break;
                case 3:
                    DATA_42203_CDodge_RIG_Successful += 1;
                    break;
                case 4:
                    DATA_42204_CDodge_BACRIG_Successful += 1;
                    break;
                case 5:
                    DATA_42205_CDodge_BAC_Successful += 1;
                    break;
                case 6:
                    DATA_42206_CDodge_BACLET_Successful += 1;
                    break;
                case 7:
                    DATA_42207_CDodge_LET_Successful += 1;
                    break;
                case 8:
                    DATA_42208_CDodge_FORLET_Successful += 1;
                    break;
            }
        }
        StylePoint += StyleP;
        //StylePoint += StyleP * Multiplier;
        //print("StylePointM = " + StylePoint);
    }
    #endregion

    public void AddData_DodgeFail(bool Is_Dodge, bool Is_ChargeDodge, int Last_Dodge_Code)
    {
        if (Is_Dodge)
        {
      

            DATA_4020X_Dodge_Total_Successful -= 1;
            switch(Last_Dodge_Code)
            {
                case 1:
                    DATA_40201_Dodge_FOR_Successful -= 1;
                    break;
                case 2:
                    DATA_40202_Dodge_FORRIG_Successful -= 1;
                    break;
                case 3:
                    DATA_40203_Dodge_RIG_Successful -= 1;
                    break;
                case 4:
                    DATA_40204_Dodge_BACRIG_Successful -= 1;
                    break;
                case 5:
                    DATA_40205_Dodge_BAC_Successful -= 1;
                    break;
                case 6:
                    DATA_40206_Dodge_BACLET_Successful -= 1;
                    break;
                case 7:
                    DATA_40207_Dodge_LET_Successful -= 1;
                    break;
                case 8:
                    DATA_40208_Dodge_FORLET_Successful -= 1;
                    break;
            }
        }
        else if (Is_ChargeDodge)
        {
            DATA_Dodge_4220X_CDODGE_Successful -= 1;
            switch (Last_Dodge_Code)
            {
                case 1:
                    DATA_42201_CDodge_FOR_Successful -= 1;
                    break;
                case 2:
                    DATA_42202_CDodge_FORRIG_Successful -= 1;
                    break;
                case 3:
                    DATA_42203_CDodge_RIG_Successful -= 1;
                    break;
                case 4:
                    DATA_42204_CDodge_BACRIG_Successful -= 1;
                    break;
                case 5:
                    DATA_42205_CDodge_BAC_Successful -= 1;
                    break;
                case 6:
                    DATA_42206_CDodge_BACLET_Successful -= 1;
                    break;
                case 7:
                    DATA_42207_CDodge_LET_Successful -= 1;
                    break;
                case 8:
                    DATA_42208_CDodge_FORLET_Successful -= 1;
                    break;
            }
        }  
    }

    public void AddData_BlockAttack(int Last_Opponent_State, float Multiplier)
    {
        print(Last_Opponent_State + " Last_Opponent_State ");
        int StyleP = 20;
        switch (Last_Opponent_State)
        {
            //Balance
            case Battle_Controller.S_ATK_BAL_N1:
                DATA_Block_20011_BN1_Successful += 1;
                DATA_Block_20011_BN1_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_BAL_N2:
                DATA_Block_20012_BN2_Successful += 1;
                DATA_Block_20012_BN2_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_BAL_N3:
                DATA_Block_20013_BN3_Successful += 1;
                DATA_Block_20013_BN3_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_BAL_N4:
                DATA_Block_20014_BN4F_Successful += 1;
                DATA_Block_20014_BN4F_Successful_Point += Multiplier;
                break;

            case Battle_Controller.S_ATK_BAL_F1:
                DATA_Block_20111_BF1_Successful += 1;
                DATA_Block_20111_BF1_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_BAL_F2:
                DATA_Block_20112_BF2_Successful += 1;
                DATA_Block_20112_BF2_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_BAL_F3:
                DATA_Block_20113_BF3_Successful += 1;
                DATA_Block_20113_BF3_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_BAL_F4:
                DATA_Block_20114_BF4F_Successful += 1;
                DATA_Block_20114_BF4F_Successful_Point += Multiplier;
                break;

            //Heavy
            case Battle_Controller.S_ATK_HEV_N1:
                DATA_Block_22011_HN1_Successful += 1;
                DATA_Block_22011_HN1_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_HEV_N2:
                DATA_Block_22012_HN2_Successful += 1;
                DATA_Block_22012_HN2_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_HEV_N3:
                DATA_Block_22013_HN3F_Successful += 1;
                DATA_Block_22013_HN3F_Successful_Point += Multiplier;
                break;

            case Battle_Controller.S_ATK_HEV_F1:
                DATA_Block_22111_HF1_Successful += 1;
                DATA_Block_22111_HF1_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_HEV_F2:
                DATA_Block_22112_HF2_Successful += 1;
                DATA_Block_22112_HF2_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_HEV_F3:
                DATA_Block_22113_HF3F_Successful += 1;
                DATA_Block_22113_HF3F_Successful_Point += Multiplier;
                break;

            //Quick
            case Battle_Controller.S_ATK_QUK_N1:
                DATA_Block_24011_QN1_Successful += 1;
                DATA_Block_24011_QN1_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_N2:
                DATA_Block_24012_QN2_Successful += 1;
                DATA_Block_24012_QN2_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_N3:
                DATA_Block_24013_QN3_Successful += 1;
                DATA_Block_24013_QN3_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_N4:
                DATA_Block_24014_QN4_Successful += 1;
                DATA_Block_24014_QN4_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_N5:
                DATA_Block_24015_QN5F_Successful += 1;
                DATA_Block_24015_QN5F_Successful_Point += Multiplier;
                break;

            case Battle_Controller.S_ATK_QUK_F1:
                DATA_Block_24111_QF1_Successful += 1;
                DATA_Block_24111_QF1_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_F2:
                DATA_Block_24112_QF2_Successful += 1;
                DATA_Block_24112_QF2_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_F3:
                DATA_Block_24113_QF3_Successful += 1;
                DATA_Block_24113_QF3_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_F4:
                DATA_Block_24114_QF4_Successful += 1;
                DATA_Block_24114_QF4_Successful_Point += Multiplier;
                break;
            case Battle_Controller.S_ATK_QUK_F5:
                DATA_Block_24115_QF5F_Successful += 1;
                DATA_Block_24115_QF5F_Successful_Point += Multiplier;
                break;

        }
        if (Last_Opponent_State >= Battle_Controller.S_CHAGE_DODGE &&
            Last_Opponent_State <= Battle_Controller.S_CDODGE_FOR_LEFT)
        {
            DATA_Block_4220X_CDODGE_Successful += 1;
            DATA_Block_4220X_CDODGE_Successful_Point += Multiplier;
        }
        StylePoint += StyleP;
        //StylePoint += StyleP * Multiplier;
        print("StylePointM = " + StylePoint);
    }

    void Start() {
        SaveDataToList(true);
    }

    void Update() {
                StylePointManage();
    }
    
    private Pair<int, float> CreatePair(int a, float b)
    {
        return new Pair<int, float>(a, b);
    }

    public void SaveDataToList(bool firstTime)
    {

        Pair<int, float>[] DataListSet = { 
                //// General/Common Data
                CreatePair(0,DATA_RemainHP_Percent) ,
                CreatePair(1,    DATA_PlayTime),
                CreatePair(2,    DATA_RunTime ),
                CreatePair(3,    DATA_RunTime_Percent ),
                CreatePair(4,    DATA_Total_Attack_Exeute ),
                CreatePair(5,    DATA_Total_Attack_Hit ),
                CreatePair(6,    DATA_Total_Attack_Percent ),
                CreatePair(7,    DATA_Total_Dodge_Execute ),
                CreatePair(8,    DATA_Total_Dodge_Sucessful ),
                CreatePair(9,    DATA_Total_Dodge_Percent ),
                CreatePair(10,    DATA_Total_Block_Execute ),
                CreatePair(11,    DATA_Total_Block_Sucessful ),
                CreatePair(12,    DATA_Total_Block_Percent ),
                CreatePair(13,    DATA_Average_Distance_Between ),
                CreatePair(14,    DATA_Pattern_Count ),

                 //// Offense Mode Data
                 /// Attacks Data
                // Balance Mode (B = Balance, N = Near, F = Far, Number = Number of attack in combo, F = the last attack of combo)
                CreatePair(200110,    DATA_20011_BN1_Execute ),
                CreatePair(200111,    DATA_20011_BN1_Successful ),
                CreatePair(200112,    DATA_20011_BN1_Percent ),
                CreatePair(200120,    DATA_20012_BN2_Execute ),
                CreatePair(200121,    DATA_20012_BN2_Successful ),
                CreatePair(200122,    DATA_20012_BN2_Percent ),
                CreatePair(200130,    DATA_20013_BN3_Execute ),
                CreatePair(200131,    DATA_20013_BN3_Successful ),
                CreatePair(200132,    DATA_20013_BN3_Percent ),
                CreatePair(200140,    DATA_20014_BN4F_Execute ),
                CreatePair(200141,    DATA_20014_BN4F_Successful ),
                CreatePair(200142,    DATA_20014_BN4F_Percent ),
                CreatePair(201110,    DATA_20111_BF1_Execute ),
                CreatePair(201111,    DATA_20111_BF1_Successful ),
                CreatePair(201112,    DATA_20111_BF1_Percent ),
                CreatePair(201120,    DATA_20112_BF2_Execute ),
                CreatePair(201121,    DATA_20112_BF2_Successful ),
                CreatePair(201122,    DATA_20112_BF2_Percent ),
                CreatePair(201130,    DATA_20113_BF3_Execute ),
                CreatePair(201131,    DATA_20113_BF3_Successful ),
                CreatePair(201132,    DATA_20113_BF3_Percent ),
                CreatePair(201140,    DATA_20114_BF4F_Execute ),
                CreatePair(201141,    DATA_20114_BF4F_Successful ),
                CreatePair(201142,    DATA_20114_BF4F_Percent ),
                 //Heavy Mode (H = Heavy, N = Near, F = Far, Number = Number of attack in combo, F = the last attack of combo)
                CreatePair(220110,    DATA_22011_HN1_Execute ),
                CreatePair(220111,    DATA_22011_HN1_Successful ),
                CreatePair(220112,    DATA_22011_HN1_Percent ),
                CreatePair(220120,    DATA_22012_HN2_Execute ),
                CreatePair(220121,    DATA_22012_HN2_Successful ),
                CreatePair(220122,    DATA_22012_HN2_Percent ),
                CreatePair(220130,    DATA_22013_HN3F_Execute ),
                CreatePair(220131,    DATA_22013_HN3F_Successful ),
                CreatePair(220132,    DATA_22013_HN3F_Percent ),
                CreatePair(221110,    DATA_22111_HF1_Execute ),
                CreatePair(221111,    DATA_22111_HF1_Successful ),
                CreatePair(221112,    DATA_22111_HF1_Percent ),
                CreatePair(221120,    DATA_22112_HF2_Execute ),
                CreatePair(221121,    DATA_22112_HF2_Successful ),
                CreatePair(221122,    DATA_22112_HF2_Percent ),
                CreatePair(221130,    DATA_22113_HF3F_Execute ),
                CreatePair(221131,    DATA_22113_HF3F_Successful ),
                CreatePair(221132,    DATA_22113_HF3F_Percent ),
                //Quick Mode (B = Balance, N = Near, F = Far, Number = Number of attack in combo, F = the last attack of combo)
                 CreatePair(240110,    DATA_24011_QN1_Execute ),
                 CreatePair(240111,    DATA_24011_QN1_Successful ),
                 CreatePair(240112,    DATA_24011_QN1_Percent ),
                 CreatePair(240120,    DATA_24012_QN2_Execute ),
                 CreatePair(240121,    DATA_24012_QN2_Successful ),
                 CreatePair(240122,    DATA_24012_QN2_Percent ),
                 CreatePair(240130,    DATA_24013_QN3_Execute ),
                 CreatePair(240131,    DATA_24013_QN3_Successful ),
                 CreatePair(240132,    DATA_24013_QN3_Percent ),
                 CreatePair(240140,    DATA_24014_QN4_Execute ),
                 CreatePair(240141,    DATA_24014_QN4_Successful ),
                 CreatePair(240142,    DATA_24014_QN4_Percent ),
                 CreatePair(240150,    DATA_24015_QN5F_Execute ),
                 CreatePair(240151,    DATA_24015_QN5F_Successful ),
                 CreatePair(240152,    DATA_24015_QN5F_Percent ),

                 CreatePair(241110,    DATA_24111_QF1_Execute ),
                 CreatePair(241111,    DATA_24111_QF1_Successful ),
                 CreatePair(241112,    DATA_24111_QF1_Percent ),
                 CreatePair(241120,    DATA_24112_QF2_Execute ),
                 CreatePair(241121,    DATA_24112_QF2_Successful ),
                 CreatePair(241122,    DATA_24112_QF2_Percent ),
                 CreatePair(241130,    DATA_24113_QF3_Execute ),
                 CreatePair(241131,    DATA_24113_QF3_Successful ),
                 CreatePair(241132,    DATA_24113_QF3_Percent ),
                 CreatePair(241140,    DATA_24114_QF4_Execute ),
                 CreatePair(241141,    DATA_24114_QF4_Successful ),
                 CreatePair(241142,    DATA_24114_QF4_Percent ),
                 CreatePair(241150,    DATA_24115_QF5F_Execute ),
                 CreatePair(241151,    DATA_24115_QF5F_Successful ),
                 CreatePair(241152,    DATA_24115_QF5F_Percent ),

                 // Dodge Direction Data  (FOR = Forward, RIG = Right, BAC = Backward, LET = Left)
                 CreatePair(402090,    DATA_4020X_Dodge_Total_Execute ),
                 CreatePair(402091,    DATA_4020X_Dodge_Total_Successful ),
                 CreatePair(402092,    DATA_4020X_Dodge_Total_Percent ),
                 CreatePair(402010,    DATA_40201_Dodge_FOR_Execute ),
                 CreatePair(402011,    DATA_40201_Dodge_FOR_Successful ),
                 CreatePair(402012,    DATA_40201_Dodge_FOR_Percent ),
                 CreatePair(402020,    DATA_40202_Dodge_FORRIG_Execute ),
                 CreatePair(402021,    DATA_40202_Dodge_FORRIG_Successful ),
                 CreatePair(402022,    DATA_40202_Dodge_FORRIG_Percent ),
                 CreatePair(402030,    DATA_40203_Dodge_RIG_Execute ),
                 CreatePair(402031,    DATA_40203_Dodge_RIG_Successful ),
                 CreatePair(402032,    DATA_40203_Dodge_RIG_Percent ),
                 CreatePair(402040,    DATA_40204_Dodge_BACRIG_Execute ),
                 CreatePair(402041,    DATA_40204_Dodge_BACRIG_Successful ),
                 CreatePair(402042,    DATA_40204_Dodge_BACRIG_Percent ),
                 CreatePair(402050,    DATA_40205_Dodge_BAC_Execute ),
                 CreatePair(402051,    DATA_40205_Dodge_BAC_Successful ),
                 CreatePair(402052,    DATA_40205_Dodge_BAC_Percent ),
                 CreatePair(402060,    DATA_40206_Dodge_BACLET_Execute ),
                 CreatePair(402061,    DATA_40206_Dodge_BACLET_Successful ),
                 CreatePair(402062,    DATA_40206_Dodge_BACLET_Percent ),
                 CreatePair(402070,    DATA_40207_Dodge_LET_Execute ),
                 CreatePair(402071,    DATA_40207_Dodge_LET_Successful ),
                 CreatePair(402072,    DATA_40207_Dodge_LET_Percent ),
                 CreatePair(402080,    DATA_40208_Dodge_FORLET_Execute ),
                 CreatePair(402081,    DATA_40208_Dodge_FORLET_Successful ),
                 CreatePair(402082,    DATA_40208_Dodge_FORLET_Percent ),

                // Charge Dodge in Heavy Mode (still consider changing/removing this move),
                 CreatePair(422090,    DATA_4220X_CDodge_Total_Execute ),
                 CreatePair(422091,    DATA_4220X_CDodge_Total_Successful ),
                 CreatePair(422092,    DATA_4220X_CDodge_Total_Percent ),
                 CreatePair(422010,    DATA_42201_CDodge_FOR_Execute ),
                 CreatePair(422011,    DATA_42201_CDodge_FOR_Successful ),
                 CreatePair(422012,    DATA_42201_CDodge_FOR_Percent ),
                 CreatePair(422020,     DATA_42202_CDodge_FORRIG_Execute ),
                 CreatePair(422021,     DATA_42202_CDodge_FORRIG_Successful ),
                 CreatePair(422022,     DATA_42202_CDodge_FORRIG_Percent ),
                 CreatePair(422030,     DATA_42203_CDodge_RIG_Execute ),
                 CreatePair(422031,     DATA_42203_CDodge_RIG_Successful ),
                 CreatePair(422032,     DATA_42203_CDodge_RIG_Percent ),
                 CreatePair(422040,     DATA_42204_CDodge_BACRIG_Execute ),
                 CreatePair(422041,     DATA_42204_CDodge_BACRIG_Successful ),
                 CreatePair(422042,     DATA_42204_CDodge_BACRIG_Percent ),
                 CreatePair(422050,     DATA_42205_CDodge_BAC_Execute ),
                 CreatePair(422051,     DATA_42205_CDodge_BAC_Successful ),
                 CreatePair(422052,     DATA_42205_CDodge_BAC_Percent ),
                 CreatePair(422060,     DATA_42206_CDodge_BACLET_Execute ),
                 CreatePair(422061,     DATA_42206_CDodge_BACLET_Successful ),
                 CreatePair(422062,     DATA_42206_CDodge_BACLET_Percent ),
                 CreatePair(422070,     DATA_42207_CDodge_LET_Execute ),
                 CreatePair(422071,     DATA_42207_CDodge_LET_Successful ),
                 CreatePair(422072,     DATA_42207_CDodge_LET_Percent ),
                 CreatePair(422080,     DATA_42208_CDodge_FORLET_Execute ),
                 CreatePair(422081,     DATA_42208_CDodge_FORLET_Successful ),
                 CreatePair(422082,     DATA_42208_CDodge_FORLET_Percent ),

                // Dodge Against Attack Data)
                 CreatePair(400111,     DATA_Dodge_20011_BN1_Successful ),
                 CreatePair(400121,     DATA_Dodge_20012_BN2_Successful ),
                 CreatePair(400131,     DATA_Dodge_20013_BN3_Successful ),
                 CreatePair(400141,     DATA_Dodge_20014_BN4F_Successful ),
                 CreatePair(401111,     DATA_Dodge_20111_BF1_Successful ),
                 CreatePair(401121,     DATA_Dodge_20112_BF2_Successful ),
                 CreatePair(401131,     DATA_Dodge_20113_BF3_Successful ),
                 CreatePair(401141,     DATA_Dodge_20114_BF4F_Successful ),

                 CreatePair(420111,     DATA_Dodge_22011_HN1_Successful ),
                 CreatePair(420121,     DATA_Dodge_22012_HN2_Successful ),
                 CreatePair(420131,     DATA_Dodge_22013_HN3F_Successful ),
                 CreatePair(421111,     DATA_Dodge_22111_HF1_Successful ),
                 CreatePair(421121,     DATA_Dodge_22112_HF2_Successful ),
                 CreatePair(421131,     DATA_Dodge_22113_HF3F_Successful ),

                 CreatePair(440111,     DATA_Dodge_24011_QN1_Successful ),
                 CreatePair(440121,     DATA_Dodge_24012_QN2_Successful ),
                 CreatePair(440131,     DATA_Dodge_24013_QN3_Successful ),
                 CreatePair(440141,     DATA_Dodge_24014_QN4_Successful ),
                 CreatePair(440151,     DATA_Dodge_24015_QN5F_Successful ),
                 CreatePair(441111,     DATA_Dodge_24111_QF1_Successful ),
                 CreatePair(441121,     DATA_Dodge_24112_QF2_Successful ),
                 CreatePair(441131,     DATA_Dodge_24113_QF3_Successful ),
                 CreatePair(441141,     DATA_Dodge_24114_QF4_Successful ),
                 CreatePair(441151,     DATA_Dodge_24115_QF5F_Successful ),
                 CreatePair(462090,     DATA_Dodge_4220X_CDODGE_Successful ),

                // Block Against Attack Data )
                 CreatePair(600110,     DATA_Block_20011_BN1_Successful ),
                 CreatePair(600120,     DATA_Block_20012_BN2_Successful ),
                 CreatePair(600130,     DATA_Block_20013_BN3_Successful ),
                 CreatePair(600140,     DATA_Block_20014_BN4F_Successful ),
                 CreatePair(601110,     DATA_Block_20111_BF1_Successful ),
                 CreatePair(601120,     DATA_Block_20112_BF2_Successful ),
                 CreatePair(601130,     DATA_Block_20113_BF3_Successful ),
                 CreatePair(601140,     DATA_Block_20114_BF4F_Successful),

                 CreatePair(620110,     DATA_Block_22011_HN1_Successful ),
                 CreatePair(620120,     DATA_Block_22012_HN2_Successful ),
                 CreatePair(620130,     DATA_Block_22013_HN3F_Successful ),
                 CreatePair(621110,     DATA_Block_22111_HF1_Successful ),
                 CreatePair(621120,     DATA_Block_22112_HF2_Successful ),
                 CreatePair(621130,     DATA_Block_22113_HF3F_Successful ),

                 CreatePair(640110,     DATA_Block_24011_QN1_Successful ),
                 CreatePair(640120,     DATA_Block_24012_QN2_Successful ),
                 CreatePair(640130,     DATA_Block_24013_QN3_Successful ),
                 CreatePair(640140,     DATA_Block_24014_QN4_Successful ),
                 CreatePair(640150,     DATA_Block_24015_QN5F_Successful ),
                 CreatePair(641110,     DATA_Block_24111_QF1_Successful ),
                 CreatePair(641120,     DATA_Block_24112_QF2_Successful ),
                 CreatePair(641130,     DATA_Block_24113_QF3_Successful ),
                 CreatePair(641140,     DATA_Block_24114_QF4_Successful ),
                 CreatePair(641150,     DATA_Block_24115_QF5F_Successful ),

                 CreatePair(660000,     DATA_Block_4220X_CDODGE_Successful)

     };

        /*
        List<float> DataList = new List<float>();
        float[] DataListSet = { 
    //// General/Common Data
    DATA_RemainHP_Percent,
    DATA_PlayTime,
    DATA_RunTime ,
    DATA_RunTime_Percent ,
    DATA_Total_Attack_Exeute ,
    DATA_Total_Attack_Hit ,
    DATA_Total_Attack_Percent ,
    DATA_Total_Dodge_Execute ,
    DATA_Total_Dodge_Sucessful ,
    DATA_Total_Dodge_Percent ,
    DATA_Total_Block_Execute ,
    DATA_Total_Block_Sucessful ,
    DATA_Total_Block_Percent ,
    DATA_Average_Distance_Between ,
    
    //// Offense Mode Data
    /// Attacks Data
    // Balance Mode (B = Balance, N = Near, F = Far, Number = Number of attack in combo, F = the last attack of combo)
    DATA_20011_BN1_Execute ,
    DATA_20011_BN1_Successful ,
    DATA_20011_BN1_Percent ,
    DATA_20012_BN2_Execute ,
    DATA_20012_BN2_Successful ,
    DATA_20012_BN2_Percent ,
    DATA_20013_BN3_Execute ,
    DATA_20013_BN3_Successful ,
    DATA_20013_BN3_Percent ,
    DATA_20014_BN4F_Execute ,
    DATA_20014_BN4F_Successful ,
    DATA_20014_BN4F_Percent ,

    DATA_20111_BF1_Execute ,
    DATA_20111_BF1_Successful ,
    DATA_20111_BF1_Percent ,
    DATA_20112_BF2_Execute ,
    DATA_20112_BF2_Successful ,
    DATA_20112_BF2_Percent ,
    DATA_20113_BF3_Execute ,
    DATA_20113_BF3_Successful ,
    DATA_20113_BF3_Percent ,
    DATA_20114_BF4F_Execute ,
    DATA_20114_BF4F_Successful ,
    DATA_20114_BF4F_Percent ,

    //Heavy Mode (H = Heavy, N = Near, F = Far, Number = Number of attack in combo, F = the last attack of combo)
    DATA_22011_HN1_Execute ,
    DATA_22011_HN1_Successful ,
    DATA_22011_HN1_Percent ,
    DATA_22012_HN2_Execute ,
    DATA_22012_HN2_Successful ,
    DATA_22012_HN2_Percent ,
    DATA_22013_HN3F_Execute ,
    DATA_22013_HN3F_Successful ,
    DATA_22013_HN3F_Percent ,

    DATA_22111_HF1_Execute ,
    DATA_22111_HF1_Successful ,
    DATA_22111_HF1_Percent ,
    DATA_22112_HF2_Execute ,
    DATA_22112_HF2_Successful ,
    DATA_22112_HF2_Percent ,
    DATA_22113_HF3F_Execute ,
    DATA_22113_HF3F_Successful ,
    DATA_22113_HF3F_Percent ,

    //Quick Mode (B = Balance, N = Near, F = Far, Number = Number of attack in combo, F = the last attack of combo)
    DATA_24011_QN1_Execute ,
    DATA_24011_QN1_Successful ,
    DATA_24011_QN1_Percent ,
    DATA_24012_QN2_Execute ,
    DATA_24012_QN2_Successful ,
    DATA_24012_QN2_Percent ,
    DATA_24013_QN3_Execute ,
    DATA_24013_QN3_Successful ,
    DATA_24013_QN3_Percent ,
    DATA_24014_QN4_Execute ,
    DATA_24014_QN4_Successful ,
    DATA_24014_QN4_Percent ,
    DATA_24015_QN5F_Execute ,
    DATA_24015_QN5F_Successful ,
    DATA_24015_QN5F_Percent ,

    DATA_24111_QF1_Execute ,
    DATA_24111_QF1_Successful ,
    DATA_24111_QF1_Percent ,
    DATA_24112_QF2_Execute ,
    DATA_24112_QF2_Successful ,
    DATA_24112_QF2_Percent ,
    DATA_24113_QF3_Execute ,
    DATA_24113_QF3_Successful ,
    DATA_24113_QF3_Percent ,
    DATA_24114_QF4_Execute ,
    DATA_24114_QF4_Successful ,
    DATA_24114_QF4_Percent ,
    DATA_24115_QF5F_Execute ,
    DATA_24115_QF5F_Successful ,
    DATA_24115_QF5F_Percent ,

    // Dodge Direction Data  (FOR = Forward, RIG = Right, BAC = Backward, LET = Left)
    DATA_4020X_Dodge_Total_Execute ,
    DATA_4020X_Dodge_Total_Successful ,
    DATA_4020X_Dodge_Total_Percent ,
    DATA_40201_Dodge_FOR_Execute ,
    DATA_40201_Dodge_FOR_Successful ,
    DATA_40201_Dodge_FOR_Percent ,
    DATA_40202_Dodge_FORRIG_Execute ,
    DATA_40202_Dodge_FORRIG_Successful ,
    DATA_40202_Dodge_FORRIG_Percent ,
    DATA_40203_Dodge_RIG_Execute ,
    DATA_40203_Dodge_RIG_Successful ,
    DATA_40203_Dodge_RIG_Percent ,
    DATA_40204_Dodge_BACRIG_Execute ,
    DATA_40204_Dodge_BACRIG_Successful ,
    DATA_40204_Dodge_BACRIG_Percent ,
    DATA_40205_Dodge_BAC_Execute ,
    DATA_40205_Dodge_BAC_Successful ,
    DATA_40205_Dodge_BAC_Percent ,
    DATA_40206_Dodge_BACLET_Execute ,
    DATA_40206_Dodge_BACLET_Successful ,
    DATA_40206_Dodge_BACLET_Percent ,
    DATA_40207_Dodge_LET_Execute ,
    DATA_40207_Dodge_LET_Successful ,
    DATA_40207_Dodge_LET_Percent ,
    DATA_40208_Dodge_FORLET_Execute ,
    DATA_40208_Dodge_FORLET_Successful ,
    DATA_40208_Dodge_FORLET_Percent ,

    // Charge Dodge in Heavy Mode (still consider changing/removing this move)
    DATA_4220X_CDodge_Total_Execute ,
    DATA_4220X_CDodge_Total_Successful ,
    DATA_4220X_CDodge_Total_Percent ,
    DATA_42201_CDodge_FOR_Execute ,
    DATA_42201_CDodge_FOR_Successful ,
    DATA_42201_CDodge_FOR_Percent ,
     DATA_42202_CDodge_FORRIG_Execute ,
     DATA_42202_CDodge_FORRIG_Successful ,
     DATA_42202_CDodge_FORRIG_Percent ,
     DATA_42203_CDodge_RIG_Execute ,
     DATA_42203_CDodge_RIG_Successful ,
     DATA_42203_CDodge_RIG_Percent ,
     DATA_42204_CDodge_BACRIG_Execute ,
     DATA_42204_CDodge_BACRIG_Successful ,
     DATA_42204_CDodge_BACRIG_Percent ,
     DATA_42205_CDodge_BAC_Execute ,
     DATA_42205_CDodge_BAC_Successful ,
     DATA_42205_CDodge_BAC_Percent ,
     DATA_42206_CDodge_BACLET_Execute ,
     DATA_42206_CDodge_BACLET_Successful ,
     DATA_42206_CDodge_BACLET_Percent ,
     DATA_42207_CDodge_LET_Execute ,
     DATA_42207_CDodge_LET_Successful ,
     DATA_42207_CDodge_LET_Percent ,
     DATA_42208_CDodge_FORLET_Execute ,
     DATA_42208_CDodge_FORLET_Successful ,
     DATA_42208_CDodge_FORLET_Percent ,

    // Dodge Against Attack Data 
     DATA_Dodge_20011_BN1_Successful ,
     DATA_Dodge_20012_BN2_Successful ,
     DATA_Dodge_20013_BN3_Successful ,
     DATA_Dodge_20014_BN4F_Successful ,
     DATA_Dodge_20111_BF1_Successful ,
     DATA_Dodge_20112_BF2_Successful ,
     DATA_Dodge_20113_BF3_Successful ,
     DATA_Dodge_20114_BF4F_Successful ,

     DATA_Dodge_22011_HN1_Successful ,
     DATA_Dodge_22012_HN2_Successful ,
     DATA_Dodge_22013_HN3F_Successful ,
     DATA_Dodge_22111_HF1_Successful ,
     DATA_Dodge_22112_HF2_Successful ,
     DATA_Dodge_22113_HF3F_Successful ,

     DATA_Dodge_24011_QN1_Successful ,
     DATA_Dodge_24012_QN2_Successful ,
     DATA_Dodge_24013_QN3_Successful ,
     DATA_Dodge_24014_QN4_Successful ,
     DATA_Dodge_24015_QN5F_Successful ,
     DATA_Dodge_24111_QF1_Successful ,
     DATA_Dodge_24112_QF2_Successful ,
     DATA_Dodge_24113_QF3_Successful ,
     DATA_Dodge_24114_QF4_Successful ,
     DATA_Dodge_24115_QF5F_Successful ,

     DATA_Dodge_4220X_CDODGE_Successful ,

    // Block Against Attack Data 
     DATA_Block_20011_BN1_Successful ,
     DATA_Block_20012_BN2_Successful ,
     DATA_Block_20013_BN3_Successful ,
     DATA_Block_20014_BN4F_Successful ,
     DATA_Block_20111_BF1_Successful ,
     DATA_Block_20112_BF2_Successful ,
     DATA_Block_20113_BF3_Successful ,
     DATA_Block_20114_BF4F_Successful ,

     DATA_Block_22011_HN1_Successful ,
     DATA_Block_22012_HN2_Successful ,
     DATA_Block_22013_HN3F_Successful ,
     DATA_Block_22111_HF1_Successful ,
     DATA_Block_22112_HF2_Successful ,
     DATA_Block_22113_HF3F_Successful ,

     DATA_Block_24011_QN1_Successful ,
     DATA_Block_24012_QN2_Successful ,
     DATA_Block_24013_QN3_Successful ,
     DATA_Block_24014_QN4_Successful ,
     DATA_Block_24015_QN5F_Successful ,
     DATA_Block_24111_QF1_Successful ,
     DATA_Block_24112_QF2_Successful ,
     DATA_Block_24113_QF3_Successful ,
     DATA_Block_24114_QF4_Successful ,
     DATA_Block_24115_QF5F_Successful ,

     DATA_Block_4220X_CDODGE_Successful ,

     DATA_Total_ATK_Close_Successful_Point 

     };
     */
        print(DataListSet.Length);


    DataValueList= DataListSet;
    /*
     int count = 0;
        foreach (float i in DataListSet)
        {
            if(DataListSet.Length<=count){
                DataValueList.Add(i+"");
            }
            else DataValueList[count] = i+"";
            //print("Add = " + i);
            count++;
        }
          */  
    }   
    public void ResetALLData(){
        StylePoint = 0;
        //// General/Common Data
    DATA_RemainHP_Percent= 0;
    DATA_PlayTime= 0;
    DATA_RunTime = 0;
    DATA_RunTime_Percent = 0;
    DATA_Total_Attack_Exeute = 0;
    DATA_Total_Attack_Hit = 0;
    DATA_Total_Attack_Percent = 0;
    DATA_Total_Dodge_Execute = 0;
    DATA_Total_Dodge_Sucessful = 0;
    DATA_Total_Dodge_Percent = 0;
    DATA_Total_Block_Execute = 0;
    DATA_Total_Block_Sucessful = 0;
    DATA_Total_Block_Percent = 0;
    DATA_Average_Distance_Between = 0;
    
    //// Offense Mode Data
    /// Attacks Data
    // Balance Mode (B = Balance, N = Near, F = Far, Number = Number of attack in combo, F = the last attack of combo)
    DATA_20011_BN1_Execute = 0;
    DATA_20011_BN1_Successful = 0;
    DATA_20011_BN1_Percent = 0;
    DATA_20012_BN2_Execute = 0;
    DATA_20012_BN2_Successful = 0;
    DATA_20012_BN2_Percent = 0;
    DATA_20013_BN3_Execute = 0;
    DATA_20013_BN3_Successful = 0;
    DATA_20013_BN3_Percent = 0;
    DATA_20014_BN4F_Execute = 0;
    DATA_20014_BN4F_Successful = 0;
    DATA_20014_BN4F_Percent = 0;

    DATA_20111_BF1_Execute = 0;
    DATA_20111_BF1_Successful = 0;
    DATA_20111_BF1_Percent = 0;
    DATA_20112_BF2_Execute = 0;
    DATA_20112_BF2_Successful = 0;
    DATA_20112_BF2_Percent = 0;
    DATA_20113_BF3_Execute = 0;
    DATA_20113_BF3_Successful = 0;
    DATA_20113_BF3_Percent = 0;
    DATA_20114_BF4F_Execute = 0;
    DATA_20114_BF4F_Successful = 0;
    DATA_20114_BF4F_Percent = 0;

    //Heavy Mode (H = Heavy, N = Near, F = Far, Number = Number of attack in combo, F = the last attack of combo)
    DATA_22011_HN1_Execute = 0;
    DATA_22011_HN1_Successful = 0;
    DATA_22011_HN1_Percent = 0;
    DATA_22012_HN2_Execute = 0;
    DATA_22012_HN2_Successful = 0;
    DATA_22012_HN2_Percent = 0;
    DATA_22013_HN3F_Execute = 0;
    DATA_22013_HN3F_Successful = 0;
    DATA_22013_HN3F_Percent = 0;

    DATA_22111_HF1_Execute = 0;
    DATA_22111_HF1_Successful = 0;
    DATA_22111_HF1_Percent = 0;
    DATA_22112_HF2_Execute = 0;
    DATA_22112_HF2_Successful = 0;
    DATA_22112_HF2_Percent = 0;
    DATA_22113_HF3F_Execute = 0;
    DATA_22113_HF3F_Successful = 0;
    DATA_22113_HF3F_Percent = 0;

    //Quick Mode (B = Balance, N = Near, F = Far, Number = Number of attack in combo, F = the last attack of combo)
    DATA_24011_QN1_Execute = 0;
    DATA_24011_QN1_Successful = 0;
    DATA_24011_QN1_Percent = 0;
    DATA_24012_QN2_Execute = 0;
    DATA_24012_QN2_Successful = 0;
    DATA_24012_QN2_Percent = 0;
    DATA_24013_QN3_Execute = 0;
    DATA_24013_QN3_Successful = 0;
    DATA_24013_QN3_Percent = 0;
    DATA_24014_QN4_Execute = 0;
    DATA_24014_QN4_Successful = 0;
    DATA_24014_QN4_Percent = 0;
    DATA_24015_QN5F_Execute = 0;
    DATA_24015_QN5F_Successful = 0;
    DATA_24015_QN5F_Percent = 0;

    DATA_24111_QF1_Execute = 0;
    DATA_24111_QF1_Successful = 0;
    DATA_24111_QF1_Percent = 0;
    DATA_24112_QF2_Execute = 0;
    DATA_24112_QF2_Successful = 0;
    DATA_24112_QF2_Percent = 0;
    DATA_24113_QF3_Execute = 0;
    DATA_24113_QF3_Successful = 0;
    DATA_24113_QF3_Percent = 0;
    DATA_24114_QF4_Execute = 0;
    DATA_24114_QF4_Successful = 0;
    DATA_24114_QF4_Percent = 0;
    DATA_24115_QF5F_Execute = 0;
    DATA_24115_QF5F_Successful = 0;
    DATA_24115_QF5F_Percent = 0;

    // Dodge Direction Data  (FOR = Forward, RIG = Right, BAC = Backward, LET = Left)
    DATA_4020X_Dodge_Total_Execute = 0;
    DATA_4020X_Dodge_Total_Successful = 0;
    DATA_4020X_Dodge_Total_Percent = 0;
    DATA_40201_Dodge_FOR_Execute = 0;
    DATA_40201_Dodge_FOR_Successful = 0;
    DATA_40201_Dodge_FOR_Percent = 0;
    DATA_40202_Dodge_FORRIG_Execute = 0;
    DATA_40202_Dodge_FORRIG_Successful = 0;
    DATA_40202_Dodge_FORRIG_Percent = 0;
    DATA_40203_Dodge_RIG_Execute = 0;
    DATA_40203_Dodge_RIG_Successful = 0;
    DATA_40203_Dodge_RIG_Percent = 0;
    DATA_40204_Dodge_BACRIG_Execute = 0;
    DATA_40204_Dodge_BACRIG_Successful = 0;
    DATA_40204_Dodge_BACRIG_Percent = 0;
    DATA_40205_Dodge_BAC_Execute = 0;
    DATA_40205_Dodge_BAC_Successful = 0;
    DATA_40205_Dodge_BAC_Percent = 0;
    DATA_40206_Dodge_BACLET_Execute = 0;
    DATA_40206_Dodge_BACLET_Successful = 0;
    DATA_40206_Dodge_BACLET_Percent = 0;
    DATA_40207_Dodge_LET_Execute = 0;
    DATA_40207_Dodge_LET_Successful = 0;
    DATA_40207_Dodge_LET_Percent = 0;
    DATA_40208_Dodge_FORLET_Execute = 0;
    DATA_40208_Dodge_FORLET_Successful = 0;
    DATA_40208_Dodge_FORLET_Percent = 0;

    // Charge Dodge in Heavy Mode (still consider changing/removing this move)
    DATA_4220X_CDodge_Total_Execute = 0;
    DATA_4220X_CDodge_Total_Successful = 0;
    DATA_4220X_CDodge_Total_Percent = 0;
    DATA_42201_CDodge_FOR_Execute = 0;
    DATA_42201_CDodge_FOR_Successful = 0;
    DATA_42201_CDodge_FOR_Percent = 0;
     DATA_42202_CDodge_FORRIG_Execute = 0;
     DATA_42202_CDodge_FORRIG_Successful = 0;
     DATA_42202_CDodge_FORRIG_Percent = 0;
     DATA_42203_CDodge_RIG_Execute = 0;
     DATA_42203_CDodge_RIG_Successful = 0;
     DATA_42203_CDodge_RIG_Percent = 0;
     DATA_42204_CDodge_BACRIG_Execute = 0;
     DATA_42204_CDodge_BACRIG_Successful = 0;
     DATA_42204_CDodge_BACRIG_Percent = 0;
     DATA_42205_CDodge_BAC_Execute = 0;
     DATA_42205_CDodge_BAC_Successful = 0;
     DATA_42205_CDodge_BAC_Percent = 0;
     DATA_42206_CDodge_BACLET_Execute = 0;
     DATA_42206_CDodge_BACLET_Successful = 0;
     DATA_42206_CDodge_BACLET_Percent = 0;
     DATA_42207_CDodge_LET_Execute = 0;
     DATA_42207_CDodge_LET_Successful = 0;
     DATA_42207_CDodge_LET_Percent = 0;
     DATA_42208_CDodge_FORLET_Execute = 0;
     DATA_42208_CDodge_FORLET_Successful = 0;
     DATA_42208_CDodge_FORLET_Percent = 0;

    // Dodge Against Attack Data 
     DATA_Dodge_20011_BN1_Successful = 0;
     DATA_Dodge_20012_BN2_Successful = 0;
     DATA_Dodge_20013_BN3_Successful = 0;
     DATA_Dodge_20014_BN4F_Successful = 0;
     DATA_Dodge_20111_BF1_Successful = 0;
     DATA_Dodge_20112_BF2_Successful = 0;
     DATA_Dodge_20113_BF3_Successful = 0;
     DATA_Dodge_20114_BF4F_Successful = 0;

     DATA_Dodge_22011_HN1_Successful = 0;
     DATA_Dodge_22012_HN2_Successful = 0;
     DATA_Dodge_22013_HN3F_Successful = 0;
     DATA_Dodge_22111_HF1_Successful = 0;
     DATA_Dodge_22112_HF2_Successful = 0;
     DATA_Dodge_22113_HF3F_Successful = 0;

     DATA_Dodge_24011_QN1_Successful = 0;
     DATA_Dodge_24012_QN2_Successful = 0;
     DATA_Dodge_24013_QN3_Successful = 0;
     DATA_Dodge_24014_QN4_Successful = 0;
     DATA_Dodge_24015_QN5F_Successful = 0;
     DATA_Dodge_24111_QF1_Successful = 0;
     DATA_Dodge_24112_QF2_Successful = 0;
     DATA_Dodge_24113_QF3_Successful = 0;
     DATA_Dodge_24114_QF4_Successful = 0;
     DATA_Dodge_24115_QF5F_Successful = 0;

     DATA_Dodge_4220X_CDODGE_Successful = 0;

    // Block Against Attack Data 
     DATA_Block_20011_BN1_Successful = 0;
     DATA_Block_20012_BN2_Successful = 0;
     DATA_Block_20013_BN3_Successful = 0;
     DATA_Block_20014_BN4F_Successful = 0;
     DATA_Block_20111_BF1_Successful = 0;
     DATA_Block_20112_BF2_Successful = 0;
     DATA_Block_20113_BF3_Successful = 0;
     DATA_Block_20114_BF4F_Successful = 0;

     DATA_Block_22011_HN1_Successful = 0;
     DATA_Block_22012_HN2_Successful = 0;
     DATA_Block_22013_HN3F_Successful = 0;
     DATA_Block_22111_HF1_Successful = 0;
     DATA_Block_22112_HF2_Successful = 0;
     DATA_Block_22113_HF3F_Successful = 0;

     DATA_Block_24011_QN1_Successful = 0;
     DATA_Block_24012_QN2_Successful = 0;
     DATA_Block_24013_QN3_Successful = 0;
     DATA_Block_24014_QN4_Successful = 0;
     DATA_Block_24015_QN5F_Successful = 0;
     DATA_Block_24111_QF1_Successful = 0;
     DATA_Block_24112_QF2_Successful = 0;
     DATA_Block_24113_QF3_Successful = 0;
     DATA_Block_24114_QF4_Successful = 0;
     DATA_Block_24115_QF5F_Successful = 0;

     DATA_Block_4220X_CDODGE_Successful = 0;

     DATA_Total_ATK_Close_Successful_Point =0;
    }

}



        

