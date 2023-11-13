using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManeger : MonoBehaviour
{

    /// ///////////////////////////////////// DATA ARRAY ////////////////////////////////////////
    public static float[,] DATABASE = new float[10000, 2];

    /// ///////////////////////////////////// DATA CODE ////////////////////////////////////////
    /*
     General Infomation = 0 - 999
     Attack Information = 1000 - 1999
     Command Information = 2000 - 2999
     Defense Information = 3000 - 3999
     Movement Information = 4000 - 4999
     Input Information = 5000 - 5999
     Point/Score Information = 8000 - 9999
      
     */
    readonly public static int PLAYER = 0;
    readonly public static int ENEMY = 1;


    readonly public static int DATA_NUM_RemainHP_Percent = 0;
    readonly public static int DATA_NUM_PlayTime = 1;
    readonly public static int DATA_NUM_RunTime = 2;
    readonly public static int DATA_NUM_Attack_Exeute = 1000;
    readonly public static int DATA_NUM_Attack_Hit = 1001;

    readonly public static int DATA_NUM_Dodge_Execute = 3000;
    readonly public static int DATA_NUM_Dodge_Sucessful = 3001;
    readonly public static int DATA_NUM_Block_Execute = 3002;
    readonly public static int DATA_NUM_Block_Sucessful = 3003;
    readonly public static int DATA_NUM_Jump_Execute = 4000;
    readonly public static int DATA_NUM_Jump_Sucessful = 4001;
    readonly public static int DATA_NUM_Average_Distance_Between = 4010;

    readonly public static int DATA_NUM_ATK_Close_Execute = 1100;
    readonly public static int DATA_NUM_ATK_Close_Successful = 1101;
    readonly public static int DATA_NUM_ATK_Mid_Execute = 1102;
    readonly public static int DATA_NUM_ATK_Mid_Successful = 1103;
    readonly public static int DATA_NUM_ATK_Cone_Execute = 1104;
    readonly public static int DATA_NUM_ATK_Cone_Successful = 1105;

    readonly public static int DATA_NUM_PATK_Close_Execute = 1200;
    readonly public static int DATA_NUM_PATK_Close_Successful = 1201;
    readonly public static int DATA_NUM_PATK_Far_Execute = 1202;
    readonly public static int DATA_NUM_PATK_Far_Successful = 1203;

    readonly public static int DATA_NUM_FIN_Close_Execute = 1300;
    readonly public static int DATA_NUM_FIN_Close_Successful = 1301;
    readonly public static int DATA_NUM_FIN_Far_Execute = 1302;
    readonly public static int DATA_NUM_FIN_Far_Successful = 1303;

    readonly public static int DATA_NUM_Dodge_Off_Execute = 3100;
    readonly public static int DATA_NUM_Dodge_Off_Successful = 3101;
    readonly public static int DATA_NUM_Dodge_OffSide_Execute = 3102;
    readonly public static int DATA_NUM_Dodge_OffSide_Successful = 3103;
    readonly public static int DATA_NUM_Dodge_Side_Execute = 3104;
    readonly public static int DATA_NUM_Dodge_Side_Successful = 3105;
    readonly public static int DATA_NUM_Dodge_DefSide_Execute = 3106;
    readonly public static int DATA_NUM_Dodge_DefSide_Successful = 3107;
    readonly public static int DATA_NUM_Dodge_Def_Execute = 3108;
    readonly public static int DATA_NUM_Dodge_Def_Successful = 3019;


    ///  Point Related
    readonly public static int DATA_NUM_ATK_Close_Hit_Point = 8100;
    readonly public static int DATA_NUM_ATK_Mid_Hit_Point = 8102;
    readonly public static int DATA_NUM_ATK_Cone_Hit_Point = 8104;

    readonly public static int DATA_NUM_PATK_Close_Hit_Point = 8200;
    readonly public static int DATA_NUM_PATK_Far_Hit_Point = 8202;

    readonly public static int DATA_NUM_FIN_Close_Hit_Point = 8300;
    readonly public static int DATA_NUM_FIN_Far_Hit_Point = 8302;
    ///  Point Related
    readonly public static int DATA_NUM_ATK_Close_Dodge_Point = 9100;
    readonly public static int DATA_NUM_ATK_Mid_Dodge_Point = 9102;
    readonly public static int DATA_NUM_ATK_Cone_Dodge_Point = 9104;

    readonly public static int DATA_NUM_PATK_Close_Dodge_Point = 9200;
    readonly public static int DATA_NUM_PATK_Far_Dodge_Point = 9202;

    readonly public static int DATA_NUM_FIN_Close_Dodge_Point = 9300;
    readonly public static int DATA_NUM_FIN_Far_Dodge_Point = 9302;
    /// ///////////////////////////////////// DATA CODE ////////////////////////////////////////

    /// ///////////////////////////////////// INDV DATA ////////////////////////////////////////
    private static float[] DefaultArray = { 0, 0 };

    public float[] DATA_RemainHP_Percent = DefaultArray;
    public float[] DATA_PlayTime = DefaultArray;
    public float[] DATA_RunTime = DefaultArray;
    public float[] DATA_RunTime_Percent = DefaultArray;
    public float[] DATA_Total_Attack_Exeute = DefaultArray;
    public float[] DATA_Total_Attack_Hit = DefaultArray;
    public float[] DATA_Total_Attack_Percent = DefaultArray;
    public float[] DATA_Total_Dodge_Execute = DefaultArray;
    public float[] DATA_Total_Dodge_Sucessful = DefaultArray;
    public float[] DATA_Total_Dodge_Percent = DefaultArray;
    public float[] DATA_Total_Block_Execute = DefaultArray;
    public float[] DATA_Total_Block_Sucessful = DefaultArray;
    public float[] DATA_Total_Block_Percent = DefaultArray;
    public float[] DATA_Total_Jump_Execute = DefaultArray;
    public float[] DATA_Total_Jump_Sucessful = DefaultArray;
    public float[] DATA_Total_Jump_Percent = DefaultArray;
    public float[] DATA_Average_Distance_Between = DefaultArray;

    public float[] DATA_Total_ATK_Close_Execute = DefaultArray;
    public float[] DATA_Total_ATK_Close_Successful = DefaultArray;
    public float[] DATA_Total_ATK_Close_Percent = DefaultArray;
    public float[] DATA_Total_ATK_Mid_Execute = DefaultArray;
    public float[] DATA_Total_ATK_Mid_Successful = DefaultArray;
    public float[] DATA_Total_ATK_Mid_Percent = DefaultArray;
    public float[] DATA_Total_ATK_Cone_Execute = DefaultArray;
    public float[] DATA_Total_ATK_Cone_Successful = DefaultArray;
    public float[] DATA_Total_ATK_Cone_Percent = DefaultArray;

    public float[] DATA_Total_PATK_Close_Execute = DefaultArray;
    public float[] DATA_Total_PATK_Close_Successful = DefaultArray;
    public float[] DATA_Total_PATK_Close_Pecent = DefaultArray;
    public float[] DATA_Total_PATK_Far_Execute = DefaultArray;
    public float[] DATA_Total_PATK_Far_Successful = DefaultArray;
    public float[] DATA_Total_PATK_Far_Pecent = DefaultArray;

    public float[] DATA_Total_FIN_Close_Execute = DefaultArray;
    public float[] DATA_Total_FIN_Close_Successful = DefaultArray;
    public float[] DATA_Total_FIN_Close_Pecent = DefaultArray;
    public float[] DATA_Total_FIN_Far_Execute = DefaultArray;
    public float[] DATA_Total_FIN_Far_Successful = DefaultArray;
    public float[] DATA_Total_FIN_Far_Pecent = DefaultArray;

    public float[] DATA_Total_Dodge_Off_Execute = DefaultArray;
    public float[] DATA_Total_Dodge_Off_Successful = DefaultArray;
    public float[] DATA_Total_Dodge_Off_Percent = DefaultArray;
    public float[] DATA_Total_Dodge_OffSide_Execute = DefaultArray;
    public float[] DATA_Total_Dodge_OffSide_Successful = DefaultArray;
    public float[] DATA_Total_Dodge_OffSide_Percent = DefaultArray;
    public float[] DATA_Total_Dodge_Side_Execute = DefaultArray;
    public float[] DATA_Total_Dodge_Side_Successful = DefaultArray;
    public float[] DATA_Total_Dodge_Side_Percent = DefaultArray;
    public float[] DATA_Total_Dodge_Def_Execute = DefaultArray;
    public float[] DATA_Total_Dodge_Def_Successful = DefaultArray;
    public float[] DATA_Total_Dodge_Def_Percent = DefaultArray;
    public float[] DATA_Total_Dodge_DefSide_Execute = DefaultArray;
    public float[] DATA_Total_Dodge_DefSide_Successful = DefaultArray;
    public float[] DATA_Total_Dodge_DefSide_Percent = DefaultArray;

    /// <summary>
    /// //
    /// </summary>
    /////////////////////// For Score_1_Attack //////////////////////////

    public float[] DATA_Total_ATK_Close_Successful_Point = DefaultArray;
    public float[] DATA_Total_ATK_Mid_Successful_Point = DefaultArray;
    public float[] DATA_Total_ATK_Cone_Successful_Point = DefaultArray;
    public float[] DATA_Total_PATK_Close_Successful_Point = DefaultArray;
    public float[] DATA_Total_PATK_Far_Successful_Point = DefaultArray;
    public float[] DATA_Total_FIN_Close_Successful_Point = DefaultArray;
    public float[] DATA_Total_FIN_Far_Successful_Point = DefaultArray;
    /// /////////////////////////////////////////////////////////////////////

    /////////////////////// For Score_2_Dodge //////////////////////////
    public float[] DATA_Total_Dodge_Atk_Close_Successful = DefaultArray;
    public float[] DATA_Total_Dodge_Atk_Mid_Successful = DefaultArray;
    public float[] DATA_Total_Dodge_Atk_Cone_Successful = DefaultArray;
    public float[] DATA_Total_Dodge_Pre_Close_Successful = DefaultArray;
    public float[] DATA_Total_Dodge_Pre_Far_Successful = DefaultArray;
    public float[] DATA_Total_Dodge_Fin_Close_Successful = DefaultArray;
    public float[] DATA_Total_Dodge_Fin_Far_Successful = DefaultArray;

    public float[] DATA_Total_Dodge_Atk_Close_Successful_Point = DefaultArray;
    public float[] DATA_Total_Dodge_Atk_Mid_Successful_Point = DefaultArray;
    public float[] DATA_Total_Dodge_Atk_Cone_Successful_Point = DefaultArray;
    public float[] DATA_Total_Dodge_Pre_Close_Successful_Point = DefaultArray;
    public float[] DATA_Total_Dodge_Pre_Far_Successful_Point = DefaultArray;
    public float[] DATA_Total_Dodge_Fin_Close_Successful_Point = DefaultArray;
    public float[] DATA_Total_Dodge_Fin_Far_Successful_Point = DefaultArray;
    /// ///////////////////////////////////// INDV DATA ////////////////////////////////////////

    /// ///////////////////////////////////// Text /////////////////////////////////////////////
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
    /// ///////////////////////////////////// Text /////////////////////////////////////////////
     //float[] A = { 0, 0 };
    private float[] TwoDArrayRowto1DArray(float[,] Database, int row)
    {
        float[] A = DefaultArray;
        for (int i = 0; i < DefaultArray.Length; i++)
            A[i] = DATABASE[row, i];
        return A;
    }

    private float[] DivideWholeArray(float[] Top, float[] Bottom)
    {
        float[] A = DefaultArray;
        for (int i = 0; i < DefaultArray.Length; i++)
        {
            A[i] = Top[i] / Bottom[i];
            if (A[i] > 1) Debug.Log("Something is wrong with Percent Vaule : Over 1 ; " + Top[i] + " " + Bottom[i]);
            if (A[i] < 0) Debug.Log("Something is wrong with Percent Vaule : Under 0 ; " + Top[i] + " " + Bottom[i]);
        }


        return A;

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDATA();
    }

    private void UpdateDATA()
    {
        DATA_RemainHP_Percent = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_RemainHP_Percent);
        DATA_PlayTime = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_PlayTime);
        DATA_RunTime = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_RunTime);
        DATA_RunTime_Percent = DivideWholeArray(DATA_PlayTime, DATA_RunTime);

        DATA_Total_Attack_Exeute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Attack_Exeute);
        DATA_Total_Attack_Hit = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Attack_Hit);
        DATA_Total_Attack_Percent = DivideWholeArray(DATA_Total_Attack_Hit, DATA_Total_Attack_Exeute);

        DATA_Total_Dodge_Execute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Dodge_Execute);
        DATA_Total_Dodge_Sucessful = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Dodge_Sucessful);
        DATA_Total_Dodge_Percent = DivideWholeArray(DATA_Total_Dodge_Sucessful, DATA_Total_Dodge_Execute);

        DATA_Total_Block_Execute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Block_Execute);
        DATA_Total_Block_Sucessful = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Block_Sucessful);
        DATA_Total_Block_Percent = DivideWholeArray(DATA_Total_Block_Sucessful, DATA_Total_Block_Execute);

        DATA_Total_Jump_Execute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Jump_Execute);
        DATA_Total_Jump_Sucessful = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Jump_Sucessful);
        DATA_Total_Jump_Percent = DivideWholeArray(DATA_Total_Jump_Sucessful, DATA_Total_Jump_Execute);

        DATA_Average_Distance_Between = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Average_Distance_Between);

        DATA_Total_ATK_Close_Execute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_ATK_Close_Execute);
        DATA_Total_ATK_Close_Successful = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_ATK_Close_Successful);
        DATA_Total_ATK_Close_Percent = DivideWholeArray(DATA_Total_ATK_Close_Successful, DATA_Total_ATK_Close_Execute);

        DATA_Total_ATK_Mid_Execute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_ATK_Mid_Execute);
        DATA_Total_ATK_Mid_Successful = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_ATK_Mid_Successful);
        DATA_Total_ATK_Mid_Percent = DivideWholeArray(DATA_Total_ATK_Mid_Successful, DATA_Total_ATK_Mid_Execute);

        DATA_Total_ATK_Cone_Execute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_ATK_Cone_Execute);
        DATA_Total_ATK_Cone_Successful = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_ATK_Cone_Successful);
        DATA_Total_ATK_Cone_Percent = DivideWholeArray(DATA_Total_ATK_Cone_Successful, DATA_Total_ATK_Cone_Execute);

        DATA_Total_PATK_Close_Execute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_PATK_Close_Execute);
        DATA_Total_PATK_Close_Successful = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_PATK_Close_Successful);
        DATA_Total_PATK_Close_Pecent = DivideWholeArray(DATA_Total_PATK_Close_Successful, DATA_Total_PATK_Close_Execute);

        DATA_Total_PATK_Far_Execute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_PATK_Far_Execute);
        DATA_Total_PATK_Far_Successful = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_PATK_Far_Successful);
        DATA_Total_PATK_Far_Pecent = DivideWholeArray(DATA_Total_PATK_Far_Successful, DATA_Total_PATK_Far_Execute);

        DATA_Total_FIN_Close_Execute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_FIN_Close_Execute);
        DATA_Total_FIN_Close_Successful = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_FIN_Close_Successful);
        DATA_Total_FIN_Close_Pecent = DivideWholeArray(DATA_Total_FIN_Close_Successful, DATA_Total_FIN_Close_Execute);

        DATA_Total_FIN_Far_Execute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_FIN_Far_Execute);
        DATA_Total_FIN_Far_Successful = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_FIN_Far_Successful);
        DATA_Total_FIN_Far_Pecent = DivideWholeArray(DATA_Total_FIN_Far_Successful, DATA_Total_FIN_Far_Execute);

        DATA_Total_Dodge_Off_Execute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Dodge_Off_Execute);
        DATA_Total_Dodge_Off_Successful = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Dodge_Off_Successful);
        DATA_Total_Dodge_Off_Percent = DivideWholeArray(DATA_Total_Dodge_Off_Successful, DATA_Total_Dodge_Off_Execute);

        DATA_Total_Dodge_OffSide_Execute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Dodge_OffSide_Execute);
        DATA_Total_Dodge_OffSide_Successful = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Dodge_OffSide_Successful);
        DATA_Total_Dodge_OffSide_Percent = DivideWholeArray(DATA_Total_Dodge_OffSide_Successful, DATA_Total_Dodge_OffSide_Execute);

        DATA_Total_Dodge_Side_Execute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Dodge_Side_Execute);
        DATA_Total_Dodge_Side_Successful = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Dodge_Side_Successful);
        DATA_Total_Dodge_Side_Percent = DivideWholeArray(DATA_Total_Dodge_Side_Successful, DATA_Total_Dodge_Side_Execute);

        DATA_Total_Dodge_DefSide_Execute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Dodge_DefSide_Execute);
        DATA_Total_Dodge_DefSide_Successful = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Dodge_DefSide_Successful);
        DATA_Total_Dodge_DefSide_Percent = DivideWholeArray(DATA_Total_Dodge_DefSide_Successful, DATA_Total_Dodge_DefSide_Execute);

        DATA_Total_Dodge_Def_Execute = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Dodge_Def_Execute);
        DATA_Total_Dodge_Def_Successful = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_Dodge_Def_Successful);
        DATA_Total_Dodge_Def_Percent = DivideWholeArray(DATA_Total_Dodge_Def_Successful, DATA_Total_Dodge_Def_Execute);

        DATA_Total_ATK_Close_Successful_Point = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_ATK_Close_Hit_Point);
        DATA_Total_ATK_Mid_Successful_Point = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_ATK_Mid_Hit_Point);
        DATA_Total_ATK_Cone_Successful_Point = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_ATK_Cone_Hit_Point);
        DATA_Total_PATK_Close_Successful_Point = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_PATK_Close_Hit_Point);
        DATA_Total_PATK_Far_Successful_Point = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_PATK_Far_Hit_Point);
        DATA_Total_FIN_Close_Successful_Point = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_FIN_Close_Hit_Point);
        DATA_Total_FIN_Far_Successful_Point = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_FIN_Far_Hit_Point);

        DATA_Total_Dodge_Atk_Close_Successful_Point = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_ATK_Close_Dodge_Point);
        DATA_Total_Dodge_Atk_Mid_Successful_Point = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_ATK_Mid_Dodge_Point);
        DATA_Total_Dodge_Atk_Cone_Successful_Point = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_ATK_Cone_Dodge_Point);
        DATA_Total_Dodge_Pre_Close_Successful_Point = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_PATK_Close_Dodge_Point);
        DATA_Total_Dodge_Pre_Far_Successful_Point = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_PATK_Far_Dodge_Point);
        DATA_Total_Dodge_Fin_Close_Successful_Point = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_FIN_Close_Dodge_Point);
        DATA_Total_Dodge_Fin_Far_Successful_Point = TwoDArrayRowto1DArray(DATABASE, DATA_NUM_FIN_Far_Dodge_Point);

    }

    public void UpdateText()
    {
        DATA_PlayTime_text.text = "PlayTime = " + DATA_PlayTime;
        DATA_Total_Attack_Exeute_text.text = "Total_Attack_Exeute = " + DATA_Total_Attack_Exeute;
        DATA_Total_Attack_Hit_text.text = "Total_Attack_Hit = " + DATA_Total_Attack_Hit;
        DATA_Total_Block_Execute_text.text = "Total_Block_Execute = " + DATA_Total_Block_Execute;
        DATA_Total_Block_Sucessful_text.text = "Total_Block_Sucessful = " + DATA_Total_Block_Sucessful;
        DATA_Total_Dodge_Execute_text.text = "Total_Dodge_Execute = " + DATA_Total_Dodge_Execute;
        DATA_Total_Dodge_Sucessful_text.text = "Total_Dodge_Sucessful = " + DATA_Total_Dodge_Sucessful;
        DATA_Total_Jump_Execute_text.text = "Total_Jump_Execute = " + DATA_Total_Jump_Execute;
        DATA_Total_Jump_Sucessful_text.text = "Total_Jump_Sucessful = " + DATA_Total_Jump_Sucessful;
        DATA_Average_Distance_Between_text.text = "Average_Distance_Between = " + DATA_Average_Distance_Between;
    }

    public static void Update_DATABASE(int CharacterCode, int dataname, float value)
    {
        DATABASE[CharacterCode, dataname] = value;
    }
    public static void UpdateADD_DATABASE(int CharacterCode, int dataname, float value)
    {
        DATABASE[CharacterCode, dataname] += value;
    }

    public static void Reset_DATA(int CharacterCode)
    {
        for (int i = 0; i < DATABASE.Length; i++)
        {
            DATABASE[CharacterCode, i] = 0;
        }

    }

    public int Score_1_Attack;
    public int Score_2_Dodge;
    public bool Win;


    /// ==================================== Score List (for Score update) ======================== 

    public float Score_Scale_LastS;
    public float Score_Scale_CurrS;

    public float Score_Scale_All_Dodge;
    public float Score_Dodge_B_Clo;
    public float Score_Dodge_B_Mid;
    public float Score_Dodge_B_Con;
    public float Score_Dodge_P_Clo;
    public float Score_Dodge_P_Mid;
    public float Score_Dodge_F_Clo;
    public float Score_Dodge_F_Far;

    public float Score_Scale_All_Atk;
    public float Score_Atk_B_Clo;
    public float Score_Atk_B_Mid;
    public float Score_Atk_B_Con;
    public float Score_Atk_P_Clo;
    public float Score_Atk_P_Mid;
    public float Score_Atk_F_Clo;
    public float Score_Atk_F_Far;

    public float Score_Plus_Last_Score;

    public float Score_Win_Time;
    public float Score_Last_Win_Time;
    public float Score_Lose_HP;
    public float Score_Last_Lose_HP;
    float Sudden_Remaining_HP_Collect;
    bool Game_End_Score_Signal = false;
    bool Game_End_Score_Updated = false;
    //private int Current_Opponent_Code;

    int currentThisCharacterCode;
    int currentOpponentCharacterCode;
    bool Is_Player_or_Enemy;
    Character_Stat currentCharacterStat;


    private void Score_Update()
    {
        for (int i = 0; i < Battle_Controller.SLOT_NUMBER; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                if (j == 1)
                {
                    currentThisCharacterCode = Battle_Controller.Pair_Test_List_Static[i, 1];
                    currentOpponentCharacterCode = Battle_Controller.Pair_Test_List_Static[i, 2];
                    Is_Player_or_Enemy = true;
                    currentCharacterStat
                        = Battle_Controller.playerStat[Mathf.Abs(currentThisCharacterCode)];
                }
                else if (j == 2)
                {
                    currentThisCharacterCode = Battle_Controller.Pair_Test_List_Static[i, 2];
                    currentOpponentCharacterCode = Battle_Controller.Pair_Test_List_Static[i, 1];
                    Is_Player_or_Enemy = false;
                    currentCharacterStat
                         = Battle_Controller.enemyStat[Mathf.Abs(currentThisCharacterCode)];
                }
            }


            //      Current_Opponent_Code = Battle_Controller.Check_Opponent_Character(currentPlayerCharacterCode);
            Win = Is_Player_or_Enemy ?
                (Battle_Controller.Is_Enemy_In_Level_Up[currentOpponentCharacterCode] &&
        !Battle_Controller.Is_Player_In_Level_Up[currentThisCharacterCode])
        : (Battle_Controller.Is_Player_In_Level_Up[currentThisCharacterCode] &&
        !Battle_Controller.Is_Enemy_In_Level_Up[currentOpponentCharacterCode]);


            if (Win) print((Is_Player_or_Enemy ? "P_Enemy : " : "  Enemy : ") + " WINNNN NNNNNNNNNNNNNNNNNNNNNNNNN");

            if (currentCharacterStat.Cur_HP_Per > 0 &&
                !(Battle_Controller.Is_Enemy_In_Level_Up[currentOpponentCharacterCode] || Battle_Controller.Is_Player_In_Level_Up[currentThisCharacterCode])
                && Battle_Controller.Time_limit[currentThisCharacterCode] > 0
                && Battle_Controller.Time_limit[currentThisCharacterCode] < 0.1f)
            {
                // print("Get Sunnden HP = " + enemystat.Cur_HP_Per);
                Sudden_Remaining_HP_Collect = currentCharacterStat.Cur_HP_Per;
                Game_End_Score_Signal = true;
            }
            else if (Battle_Controller.Time_limit[currentThisCharacterCode] > (Battle_Controller.Set_Time_limit2[currentThisCharacterCode] - 0.1f))
            {
                Sudden_Remaining_HP_Collect = 0;
                Game_End_Score_Signal = false;
                Game_End_Score_Updated = false;
            }


            //////////////////////////////////////////////////////// Old one /////////////////////////////////////
            //////////////////////////////////////////////////////// New one /////////////////////////////////////
            if (Game_End_Score_Updated == false)
            {
                if (Game_End_Score_Signal) Game_End_Score_Updated = true;
                if (Is_Player_or_Enemy ? (Battle_Controller.Score_mode[currentThisCharacterCode] == 1) : (Battle_Controller.E_Score_mode[currentThisCharacterCode] == 1))
                {

                    Score_Scale_LastS = (1 + ((float)Battle_Controller.Score_1_Attack_Last[currentThisCharacterCode] / 1500));
                    Score_Scale_CurrS = (1 + ((float)Score_1_Attack / 1400));

                    Score_Scale_All_Dodge = 1.0f;
                    /*
                    Score_Dodge_B_Clo = DATA_Total_Dodge_Atk_Close_Successful_Point * 5f;
                    Score_Dodge_B_Mid = DATA_Total_Dodge_Atk_Mid_Successful_Point * 2.4f;
                    Score_Dodge_B_Con = DATA_Total_Dodge_Atk_Cone_Successful_Point * 1.8f;
                    Score_Dodge_P_Clo = DATA_Total_Dodge_Pre_Close_Successful_Point * 2.4f;
                    Score_Dodge_P_Mid = DATA_Total_Dodge_Pre_Far_Successful_Point * 1.6f;
                    Score_Dodge_F_Clo = DATA_Total_Dodge_Fin_Close_Successful_Point * 1.8f;
                    Score_Dodge_F_Far = DATA_Total_Dodge_Fin_Far_Successful_Point * 1.2f;

                    Score_Scale_All_Atk = 2.2f;
                    Score_Atk_B_Clo = DATA_Total_ATK_Close_Successful_Point * 15f;
                    Score_Atk_B_Mid = DATA_Total_ATK_Mid_Successful_Point * 12f;
                    Score_Atk_B_Con = DATA_Total_ATK_Cone_Successful_Point * 18f;
                    Score_Atk_P_Clo = DATA_Total_PATK_Close_Successful_Point * 10f;
                    Score_Atk_P_Mid = DATA_Total_PATK_Far_Successful_Point * 12f;
                    Score_Atk_F_Clo = DATA_Total_FIN_Close_Successful_Point * 23f;
                    Score_Atk_F_Far = DATA_Total_FIN_Far_Successful_Point * 25f;
                    */
                    Score_Plus_Last_Score = (Battle_Controller.Score_1_Attack_Last[currentThisCharacterCode] /
                                    (1.8f * (1 + (Battle_Controller.Score_1_Attack_Last[currentThisCharacterCode] / 1500)))
                                    );

                    Score_Win_Time = ((Win ? 250 : 0) * (Battle_Controller.Set_Time_limit2[currentThisCharacterCode]
                        - Battle_Controller.Last_Time_Limit[currentThisCharacterCode])
                    / Battle_Controller.Set_Time_limit2[currentThisCharacterCode]);
                    Score_Lose_HP = Sudden_Remaining_HP_Collect;
                    Score_1_Attack = (int)((Score_Dodge_B_Clo
                           + Score_Dodge_B_Mid
                           + Score_Dodge_B_Con
                           + Score_Dodge_P_Clo
                           + Score_Dodge_P_Mid
                           + Score_Dodge_F_Clo
                           + Score_Dodge_F_Far)
                           + ((Score_Atk_B_Clo
                           + Score_Atk_B_Mid
                           + Score_Atk_B_Con
                           + Score_Atk_P_Clo
                           + Score_Atk_P_Mid
                           + Score_Atk_F_Clo
                           + Score_Atk_F_Far
                           + Score_Win_Time
                             + Score_Lose_HP) * (1 + ((float)Round_Number / 1000))));
                    //   if (!Is_P_Enemy)
                    //     print("SCORE_1_ATTACK = " + Score_1_Attack);
                }
                if (Is_Player_or_Enemy ? (Battle_Controller.Score_mode[currentThisCharacterCode] == 2) : (Battle_Controller.E_Score_mode[currentThisCharacterCode] == 2))
                {
                    Score_Scale_LastS = (1 + ((float)Battle_Controller.Score_2_Dodge_Last[currentThisCharacterCode] / 1500));
                    Score_Scale_CurrS = (1 + ((float)Score_2_Dodge / 1400));
                    //print((float)Battle_Controller.Score_2_Dodge_Last / 1100);
                    Score_Scale_All_Dodge = 2.2f;
                    /*
                    Score_Dodge_B_Clo = DATA_Total_Dodge_Atk_Close_Successful_Point * 20f;
                    Score_Dodge_B_Mid = DATA_Total_Dodge_Atk_Mid_Successful_Point * 15f;
                    Score_Dodge_B_Con = DATA_Total_Dodge_Atk_Cone_Successful_Point * 10f;
                    Score_Dodge_P_Clo = DATA_Total_Dodge_Pre_Close_Successful_Point * 15f;
                    Score_Dodge_P_Mid = DATA_Total_Dodge_Pre_Far_Successful_Point * 12f;
                    Score_Dodge_F_Clo = DATA_Total_Dodge_Fin_Close_Successful_Point * 9f;
                    Score_Dodge_F_Far = DATA_Total_Dodge_Fin_Far_Successful_Point * 6f;

                    Score_Scale_All_Atk = 1.0f;
                    Score_Atk_B_Clo = DATA_Total_ATK_Close_Successful_Point * 2.5f;
                    Score_Atk_B_Mid = DATA_Total_ATK_Mid_Successful_Point * 3f;
                    Score_Atk_B_Con = DATA_Total_ATK_Cone_Successful_Point * 4f;
                    Score_Atk_P_Clo = DATA_Total_PATK_Close_Successful_Point * 2f;
                    Score_Atk_P_Mid = DATA_Total_PATK_Far_Successful_Point * 2f;
                    Score_Atk_F_Clo = DATA_Total_FIN_Close_Successful_Point * 2f;
                    Score_Atk_F_Far = DATA_Total_FIN_Far_Successful_Point * 2f;
                    */
                    Score_Plus_Last_Score = (Battle_Controller.Score_2_Dodge_Last[currentThisCharacterCode] /
                                    (1.8f * (1 + (Battle_Controller.Score_1_Attack_Last[currentThisCharacterCode] / 1500)))
                                    );

                    Score_Win_Time = ((Win ? 250 : 0) * (Battle_Controller.Set_Time_limit2[currentThisCharacterCode]
                        - Battle_Controller.Last_Time_Limit[currentThisCharacterCode])
                    / Battle_Controller.Set_Time_limit2[currentThisCharacterCode]);
                    Score_Lose_HP = Sudden_Remaining_HP_Collect;
                    Score_2_Dodge = (int)(((Score_Dodge_B_Clo
                            + Score_Dodge_B_Mid
                            + Score_Dodge_B_Con
                            + Score_Dodge_P_Clo
                            + Score_Dodge_P_Mid
                            + Score_Dodge_F_Clo
                            + Score_Dodge_F_Far) * (1 + ((float)Round_Number / 1000)))
                    + (Score_Atk_B_Clo
                            + Score_Atk_B_Mid
                            + Score_Atk_B_Con
                            + Score_Atk_P_Clo
                            + Score_Atk_P_Mid
                            + Score_Atk_F_Clo
                            + Score_Atk_F_Far
                            + Score_Win_Time
                             + Score_Lose_HP));
                }
            }

            if (Score_Lose_HP > 0 && Score_Lose_HP != Score_Last_Lose_HP)
            {
                Score_Last_Lose_HP = Score_Lose_HP;
            }
            if (Score_Win_Time > 0 && Score_Win_Time != Score_Last_Win_Time)
            {
                Score_Last_Win_Time = Score_Win_Time;
            }

            //////////////////////////////////////////////////////// New one /////////////////////////////////////
            if (Is_Player_or_Enemy)
            {
                if (Battle_Controller.Score_mode[currentThisCharacterCode] == 1 && Battle_Controller.game_Mode[currentThisCharacterCode] == 2)
                    Battle_Controller.Score_1_Attack[currentThisCharacterCode] = Score_1_Attack;
                if (Battle_Controller.Score_mode[currentThisCharacterCode] == 2 && Battle_Controller.game_Mode[currentThisCharacterCode] == 2)
                {
                    Battle_Controller.Score_2_Dodge[currentThisCharacterCode] = Score_2_Dodge;
                    // print("fdfdf");
                }
            }
            else
            {
                if (Battle_Controller.E_Score_mode[currentThisCharacterCode] == 1 && Battle_Controller.game_Mode[currentThisCharacterCode] == 2)
                    Battle_Controller.E_Score_1_Attack[currentThisCharacterCode] = Score_1_Attack;
                if (Battle_Controller.E_Score_mode[currentThisCharacterCode] == 2 && Battle_Controller.game_Mode[currentThisCharacterCode] == 2)
                {
                    Battle_Controller.E_Score_2_Dodge[currentThisCharacterCode] = Score_2_Dodge;
                    print("fdfdf");
                }
            }


        }
    }
    public void Reset_SCORE(int currentThisCharacterCode)
    {
        DATA_Total_ATK_Close_Successful_Point[currentThisCharacterCode] = 0f;
        DATA_Total_ATK_Mid_Successful_Point[currentThisCharacterCode] = 0f;
        DATA_Total_ATK_Cone_Successful_Point[currentThisCharacterCode] = 0f;
        DATA_Total_PATK_Close_Successful_Point[currentThisCharacterCode] = 0f;
        DATA_Total_PATK_Far_Successful_Point[currentThisCharacterCode] = 0f;
        DATA_Total_FIN_Close_Successful_Point[currentThisCharacterCode] = 0f;
        DATA_Total_FIN_Far_Successful_Point[currentThisCharacterCode] = 0f;
        DATA_Total_Dodge_Atk_Close_Successful_Point[currentThisCharacterCode] = 0f;
        DATA_Total_Dodge_Atk_Cone_Successful_Point[currentThisCharacterCode] = 0f;
        DATA_Total_Dodge_Atk_Mid_Successful_Point[currentThisCharacterCode] = 0f;
        DATA_Total_Dodge_Pre_Close_Successful_Point[currentThisCharacterCode] = 0f;
        DATA_Total_Dodge_Pre_Far_Successful_Point[currentThisCharacterCode] = 0f;
        DATA_Total_Dodge_Fin_Close_Successful_Point[currentThisCharacterCode] = 0f;
        DATA_Total_Dodge_Fin_Far_Successful_Point[currentThisCharacterCode] = 0f;

    }
    public int Round_Number;
    // private int Last_Dodge_Dir_Code; // 1 off 2 off-side 3 side 4 def-side 5 def
    private float Dodge_Score_Multiplier(int Last_Dodge_Dir_Code)
    {
        switch (Last_Dodge_Dir_Code)
        {
            case 1:
                return 1.35f;
                break;
            case 2:
                return 1.15f;
                break;
            case 3:
                return 1.0f;
                break;
            case 4:
                return 0.9f;
                break;
            case 5:
                return 0.8f;
                break;
            default:
                return 1f;
                break;
        }
    }
    //bool Is_P_Enemy;
    /*
    private float Attack_Score_Multiplier(bool Is_Player_or_Enemy, int state)
    {
        int A = Is_Player_or_Enemy ? Battle_Controller.P_STATE : Battle_Controller.E_STATE;
        switch (state)
        {
            
            case Battle_Controller.S_STILL:
                return 1f;
                break;
            case Battle_Controller.S_RUN_FORWARD:
                return 1.1f;
                break;
            case Battle_Controller.S_RUN_DIA_FORWARD:
                return 1.4f;
                break;
            case Battle_Controller.S_RUN_SIDE:
                return 2.0f;
                break;
            case Battle_Controller.S_RUN_DIA_BACKWARD:
                return 2.2f;
                break;
            case Battle_Controller.S_RUN_BACKWARD:
                return 2.4f;
                break;
            case Battle_Controller.S_DODGE_FORWARD:
                return 1.1f * 2.2f;
                break;
            case Battle_Controller.S_DODGE_DIA_FORWARD:
                return 1.4f * 2.2f;
                break;
            case Battle_Controller.S_DODGE_SIDE:
                return 2.0f * 2.2f;
                break;
            case Battle_Controller.S_DODGE_DIA_BACKWARD:
                return 2.2f * 2.2f;
                break;
            case Battle_Controller.S_DODGE_BACKWARD:
                return 2.4f * 2.2f;
                break;
            case Battle_Controller.S_BLOCK:
                return 3.0f;
                break;
            case Battle_Controller.S_HURT:
                return 0.75f;
                break;
            case Battle_Controller.S_ATK_BASIC_CLOSE:
                return 2.0f;
                break;
            case Battle_Controller.S_ATK_BASIC_MID:
                return 1.65f;
                break;
            case Battle_Controller.S_ATK_BASIC_CONE:
                return 1.3f;
                break;
            case Battle_Controller.S_ATK_PRE_CLOSE:
                return 1.75f;
                break;
            case Battle_Controller.S_ATK_PRE_FAR:
                return 1.4f;
                break;
            case Battle_Controller.S_ATK_FIN_CLOSE:
                return 1.5f;
                break;
            case Battle_Controller.S_ATK_FIN_FAR:
                return 1.25f;
                break;

            default:
                return 1f;
                break;
        }
    }
*/

    
}
