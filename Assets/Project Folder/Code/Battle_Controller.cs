using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Battle_Controller : MonoBehaviour {

    #region Variable
    /// ==================================== General ========================================
    public static int SLOT_NUMBER = 1;
    static int GameMode;
    static int[] ScoreMode;
    public Text[] Match_Time_text;
    public Text[] Round_Number_text;


    /// ==================================== General ========================================

    /// ==================================== Save FIle ========================================
    public int fileSaveNumber;
    public int fileLoadNumber;
    public static int[] LoadFile_P;
    public static int[] SaveFile_P;
    public static int[] LoadFile_E;
    public static int[] SaveFile_E;
    /// ==================================== Save FIle ========================================
    /// 
    /// 
    /// ==================================== Testing_Character_Pair_List ==========================
    public int[,] Pair_Test_List = new int[SLOT_NUMBER, 2];
    public static int[,] Pair_Test_List_Static = new int[SLOT_NUMBER, 2];
    /// ==================================== Testing_Character_Pair_List ==========================
    /// 
    /// ==================================== Character (Game Object + Other Component) ==========================
    public GameObject[] player;
    public GameObject[] enemy;
    public static Character_1[] playerCharacter;
    public static Character_1[] enemyCharacter;
    public static Character_Stat[] playerStat;
    public static Character_Stat[] enemyStat;

    public static Character_1_DataWrite[] playerCharacterDataWrite;
    public static Character_1_DataWrite[] enemyCharacterDataWrite;

    public static Character_1_AIWrite[] enemyCharacterAIWrite;
    public static Dynamic_Script[] enemyCharacterDynamicScript;
    /// ==================================== Character (Game Object + Other Component) ==========================

    /// ==================================== Character Stat ===================================
    public const int S_STILL = 10010;
    public const int S_RUN_FORWARD = 10011;
    public const int S_RUN_FOR_RIGHT = 10012;
    public const int S_RUN_RIGHT = 10013;
    public const int S_RUN_BAC_RIGHT = 10014;
    public const int S_RUN_BACKWARD = 10015;
    public const int S_RUN_BAC_LEFT = 10016;
    public const int S_RUN_LEFT = 10017;
    public const int S_RUN_FOR_LEFT = 10018;
    public const int S_ATK = 20000;
    public const int S_ATK_BAL_N1 = 20011;
    public const int S_ATK_BAL_N2 = 20012;
    public const int S_ATK_BAL_N3 = 20013;
    public const int S_ATK_BAL_N4 = 20014;
    public const int S_ATK_BAL_F1 = 20111;
    public const int S_ATK_BAL_F2 = 20112;
    public const int S_ATK_BAL_F3 = 20113;
    public const int S_ATK_BAL_F4 = 20114;
    public const int S_ATK_HEV_N1 = 22011;
    public const int S_ATK_HEV_N2 = 22012;
    public const int S_ATK_HEV_N3 = 22013;
    public const int S_ATK_HEV_F1 = 22111;
    public const int S_ATK_HEV_F2 = 22112;
    public const int S_ATK_HEV_F3 = 22113;
    public const int S_ATK_QUK_N1 = 24011;
    public const int S_ATK_QUK_N2 = 24012;
    public const int S_ATK_QUK_N3 = 24013;
    public const int S_ATK_QUK_N4 = 24014;
    public const int S_ATK_QUK_N5 = 24015;
    public const int S_ATK_QUK_F1 = 24111;
    public const int S_ATK_QUK_F2 = 24112;
    public const int S_ATK_QUK_F3 = 24113;
    public const int S_ATK_QUK_F4 = 24114;
    public const int S_ATK_QUK_F5 = 24115;
    public const int S_BLOCK = 40010;
    public const int S_BLOCK_SUCESS = 40013;

    public const int S_DODGE = 40200;
    public const int S_DODGE_FORWARD = 40201;
    public const int S_DODGE_FOR_RIGHT = 40202;
    public const int S_DODGE_RIGHT = 40203;
    public const int S_DODGE_BAC_RIGHT = 40204;
    public const int S_DODGE_BACKWARD = 40205;
    public const int S_DODGE_BAC_LEFT = 40206;
    public const int S_DODGE_LEFT = 40207;
    public const int S_DODGE_FOR_LEFT = 40208;

    public const int S_CHAGE_DODGE = 42200;
    public const int S_CDODGE_FORWARD = 42201;
    public const int S_CDODGE_FOR_RIGHT = 42202;
    public const int S_CDODGE_RIGHT = 42203;
    public const int S_CDODGE_BAC_RIGHT = 42204;
    public const int S_CDODGE_BACKWARD = 42205;
    public const int S_CDODGE_BAC_LEFT = 42206;
    public const int S_CDODGE_LEFT = 42207;
    public const int S_CDODGE_FOR_LEFT = 42208;

    public const int S_BACKDODGE = 44201;

    public const int S_JUMP = 60;
    public const int S_HURT = 90;
    public const int S_LEVEL_UP = 99;
    /// ==================================== Character Stat ===================================
    /// ==================================== Character Mode ===================================
    public const int MODE_BALANCE = 1;
    public const int MODE_HEAVY = 2;
    public const int MODE_QUICK = 3;
    /// ==================================== Character Mode ===================================
    /// ==================================== Character Stat (Old) ===================================
    
    //public const int S_STILL                 = 00;
    public const int S_RUN_FOR = 01;
    public const int S_RUN_DIA_FORWARD = 02;
    public const int S_RUN_SIDE = 03;
    public const int S_RUN_DIA_BACKWARD = 04;
    public const int S_RUN_BACK = 05;
    //public const int S_ATK = 100;
    public const int S_ATK_BASIC_CLOSE       = 10;
    public const int S_ATK_BASIC_MID            = 11;
    public const int S_ATK_BASIC_CONE       = 12;
    public const int S_ATK_PRE_CLOSE        = 20;
    public const int S_ATK_PRE_FAR          = 21;
    public const int S_ATK_FIN_CLOSE        = 30;
    public const int S_ATK_FIN_FAR          = 31;
   // public const int S_DODGE = 400;
   // public const int S_DODGE_FORWARD = 41;
    public const int S_DODGE_DIA_FORWARD = 42;
    public const int S_DODGE_SIDE = 43;
    public const int S_DODGE_DIA_BACKWARD    = 44;
    //public const int S_DODGE_BACKWARD        = 45;
    //public const int S_BLOCK = 50;
    //public const int S_JUMP                  = 60;
    //public const int S_HURT = 90;
   // public const int S_LEVEL_UP              = 99;
    
    /// ==================================== Character Stat ===================================

    /// ==================================== Score ===================================
    public static int[] Score = new int[SLOT_NUMBER];
    public static int[] Last_Score = new int[SLOT_NUMBER];
    public static int[] E_Score = new int[SLOT_NUMBER];
    public static int[] E_Last_Score = new int[SLOT_NUMBER];
    /// ==================================== Score ===================================

    /// ==================================== UI ===================================
    public Text[] TEXT_P_State;
    public Text[] TEXT_E_State;

    public Text[] TEXT_Score;
    public Text[] TEXT_Last_Score;

    public Text[] E_TEXT_Score;
    public Text[] E_TEXT_Last_Score;

    public Character_1_ModeSwitchUI[] P_MODE_UI;
	public Character_1_ModeSwitchUI[] E_MODE_UI;
    /// ==================================== UI ===================================

    public GameObject ControlMap;
	public GameObject PatternDesc;


    /// ==================================== Default List for set up ===================================
    /*
    private static int[] Default_Int_List = Insert_OneVal_WholeArray_Int(SLOT_NUMBER, 0);
    private static int[] Default_Int_1_List = Insert_OneVal_WholeArray_Int(SLOT_NUMBER, 1);
    private static float[] Default_Float_List = Insert_OneVal_WholeArray_Float(SLOT_NUMBER, 0.0f);
    private static bool[] Default_Bool_List = Insert_OneVal_WholeArray_Bool(SLOT_NUMBER, false);
    */
    private static int[] Default_Int_List = new int[SLOT_NUMBER];
    private static int[] Default_Int_1_List = new int[SLOT_NUMBER];
    private static float[] Default_Float_List = new float[SLOT_NUMBER];
    private static bool[] Default_Bool_List = new bool[SLOT_NUMBER];
    /// ==================================== Default List for set up ===================================

    /// ==================================== Time Limit / Game Reset Control ===================================
    /// 
    public float TIME = 0f;
    public float[] RoundNumber = new float[SLOT_NUMBER];

    public float[] Set_Time_limit;
    /*
    public static float[] Set_Time_limit2 = Default_Float_List;
    public static float[] Time_limit = Default_Float_List;
    public static float[] Last_Time_Limit = Default_Float_List;
    */
    public static float[] Set_Time_limit2 = new float[SLOT_NUMBER];
    public static float[] Time_limit = new float[SLOT_NUMBER];
    public static float[] Last_Time_Limit = new float[SLOT_NUMBER];

    private static float[] Time_Limit_Set_d = new float[SLOT_NUMBER];
    private bool[] Is_Check_Last_Time_Limit = new bool[SLOT_NUMBER];
    private bool[] Is_Check_Reset_Game_Dead = new bool[SLOT_NUMBER];
    private bool[] Is_Check_Reset_Game_Dead_Trigger = new bool[SLOT_NUMBER];

    private bool[] Is_Reset_Time_UP_Once = new bool[SLOT_NUMBER];
    /// ==================================== Time Limit / Game Reset Control ===================================

    /// ==================================== Game End Signal / Winning Check ===================================
    public static bool[] Game_End_Signal;
    public static bool[] Game_End_Signal_PlayerDead = new bool[SLOT_NUMBER];
    public static bool[] Game_End_Signal_EnemyDead = new bool[SLOT_NUMBER];
    public static bool[] Is_In_Game_End_Signal_Player = new bool[SLOT_NUMBER];
    public static bool[] Is_In_Game_End_Signal_Enemy = new bool[SLOT_NUMBER];
    public static bool[] Game_End_Signal_Time = new bool[SLOT_NUMBER];
    public static bool[] Is_In_Game_End_Signal_Time = new bool[SLOT_NUMBER];

    public static bool[] Player_Check_Win = new bool[SLOT_NUMBER];
    public static bool[] Enemy_Check_Win = new bool[SLOT_NUMBER];
    public static bool[] Is_In_Game_End_Signal_Total = new bool[SLOT_NUMBER];//= { Default_Bool_List, Default_Bool_List, Default_Bool_List };
                                                                              // 0 = Total code, 1 = Player code, 2 = Eemy code

    /// ==================================== Game End Signal / Winning Check ===================================

    /// ==================================== Character State ===================================
    /// ==================================== Character State ===================================

    /// ==================================== Character in Level up check ===================================
    public static bool[] Is_Character_In_Level_Up = new bool[SLOT_NUMBER];
    public static bool[] Is_Player_In_Level_Up = new bool[SLOT_NUMBER];
    public static bool[] Is_Enemy_In_Level_Up = new bool[SLOT_NUMBER];

    public static bool[] CALL_FULL_HEAL = new bool[SLOT_NUMBER];
    /// ==================================== Character in Level up check ===================================

    /// ==================================== Character Position ===================================
    public static Transform[] Current_Character_Position = { };
    public static Transform[] Current_Player_Position = { };
    public static Transform[] Current_Enemy_Position = { };
    /// ==================================== Character Position ===================================


    public enum Mode_List
    {
        GM_Player_VS_Enemy,
        GM_P_Enemy_VS_Enemy,
        SM_2_Dodge_Mode,
        SM_1_Attack_Mode,
        Combo_List,
        Pattern_List

    }

    /// ==================================== Game/Score Mode ===================================
    public static int[] game_Mode = new int[SLOT_NUMBER]; // 1 = Player VS Enemy /// 2 = P_Enemy VS Enemy

    public static int[] Score_mode = new int[SLOT_NUMBER]; //  2 = Dodge Trial Mode // 1 = Attack Trial Mode // 
    public static int[] Score_mode_Text_Show = new int[SLOT_NUMBER];
    public static int[] E_Score_mode = new int[SLOT_NUMBER]; //  2 = Dodge Trial Mode // 1 = Attack Trial Mode // 
    public static int[] E_Score_mode_Text_Show = new int[SLOT_NUMBER];
    public Mode_List[] GMode ; // 1 = Player VS Enemy /// 2 = P_Enemy VS Enemy
    public Mode_List[] SMode; // 1 = Standard // 2 = Dodge Trial Mode // 3 = Attack Trial Mode // 
    public Mode_List[] E_SMode; // 1 = Standard // 2 = Dodge Trial Mode // 3 = Attack Trial Mode //
    public Mode_List[] S_Show_Mode; // 1 = Standard // 2 = Dodge Trial Mode // 3 = Attack Trial Mode // 

    /// ==================================== Game/Score Mode ===================================

    /// ==================================== Score_List ===================================
    public static int[] Score_1_Attack  =  new int[SLOT_NUMBER];
    public static int[] Score_1_Attack_Last =  new int[SLOT_NUMBER];
    public static int[] Score_2_Dodge =  new int[SLOT_NUMBER];
    public static int[] Score_2_Dodge_Last =  new int[SLOT_NUMBER];
    public static int[] E_Score_1_Attack =  new int[SLOT_NUMBER];
    public static int[] E_Score_1_Attack_Last =  new int[SLOT_NUMBER];
    public static int[] E_Score_2_Dodge =  new int[SLOT_NUMBER];
    public static int[] E_Score_2_Dodge_Last =  new int[SLOT_NUMBER];
    /// ==================================== Score_List ===================================

    /// ==================================== Character Health ===================================
    public static float[] Player_percentHP = new float[SLOT_NUMBER];
    public static float[] Player_HPMax = new float[SLOT_NUMBER];
    public static float[] Enemy_percentHP = new float[SLOT_NUMBER];
    public static float[] Enemy_HPMax = new float[SLOT_NUMBER];
    /// ==================================== Character Health =================================== <summary>

    

    /////////////////////////////////////////////////////////////////////////////////////////// 

    public static int TotalEnemy = 0;
    private int startframe = 0;
    //===============================================

    public static float DATA_P_0XXXX_PlayTime = 0;
    public static int DATA_P_10XXX_TOL_ATK_EXC = 0;
    public static int DATA_P_11XXX_TOL_ATK_HIT = 0;
    public static int DATA_P_20XXX_TOL_DOG_EXC = 0;
    public static int DATA_P_21XXX_TOL_DOG_SUC = 0;
    public static int DATA_P_30XXX_TOL_BOK_EXC = 0;
    public static int DATA_P_31XXX_TOL_BOK_SUC = 0;
    public static int DATA_P_40XXX_TOL_JUMP_EXC = 0;
    public static float DATA_P_90XXX_AVG_DIS_BETW = 0;

    public static float DATA_E_0XXXX_PlayTime = 0;
    public static int DATA_E_10XXX_TOL_ATK_EXC = 0;
    public static int DATA_E_11XXX_TOL_ATK_HIT = 0;
    public static int DATA_E_20XXX_TOL_DOG_EXC = 0;
    public static int DATA_E_21XXX_TOL_DOG_SUC = 0;
    public static int DATA_E_30XXX_TOL_BOK_EXC = 0;
    public static int DATA_E_31XXX_TOL_BOK_SUC = 0;
    public static int DATA_E_40XXX_TOL_JUMP_EXC = 0;
    public static float DATA_E_90XXX_AVG_DIS_BETW = 0;

    public static float DATA_PE_0XXXX_PlayTime = 0;
    public static int DATA_PE_10XXX_TOL_ATK_EXC = 0;
    public static int DATA_PE_11XXX_TOL_ATK_HIT = 0;
    public static int DATA_PE_20XXX_TOL_DOG_EXC = 0;
    public static int DATA_PE_21XXX_TOL_DOG_SUC = 0;
    public static int DATA_PE_30XXX_TOL_BOK_EXC = 0;
    public static int DATA_PE_31XXX_TOL_BOK_SUC = 0;
    public static int DATA_PE_40XXX_TOL_JUMP_EXC = 0;
    public static float DATA_PE_90XXX_AVG_DIS_BETW = 0;

    // ================ Player & Enemy State ===========================
    public static int[] PLAYER_STATE = new int[SLOT_NUMBER];
    public static int[] P_ENEMY_STATE = new int[SLOT_NUMBER];
    public static int[] E_STATE = new int[SLOT_NUMBER];
    public static int[] P_STATE = new int[SLOT_NUMBER];
    // ===============================================================
    // ================ Player & Enemy State ===========================
    public static int[] PLAYER_MODE = new int[SLOT_NUMBER];
    public static int[] P_ENEMY_MODE = new int[SLOT_NUMBER];
    public static int[] E_MODE = new int[SLOT_NUMBER];
    public static int[] P_MODE = new int[SLOT_NUMBER];
    // ===============================================================

    public static bool[] PLAYER_HITBOX_ACTIVE = new bool[SLOT_NUMBER];
    public static bool[] P_ENEMY_HITBOX_ACTIVE = new bool[SLOT_NUMBER];
    public static bool[] ENEMY_HITBOX_ACTIVE = new bool[SLOT_NUMBER];


    // Player Character = -;
    // Enemy Character = +;
    // Corespon with 

    #endregion

    public void All_Full_Heal(int i)
    {        
        if (game_Mode[i] == 1)
        {
            playerStat[i].SetCurrentHPPercentage(1);
            playerStat[i].Is_Reset = true;
            enemyStat[i].SetCurrentHPPercentage(1);
            enemyStat[i].Is_Reset = true;
        }
        else
        {
            playerStat[i].SetCurrentHPPercentage(1);
            playerStat[i].Is_Reset = true;
            enemyStat[i].SetCurrentHPPercentage(1);
            enemyStat[i].Is_Reset = true;

        }

    }

    //===== Audio-Related Stuffs===================
    ///////////////////////////////////////////////////// public AudioSource audioSource; //Link to main Audiosource

    ///////////////////////////////////////////////////// public AudioClip selectSound;
    ///////////////////////////////////////////////////// public AudioClip confirmedSound;





    #region Start

    

    // Use this for initialization
    void Start()
        
    {
        
        //float[] Set_Time_limit3 = Set_Time_limit;
        //print(game_Mode[i] + "    " + i);
        // print(Default_Float_List.Length);
        playerCharacter = new Character_1[SLOT_NUMBER];
        enemyCharacter = new Character_1[SLOT_NUMBER];
        playerStat = new Character_Stat[SLOT_NUMBER];
        enemyStat = new Character_Stat[SLOT_NUMBER];
        playerCharacterDataWrite = new Character_1_DataWrite[SLOT_NUMBER];
        enemyCharacterDataWrite = new Character_1_DataWrite[SLOT_NUMBER];
        enemyCharacterAIWrite = new Character_1_AIWrite[SLOT_NUMBER];
        enemyCharacterDynamicScript = new Dynamic_Script[SLOT_NUMBER];
      
        for (int i = 0; i < player.Length; i++)
        {
             
            playerCharacter[i] = player[i].GetComponent<Character_1>();
            playerStat[i] = player[i].GetComponent<Character_Stat>();
            Player_percentHP[i] = playerStat[i].GetCurrentHPPercentage();
            Pair_Test_List[i, 0] = playerCharacter[i].CharacterCode;
            Player_HPMax[i] = playerStat[i].GetHP();
            playerCharacterDataWrite[i] = player[i].GetComponent<Character_1_DataWrite>();
               
        }
          
        for (int i = 0; i < enemy.Length; i++)
        {
            enemyCharacter[i] = enemy[i].GetComponent<Character_1>();
            enemyStat[i] = enemy[i].GetComponent<Character_Stat>();
            Pair_Test_List[i, 1] = enemyCharacter[i].CharacterCode;
            Enemy_percentHP[i] = enemyStat[i].GetCurrentHPPercentage();
            Enemy_HPMax[i] = enemyStat[i].GetHP();
            enemyCharacterDataWrite[i] = enemy[i].GetComponent<Character_1_DataWrite>();
            enemyCharacterAIWrite[i] = enemy[i].GetComponent<Character_1_AIWrite>();
            enemyCharacterDynamicScript[i] = enemy[i].GetComponent<Dynamic_Script>();
               
        }
       
        Pair_Test_List_Static = Pair_Test_List;
        print("Set_Time_limit[i] = [" + 0 + "] = " + Set_Time_limit[0]);   
        Game_End_Signal = new bool[SLOT_NUMBER];
        Game_End_Signal_PlayerDead= new bool[SLOT_NUMBER];
        Game_End_Signal_EnemyDead= new bool[SLOT_NUMBER];
        Game_End_Signal_Time=new bool[SLOT_NUMBER];

        Last_Time_Limit = (float[])Set_Time_limit.Clone();
        Set_Time_limit2 = (float[])Set_Time_limit.Clone();
        Time_limit = (float[])Set_Time_limit.Clone();
        Time_Limit_Set_d = (float[])Set_Time_limit.Clone();
     
       // print(game_Mode[i] + "    " + i);

        if (Main_Menu.GameMode >= 1)
        {
            print(game_Mode[i] + "    " + i);
            game_Mode[0] = Main_Menu.GameMode;
            Score_mode = Insert_OneVal_WholeArray_Int(Default_Float_List.Length
                        , Main_Menu.ScoreMode)
                ;
            if (Main_Menu.GameMode == 1)
            {
                for(int i = 0; i < player.Length; i++)
                {
                    if(player[i])
                        player[i].SetActive(true);
                }
                
                //Player_Enemy.SetActive(false);
            }
            else if (Main_Menu.GameMode == 2)
            {
            /////////////////garwgargsegsergsehsehserhserhsehsehserhserhserfhserhserhwsehshfet
            }
        }
        
        else
        {
           // print(game_Mode[i] + "    " + i);
            for (int i = 0; i < SLOT_NUMBER; i++)
            {
                
                game_Mode[i] = (GMode[i] == Mode_List.GM_Player_VS_Enemy) ? 1 : 2;

                //print(game_Mode[i] + "    " + i);

                Score_mode[i] = ((SMode[i] == Mode_List.SM_1_Attack_Mode) ? 1 :
                             (SMode[i] == Mode_List.SM_2_Dodge_Mode) ? 2 : 0);

                E_Score_mode[i] = ((E_SMode[i] == Mode_List.SM_1_Attack_Mode) ? 1 :
                            (E_SMode[i] == Mode_List.SM_2_Dodge_Mode) ? 2 : 0);
                    
                Score_mode_Text_Show[i] = ((S_Show_Mode[i] == Mode_List.Pattern_List) ? 1 :
                            (S_Show_Mode[i] == Mode_List.Combo_List) ? 2 : 0);

                ALL_SaveData(i);

            }
        }
        
        i = 0;
    }

    #endregion

    #region Update



    void Update()
    {
        Pair_Test_List_Static = Pair_Test_List;
        for (int i = 0; i < SLOT_NUMBER; i++)
        {

            Score_mode_Text_Show[i] = (S_Show_Mode[i] == Mode_List.Pattern_List) ? 1 :
           (S_Show_Mode[i] == Mode_List.Combo_List) ? 2 : 0;
            Pair_Test_List[i, 0] = i;
            if (!Is_In_Game_End_Signal_Total[i]) {
               
                Is_Check_Reset_Game_Dead_Trigger[i] = false;
            }

            if (Is_In_Game_End_Signal_Total[i] && !Is_Check_Reset_Game_Dead_Trigger[i])
            {
                Is_Check_Reset_Game_Dead[i] = true;
                Is_Check_Reset_Game_Dead_Trigger[i] = true;
                print(i + " " + Is_Check_Reset_Game_Dead[i] + "  " +Is_Check_Reset_Game_Dead_Trigger[i] );
            }
            if (Is_Check_Reset_Game_Dead[i])
            {
                Is_Check_Reset_Game_Dead[i] = false;
                Reset_Game(true, Is_In_Game_End_Signal_Player[i], i);
                Time_limit[i] +=5f;
                if (!Is_Check_Last_Time_Limit[i])
                {
                    Is_Check_Last_Time_Limit[i] = true;
                    Last_Time_Limit[i] = Time_limit[i];
                }
            }

            if (!Is_In_Game_End_Signal_Total[i])
                Is_Check_Last_Time_Limit[i] = false;

            Is_In_Game_End_Signal_Total[i] = false;

            if(Game_End_Signal_PlayerDead[i]){
                Is_In_Game_End_Signal_Player[i] = true ;
                Game_End_Signal_PlayerDead[i] = false;
            }
            if(Game_End_Signal_EnemyDead[i]){
                Is_In_Game_End_Signal_Enemy[i] = true ;
                Game_End_Signal_EnemyDead[i] = false;
            }
            if(Game_End_Signal_Time[i]){
                Is_In_Game_End_Signal_Time[i] = true;
                Game_End_Signal_Time[i] = false;
            }
            /*
            Is_Player_In_Level_Up[i] = Game_End_Signal_PlayerDead[i];
            Is_Enemy_In_Level_Up[i] = Game_End_Signal_EnemyDead[i];
                                    = Game_End_Signal_Time[i];
            */
            TIME = Time.deltaTime;
            if( Match_Time_text[i] != null)
                Match_Time_text[i].text = " " + (int)Time_limit[i] + " ";

              Game_End_Signal[i] = Game_End_Signal_PlayerDead[i] || Game_End_Signal_EnemyDead[i] || Game_End_Signal_Time[i];

            if (Time_limit[i] <= -0.15f || (Is_Check_Last_Time_Limit[i] && Time_limit[i] <= Last_Time_Limit[i] - 0.15f))
            {
                 //print("TimeLimit Minus = [" + i + "] = " + TIME);
                Game_End_Signal[i] = false;
                Game_End_Signal_PlayerDead[i] = false;
                Game_End_Signal_EnemyDead[i] = false;
                Game_End_Signal_Time[i] = false;
                //playerCharacterDataWrite[i].UpdateData();

            }
          
            /*
            else if (playerStat[i] != null)
            {
                if (playerStat[i].GetCurrentHPPercentage() <= 0 && Time_limit[i] > 0)
                {

                }
            }
            else if (enemyStat[i] != null)
            {
                if (enemyStat[i].GetCurrentHPPercentage() <= 0 && Time_limit[i] > 0)
                {

                }
            }
            */

            else if (Time_limit[i] <= 0)
            {
               
                if (!Is_Reset_Time_UP_Once[i])
                {
                    print("Reset_Game");
                    Game_End_Signal_PlayerDead[i] = true;
                    Game_End_Signal_EnemyDead[i] = true;
                    //Game_End_Signal_Time[i] = true;
                    Is_Reset_Time_UP_Once[i] = true;
                }
                else
                    Time_limit[i] -= TIME;
            }

            else if (!Is_In_Game_End_Signal_Total[i]) {
               // print("TimeLimit Minus = [" + i + "] = " + TIME);
                Time_limit[i] -= TIME;
               
            }
            if (Time_limit[i] > 0)
            {
                Is_Reset_Time_UP_Once[i] = false;
                check_Score_Once = false;
            }

            //print(TotalEnemy);
            if (startframe < 60)
            {
                startframe++;
            }

            if (TotalEnemy <= 0 && startframe >= 60)
            {
                //SceneManager.LoadScene("victory", LoadSceneMode.Single);
            }
            //print(currentATB);
            /*
            if (!Pause_Menu.GameIsPause)
            {
                KeyboardUpdate();
                HurtCheck();
                percentHP = player.GetCurrentHPPercentage();
            }
            */
            //////////////////////////////////////////////////////////////////////////////
            if (game_Mode[i] == 1)
            {
                if(playerStat[i] != null)
                {
                    Player_percentHP[i] = playerStat[i].GetCurrentHPPercentage();
                    //P_STATE[i] = PLAYER_STATE[i];
                    Player_HPMax[i] = playerStat[i].GetHP();
                }

            }

            else
            {
                if (playerStat[i] != null)
                {
                    Player_percentHP[i] = playerStat[i].GetCurrentHPPercentage();
                    //P_STATE[i] = P_ENEMY_STATE[i];
                    Player_HPMax[i] = playerStat[i].GetHP();
                }
            }
     
            if (CALL_FULL_HEAL[i])
            {
                CALL_FULL_HEAL[i] = false; 
                All_Full_Heal(i);

            }
            //////////////// Score Path //////////////////
            if (Score_mode[i] == 1)
            {

                Score[i] = Score_1_Attack[i];
                Last_Score[i] = Score_1_Attack_Last[i];
            }
            else if (Score_mode[i] == 2)
            {
                Score[i] = Score_2_Dodge[i];
                Last_Score[i] = Score_2_Dodge_Last[i];
            }
            if (E_Score_mode[i] == 1)
            {

                E_Score[i] = E_Score_1_Attack[i];
                E_Last_Score[i] = E_Score_1_Attack_Last[i];
            }
            else if (E_Score_mode[i] == 2)
            {
                E_Score[i] = E_Score_2_Dodge[i];
                E_Last_Score[i] = E_Score_2_Dodge_Last[i];
            }
        
            if(TEXT_Score[i] != null && E_TEXT_Score[i] != null)
            {
                TEXT_Score[i].text = "Score = " + Score;
                TEXT_Last_Score[i].text = "Last Score = " + Last_Score;

                E_TEXT_Score[i].text = "Score = " + E_Score;
                E_TEXT_Last_Score[i].text = "Last Score = " + E_Last_Score;
                //print(i);
                Enemy_percentHP[i] = enemyStat[i].GetCurrentHPPercentage();
                
                /////////////////////////////////////////// CHANGE STATE UI ///////////////////////////////////////////
                if (Pause_Menu.GameIsPause == false)
                {
                     TEXT_P_State[i].text = GetStateText(P_STATE[i]);
                     TEXT_E_State[i].text = GetStateText(E_STATE[i]);

                     P_MODE_UI[i].UIModeSwitch(P_MODE[i]);
                     E_MODE_UI[i].UIModeSwitch(E_MODE[i]);
                     
                }
            }
            else
            {
                //print("Text slot " + i + " is not assign");
            }
           
           Is_In_Game_End_Signal_Total[i] = Is_In_Game_End_Signal_Player[i] || Is_In_Game_End_Signal_Enemy[i] || Is_In_Game_End_Signal_Time[i]; 
        }
        
        
    }


    #endregion

    #region Method

        #region  Update UI State

        private string GetStateText(int state){
            switch (state)
                    {
                        case S_STILL: return "10010_STILL";
                        case S_RUN_FORWARD: return"10011_RUN_FORWARD"; 
                        case S_RUN_FOR_RIGHT: return"10012_RUN_FOR_RIGHT"; 
                        case S_RUN_RIGHT: return"10013_RUN_RIGHT"; 
                        case S_RUN_BAC_RIGHT: return"10014_RUN_BAC_RIGHT"; 
                        case S_RUN_BACKWARD: return"10015_RUN_BACKWARD"; 
                        case S_RUN_BAC_LEFT: return"10016_RUN_BAC_LEFT"; 
                        case S_RUN_LEFT: return"10017_RUN_LEFT"; 
                        case S_RUN_FOR_LEFT: return"10018_RUN_FOR_LEFT_"; 
                        case S_ATK: return"20000_ATK"; 
                        case S_ATK_BAL_N1: return"20011_A_BAL_N1"; 
                        case S_ATK_BAL_N2: return"20012_A_BAL_N2"; 
                        case S_ATK_BAL_N3: return"20013_A_BAL_N3"; 
                        case S_ATK_BAL_N4: return"20014_A_BAL_N4"; 
                        
                        case S_ATK_BAL_F1: return"20111_A_BAL_F1"; 
                        case S_ATK_BAL_F2: return"20112_A_BAL_F2"; 
                        case S_ATK_BAL_F3: return"20113_A_BAL_F3"; 
                        case S_ATK_BAL_F4: return"20114_A_BAL_F4"; 

                        case S_ATK_HEV_N1: return"22011_A_HEV_N1"; 
                        case S_ATK_HEV_N2: return"22012_A_HEV_N2"; 
                        case S_ATK_HEV_N3: return"22013_A_HEV_N3"; 

                        case S_ATK_HEV_F1: return"22111_A_HEV_F1"; 
                        case S_ATK_HEV_F2: return"22112_A_HEV_F2"; 
                        case S_ATK_HEV_F3: return"22113_A_HEV_F3"; 

                        case S_ATK_QUK_N1: return"24011_A_QUK_N1"; 
                        case S_ATK_QUK_N2: return"24012_A_QUK_N2"; 
                        case S_ATK_QUK_N3: return"24013_A_QUK_N3"; 
                        case S_ATK_QUK_N4: return"24014_A_QUK_N4"; 
                        case S_ATK_QUK_N5: return"24015_A_QUK_N5"; 

                        case S_ATK_QUK_F1: return"24111_A_QUK_F1"; 
                        case S_ATK_QUK_F2: return"24112_A_QUK_F2"; 
                        case S_ATK_QUK_F3: return"24113_A_QUK_F3"; 
                        case S_ATK_QUK_F4: return"24114_A_QUK_F4"; 
                        case S_ATK_QUK_F5: return"24115_A_QUK_F5"; 

                        case S_BLOCK: return"40010_BLOCK"; 
                        case S_BLOCK_SUCESS: return"40013_BLOCK_SUC"; 

                        case S_DODGE: return"40200_DODGE"; 
                        case S_DODGE_FORWARD: return"40201_DODGE_FORWARD"; 
                        case S_DODGE_FOR_RIGHT: return"40202_DODGE_FOR_RIGHT"; 
                        case S_DODGE_RIGHT: return"40203_DODGE_RIGHT"; 
                        case S_DODGE_BAC_RIGHT: return"40204_DODGE_BAC_RIGHT"; 
                        case S_DODGE_BACKWARD: return"40205_DODGE_BACKWARD"; 
                        case S_DODGE_BAC_LEFT: return"40206_DODGE_BAC_LEFT"; 
                        case S_DODGE_LEFT: return"40207_DODGE_LEFT"; 
                        case S_DODGE_FOR_LEFT: return"40208_DODGE_FOR_LEFT"; 

                        case S_CHAGE_DODGE: return"42200_CDODGE"; 
                        case S_CDODGE_FORWARD: return"42201_CDODGE_FOR"; 
                        case S_CDODGE_FOR_RIGHT: return"42202_CDODGE_FOR_RIGHT"; 
                        case S_CDODGE_RIGHT: return"42203_CDODGE_RIGHT"; 
                        case S_CDODGE_BAC_RIGHT: return"42204_CDODGE_BAC_RIGHT"; 
                        case S_CDODGE_BACKWARD: return"42205_CDODGE_BAC"; 
                        case S_CDODGE_BAC_LEFT: return"42206_CDODGE_BAC_LEFT"; 
                        case S_CDODGE_LEFT: return"42207_CDODGE_LEFT"; 
                        case S_CDODGE_FOR_LEFT: return"42208_CDODGE_FOR_LEFT"; 

                        case S_BACKDODGE: return"44201_BACKDODGE"; 

                        case S_JUMP: return"60_JUMP"; 
                        case S_HURT: return"90_HURT"; 
                        case S_LEVEL_UP: return"99_LEVEL_UP"; 


                        default: return "NULL";
                    }
        }
        
        #endregion

        #region Battle Controller Method

    bool check_Score_Once = false; 

    void ALL_DynamicScirpt(int i){
        enemyCharacterDynamicScript[i].DS_generateAiALL();
    }
    void ALL_SaveData(int i){

        playerCharacterDataWrite[i].UpdateData();
        enemyCharacterDataWrite[i].UpdateData();
        
        enemyCharacterAIWrite[i].UpdateData();
    }
    

    void Reset_Game(bool is_Dead, bool P_Dead , int i)
    {
        print("Reset Gmae = "+is_Dead);
        if (!check_Score_Once)
        {
            ALL_DynamicScirpt(i);
            ALL_SaveData(i);

            if (Score_mode[i] == 1)
            {
                Score_1_Attack_Last = Score_1_Attack;
            }
            else if (Score_mode[i] == 2)
            {
                Score_2_Dodge_Last = Score_2_Dodge;
                print("GAME RESET " + is_Dead + " " + P_Dead + " " + Time_limit);
            }
            if (E_Score_mode[i] == 1)
            {
                E_Score_1_Attack_Last = E_Score_1_Attack;
            }
            else if (E_Score_mode[i] == 2)
            {
                E_Score_2_Dodge_Last = E_Score_2_Dodge;
                print("GAME RESET " + is_Dead + " " + P_Dead + " " + Time_limit);
            }
            check_Score_Once = true;
        }
        
        if(GMode[i] == Mode_List.GM_Player_VS_Enemy)
        {
        }
        else if (GMode[i] == Mode_List.GM_P_Enemy_VS_Enemy){
        }

        //// Change Round Number UI /////
        
        RoundNumber[i] = enemyCharacterDataWrite[i].roundNumber;
        Round_Number_text[i].text =  "Round " + (RoundNumber[i]);

    }

    #endregion

        #region Static Method

    public static void Reset_Time( int code)
    {
        
        Time_limit[code] = Time_Limit_Set_d[code];
        
    }

  //  private static float[] d = 0;


    // Update is called once per frame
    //bool Is_Check_Reset_Game_Dead_Trigger[] = false;





    public static void Score_Load(int Sc,int mode, bool is_P_Enemy, int CharacterCode)
    {
        if (is_P_Enemy)
        {
            if (mode == 1)
            {
                Score_1_Attack_Last[CharacterCode] = Sc;
            }
            else if (mode == 2)
            {
                Score_2_Dodge_Last[CharacterCode] = Sc;
                //print("PPPPPPPPPPPLAST SCORE = " + Score_2_Dodge_Last);
            }
        }
        else
        {
            if (mode == 1)
            {
                E_Score_1_Attack_Last[CharacterCode] = Sc;
            }
            else if (mode == 2)
            {
                E_Score_2_Dodge_Last[CharacterCode] = Sc;
               // print("PPPPPPPPPPPLAST SCORE = " + E_Score_2_Dodge_Last);
            }
        }
        
    }

    int i;

    void FixedUpdate()
    {
        /*
        if (!Pause_Menu.GameIsPause)
            UpdateATB();
            */
    }


    public static void Reset_Round(int i)
    {
        if(game_Mode[i] == 2)
        {

        }
    }



    

    private void Collect_Data(int i)
    {
        if(game_Mode[i] == 2)
        {

        }
    }

   


    public static void AddEnemyCount()
    {
        TotalEnemy = TotalEnemy + 1;

    }

    public static void DecreaseEnemyCount()
    {
        TotalEnemy -= 1;
    }


    public static float Attack_Range_Check(int State)
    {
        float range_Check = 1.4f;
        switch (State)
        {
            //BALANCE
            case Battle_Controller.S_ATK_BAL_N1:
                range_Check *= 4f;
                break;
            case Battle_Controller.S_ATK_BAL_N2:
                range_Check *= 4f;
                break;
            case Battle_Controller.S_ATK_BAL_N3:
                range_Check *= 4.5f;
                break;
            case Battle_Controller.S_ATK_BAL_N4:
                range_Check *= 4f;
                break;


            case Battle_Controller.S_ATK_BAL_F1:
                range_Check *= 4.05f;
                break;
            case Battle_Controller.S_ATK_BAL_F2:
                range_Check *= 3.8f;
                break;
            case Battle_Controller.S_ATK_BAL_F3:
                range_Check *= 4f;
                break;
            case Battle_Controller.S_ATK_BAL_F4:
                range_Check *= 4.4f;
                break;

            //HEAVY
            case Battle_Controller.S_ATK_HEV_N1:
                range_Check *= 4.45f;
                break;
            case Battle_Controller.S_ATK_HEV_N2:
                range_Check *= 3.15f;
                break;
            case Battle_Controller.S_ATK_HEV_N3:
                range_Check *= 4.5f;
                break;

            case Battle_Controller.S_ATK_HEV_F1:
                range_Check *= 4.8f;
                break;
            case Battle_Controller.S_ATK_HEV_F2:
                range_Check *= 4f;
                break;
            case Battle_Controller.S_ATK_HEV_F3:
                range_Check *= 5.2f;
                break;

            //Quick 
            case Battle_Controller.S_ATK_QUK_N1:
                range_Check *= 3.05f;
                break;
            case Battle_Controller.S_ATK_QUK_N2:
                range_Check *= 2.75f;
                break;
            case Battle_Controller.S_ATK_QUK_N3:
                range_Check *= 2.75f;
                break;
            case Battle_Controller.S_ATK_QUK_N4:
                range_Check *= 3.05f;
                break;
            case Battle_Controller.S_ATK_QUK_N5:
                range_Check *= 3.05f;
                break;

            case Battle_Controller.S_ATK_QUK_F1:
                range_Check *= 2.25f;
                break;
            case Battle_Controller.S_ATK_QUK_F2:
                range_Check *= 2.6f;
                break;
            case Battle_Controller.S_ATK_QUK_F3:
                range_Check *= 2.3f;
                break;
            case Battle_Controller.S_ATK_QUK_F4:
                range_Check *= 3f;
                break;
            case Battle_Controller.S_ATK_QUK_F5:
                range_Check *= 4.2f;
                break;



            case Battle_Controller.S_ATK_BASIC_CLOSE:
                range_Check *= 2.5f;
                break;
            case Battle_Controller.S_ATK_BASIC_MID:
                range_Check *= 4f;
                break;
            case Battle_Controller.S_ATK_BASIC_CONE:
                range_Check *= 4.5f;
                break;
            case Battle_Controller.S_ATK_PRE_CLOSE:
                range_Check *= 3.2f;
                break;
            case Battle_Controller.S_ATK_PRE_FAR:
                range_Check *= 5f;
                break;
            case Battle_Controller.S_ATK_FIN_CLOSE:
                range_Check *= 4f;
                break;
            case Battle_Controller.S_ATK_FIN_FAR:
                range_Check *= 7.8f;
                break;

            default:
                return 0;
                break;


        }
        
        return range_Check;
    }

    public static float Return_Scaled_Value(int Val, float Start, float[] value_Mul, float[] value_Limit)
    {
        float Answer = Start;
        int Current_Score = 0;
        int step = value_Mul.Length;
        if (step == 0)
        {
            Answer = Start;
        }
        else if (step > 0)
        {
            for(int i = 0; i < step; i++)
            {
                if (value_Limit[i] >= Val)
                {
                    Answer += ((value_Mul[i]- ( (i==0)?0:value_Mul[i-1])   ) 
                        * ((float)Val - Current_Score) / (value_Limit[i]-Current_Score ));
                    Current_Score = Val;
                   // print("Last V = " + (value_Mul[i] - ((i == 0) ? 0 : value_Mul[i - 1])));
                    break;
                    
                }
                
                else
                {
                    Answer = value_Mul[i];
                    Current_Score = (int)value_Limit[i];
                }
                print("Answer = " + Answer + "     Current = " + Current_Score);
            }
        }
        else Answer = Start;
        return Answer;
    }
    public static float Return_Fixed_Value(int Val, float Start, float[] value_Mul, float[] value_Limit)
    {
        float Answer = Start;
        int Current_Score = 0;
        int step = value_Mul.Length;
        if (step == 0)
        {
            Answer = Start;
        }
        else if (step > 0)
        {
            for (int i = 0; i < step-1; i++)
            {
                if(value_Limit[i] > Val)
                {
                    if (i == 0) Answer = Start;
                    else Answer = value_Mul[i];
                    break;
                }
        
            }
        }
        else Answer = Start;
        return Answer;
    }


    private static float[] Insert_OneVal_WholeArray_Float(int size, float val)
    {
        float[] A = new float[size + 1];
            for (int i = 0; i < A.Length; i++)
                A[i] = val;
        return A;
    }
    private static int[] Insert_OneVal_WholeArray_Int(int size, int val)
    {
        int[] A = new int[size+1];
            for (int i = 0; i < A.Length; i++)
                A[i] = val;
        return A;
    }
    private static bool[] Insert_OneVal_WholeArray_Bool(int size, bool val)
    {
        bool[] A = new bool[size + 1];
        for (int i = 0; i < A.Length; i++)
            A[i] = val;
        return A;
    }
    private static Transform[] Insert_OneVal_WholeArray_Transform(int size, Transform val)
    {
        Transform[] A = new Transform[size+1];
        for (int i = 0; i < A.Length; i++)
            A[i] = val;
        return A;
    }
    public static int Check_GameMode_FromCharacter (int CharacterCode)
    {
        int game_Mode = 0;
        for(int j = 1; j <= 2; j++)
        {
            for (int i = 0; i < SLOT_NUMBER; i++)
            {
                if (Pair_Test_List_Static[i, j] == CharacterCode)
                {
                    game_Mode = Pair_Test_List_Static[i, 0];
                    return game_Mode;
                    break;
                }
                    
                
            }
        }
        return game_Mode;

    }
    public static int Check_Opponent_Character(int CharacterCode)
    {
        int Opponent = 0;
        for (int j = 1; j <= 2; j++)
        {
            for (int i = 0; i < SLOT_NUMBER; i++)
            {
                if (Pair_Test_List_Static[i, j] == CharacterCode)
                {
                    if (j == 1)
                        Opponent = Pair_Test_List_Static[i, 2];
                    else if (j == 2)
                        Opponent = Pair_Test_List_Static[i, 1];
                    return Opponent;
                    break;
                }


            }
        }
        return Opponent;

    }

    #endregion

    #endregion

    #region Unused from LR Project


    #endregion

}
