using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_1 : MonoBehaviour {

    #region Variable

    //=============== General/Important Var ==========
    public int CharacterCode = 0;               // Code of this single character, to differentate each character with the same scipt
    public bool Is_Player_or_Enemy;
    public bool Is_P_Enemy;
    public Animator animator;                   // Animator
                      
    Vector3 inputVec;                           // L Analog Vector that input in to character
    Vector3 targetDirection;                
    public Character_Stat characterStat;               // Class dictate character stat andcharacterData number (Need fixing to character stat)
    public Character_1_HitBox character_hitbox;         // Class control character hitbox (Need fixing to character hitbox)
    public Character_1_Data characterData;
    //---------------Visual / Audio ------------------
    //public SoundEffect soundEffect;

    //--------------- General/Important Var ----------

    //================ Character Controller ==========   // Used for character control in animation, etc.
    private CharacterController controller;     // Unity Default Character contorller
    private Vector3 Speed_Set1;                 // Speed moving forward during attack animation
    private Vector3 Speed_Set1_New;             // Another Speed moving forward during attack animation
    private Vector3 Speed_Set2;                 // Another Speed moving forward during attack animation
    private Vector3 In_action_Vertical_Speed;   // Yet to be used
                                                //---------------- Character Controller -----------


    //========================================Gravity / Jumping==========================================
    //=============Constant===============
    float rotationSpeed = 600;                   // Rotation Speed in general
    private float gravity = 29.0f;              // Falling Accelaration 
    private float jumpForce = 13.0f;            // Jumping Up Accelaration
    public float jump_Horizontal = 6.0f;        // Movement speed
    public int Max_Midair_Jump = 1;             // Number of Jump Possible
    //============ Dynamic Variable ======
    public int Midair_Jump_Count = 0;           // Number of jump already performated in midair
    private float verticalVelocity;             // Current Vertical Velocity
    //----------------------------------------Gravity / Jumping-------------------------------------------

    // ======================================== Attack ==========================================
    //=============Constant===============

    //============ Dynamic Variable ======
    public int Attackcode = 0;                  // Code of current attack
    public int Last_Attack_Code = 0;            // Code of previous attack
    public bool AttackDodgeCheck = false;
    public int Combo_Count = 0;                 // Number of combo count character currently done
    int checkTick = 0;                          // Used in Combo (Don't know what I was thinking before though)
    // ---------------------------------------- Attack ------------------------------------------

    // ======================================== Knockback/Hurt ==========================================
    //=============Constant===============

    //============ Dynamic Variable ======
    private float currentKnockback_Multi = 1f;   // Multiply of the knockback, can change during animation
    // ---------------------------------------- Knockback/Hurt ------------------------------------------

    // ================ Game World Object ===============
    public SmoothFollow CAMERA;                 // Camera object
    public Transform camera;                    // Position of camera
    public Transform Enemy;                     // Position of Enemy
    public Vector3 Compare_to_ENEMY;            // Vector between opponent character (Might neeed to change the name)
    private float Current_Distance;             // Current distance between character and opponent
    public Vector3 startingPos;               // Starting Position of camera
    // ---------------- Game World Object----------------

    // ======================================== For Data ==========================================
    //=============Constant===============

    //============ Dynamic Variable ======
    private Vector3 Last_Position_Before_Dodge;     // Position of this character before dodging
    private int Last_Dodge_Dir_Code;                // 1 off 2 off-side 3 side 4 def-side 5 def
    // ---------------------------------------- For Data ------------------------------------------


    //Warrior types
    public enum Warrior{Karate, Ninja, Brute, Sorceress, Knight, Mage, Archer, TwoHanded, Swordsman, Spearman, Hammer, Crossbow};
	public Warrior warrior;
                              


    //================Key Map Settings===============
    readonly public KeyCode LKey = KeyCode.Q;
    readonly public KeyCode LKey2 = KeyCode.U;
    readonly public KeyCode LKey3 = KeyCode.Keypad7;
    readonly public KeyCode RKey = KeyCode.E;
    readonly public KeyCode RKey2 = KeyCode.O;
    readonly public KeyCode RKey3 = KeyCode.Keypad9;
    readonly public KeyCode LKey4 = KeyCode.Joystick1Button6;
    readonly public KeyCode RKey4 = KeyCode.Joystick1Button7;

    readonly public KeyCode TriKey = KeyCode.I;
    readonly public KeyCode SqKey = KeyCode.J;
    readonly public KeyCode XKey = KeyCode.K;
    readonly public KeyCode CirKey = KeyCode.L;

    readonly public KeyCode TriKey2 = KeyCode.Keypad8;
    readonly public KeyCode SqKey2 = KeyCode.Keypad4;
    readonly public KeyCode XKey2 = KeyCode.Keypad5;
    readonly public KeyCode CirKey2 = KeyCode.Keypad6;

    readonly public KeyCode TriKey3 = KeyCode.Joystick1Button3;
    readonly public KeyCode SqKey3 = KeyCode.Joystick1Button0;
    readonly public KeyCode XKey3 = KeyCode.Joystick1Button1;
    readonly public KeyCode CirKey3 = KeyCode.Joystick1Button2;

    readonly public KeyCode DEBUG_HurtKey1 = KeyCode.Keypad1;
    readonly public KeyCode DEBUG_HurtKey2 = KeyCode.Keypad2;
    readonly public KeyCode DEBUG_HurtKey3 = KeyCode.Keypad3;
    //------------------Key Map Settings---------------------

    //============================Character Mode=========================
    public enum Character_Mode_List
    {
        Balance_Mode,
        Heavy_Mode,
        Quick_Mode

    }
    private Character_Mode_List Current_Mode;
    public GameObject Sword;
    public GameObject Shield;
    public GameObject BigSword;
    public Text Current_Mode_Text;
    //----------------------------Character Mode-------------------------


    //====================State List==========================//
    public bool Is_Performing_Action = false;          // Used
    private bool Is_Hold_L_Analog = false;              // Used
    private bool Is_InCombo = false;
    private bool Is_Hitting_FirstHitofCombo = false;
    public bool Is_ComboAble = false;                  // Used
    public bool Is_ComboAble_CheckFromActionCalled = false;                  // Used
    private bool Is_ComboAble_Real = false;
    private bool Is_Excepted_Combo_Button = false;      // Used
    private bool Is_Dodge_Cancel_Able = false;          // Used
    private bool Is_Attack_Cancel_Able = false;          // Used
    private bool Is_Excepted_Dodge_Cancel_Button = false;
    private bool Is_Block_Cancel_Able = false;          // Used
    private bool Is_Excepted_Block_Cancel_Button = false;
    private bool Is_Excepted_Jump_Cancel_Button = false;
    private bool Attack_Next = false;                   // Used
    private bool Attack_Next2 = false;                   // Used
    private bool Dodge_Next = false;
    private bool BackStep_Next = false;
    private bool ChargeDodge_Next = false;
    private bool Block_Next = false;
    private bool Jump_Next = false;
    private bool Teleport_Next = false;
    private int Teleport_Position_Code = 0;
    private bool Hold_Combo_Count = false;
    private bool Is_Attack = false;
    private bool Is_Block = false;                      // Used
    private bool Is_BlockFrameAcive = false;                      // Used
    private bool Is_Block_Successful = false;
    private int Is_Block_SuccessfulCountDecay = 0;
    private bool Is_Dodge = false;                      // Used
    private bool Is_ChargeDodge = false;
    private bool Is_Teleport = false;
    private bool Is_Invinciable = false;
    private bool Is_SuperArmor = false;
    private int SuperArmorLevel = 0;
    private bool Is_Landing = false;
    private bool Is_Can_Jump = true;
    private bool Is_Can_Jump_Signal = false;
    private bool Is_Hurt = false;
    private bool Is_Hurt_Activate = false;
    private bool Is_Hurt_was = false;
    private bool Is_Disable_Control = false;
    private bool Is_Aerial_REAL = false;
    private bool Is_KeepFacingOppo = false;
    private bool Is_HitBoxActivate = false;

    private bool Is_Level_Up = false;                   // Used in junction to each other
    private bool Is_In_Level_Up = false;                 // Used in junction to each other
    //--------------------State List---------------------------//




    public float Action_Count = -1.5f;
    public float TIME;                  // Local Time use for other time var
    public static float PlayTime = 0;   // PlayTime in one round
    public static float RunTime = 0;   // RunTime in one round
    private float wait = 0;             // Wait time. add to this to make character wait for X seconds
    private float MoveCount;            // Move time. add to this to make character move for X seconds (Still not used)      
    private int MoveCode = 1;           // Move code for MoveCount. 1-8 for directional
    bool checkEnter = false;            // DON'T KNOW
    bool checkCombo_Able = false;       // DON'T KNOW

   

    private float Currennt_Avg_Distance = 0;        // Current Average Distance 
    private int Currennt_Avg_Distance_Count = 1;    // Used for calculating Currennt_Avg_Distance

    public int State_P;                        // State of My character
    public int State_E;                        // State of Opponent character

    float z;                            // Horizontal Input (Positive)
    float x;                            // Vertical Input (Negative)

    /// <summary>
    /// ////////////////////////////////////////////
    /// </summary>
        #region Unused Data Part

    public float DATA_RemainHP_Percent = 0;
    public float DATA_PlayTime = 0;
    public float DATA_RunTime = 0;
    public float DATA_RunTime_Percent = 0;
     public float DATA_Average_Distance_Between = 0;
    

    private int Last_Dodge_Code = 0;
    public int Last_Attack_Code_Hit_Check = 0;

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

    public float Dodge_Check_Countdown = 1f;
    public float Dodge_Score_Time_Weight = 0.6f;
    #endregion
    /// <summary>
    /// /////////////////////////////////////////////
    /// </summary>
    #endregion

    #region Start
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator.SetBool("10001_Idle_BalanceMode", true);
        startingPos = this.transform.position;
    }
    #endregion

    #region Update

    void Update()
    {
        #region Initial Check State Part

        ///////////////////////////////////////////////////////////////////////// State Check Path //////////////////////////////////
        State_E = Battle_Controller.E_STATE[CharacterCode];
        State_P = Battle_Controller.P_STATE[CharacterCode];
        if (Battle_Controller.game_Mode[CharacterCode] == 1)
            //Score_Update();
        ///////////////////////////////////////////////////////////////////////// State Check Path //////////////////////////////////

        #endregion

        #region AI Part

        //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ AI Part $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

        Update_State();

    
       

      
        ///////////////////////////////////////////////////////////////////// Round End Part /////////////////////////////////////////////////////////////////////////
        if(
            /*
        (Battle_Controller.Is_In_Game_End_Signal_Player[CharacterCode])
        ||(Battle_Controller.Is_In_Game_End_Signal_Enemy[CharacterCode])
        ||(Battle_Controller.Is_In_Game_End_Signal_Time[CharacterCode])
        */
        Battle_Controller.Is_In_Game_End_Signal_Total[CharacterCode] 
        ) 
        {
            DATA_RemainHP_Percent = characterStat.GetCurrentHPPercentage();
            Is_In_Level_Up = true;
            /// Player Case
            if(Is_Player_or_Enemy) {
                 print("Game_End_Signal_PlayerDead = " + Battle_Controller.Game_End_Signal_PlayerDead[CharacterCode]);
                if(Battle_Controller.Is_In_Game_End_Signal_Player[CharacterCode]){
                     //animator.SetTrigger("Level_Up_Dead");
                     Select_Attack_Animation(1);
                }

                else if(Battle_Controller.Is_In_Game_End_Signal_Enemy[CharacterCode] || Battle_Controller.Is_In_Game_End_Signal_Time[CharacterCode]){
                    //animator.SetTrigger("Level_Up_Alive");
                    Select_Attack_Animation(0);
                }
                     

            }
            /// Enemy Case
            else {
                  print("Game_End_Signal_EnemyDead = " + Battle_Controller.Game_End_Signal_EnemyDead[CharacterCode]);
                if(Battle_Controller.Is_In_Game_End_Signal_Enemy[CharacterCode]){
                    print("Enemy ANimation is set to dead");
                    //animator.SetTrigger("Level_Up_Dead");
                    Select_Attack_Animation(1);
                                    Battle_Controller.Game_End_Signal_EnemyDead[CharacterCode] = false;
                Battle_Controller.Is_In_Game_End_Signal_Enemy[CharacterCode] = false;
                }
                     
                else if(Battle_Controller.Is_In_Game_End_Signal_Player[CharacterCode] || Battle_Controller.Is_In_Game_End_Signal_Time[CharacterCode]){
                     //animator.SetTrigger("Level_Up_Alive");
                     Select_Attack_Animation(0);
                                     Battle_Controller.Game_End_Signal_PlayerDead[CharacterCode] = false;
                Battle_Controller.Is_In_Game_End_Signal_Player[CharacterCode] = false;
                }
                     
                  print("Is Enemy");

            }
            print(("  Player : ") + "What");
            //if (!(Battle_Controller.Game_End_Signal_EnemyDead[CharacterCode] || Battle_Controller.Game_End_Signal_PlayerDead[CharacterCode]))
            Battle_Controller.Game_End_Signal_Time[CharacterCode] = false;
            Battle_Controller.Is_In_Game_End_Signal_Time[CharacterCode] = false;
            //audioSource.PlayOneShot(Sound_list[12]);
            Is_Disable_Control = true;
        }

        if( (characterStat.Cur_HP_Per <= 0 && !Is_In_Level_Up) ){
             print("HEREDDDD");
            Is_In_Level_Up = true;
             if(Is_Player_or_Enemy) {
                Battle_Controller.Game_End_Signal_PlayerDead[CharacterCode] = true;
            }
            else {
                Battle_Controller.Game_End_Signal_EnemyDead[CharacterCode] = true;
                print("HERE");
                print("Game_End_Signal_EnemyDead = " + Battle_Controller.Game_End_Signal_EnemyDead[CharacterCode]);
            }
        }

        /*if (characterStat.Cur_HP_Per <= 0 && !Is_In_Level_Up)
        {
            //Stat.SetCurrentHP(Stat.GetHP());
            Is_In_Level_Up = true;
            print(( "  Player : ") + "What");
            animator.SetTrigger("Level_Up");
            //audioSource.PlayOneShot(Sound_list[12]);
            Is_Disable_Control = true;
        }*/

        if (Is_Level_Up || Is_In_Level_Up) Is_Disable_Control = true;
        else Is_Disable_Control = false;
        if (Is_Disable_Control) animator.SetBool("Is_Level_Up", true);
        else animator.SetBool("Is_Level_Up", false);


        //-------------------------------- DATA + Heal Part ---------------------------------------------------------
        #region TIME/Distance

        Compare_to_ENEMY = transform.position - Enemy.position;

        Compare_to_ENEMY.y = 0;
        Current_Distance = Compare_to_ENEMY.magnitude;

        TIME = Time.deltaTime;
        PlayTime += TIME;
        characterData.DATA_PlayTime = PlayTime;
        MoveCount -= TIME;


        if (!(Is_Level_Up || Is_In_Level_Up)) DATA_RemainHP_Percent = characterStat.GetCurrentHPPercentage();

        Countdown_Check(1);

       // print("CHECKKKKK  " + Last_Attack_Code_Hit_Check);
        if (Is_KeepFacingOppo)
        {
            face_opponents();
        }

        //print("Last_Attack_Code_Hit_Check = " + Last_Attack_Code_Hit_Check);

        //if (Last_Attack_Code_Hit_Check != Combo_Count) Last_Attack_Code_Hit_Check = Combo_Count;

        if (Last_Attack_Code_Hit_Check > 0)
        {
           // print("Loop  " + Last_Attack_Code_Hit_Check);
            Attack_Hit_Check(Last_Attack_Code_Hit_Check);

            Last_Attack_Code_Hit_Check = 0;

            //Combo Store Part




        }
        
        if (PlayTime > Currennt_Avg_Distance_Count)
        {

            Currennt_Avg_Distance = (Currennt_Avg_Distance * (((float)Currennt_Avg_Distance_Count - 1) / (float)Currennt_Avg_Distance_Count)) + (Compare_to_ENEMY.magnitude / (float)Currennt_Avg_Distance_Count);
            //print("CAL = " + Compare_to_PLAYER.magnitude + "     " + (Currennt_Avg_Distance_Count - 1) / Currennt_Avg_Distance_Count + "     " + Currennt_Avg_Distance);
            Currennt_Avg_Distance_Count++;
            DATA_Average_Distance_Between = Currennt_Avg_Distance;
        }
        if (characterStat.Is_Reset)
        {
            characterStat.Is_Reset = false;
        }
        #endregion




        #endregion

        #region Check Character State Part
        //++++++++++++++++++++++++++++ Check If Action ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//


        Is_Performing_Action = Is_Attack || Is_Block || Is_Dodge || Is_Hurt || Is_Teleport || Is_ChargeDodge;

       // print(Is_Attack);

        if (Is_Hurt_Activate == true && Is_Hurt == true)
        {
            Is_Hurt_was = true;
            //Countdown_Check(false);
        }
        else if (Is_Hurt == false && Is_Hurt_was == true)
        {
            Is_Hurt_Activate = false;
            Is_Hurt_was = false;
        }

        if (Is_ComboAble == false)
        {
            Is_ComboAble_CheckFromActionCalled = false;
        }
        Is_ComboAble_Real = Is_ComboAble && !Is_ComboAble_CheckFromActionCalled;

        //print(Is_Hurt + " " + Is_Hurt_was + " " + Is_Hurt_Activate);

        //if(!Is_Performing_Action || Is_Can_Jump_Signal)
        //{
        //    Is_Can_Jump = true;
        //   Is_Can_Jump_Signal = false;
        //}    
        //else
        //    Is_Can_Jump = false;

        animator.SetBool("Is_Perfprming_Action", Is_Performing_Action);


        // Is_Performing_Action = 
        //animator.GetCurrentAnimatorStateInfo(0).IsTag("A_Attack") 
        //                   || animator.GetCurrentAnimatorStateInfo(0).IsTag("A_Attack_Fin") 
        //                    || animator.GetCurrentAnimatorStateInfo(0).IsTag("A_Dodge") 



        ;
        /*Is_InCombo = animator.GetCurrentAnimatorStateInfo(0).IsTag("A_Attack")
                                || Attack_Next
                                 ; */
        //------------------------------------------------------------------------------------------------------------------------------//

        #endregion

        

        #region Movement Part
        //++++++++++++++++++++++++++++ Moving ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
        //Get input from controls
        if (Is_Player_or_Enemy && (!Is_P_Enemy))
        {

        
        z = Input.GetAxisRaw("Horizontal");
        x = -(Input.GetAxisRaw("Vertical"));
        }
        inputVec = new Vector3(x, 0, z);

        //Apply inputs to animator
        if (Is_Disable_Control || Is_Block)
        {
            x = 0;
            z = 0;
        }
        else
        {
            //SetMovement(x, z);
            animator.SetFloat("Input X", z);
            animator.SetFloat("Input Z", -(x));
        }


        if ((x != 0 || z != 0))  //if there is some input
        {
            //set that character is moving
            Is_Hold_L_Analog = true;
            if (!Is_Performing_Action){
                animator.SetBool("Moving", true);
                RunTime += TIME;
                characterData.DATA_RunTime = RunTime;
            }
        }
        else
        {
            //character is not moving
            Is_Hold_L_Analog = false;
            // if (!Is_Performing_Action)
            animator.SetBool("Moving", false);
        }
        //------------------------------------------------------------------------------------------------------------------------------//
        #endregion

        #region Performing Action
        //+++++++++++++++++++++++++++++++++++ Perform Action +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
        if (!Is_Performing_Action)
        {
            Reset_Animation_Speed();
            if (checkTick <= 5 && Combo_Count == 1)
            {
                Is_InCombo = true;        
                checkTick++;
            }
            else
            {
                Is_InCombo = false;
               Combo_Count = 0;
            }
            Is_Invinciable = false;
            Is_SuperArmor = false;
            Is_ComboAble = false;
            Is_Excepted_Combo_Button = false;
            Is_Dodge_Cancel_Able = false;
            Is_Block_Cancel_Able = false;
            Hold_Combo_Count = false;


        }
        else checkTick = 0;
        #endregion

        #region Player Input Part

        if (!Is_Disable_Control && Is_Player_or_Enemy && !Is_Hurt && (!Is_P_Enemy))
        {
            if ((Input.GetKeyDown(CirKey) || Input.GetKeyDown(CirKey2) || Input.GetKeyDown(CirKey3)) && (Is_Excepted_Combo_Button || !Is_Performing_Action))
            {
                Attack_Next = true;

                AttackDodgeCheck = true;
            }
            else if ((Input.GetKeyDown(TriKey) || Input.GetKeyDown(TriKey2) || Input.GetKeyDown(TriKey3)) && (Is_Excepted_Combo_Button || !Is_Performing_Action))
            {
                Attack_Next2 = true;
            }
            if ((Input.GetKeyDown(SqKey) || Input.GetKeyDown(SqKey2) || Input.GetKeyDown(SqKey3)) && !Is_Hold_L_Analog
                    && (Is_Excepted_Block_Cancel_Button || !Is_Performing_Action)
                    
                    )
            {
                //print("TRigger");
                if (Current_Mode == Character_Mode_List.Balance_Mode || Current_Mode == Character_Mode_List.Heavy_Mode) Block_Next = true;
                else Block_Next = true; //BackStep_Next = true;
            }
            if ((Input.GetKeyDown(SqKey) || Input.GetKeyDown(SqKey2) || Input.GetKeyDown(SqKey3)) && Is_Hold_L_Analog
                    && (Is_Excepted_Dodge_Cancel_Button || !Is_Performing_Action) )
                    
            {
                if (Current_Mode == Character_Mode_List.Balance_Mode || Current_Mode == Character_Mode_List.Quick_Mode) Dodge_Next = true;
                else Dodge_Next = true;// ChargeDodge_Next = true;
                

                
            }

            /*
            if ((Input.GetKeyDown(XKey) || Input.GetKeyDown(XKey2) || Input.GetKeyDown(XKey3))  
                    && (Is_Excepted_Jump_Cancel_Button || !Is_Performing_Action ))
            {
                //Jump_Next = true;
                Teleport_Next = true;
                Teleport_Position_Code = MoveXYtoCode(x,z);
            }
            */
            if ((Input.GetKeyDown(LKey)) || Input.GetKeyDown(LKey2) || Input.GetKeyDown(LKey3)|| Input.GetKeyDown(LKey4)){
                Change_Mode_Input(true);
            }
            if ((Input.GetKeyDown(RKey)) || (Input.GetKeyDown(RKey2)) || Input.GetKeyDown(RKey3)|| Input.GetKeyDown(RKey4)) 
            {
                Change_Mode_Input(false);
            }
            if ((Input.GetKeyDown(DEBUG_HurtKey1)))
            {
                Activate_Hurt(1);
            }
            else if ((Input.GetKeyDown(DEBUG_HurtKey2)))
            {
                Activate_Hurt(2);
            }
            else if ((Input.GetKeyDown(DEBUG_HurtKey3)))
            {
                Activate_Hurt(3);
            }
        }

        #endregion

        #region Attacking

        if (((Attack_Next || Attack_Next2) && (Is_ComboAble || !Is_Performing_Action) && !animator.GetCurrentAnimatorStateInfo(0).IsTag("A_Attack_Fin")))
        {
            //////////////////// AI Check Part ///////////////////

            //////////////////// AI Check Part ///////////////////
            Movement_All_Reset();
           // print("Attack");
            character_hitbox.Hit_Disable = false;
            Is_Hurt = false;
            Is_Invinciable = false;
            Is_SuperArmor = false;
            character_hitbox.setHitBox(Character_1_HitBox.hitBoxes.clear);
            Set_Is_Excepted_All_BlockDodgeCombo_Button(0);

            Is_Performing_Action = true;
            Is_ComboAble = false;

            // if (CAMERA.Lock_On) transform.LookAt(new Vector3(CAMERA.Lock_on_Target.position.x, transform.position.y, CAMERA.Lock_on_Target.position.z));

            face_opponents();//if (CAMERA.Lock_On) transform.LookAt(new Vector3(CAMERA.Lock_on_Target.position.x, transform.position.y, CAMERA.Lock_on_Target.position.z));

            /*
            switch (Attackcode)
            {
                case 101:
                    DATA_Total_ATK_Mid_Execute++;
                    break;
                case 103:
                    DATA_Total_ATK_Close_Execute++;
                    break;
                case 108:
                    DATA_Total_ATK_Cone_Execute++;
                    break;

                case 201:
                    DATA_Total_PATK_Close_Execute++;
                    break;
                case 203:
                    DATA_Total_PATK_Far_Execute++;
                    break;

                case 301:
                    DATA_Total_FIN_Close_Execute++;
                    break;
                case 302:
                    DATA_Total_FIN_Far_Execute++;
                    break;
            }
            */
            Combo_Count += 1;
            if (Combo_Count == 1)
            {
                print("Is_Hitting_FirstHitofCombo = true");
                Is_Hitting_FirstHitofCombo = true;
            }
            
            Is_Excepted_Combo_Button = false;

            #region New Combo System
            
            if (Attack_Next || Attack_Next2)
            {
                checkTick = 0;
                //print("ATTACKKKKKK");
                //print("Direction = " + x);
                if (Current_Mode == Character_Mode_List.Balance_Mode)
                {
                    //if(x == 0) // Neutral Attack
                    if(Attack_Next)
                    {
                        
                        switch (Combo_Count)
                        {
                            
                            case 1:
                                Attackcode = 20011;
                                Select_Attack_Animation(20011);
                                break;
                            case 2:
                                Attackcode = 20012;
                                Select_Attack_Animation(20012);
                                break;
                            case 3:
                                Attackcode = 20013;
                                Select_Attack_Animation(20013);
                                break;
                            case 4:
                                Attackcode = 20014;
                                Combo_Count = 99;
                                Select_Attack_Animation(20014);
                                break;
                            case 5:
                                Attackcode = 20014;
                                Combo_Count = 99;
                                Select_Attack_Animation(20014);
                                break;
                        }
                    }
                    //if (x < 0) // Forward Attack
                    if (Attack_Next2)
                    {
                        switch (Combo_Count)
                        {
                            case 1:
                                Attackcode = 20111;
                                Select_Attack_Animation(20111);
                                break;
                            case 2:
                                Attackcode = 20112;
                                Select_Attack_Animation(20112);
                                break;
                            case 3:
                                Attackcode = 20113;
                                Select_Attack_Animation(20113);
                                break;
                            case 4:
                                Attackcode = 20114;
                                Combo_Count = 99;
                                Select_Attack_Animation(20114);
                                break;
                            case 5:
                                Attackcode = 20114;
                                Combo_Count = 99;
                                Select_Attack_Animation(20114);
                                break;
                        }
                    }

                }
                if (Current_Mode == Character_Mode_List.Heavy_Mode)
                {
                    //if (x == 0) // Neutral Attack
                    if (Attack_Next)
                    {
                        print("Combo Count = " + Combo_Count);
                        switch (Combo_Count)
                        {

                            case 1:
                                Attackcode = 22011;
                                Select_Attack_Animation(22011);
                                break;
                            case 2:
                                Attackcode = 22012;
                                Select_Attack_Animation(22012);
                                break;
                            case 3:
                                Attackcode = 22013;
                                Combo_Count = 99;
                                Select_Attack_Animation(22013);
                                break;
                            case 4:
                                Attackcode = 22013;
                                Combo_Count = 99;
                                Select_Attack_Animation(22013);
                                break;
                            case 5:
                                Attackcode = 22013;
                                Combo_Count = 99;
                                Select_Attack_Animation(22013);
                                break;
                        }
                    }
                    //if (x < 0) // Forward Attack
                    if (Attack_Next2)
                    {
                        switch (Combo_Count)
                        {
                            case 1:
                                Attackcode = 22111;
                                Select_Attack_Animation(22111);
                                break;
                            case 2:
                                Attackcode = 22112;
                                Select_Attack_Animation(22112);
                                break;
                            case 3:
                                Attackcode = 22113;
                                Combo_Count = 99;
                                Select_Attack_Animation(22113);
                                break;
                            case 4:
                                Attackcode = 22113;
                                Combo_Count = 99;
                                Select_Attack_Animation(22113);
                                break;
                            case 5:
                                Attackcode = 22113;
                                Combo_Count = 99;
                                Select_Attack_Animation(22113);
                                break;
                        }
                    }

                }
                if (Current_Mode == Character_Mode_List.Quick_Mode)
                {
                    //if (x == 0) // Neutral Attack
                    if (Attack_Next)
                    {
                        print("Combo Count = " + Combo_Count);
                        switch (Combo_Count)
                        {

                            case 1:
                                Attackcode = 24011;
                                Select_Attack_Animation(24011);
                                break;
                            case 2:
                                Attackcode = 24012;
                                Select_Attack_Animation(24012);
                                break;
                            case 3:
                                Attackcode = 24013;
                                Select_Attack_Animation(24013);
                                break;
                            case 4:
                                Attackcode = 24014;
                                Select_Attack_Animation(24014);
                                break;
                            case 5:
                                Attackcode = 24015;
                                Combo_Count = 99;
                                Select_Attack_Animation(24015);
                                break;
                        }
                    }
                    //if (x < 0) // Forward Attack
                    if (Attack_Next2)
                    {
                        switch (Combo_Count)
                        {
                            case 1:
                                Attackcode = 24111;
                                Select_Attack_Animation(24111);
                                break;
                            case 2:
                                Attackcode = 24112;
                                Select_Attack_Animation(24112);
                                break;
                            case 3:
                                Attackcode = 24113;
                                Select_Attack_Animation(24113);
                                break;
                            case 4:
                                Attackcode = 24114;
                                Select_Attack_Animation(24114);
                                break;
                            case 5:
                                Attackcode = 24115;
                                Combo_Count = 99;
                                Select_Attack_Animation(24115);
                                break;
                        }
                    }

                }


                //Select_Attack_Animation(101);
            }
            

            #endregion

            face_opponents();//if (CAMERA.Lock_On) transform.LookAt(new Vector3(CAMERA.Lock_on_Target.position.x, transform.position.y, CAMERA.Lock_on_Target.position.z));
            Attack_Next = false;
            Attack_Next2 = false;
            Set_All_A_B_D_J_Next_Button(0);

            Last_Attack_Code = Attackcode;

            //print(Combo_Count);
            //if (Combo_Count >= 4) Combo_Count = 1;
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("A_Attack_Fin")) Combo_Count = 0;

        }

        #endregion

        #region Blocking

        // +++++++++++++++++++++++++++++++Block ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
        if (Block_Next
                && (!Is_Performing_Action || Is_Block_Cancel_Able))
        {
            Movement_All_Reset();
            Is_Hurt = false;
            Is_Invinciable = false;
            Is_SuperArmor = false;
            character_hitbox.setHitBox(Character_1_HitBox.hitBoxes.clear);
            Set_Is_Excepted_All_BlockDodgeCombo_Button(0);
            Set_All_A_B_D_J_Next_Button(0);
            Block_Next = false;
            Reset_Animation_Speed();
            Is_Block_Cancel_Able = false;
            Is_ComboAble = false;
            Attack_Next = false; Attack_Next2 = false;
            face_opponents();//if (CAMERA.Lock_On) transform.LookAt(new Vector3(CAMERA.Lock_on_Target.position.x, transform.position.y, CAMERA.Lock_on_Target.position.z));
            characterStat.SetCurrentCommand(31);
            //print("Sucess Some howfdfasdf");
            Select_Attack_Animation(40010);
            //animator.SetTrigger("40010_Block_Start");
            if (warrior == Warrior.Brute)
                StartCoroutine(COStunPause(1.2f));
            else if (warrior == Warrior.Sorceress)
                StartCoroutine(COStunPause(1.2f));
            else
                StartCoroutine(COStunPause(.6f));
        }

        /*
        if (Is_Block_Successful)
        {
            Is_Block_SuccessfulCountDecay = 10;
            //////////////////// AI Check Part ///////////////////
            DATA_Total_Block_Sucessful += 1;
            Countdown_Check(2);
            //////////////////// AI Check Part ///////////////////
            Movement_All_Reset();
            Is_Hurt = false;
            Is_Block_Successful = false;
            print("Sucess Some how");
            Select_Attack_Animation(40013);
            //animator.SetTrigger("40013_Block_Success");
            Is_Excepted_Combo_Button = true;
            Is_ComboAble = true;
            Is_Excepted_Dodge_Cancel_Button = true;
            Is_Dodge_Cancel_Able = true;
            Is_Invinciable = true;
        }
        */
        if (Is_Block_SuccessfulCountDecay > 0) Is_Block_SuccessfulCountDecay--;
        //------------------------------------------------------------------------------------------------------------------------------//
            #endregion


            #region Dodging
            // +++++++++++++++++++++++++++++++ Dodge ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
        if ( ((Dodge_Next || BackStep_Next || ChargeDodge_Next)
                && (!Is_Performing_Action || Is_Dodge_Cancel_Able))||(Dodge_Next_AI))
        {
            if(Dodge_Next_AI)
            {
                Dodge_Next_AI = false;
                Dodge_Next = true;
            }

            Last_Position_Before_Dodge = transform.position;


            Dodge_Count_Down = Dodge_Check_Countdown;
            //print("Is_Performing_Action = " + Is_Performing_Action + "  Is_Dodge_Cancel_Able = "+ Is_Dodge_Cancel_Able);
            print("Dodge_Count_Down = " + Dodge_Count_Down);

            if (AI_DodgeDirCode != 0) {
                    Move_Code_0_to_8_9isRandom(AI_DodgeDirCode);
                    print("SetMovement X = " + x + " Z = " + z);
                    SetMovement(x, z);
                    print("SetMovement X = " + x + " Z = " + z);
                        AI_DodgeDirCode = 0;
                        }
                print("X = " + animator.GetFloat("Input X") + "       Z = " + animator.GetFloat("Input Z"));
                print("X = " + animator.GetFloat("Input X") + "       Z = " + animator.GetFloat("Input Z"));

            if (x < 0)
            {
                if (z == 0) // Up
                {
                    Last_Dodge_Dir_Code = 1;
                    //characterData.DATA_Total_Dodge_Off_Execute += 1;
                    characterData.DATA_40201_Dodge_FOR_Execute++;
                    Last_Dodge_Code = 1;
                }
                else if(z > 0)
                {
                    Last_Dodge_Dir_Code = 2;
                    //characterData.DATA_Total_Dodge_DiaOff_Execute += 1;
                    characterData.DATA_40202_Dodge_FORRIG_Execute++;
                    Last_Dodge_Code = 2;
                }
                else
                {
                    Last_Dodge_Dir_Code = 8;
                    //characterData.DATA_Total_Dodge_DiaOff_Execute += 1;
                    characterData.DATA_40208_Dodge_FORLET_Execute++;
                    Last_Dodge_Code = 8;
                }
            }
            else if (x > 0)
            {
                if (z == 0) // Up
                {
                    Last_Dodge_Dir_Code = 5;
                    //characterData.DATA_Total_Dodge_Off_Execute += 1;
                    characterData.DATA_40205_Dodge_BAC_Execute++;
                    Last_Dodge_Code = 5;
                }
                else if (z > 0)
                {
                    Last_Dodge_Dir_Code = 4;
                    //characterData.DATA_Total_Dodge_DiaOff_Execute += 1;
                    characterData.DATA_40204_Dodge_BACRIG_Execute++;
                    Last_Dodge_Code = 4;
                }
                else
                {
                    Last_Dodge_Dir_Code = 6;
                    //characterData.DATA_Total_Dodge_DiaOff_Execute += 1;
                    characterData.DATA_40206_Dodge_BACLET_Execute++;
                    Last_Dodge_Code = 6;
                }
            }
            else
            {
                 if (z > 0)
                {
                    Last_Dodge_Dir_Code = 3;
                    //characterData.DATA_Total_Dodge_DiaOff_Execute += 1;
                    characterData.DATA_40203_Dodge_RIG_Execute++;
                    Last_Dodge_Code = 3;
                }
                else if (z < 0)
                {
                    Last_Dodge_Dir_Code = 7;
                    //characterData.DATA_Total_Dodge_DiaOff_Execute += 1;
                    characterData.DATA_40207_Dodge_LET_Execute++;
                    Last_Dodge_Code = 7;
                }
            }

            //////////////////// AI Check Part ///////////////////
            if (BackStep_Next)
            {
                Select_Attack_Animation(44201);
                //animator.SetTrigger("44201_BackStep");
            }
            else if (ChargeDodge_Next)
            {
                Select_Attack_Animation(42201);
                //characterStat.SetCurrentCommand(32);
                //animator.SetTrigger("42201_ChargeDodge");
            }
            else
            {
                Select_Attack_Animation(40201);
                //animator.SetTrigger("40201_DodgeRoll");
            }
            Movement_All_Reset();
            Is_Hurt = false;
            Is_Invinciable = false;
            Is_SuperArmor = false;
            character_hitbox.setHitBox(Character_1_HitBox.hitBoxes.clear);
            Set_Is_Excepted_All_BlockDodgeCombo_Button(0);
            Set_All_A_B_D_J_Next_Button(0);
            Dodge_Next = false;
            BackStep_Next = false;
            ChargeDodge_Next = false;
            Reset_Animation_Speed();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * 500);
            Is_Dodge_Cancel_Able = false;
            Is_ComboAble = false;
            Attack_Next = false; Attack_Next2 = false;

            if (warrior == Warrior.Brute)
                StartCoroutine(COStunPause(1.2f));
            else if (warrior == Warrior.Sorceress)
                StartCoroutine(COStunPause(1.2f));
            else
                StartCoroutine(COStunPause(.6f));
        }
        //------------------------------------------------------------------------------------------------------------------------------//
        #endregion

        #region Teleport
        // +++++++++++++++++++++++++++++++ Teleport +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
        if (Teleport_Next
                && (!Is_Performing_Action || Is_Dodge_Cancel_Able))
        {
            Movement_All_Reset();
            Is_Hurt = false;
            Is_Invinciable = false;
            Is_SuperArmor = false;
            character_hitbox.setHitBox(Character_1_HitBox.hitBoxes.clear);
            Set_Is_Excepted_All_BlockDodgeCombo_Button(0);
            Set_All_A_B_D_J_Next_Button(0);
            Teleport_Next = false;
            Reset_Animation_Speed();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * 90);
            Is_Dodge_Cancel_Able = false;
            Is_ComboAble = false;
            Attack_Next = false; Attack_Next2 = false;
            Select_Attack_Animation(19000);
            //animator.SetTrigger("19000_Teleport");
            if (warrior == Warrior.Brute)
                StartCoroutine(COStunPause(1.2f));
            else if (warrior == Warrior.Sorceress)
                StartCoroutine(COStunPause(1.2f));
            else
                StartCoroutine(COStunPause(.6f));
        }

        //------------------------------------------------------------------------------------------------------------------------------//
        #endregion

        #region Jumping
        // +++++++++++++++++++++++++++++++ Jump ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
        IsAerialDoubleCheck(false);

        //if (controller.isGrounded)
        if (!Is_Aerial_REAL)
        {
            if (!animator.GetBool("isGrounded") && !Is_Performing_Action && !Is_Landing && !Is_Disable_Control && Battle_Controller.Time_limit[CharacterCode] > 0.1f)
            {
                //print("NONONONONONONONONONONONONONONONONONONONO");
                animator.SetTrigger("10113_Jump_Land");
                //animator.SetTrigger("Landing");

            }
            Midair_Jump_Count = 0;
            verticalVelocity = -gravity * Time.deltaTime;
            animator.SetBool("isJump", false);
            //if(!Is_Disable_Control)
            animator.SetBool("isGrounded", true);
            /*
            if ((((Jump_Next) && Is_Can_Jump_Signal) ||
                    ((Input.GetKeyDown(XKey) || Input.GetKeyDown(XKey2)) && !Is_Performing_Action)) && !Is_Disable_Control)
                    */
            if (((Jump_Next) && Is_Can_Jump_Signal)
                     && !Is_Disable_Control) 
            {
                IsAerialDoubleCheck(true);
                character_hitbox.setHitBox(Character_1_HitBox.hitBoxes.clear);
                Set_Is_Excepted_All_BlockDodgeCombo_Button(0);
                Set_All_A_B_D_J_Next_Button(0);
                Jump_Next = false;
                Reset_Animation_Speed();
                animator.SetBool("isGrounded", false);
                verticalVelocity = jumpForce;
                animator.SetTrigger("10111_Jump");
                animator.SetBool("isJump", true);
            }
        }
        else
        {

            animator.SetBool("isGrounded", false);
            if (Is_Block) verticalVelocity = -gravity * 3 * Time.deltaTime;
            else if (Is_Dodge || Is_ChargeDodge) verticalVelocity = 0;
            else verticalVelocity -= gravity * Time.deltaTime;
            if (((((Jump_Next) && Is_Can_Jump_Signal) ||
                    ((Input.GetKeyDown(XKey) || Input.GetKeyDown(XKey2) || Input.GetKeyDown(XKey3)) && !Is_Performing_Action)) && !Is_Disable_Control)

                && Max_Midair_Jump > Midair_Jump_Count)
            {
                character_hitbox.setHitBox(Character_1_HitBox.hitBoxes.clear);
                Set_Is_Excepted_All_BlockDodgeCombo_Button(0);
                Set_All_A_B_D_J_Next_Button(0);
                Jump_Next = false;
                Reset_Animation_Speed();
                Midair_Jump_Count++;
                animator.SetBool("isGrounded", false);
                verticalVelocity = jumpForce * 1.0f;
                animator.SetTrigger("10112_DoubleJump");
                animator.SetBool("isJump", true);
            }
        }
        #endregion

        #region Last Check (MoveVector/Camera/PerformAction/Animation Speed)

        Vector3 moveVector = new Vector3(controller.isGrounded ? 0 : (z * jump_Horizontal)
                                        , verticalVelocity - controller.velocity.y
                                        , controller.isGrounded ? 0 : (-x * jump_Horizontal));


        Vector3 Compare_to_camera = transform.position - camera.position;
        Compare_to_camera.y = 0;
        Compare_to_camera = Compare_to_camera.normalized;
        moveVector = new Vector3((moveVector.x * Compare_to_camera.z) + (moveVector.z * Compare_to_camera.x)
                                        , moveVector.y
                                        , (-moveVector.x * Compare_to_camera.x) + (moveVector.z * Compare_to_camera.z));
        //------------------------------------------------------------------------------------------------------------------------------//

        if (Is_Performing_Action)
        {

            Vector3 Compare_to_camera2 = CAMERA.Lock_on_Target_Object.transform.position - transform.position;
            Compare_to_camera2.y = 0;
            if (CAMERA.Lock_On && Is_Player_or_Enemy)
            {
                float distance_compare = (Compare_to_camera2.x * Compare_to_camera2.x) + (Compare_to_camera2.z * Compare_to_camera2.z);
                if (distance_compare <= 0)
                    Speed_Set1_New = new Vector3(0, 0, 0);

                else if (distance_compare <= 4)
                    Speed_Set1_New = Speed_Set1 * ((distance_compare - 4) / 4);
                else Speed_Set1_New = Speed_Set1;
            }
            else if (Enemy != null && !Is_Player_or_Enemy)
            {
                float distance_compare = (Compare_to_ENEMY.x * Compare_to_ENEMY.x) + (Compare_to_ENEMY.z * Compare_to_ENEMY.z);
                if (distance_compare <= 0)
                    Speed_Set1_New = new Vector3(0, 0, 0);

                else if (distance_compare <= 4)
                    Speed_Set1_New = Speed_Set1 * ((distance_compare - 4) / 4);
                else Speed_Set1_New = Speed_Set1;
            }
            else Speed_Set1_New = Speed_Set1 * 0.7f;

            if (Is_Attack) In_action_Vertical_Speed = new Vector3(0, (-gravity * 0.5f) - controller.velocity.y, 0);
            else if (Is_Block) In_action_Vertical_Speed = new Vector3(0, (-gravity * 1.3f) - controller.velocity.y, 0);
            else if (Is_Dodge || Is_ChargeDodge) In_action_Vertical_Speed = new Vector3(0, (-gravity * 0.04f) - controller.velocity.y, 0);
            else In_action_Vertical_Speed = new Vector3(0, (-gravity * 0.5f) - controller.velocity.y, 0);


            controller.Move((Speed_Set1_New + Speed_Set2 + In_action_Vertical_Speed) * Time.deltaTime);
        }
        else
        {
            Speed_Set1 = new Vector3(0, 0, 0);
            controller.Move(moveVector * Time.deltaTime);
        }

        #endregion
        //update character position and facing
        UpdateMovement();

    }

    #endregion



    #region Methods

    #region Movement Methods

    void SetMovement(float x, float z) {
        animator.SetFloat("Input X", z);
        animator.SetFloat("Input Z", -(x));
    }


    //face character along input direction
    void RotateTowardMovementDirection()
    {
        //if(Is_Player_or_Enemy){
            inputVec = new Vector3(x, 0, z);
            if (inputVec != Vector3.zero && !Is_Performing_Action)
            {
                if (targetDirection != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * rotationSpeed);
            }
            if (inputVec != Vector3.zero)
            {
                if (!Is_Hurt && Is_Hurt_Activate)
                {
                    print("ROTATEEEEEEEEEEEEEEEEEEEEEEE");
                    face_opponents();
                }
            }
        //}

    }

    void UpdateMovement()
    {
        //get movement input from controls
        Vector3 motion = inputVec;

        //reduce input for diagonal movement
        motion *= (Mathf.Abs(inputVec.x) == 1 && Mathf.Abs(inputVec.z) == 1) ? 0.7f : 1;
        if (!Is_Hurt_Activate)
            RotateTowardMovementDirection();
        else
            face_opponents();

        GetCameraRelativeMovement();
    }

    //Placeholder functions for Animation events
    void Hit()
    {
    }

    void FootR()
    {
    }

    void FootL()
    {
    }

    void OnGUI()
    {
    }
    public void face_opponents()
    {
        
        if( ((CAMERA.Lock_on_Target_Avaliable && Is_Player_or_Enemy && !Is_P_Enemy))
            )
            transform.LookAt(new Vector3(CAMERA.Lock_on_Target_Object.transform.position.x, transform.position.y, CAMERA.Lock_on_Target_Object.transform.position.z));
        else if (!Is_Player_or_Enemy || Is_P_Enemy)
        {
            transform.LookAt(new Vector3(Enemy.transform.position.x
               , transform.position.y, Enemy.transform.position.z));
        }
        /*
        if (true)
            transform.LookAt(new Vector3(CAMERA.Lock_on_Target.position.x, transform.position.y, CAMERA.Lock_on_Target.position.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(CAMERA.Lock_on_Target.position.x
                                                                                                        , transform.position.y
                                                                                                        , CAMERA.Lock_on_Target.position.z) - transform.position), Time.time * CAMERA.cameraSpeed * 100);
        StartCoroutine(COStunPause(1f));
        */
    }

    public void face_opponent_Hold(int TorF)
    {
        Is_KeepFacingOppo = TorF == 1 ? true : false;
    }
    public Vector3 Teleport_Pos_CompareToPlayer = new Vector3(3.5f, 0f, 0f);
    public float Teleport_Distance = 3f;
    public void Activate_Teleport(int code)
    {
        /*
            *  Pattern Example{}
            1.Move around at direction
               8    1   2       z
               7    0   3           x
               6    5   4
            */

        float delta_x = Enemy.transform.position.x- transform.position.x;
        float delta_z = Enemy.transform.position.z - transform.position.z;
        float theta_radians = Mathf.Atan2(-delta_z, -delta_x);
        float theta_degree = RadiansToConvert(theta_radians);
        Vector3 TelPos;
        int c = code == 9 ? Teleport_Position_Code : code;
        print("Degree = " + theta_degree);
        switch (c)
        {
            case 0:
                TelPos = new Vector3(Teleport_Distance * Mathf.Cos(ConvertToRadians(theta_degree + 0)), 0f, Teleport_Distance * Mathf.Sin(ConvertToRadians(theta_degree + 0)));
                break;
            case 1:
                TelPos = new Vector3(Teleport_Distance * Mathf.Cos(ConvertToRadians(theta_degree + 0)), 0f, Teleport_Distance * Mathf.Sin(ConvertToRadians(theta_degree + 0)));
                break;
            case 2:
                TelPos = new Vector3(Teleport_Distance * Mathf.Cos(ConvertToRadians(theta_degree + 45)), 0f, Teleport_Distance * Mathf.Sin(ConvertToRadians(theta_degree + 45)));
                break;
            case 3:
                TelPos = new Vector3(Teleport_Distance * Mathf.Cos(ConvertToRadians(theta_degree + 90)), 0f, Teleport_Distance * Mathf.Sin(ConvertToRadians(theta_degree + 90)));
                break;
            case 4:
                TelPos = new Vector3(Teleport_Distance * Mathf.Cos(ConvertToRadians(theta_degree + 135)), 0f, Teleport_Distance * Mathf.Sin(ConvertToRadians(theta_degree + 135)));
                break;
            case 5:
                TelPos = new Vector3(Teleport_Distance * Mathf.Cos(ConvertToRadians(theta_degree + 180)), 0f, Teleport_Distance * Mathf.Sin(ConvertToRadians(theta_degree + 180)));
                break;
            case 6:
                TelPos = new Vector3(Teleport_Distance * Mathf.Cos(ConvertToRadians(theta_degree + 225)), 0f, Teleport_Distance * Mathf.Sin(ConvertToRadians(theta_degree + 225)));
                break;
            case 7:
                TelPos = new Vector3(Teleport_Distance * Mathf.Cos(ConvertToRadians(theta_degree + 270)), 0f, Teleport_Distance * Mathf.Sin(ConvertToRadians(theta_degree + 270)));
                break;
            case 8:
                TelPos = new Vector3(Teleport_Distance * Mathf.Cos(ConvertToRadians(theta_degree + 315)), 0f, Teleport_Distance * Mathf.Sin(ConvertToRadians(theta_degree + 315)));
                break;
            default:
                TelPos = new Vector3(Teleport_Distance * Mathf.Cos(ConvertToRadians(theta_degree + 0)), 0f, Teleport_Distance * Mathf.Sin(ConvertToRadians(theta_degree + 0)));
                break;
        }
        print("TelPos = " + TelPos +  "   c");
        transform.position = OutOfBoundCorrect(Enemy.transform.position + TelPos);
        //transform.position = Enemy.transform.position + TelPos;
        //transform.position += temp;
        //CheckBorder();

    }


    public float ConvertToRadians(float angle)
    {
        return (Mathf.PI / 180) * angle;
    }
    public float RadiansToConvert(float radians)
    {
        return (180 /Mathf.PI ) * radians;
    }

    public Vector3 OutOfBoundCorrect(Vector3 input)
    {
        float disSqaure = ((input.x - GameData.ArenaCenter.x)* (input.x - GameData.ArenaCenter.x)) 
                            + ((input.z - GameData.ArenaCenter.z) * (input.z - GameData.ArenaCenter.z));
        if (disSqaure > (GameData.ArenaRadius * GameData.ArenaRadius))
        {
            float disMul = (GameData.ArenaRadius - 1) / Mathf.Sqrt(disSqaure);
            Vector3 newPos = new Vector3(input.x * disMul, input.y, input.z * disMul);
            return newPos;
        }
        else return input;
    }

    public int MoveXYtoCode(float x, float z)
    {
        switch ((int)x)
        {
            case -1:
                switch ((int)z)
                {
                    case -1:
                        return 6;
                        break;
                    case 0:
                        return 5;
                        break;
                    case 1:
                        return 4;
                        break;
                }
                break;
            case 0:
                switch ((int)z)
                {
                    case -1:
                        return 7;
                        break;
                    case 0:
                        return 0;
                        break;
                    case 1:
                        return 3;
                        break;
                }
                break;
            case 1:
                switch ((int)z)
                {
                    case -1:
                        return 8;
                        break;
                    case 0:
                        return 1;
                        break;
                    case 1:
                        return 2;
                        break;
                }
                break;
        }
        return 0;    
    }

    private void Move_Code_0_to_8_9isRandom(int d)
    {
        int code = d;
        if (code == 9) code = (int)Random.Range(1, 8.999999f);
         print((Is_P_Enemy ? "P_Enemy : " : "  Enemy : ") + "d = " + d);
        switch (d)
        {
            case 0:
                x = 0.0f;
                z = 0.0f;
                break;
            case 1:
                x = 0.0f;
                z = 1.0f;
                break;
            case 2:
                x = 1.0f;
                z = 1.0f;
                break;
            case 3:
                x = 1.0f;
                z = 0.0f;
                break;
            case 4:
                x = 1.0f;
                z = -1.0f;
                break;
            case 5:
                x = 0.0f;
                z = -1.0f;
                break;
            case 6:
                x = -1.0f;
                z = -1.0f;
                break;
            case 7:
                x = -1.0f;
                z = 0.0f;
                break;
            case 8:
                x = -1.0f;
                z = 1.0f;
                break;
        }
    }


    #endregion

    #region Defense Methods

    #region Block Methods
    public void Block_Successful_Activate(bool n)
    {
        Is_Block_Successful = n;
        if (n)
        {
            characterData.AddData_BlockAttack(Last_Opponent_State, 1f);
            Is_Block_SuccessfulCountDecay = 10;

            Countdown_Check(2);
            Movement_All_Reset();
            Is_Hurt = false;
            Is_Block_Successful = false;
            print("Sucess Some how");
            Select_Attack_Animation(40013);
            //animator.SetTrigger("40013_Block_Success");
            Is_Excepted_Combo_Button = true;
            Is_ComboAble = true;
            Is_Excepted_Dodge_Cancel_Button = true;
            Is_Dodge_Cancel_Able = true;
            Is_Invinciable = true;
        }
    }
    #endregion

    #endregion

    #region Stun Methods

    public IEnumerator COStunPause(float pauseTime)
    {
        //Is_Performing_Action = true;
        yield return new WaitForSeconds(pauseTime);
        //Is_Performing_Action = false;
    }

    #endregion

        #region Camera Methods
    //converts control input vectors into camera facing vectors
    void GetCameraRelativeMovement()
    {
        Transform cameraTransform = Camera.main.transform;

        // Forward vector relative to the camera along the x-z plane   
        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        //print(forward);
        forward.y = 0;
        forward = forward.normalized;

        // Right vector relative to the camera
        // Always orthogonal to the forward vector
        Vector3 right = new Vector3(forward.z, 0, -forward.x);

        float v, h;

        //directional inputs
        if (Is_Player_or_Enemy && !Is_P_Enemy)
        {
            v = Input.GetAxisRaw("Vertical");
            h = Input.GetAxisRaw("Horizontal");
        }
        else if(Is_P_Enemy){
            v = z;
            h = x;
        }
        else
        {
            v = -z;
            h = -x;
        }


        // Target direction relative to the camera
        targetDirection = h * right + v * forward;
    }

    #endregion

        #region Animation Select

    void Select_Attack_Animation(int i)
    {
        print("Choose = " + i + "    Is_Player_or_Enemy = " + Is_Player_or_Enemy);
        switch (i)
        {
            //// 20000-21999 = Balance Mode
            //// 22000-23999 = Heavy Mode
            //// 24000-25999 = Quick Mode
            //// 30000 = Magic
            //////
            ///
             /*
            case 0: //Round End animation (Win)
                animator.SetTrigger("Level_Up_Alive");
                break;
            case 1: //Round End animation (Lose)
                animator.SetTrigger("Level_Up_Dead");
                break;
            */
            case 0: //Round End animation (Win)
                animator.SetBool("00000_Is_Level_Up_Alive", true);
                animator.SetTrigger("Level_Up_Alive");
                break;
            case 1: //Round End animation (Lose)
                animator.SetBool("00001_Is_Level_Up_Dead", true);
                animator.SetTrigger("Level_Up_Dead");
                break;
            #region KnockBack animation
            case 01001:
                animator.SetTrigger("01001_WeakKnock");
                break;
            case 01002:
                animator.SetTrigger("01002_MedKnock");
                break;
            case 01003:
                animator.SetTrigger("01003_StrongKnock");
                print("Set Stonge Knockback");
                break;
            #endregion
            #region Balance Mode
            case 20011:
                characterStat.SetCurrentCommand(7);
                characterData.DATA_20011_BN1_Execute++;
                animator.SetTrigger("20011_Slash_1");
                if(!Is_Player_or_Enemy)print("Called 20011 Slash 1");
                break;
            case 20012:
                characterStat.SetCurrentCommand(8);
                characterData.DATA_20012_BN2_Execute++;
                animator.SetTrigger("20012_Slash_2");
                break;
            case 20013:
                characterStat.SetCurrentCommand(9);
                characterData.DATA_20013_BN3_Execute++;
                animator.SetTrigger("20013_Slash_3");
                break;
            case 20014:
                characterStat.SetCurrentCommand(10);
                characterData.DATA_20014_BN4F_Execute++;
                animator.SetTrigger("20014_Slash_4F");
                break;

            case 20111:
                characterStat.SetCurrentCommand(11);
                characterData.DATA_20111_BF1_Execute++;
                animator.SetTrigger("20111_Stab_1");
                break;
            case 20112:
                characterStat.SetCurrentCommand(12);
                characterData.DATA_20112_BF2_Execute++;
                animator.SetTrigger("20112_Stab_2");
                break;
            case 20113:
                characterStat.SetCurrentCommand(13);
                characterData.DATA_20113_BF3_Execute++;
                animator.SetTrigger("20113_Stab_3");
                break;
            case 20114:
                characterStat.SetCurrentCommand(14);
                characterData.DATA_20114_BF4F_Execute++;
                animator.SetTrigger("20114_Stab_4F");
                break;

            #endregion

            #region Heavy Mode

            case 22011:
                characterStat.SetCurrentCommand(15);
                characterData.DATA_22011_HN1_Execute++;
                animator.SetTrigger("22011_Smash_1");
                break;
            case 22012:
                characterStat.SetCurrentCommand(16);
                characterData.DATA_22012_HN2_Execute++;
                animator.SetTrigger("22012_Smash_2");
                break;
            case 22013:
                characterStat.SetCurrentCommand(17);
                characterData.DATA_22013_HN3F_Execute++;
                animator.SetTrigger("22013_Smash_3F");
                break;

            case 22111:
                characterStat.SetCurrentCommand(18);
                characterData.DATA_22111_HF1_Execute++;
                animator.SetTrigger("22111_Slam_1");
                break;
            case 22112:
                characterStat.SetCurrentCommand(19);
                characterData.DATA_22112_HF2_Execute++;
                animator.SetTrigger("22112_Slam_2");
                break;
            case 22113:
                characterStat.SetCurrentCommand(20);
                characterData.DATA_22113_HF3F_Execute++;
                animator.SetTrigger("22113_Slam_3F");
                break;

            case 22211:
                animator.SetTrigger("22211_GroundPound_1");
                break;

            #endregion

            #region Quick Mode

            case 24011:
                characterStat.SetCurrentCommand(21);
                characterData.DATA_24011_QN1_Execute++;
                animator.SetTrigger("24011_Punch_1");
                break;
            case 24012:
                characterStat.SetCurrentCommand(22);
                characterData.DATA_24012_QN2_Execute++;
                animator.SetTrigger("24012_Punch_2");
                break;
            case 24013:
                characterStat.SetCurrentCommand(23);
                characterData.DATA_24013_QN3_Execute++;
                animator.SetTrigger("24013_Punch_3");
                break;
            case 24014:
                characterStat.SetCurrentCommand(24);
                characterData.DATA_24014_QN4_Execute++;
                animator.SetTrigger("24014_Punch_4");
                break;
            case 24015:
                characterStat.SetCurrentCommand(25);
                characterData.DATA_24015_QN5F_Execute++;
                animator.SetTrigger("24015_Punch_5F");
                break;

            case 24111:
                characterStat.SetCurrentCommand(26);
                characterData.DATA_24111_QF1_Execute++;
                animator.SetTrigger("24111_Kick_1");
                break;
            case 24112:
                characterStat.SetCurrentCommand(27);
                characterData.DATA_24112_QF2_Execute++;
                animator.SetTrigger("24112_Kick_2");
                break;
            case 24113:
                characterStat.SetCurrentCommand(28);
                characterData.DATA_24113_QF3_Execute++;
                animator.SetTrigger("24113_Kick_3");
                break;
            case 24114:
                characterStat.SetCurrentCommand(29);
                characterData.DATA_24114_QF4_Execute++;
                animator.SetTrigger("24114_Kick_4");
                break;
            case 24115:
                characterStat.SetCurrentCommand(30);
                characterData.DATA_24115_QF5F_Execute++;
                animator.SetTrigger("24115_Kick_5F");
                break;

            #endregion

            #region Magic

            case 30011:
                animator.SetTrigger("30011_Fire");
                break;
            case 30021:
                animator.SetTrigger("30021_Ice");
                break;
            case 30031:
                animator.SetTrigger("30031_Thunder");
                break;
            case 30041:
                animator.SetTrigger("30041_Wind");
                break;


            #endregion

            #region Defense
            case 40010:
                animator.SetTrigger("40010_Block_Start");
                break;
            case 40013:
                animator.SetTrigger("40013_Block_Success");
                break;
                
            case 40201:
                RotateTowardMovementDirection();
                 transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * 500);
                /*
                if (AI_DodgeDirCode != 0) {
                    Move_Code_0_to_8_9isRandom(AI_DodgeDirCode);
                    print("SetMovement X = " + x + " Z = " + z);
                    SetMovement(x, z);
                    print("SetMovement X = " + x + " Z = " + z);
                        AI_DodgeDirCode = 0;
                        }
                print("X = " + animator.GetFloat("Input X") + "       Z = " + animator.GetFloat("Input Z"));
                print("X = " + animator.GetFloat("Input X") + "       Z = " + animator.GetFloat("Input Z"));
                */
                print("40201_DodgeRoll x = " + x + " z = " + z);
                print("X = " + animator.GetFloat("Input X") + "       Z = " + animator.GetFloat("Input Z"));
                animator.SetTrigger("40201_DodgeRoll");
                print("X = " + animator.GetFloat("Input X") + "       Z = " + animator.GetFloat("Input Z"));
                break;
            case 42201:
                characterStat.SetCurrentCommand(32);
                if (AI_DodgeDirCode != 0)
                {
                    Move_Code_0_to_8_9isRandom(AI_DodgeDirCode);
                    SetMovement(x, z);
                    AI_DodgeDirCode = 0;
                }
                
                animator.SetTrigger("42201_ChargeDodge");
                break;
            case 44201:
                animator.SetTrigger("44201_BackStep");
                break;
            case 19000:
                animator.SetTrigger("19000_Teleport");
                break;
                
                #endregion
                //agewwwwwwwwwwwwwagewagawegawegew

        }
    }

    public void PrintInput()
    {
        print("X = " + animator.GetFloat("Input X") + "       Z = " + animator.GetFloat("Input Z"));
    }
    #endregion

    #region Called Action Fro AIControl

    private bool Dodge_Next_AI;
    private int AI_DodgeDirCode;
    private bool Is_Invinciable_FromAI = false;

        public void Call_Action_For_AIControl(int code)
    {
        // Attack case
        if(code>20000 && code < 40000)
        {
            if(code>=20000 && code <22000) Change_Mode_Real(1);
            else if(code>=22000 && code <24000) Change_Mode_Real(2);
            else if(code>=24000 && code <26000) Change_Mode_Real(3);
            face_opponents();
            Last_Attack_Code = code;
            Select_Attack_Animation(code);
        }
        else if (code > 50000 && code < 60000)
        {
            Teleport_Next = true;
            Teleport_Distance = (code % 1000)/10;
            Teleport_Position_Code = ((int)((code-50000)/1000));
        }
        else
        {
            switch (code) {
                case 40010: Block_Next = true; break;
                case 40200: case 40201: case 40202: case 40203: case 40204: case 40205: case 40206: case 40207: case 40208: case 40209:
                    print("Call");
                    AI_DodgeDirCode = code - 40200;
                    Dodge_Next_AI = true;
                    Move_Code_0_to_8_9isRandom(AI_DodgeDirCode);
                    
                    break;
                case 42200:  case 42201: case 42202: case 42203: case 42204: case 42205: case 42206: case 42207: case 42208: case 42209:
                    ChargeDodge_Next = true; break;
                    
                case 44201: BackStep_Next = true; break;
                
            }

        }
    }

    public void Call_Movement_For_AIControl(float moveX , float moveZ)
    {
        x = moveX;
        z = moveZ;
        
    }

    public void Set_Is_Invinciable_FromAI(bool Iv)
    {
        Is_Invinciable_FromAI = Iv;
    }

    #endregion

    #region Mode Change Methods

    public void Change_Mode_Input(bool isLeft)
    {
        if((Current_Mode == Character_Mode_List.Balance_Mode && !isLeft)
            || (Current_Mode == Character_Mode_List.Quick_Mode && isLeft))
        {
            Change_Mode_Real(2);
        }
        else if ((Current_Mode == Character_Mode_List.Heavy_Mode && !isLeft)
            || (Current_Mode == Character_Mode_List.Balance_Mode && isLeft))
        {
            Change_Mode_Real(3);
        }
        else if ((Current_Mode == Character_Mode_List.Quick_Mode && !isLeft)
            || (Current_Mode == Character_Mode_List.Heavy_Mode && isLeft))
        {
            Change_Mode_Real(1);
        }
    }

    public void Change_Mode_Real(int code)
    {
        if (code == 2)
        {
            Sword.SetActive(false);
            Shield.SetActive(true);
            BigSword.SetActive(true);
            Current_Mode = Character_Mode_List.Heavy_Mode;
            animator.SetBool("10001_Idle_BalanceMode", false);
            animator.SetBool("10002_Idle_HeavyMode", true);
            animator.SetBool("10003_Idle_QuickMode", false);
            Current_Mode_Text.text = "Heavy Mode";
        }
        else if (code == 3)
        {
            Sword.SetActive(false);
            Shield.SetActive(false);
            BigSword.SetActive(false);
            Current_Mode = Character_Mode_List.Quick_Mode;
            animator.SetBool("10001_Idle_BalanceMode", false);
            animator.SetBool("10002_Idle_HeavyMode", false);
            animator.SetBool("10003_Idle_QuickMode", true);
            Current_Mode_Text.text = "Quick Mode";
        }
        else if (code == 1)
        {
            Sword.SetActive(true);
            Shield.SetActive(true);
            BigSword.SetActive(false);
            Current_Mode = Character_Mode_List.Balance_Mode;
            animator.SetBool("10001_Idle_BalanceMode", true);
            animator.SetBool("10002_Idle_HeavyMode", false);
            animator.SetBool("10003_Idle_QuickMode", false);
            Current_Mode_Text.text = "Balance Mode";
        }
    }
    #endregion

    #region Animation Frames Method

    #region Other

    public void Start_Attack_Ani()
    {
        Movement_All_Reset();
        Is_Attack = true;
        Is_ComboAble = false;
        character_hitbox.setHitBox(Character_1_HitBox.hitBoxes.clear);
    }
    public void Call_Reset_Time()
    {
        Is_Level_Up = false;
        Battle_Controller.Reset_Time(CharacterCode);

    }
    public void Reset_Position_ToStart()
    {
        transform.position = startingPos;
        print("START = " + startingPos.x);
    }
            #endregion

            #region Movement

    public void Movement_Y(int lenght)
    {
        Vector3 temp = new Vector3(0, lenght, 0);
        transform.position += temp;
        //CheckBorder();
    }

    public void Movement_All_Reset()
    {
        Movement_Forward_div_100(0);
        Movement_Speed_Forward2_Fixed_div_100(0);
        Movement_Speed_Forward_div_100(0);
        Movement_Speed_Hurt_div_100(0);
        face_opponent_Hold(0);
    }

    public void Movement_Forward_div_100(int lenght)
    {
        //print(transform.rotation.eulerAngles.y);
        //print(Mathf.Sin(transform.rotation.eulerAngles.y) + "  " + Mathf.Cos(transform.rotation.eulerAngles.y));
        Vector3 temp = new Vector3((lenght / 100) * +Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad)
                                    , 0
                                    , (lenght / 100) * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
        controller.Move(temp);
        //transform.position += temp;
        //CheckBorder();
    }
    public void Movement_Speed_Forward_div_100(int lenght)
    {
        //lenght = 0;
        /*
        Vector3 Compare_to_camera = CAMERA.Lock_on_Target.position - transform.position;
        Compare_to_camera.y = 0;
        
        if (CAMERA.Lock_On)
        {
            float distance_compare = (Compare_to_camera.x * Compare_to_camera.x) + (Compare_to_camera.z * Compare_to_camera.z);
            if (distance_compare <= 8)
                Speed_Set1 = new Vector3(0,0,0);

            else if (distance_compare <= 12)        
                Speed_Set1 = new Vector3((lenght / 100) * +Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad *  ((distance_compare - 8)/4)  )
                                        , 0
                                        , (lenght / 100) * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad * ((distance_compare - 8) / 4)));
            else
                Speed_Set1 = new Vector3((lenght / 100) * +Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad)
                                        , 0
                                        , (lenght / 100) * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
        }
        else
            */
        Speed_Set1 = new Vector3((lenght / 100) * +Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad)
                                    , 0
                                    , (lenght / 100) * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
        //print((Compare_to_camera.x * Compare_to_camera.x) + (Compare_to_camera.z * Compare_to_camera.z));
        //controller.Move(Speed_Set1 * Time.deltaTime);
        //transform.position += temp;
        //CheckBorder();
    }
    public void Movement_Speed_ForwardTracking_div_100(int lenght)
    {
        face_opponents();
        Speed_Set1 = new Vector3((lenght / 100) * +Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad)
                                    , 0
                                    , (lenght / 100) * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
        

    }
    public void Movement_Speed_Forward2_Fixed_div_100(int lenght)
    {
        //lenght = 0;

        Speed_Set2 = new Vector3((lenght / 100) * +Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad)
                                , 0
                                , (lenght / 100) * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
    }
    public void Movement_Speed_Hurt_div_100(int lenght)
    {
        //lenght = 0;

        Speed_Set2 = new Vector3((lenght / 100) * +Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * currentKnockback_Multi
                                , 0
                                , (lenght / 100) * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * currentKnockback_Multi);
    }

    public void Reset_Animation_Speed()
    {
        Speed_Set1 = new Vector3(0, 0, 0);
        Speed_Set2 = new Vector3(0, 0, 0);
    }
    #endregion

            #region Getter_Setter Method


    public void Set_All_A_B_D_J_Next_Button(int n)
    {
        if (n >= 1)
        {
            Attack_Next = true; Attack_Next2 = true;
            Block_Next = true;
            Dodge_Next = true;
            BackStep_Next = true;
            ChargeDodge_Next = true;
            Jump_Next = true;
        }
        else
        {
            Attack_Next = false; Attack_Next2 = false;
            Block_Next = false;
            Dodge_Next = false;
            BackStep_Next = false;
            ChargeDodge_Next = false;
            Jump_Next = false;
        }
    }
    public void Set_Is_ComboAble_AND_Faster_D_B_Able(int n)
    {
        if (n >= 1)
        {
            Is_ComboAble = true;
            Is_Dodge_Cancel_Able = true;
            Is_Block_Cancel_Able = true;
            Hold_Combo_Count = true;
        }
        else
        {
            Is_ComboAble = false;
            Is_Dodge_Cancel_Able = false;
            Is_Block_Cancel_Able = false;
            Hold_Combo_Count = false;
        }
    }

    public void Set_Is_Attack_Block_Dodge(int n)
    {
        if (n >= 1)
        {
            Is_Attack = true;
            Is_Block = true;
            Is_Dodge = true;
            Is_ChargeDodge = true;
            Is_Landing = false;
            Is_Hurt = false;
            Is_Teleport = false;
            Hold_Combo_Count = true;
        }
        else
        {
            Is_Attack = false;
            Is_Block = false;
            Is_Dodge = false;
            Is_ChargeDodge = false;
            Is_Landing = false;
            Is_Hurt = false;
            Is_Teleport = false;
            Hold_Combo_Count = false;
        }
    }
    public void Set_Is_Attack(int n)
    {
        if (n >= 1)
            Is_Attack = true;
        else
            Is_Attack = false;
    }
    public void Set_Is_Block(int n)
    {
        if (n >= 1)
            Is_Block = true;
        else
            Is_Block = false;
    }
    public bool Get_Is_Block()
    {
        return Is_Block;
    }

    public void Set_Is_BlockFrameAcive(int n)
    {
        if (n >= 1)
            Is_BlockFrameAcive = true;
        else
            Is_BlockFrameAcive = false;
    }
    public bool Get_Is_BlockFrameAcive()
    {
        return Is_BlockFrameAcive;
    }
    

    public void Set_Is_Dodge(int n)
    {
        if (n >= 1)
        {
            Is_Dodge = true;
            Hold_Combo_Count = true;
        }
        else
        {
            Is_Dodge = false;
            Hold_Combo_Count = false;
        }
    }
    public void Set_Is_ChargeDodge(int n)
    {
        if (n >= 1)
        {
            Is_ChargeDodge = true;
            Hold_Combo_Count = true;
        }
        else
        {
            Is_ChargeDodge = false;
            Hold_Combo_Count = false;
        }
    }
    public void Set_Is_Teleport(int n)
    {
        if (n >= 1)
            Is_Teleport = true;
        else
            Is_Teleport = false;
    }
    public void Set_Is_Invinciable(int n)
    {
        if (n >= 1)
            Is_Invinciable = true;
        else
            Is_Invinciable = false;
    }
    public void Set_Is_SuperArmor(int n)
    {
        if (n >= 1)
            Is_SuperArmor = true;
        else
            Is_SuperArmor = false;
    }
    public void Set_SuperArmorLevel(int n)
    {

        SuperArmorLevel = n;
        //print("Set " + SuperArmorLevel);
    }
    public int Get_SuperArmorLevel()
    {
        //print("Get " + SuperArmorLevel);
        return SuperArmorLevel;

    }

    public void Set_Is_Block_Successful(int n)
    {
        if (n >= 1)
            Is_Block_Successful = true;
        else
            Is_Block_Successful = false;
    }
    public bool Get_Is_Block_SuccessfulCountDecay()
    {
        return Is_Block_SuccessfulCountDecay > 0;
    }
    public void Set_Is_Landing(int n)
    {
        if (n >= 1)
            Is_Landing = true;
        else
            Is_Landing = false;
    }
    public void Set_Is_Hurt(int n)
    {
        if (n >= 1){
        print("Set_Is_Hurt = " + n + "    " + Is_Player_or_Enemy);
            Is_Hurt = true;
        }
        else
            Is_Hurt = false;
    }
    public void Set_Is_HitBoxActivate(int n)
    {
        if (n >= 1)
        {
            Is_HitBoxActivate = true;
            if (Is_Player_or_Enemy)
                Battle_Controller.PLAYER_HITBOX_ACTIVE[CharacterCode] = true;
            else Battle_Controller.ENEMY_HITBOX_ACTIVE[CharacterCode] = true;

            
        }
        else
        {
            Is_HitBoxActivate = false;
            if (Is_Player_or_Enemy)
                Battle_Controller.PLAYER_HITBOX_ACTIVE[CharacterCode] = false;
            else Battle_Controller.ENEMY_HITBOX_ACTIVE[CharacterCode] = false;
        }
      //  print("Set_Is_HitBoxActivate " + n);

    }
    
    public void Set_Is_Excepted_Combo_Button(int n)
    {
        if (n >= 1)
            Is_Excepted_Combo_Button = true;
        else
            Is_Excepted_Combo_Button = false;
    }
    public void Set_Is_Excepted_Dodge_Cancel_Button(int n)
    {
        if (n >= 1)
            Is_Excepted_Dodge_Cancel_Button = true;
        else
            Is_Excepted_Dodge_Cancel_Button = false;
    }
    public void Set_Is_Excepted_Jump_Cancel_Button(int n)
    {
        if (n >= 1)
            Is_Excepted_Jump_Cancel_Button = true;
        else
            Is_Excepted_Jump_Cancel_Button = false;
    }
    public void Set_Is_Can_Jump_Signal(int n)
    {
        if (n >= 1)
            Is_Can_Jump_Signal = true;
        else
            Is_Can_Jump_Signal = false;
    }
    public void Set_Is_Excepted_Block_Cancel_Button(int n)
    {
        if (n >= 1)
            Is_Excepted_Block_Cancel_Button = true;
        else
            Is_Excepted_Block_Cancel_Button = false;
    }
    public void Set_Is_Excepted_All_BlockDodgeCombo_Button(int n)
    {
        if (n >= 1)
        {
            Is_Excepted_Combo_Button = true;
            Is_Excepted_Dodge_Cancel_Button = true;
            Is_Excepted_Block_Cancel_Button = true;
            Is_Excepted_Jump_Cancel_Button = true;

        }

        else
        {
            Is_Excepted_Combo_Button = false;
            Is_Excepted_Dodge_Cancel_Button = false;
            Is_Excepted_Block_Cancel_Button = false;
            Is_Excepted_Jump_Cancel_Button = false;
        }

    }
    public void Set_Is_Dodge_Block_Able(int n)
    {
        if (n >= 1)
        {
            Is_Dodge_Cancel_Able = true;
            Is_Block_Cancel_Able = true;
        }
        else
        {
            Is_Dodge_Cancel_Able = false;
            Is_Block_Cancel_Able = false;
        }
    }

    public void Set_Is_Level_Up(int n)
    {   
        print("Is_level_Up = " + Is_Level_Up + "   " + Is_Player_or_Enemy);
        if (n >= 1){
            Is_Level_Up = true;
            
        }
        else
        {
            Is_Level_Up = false;
            Is_In_Level_Up = false;
            //print("Called the damn thing");
        }

    }

    public void Set_Is_In_MiddleOfLevelUpReset(){
        animator.SetBool("00000_Is_Level_Up_Alive",false);
        animator.SetBool("00001_Is_Level_Up_Dead",false);
    }
 
    public bool Get_Is_Level_up()
    {
        return Is_Level_Up;
    }

    #endregion





    #endregion

        #region Getter Setter Method

    public bool Get_Is_InCombo()
    {
        return Is_InCombo;
    }
    public void Set_Is_InCombo(bool incombo)
    {
        Is_InCombo = incombo;
    }

    public bool Get_Is_Disable_Control()
    {
        return Is_Disable_Control;
    }
    public void Set_Is_Disable_Control(bool isDis)
    {
        Is_Disable_Control = isDis;
    }

    public bool Get_Is_Hurt_Total()
    {
        return (Is_Hurt || Is_Hurt_Activate);

    }
    public void Set_Is_Hurt(bool isH)
    {
       
        Is_Hurt = isH;
    }
    public void Set_Is_Hurt_Activate(bool isHAct)
    {
        Is_Hurt_Activate = isHAct;
    }
    public bool Get_Is_ComboAble_CheckFromActionCalled()
    {
        return Is_ComboAble_CheckFromActionCalled;
    }
    public void Set_Is_ComboAble_CheckFromActionCalled(bool isCCFAC)
    {
        Is_ComboAble_CheckFromActionCalled = isCCFAC;
    }
    public bool Get_Is_ComboAble_Real()
    {
        return Is_ComboAble_Real;
    }
    public void Set_Is_ComboAble_Real(bool isCAR)
    {
        Is_ComboAble_Real = isCAR;
    }
    public int Get_Character_State()
    {
        return State_P;
    }
    public int Get_Opponent_State()
    {
        return State_E;
    }

    #endregion

        #region Hurt Methods

    public void Activate_Hurt( int knockbackCode)
    {
        if(characterStat.GetCurrentHPPercentage() <= 0) {
            print("Health below 1, Can't die LOL");
            return;
        }
        //////////////////// AI Check Part ///////////////////
        characterData.AddData_DodgeFail(Is_Dodge, Is_ChargeDodge, Last_Dodge_Code);
        if (!Is_Hurt_was)
        {
            characterData.DeStyleRank();
        }
    
        Is_Hurt_Activate = true;
        face_opponents();
        print("Ouch");
        Movement_All_Reset();
        //if (CAMERA.Lock_On) transform.LookAt(new Vector3(CAMERA.Lock_on_Target.position.x, transform.position.y, CAMERA.Lock_on_Target.position.z));
        COStunPause(0.08f);
         print("Activate Hurt" +"   " + Is_Player_or_Enemy + "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz");
        if (knockbackCode == 1)
            Select_Attack_Animation(01001);

        else if (knockbackCode == 2)
            Select_Attack_Animation(1002);

        else if (knockbackCode == 3)
            Select_Attack_Animation(1003);

        COStunPause(0.08f);

        // DeRank Here
        


        //Is_Hurt_Activate = false;
    }
    public void setKnockback_Multi(float v)
    {
        currentKnockback_Multi = v;
    }

    #endregion

    #region Aerial/Landing Part Methods

    float IsAerialDoubleCheckCount = 0;

    public void IsAerialDoubleCheck(bool jump)
    {
        //print("IsAerialDoubleCheckCount = " + IsAerialDoubleCheckCount);
        if (jump)
            IsAerialDoubleCheckCount = 0.2f;
        else if (controller.isGrounded)
            IsAerialDoubleCheckCount = 0;
        else
            IsAerialDoubleCheckCount += TIME;
        
        if(IsAerialDoubleCheckCount >= 0.2f)
            Is_Aerial_REAL = true;
        else
            Is_Aerial_REAL = false;

    }

    #endregion

    #region LevelUp Methods

    public void LevelUp()
    {

        characterData.DATA_PlayTime = PlayTime;
        PlayTime = 0;
        characterData.DATA_RunTime = RunTime;

        //StatUPCheck();
    }

    public void Full_Heal()
    {
        print("FULL HEALLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL = " + DATA_RemainHP_Percent);
        //characterStat.SetCurrentHPPercentage(1);
        //DATA_RemainHP_Percent = characterStat.GetCurrentHPPercentage();
        characterStat.SetCurrentHPPercentage(1);
        Battle_Controller.CALL_FULL_HEAL[CharacterCode] = true;
    }
    public void Heal_1_HP()
    {
        characterStat.SetCurrentHPPercentage(1 / characterStat.HP);
    }
    public bool IS_Invinciable_Real()
    {
        return Is_Invinciable || Is_Invinciable_FromAI;
    }
    public bool IS_SuperArmor()
    {
        return Is_SuperArmor;
    }

    #endregion

    #region Character State Methods

    int Last_Direction_Save_For_Dodge;
    int Old_STATE;

    private void Update_State()
    {
        
        ///////////////////////////// Update State /////////////////////////
        int STATE = 0;
        bool IS_D = false;

        if (Is_Hurt) STATE = 90;
        else if (Is_Attack)
        {
            STATE = Last_Attack_Code;
            /*
            if (Last_Attack_Code == 103) STATE = 10;
            else if (Last_Attack_Code == 101) STATE = 11;
            else if (Last_Attack_Code == 108) STATE = 12;
            else if (Last_Attack_Code == 201) STATE = 20;
            else if (Last_Attack_Code == 202) STATE = 21;
            else if (Last_Attack_Code == 301) STATE = 30;
            else if (Last_Attack_Code == 303) STATE = 31;
            */
        }
        else if (Is_Block) STATE = 40010;
        else if (animator.GetBool("isJump")) STATE = 60;
        else if (Is_Hurt) STATE = 90;
        else if (Is_Level_Up) STATE = 99;

        else if (animator.GetBool("Moving") || Is_Dodge || Is_ChargeDodge)
        {
            
            if( (Old_STATE >= 40200 && Old_STATE <= 40209)
                || (Old_STATE >= 42200 && Old_STATE <= 42209))
            {
                STATE = Last_Direction_Save_For_Dodge;
            }
            else if (x == -1)
            {
                if (z == 0) STATE = 1;
                else if (z > 0) STATE = 2;
                else STATE = 8;
            }
            else if (x == 0)
            {
                if (z == 0) STATE = 0;
                else if (z > 0) STATE = 3;
                else STATE = 7;
            }
            else if (x == 1)
            {
                if (z == 0) STATE = 5;
                else if (z > 0) STATE = 4;
                else STATE = 6;
            }
            Last_Direction_Save_For_Dodge = STATE;
            if (Is_Dodge) STATE += 40200;
            else if (Is_ChargeDodge) STATE += 42200;
            else STATE += 10010;
        }
        else STATE = 10010;

        Old_STATE = STATE;

        if (Is_Player_or_Enemy)
            Battle_Controller.P_STATE[CharacterCode] = STATE;
        else Battle_Controller.E_STATE[CharacterCode] = STATE;
        
        ///////////////////////////// Update Mode /////////////////////////
        int MODE = 0;
        switch(Current_Mode){
            case Character_Mode_List.Balance_Mode: MODE = 1; break;
            case Character_Mode_List.Heavy_Mode: MODE = 2; break;
            case Character_Mode_List.Quick_Mode: MODE = 3; break;
            default: MODE = 1; break;
        }
        if (Is_Player_or_Enemy)
            Battle_Controller.P_MODE[CharacterCode] = MODE;
        else Battle_Controller.E_MODE[CharacterCode] = MODE;
        
    }

    private void Start_Counting_Countdown(int i, float amount)
    {
        if (i == 1)
        {
            Dodge_Count_Down = amount;
        }
        else if (i == 2)
        {
            // Block_Count_Down = amount;
        }
    }


    #endregion

    #region Style Point Method

    #endregion

    #region Mode Method

    public void Set_CharacterMode(int a)
    {
        //print("Set_CharacterMode = " +a);
        Change_Mode_Real(a);

    }
    public int Get_CharacterMode_B1H2Q3()
    {
        return (Current_Mode == Character_Mode_List.Balance_Mode) ? 1 :
            (Current_Mode == Character_Mode_List.Heavy_Mode) ? 2 :
            (Current_Mode == Character_Mode_List.Quick_Mode) ? 3 : 1;
    }

    #endregion


    #region Score part (Need a lot of Change later)

    #region State Check Part

    private void Attack_Hit_Check(int code)
    {

        float point = 1f;
        /*
        switch (State_E)
        {
            case Battle_Controller.S_DODGE:
                point = 1.2f;
                break;
            case Battle_Controller.S_BLOCK:
                point = 1.3f;
                break;
            case Battle_Controller.S_HURT:
                point = 0.7f;
                break;
            case Battle_Controller.S_JUMP:
                point = 1.1f;
                break;
            default:
                print("Score_1_Attack = " + Score_1_Attack);
                point = 1f;
                break;
        }
        */
        characterData.AddComboListData(code, Is_Hitting_FirstHitofCombo);
        characterData.AddData_AttackHit(code, point);
        //characterData.AddComboListData(code, Combo_Count == 1 ? true : false);
        
        Is_Hitting_FirstHitofCombo = false;
        


    }

    public void Countdown_Check_On_Animation(int i)
    {
        Countdown_Check(i);
    }


    public int Last_Opponent_State = 0;
    bool In_Check_Walk_Dodge = false;
    bool Is_Hurt_Dodge_BlockHit_ATK_After_Attack = false;
    float Distane = 0;
    float Spam_Block_Count = 0;
    bool OpponentHitboxUActive;
     float OpponentHitboxUsedToActiveCount = 0;

    private void Countdown_Check(int ZeroHurt_OneDodge_TwoBlock)
    {


        if (ZeroHurt_OneDodge_TwoBlock == 1)
        {
            //print("In Step 1");
            Vector3 D;

            In_Check_Walk_Dodge = (State_E >= Battle_Controller.S_ATK_BAL_N1
                && State_E <= Battle_Controller.S_ATK_QUK_F5);

            if ((State_E >= Battle_Controller.S_ATK_BAL_N1
                && State_E <= Battle_Controller.S_ATK_QUK_F5)
                && (Last_Opponent_State < Battle_Controller.S_ATK_BAL_N1
                || Last_Opponent_State > Battle_Controller.S_ATK_QUK_F5)
                && State_E != Last_Opponent_State)
            {

                D = transform.position - Enemy.position;
                D.y = 0;
                Distane = Compare_to_ENEMY.magnitude;
            }

            /*if (AttackDodgeCheck)
            {
                print ("fdpasdjfoijasoighahajogbaow;");
                D = transform.position - player.position;
                D.y = 0;
                Distane = Compare_to_PLAYER.magnitude;
            }*/

            /*
            if ((Last_Opponent_State >= Battle_Controller.S_ATK_BAL_N1
                && Last_Opponent_State <= Battle_Controller.S_ATK_QUK_F5)
                && ((State_E < Battle_Controller.S_ATK_BAL_N1
                || State_E > Battle_Controller.S_ATK_QUK_F5)
              ))
                */
            if(Is_Player_or_Enemy ? Battle_Controller.ENEMY_HITBOX_ACTIVE[CharacterCode] : Battle_Controller.PLAYER_HITBOX_ACTIVE[CharacterCode]){
                OpponentHitboxUActive = true;
            }
            //IsOpponentHitboxUsedToActiveCount = (Is_Player_or_Enemy ? Battle_Controller.ENEMY_HITBOX_ACTIVE : Battle_Controller.PLAYER_HITBOX_ACTIVE)
            if(OpponentHitboxUActive &&
            !(Is_Player_or_Enemy ? Battle_Controller.ENEMY_HITBOX_ACTIVE[CharacterCode] : Battle_Controller.PLAYER_HITBOX_ACTIVE[CharacterCode]))
            {
                OpponentHitboxUActive = false;
                Vector3 Current_Position;
                Current_Position = transform.position - Enemy.position;
                Current_Position.y = 0;
                print ("THISSSS");
                float Multiplier = 1.3f - ((Current_Position.magnitude < 10) ?
                    (2.0f - (Current_Position.magnitude * 0.20f)) / 10 : 1.1f);
                if ((Battle_Controller.Attack_Range_Check(Last_Opponent_State) + 1.2f
                    /*
                    * (1.4f - ((Battle_Controller.Score[CharacterCode] < 300) ? 0 :
                    (Battle_Controller.Score[CharacterCode] < 1300) ? (0.6f * (Battle_Controller.Score[CharacterCode] - 300) / 1000) : 0.6f)
                    */
                    ) >= Distane && //!Is_Hurt_Dodge_BlockHit_ATK_After_Attack 
                    !Is_Hurt
                    //&& (Is_Player_or_Enemy?Battle_Controller.ENEMY_HITBOX_ACTIVE:Battle_Controller.PLAYER_HITBOX_ACTIVE)
                    )
                {
                    print ("THISSSS Is_Hurt = " + Is_Hurt);
                    Spam_Block_Count -= 5f;
                    if (Spam_Block_Count < 0) Spam_Block_Count = 0;
                    
                    
                    ///// Call character Data
                    characterData.AddData_DodgeAttack(Last_Opponent_State, Multiplier);
                    ///// Call character Data

                    //print( "Dodge_Count_Down       " + Dodge_Count_Down);
                    if ((Dodge_Count_Down > 0))
                    {
                        characterData.AddData_DodgeDirection(Is_Dodge, Is_ChargeDodge, Last_Dodge_Dir_Code);
                    }
                    Dodge_Count_Down = 0;
                    //Is_HitBoxActivate = false;
                }

                Is_Hurt_Dodge_BlockHit_ATK_After_Attack = false;
            }
            AttackDodgeCheck = false;
            if (In_Check_Walk_Dodge && (Is_Hurt || Is_Block_Successful || Is_Dodge || Is_Attack  || Is_ChargeDodge))
            {
                // print("CHECKKKKKKKKKKKKKKKKKKKKKKK");
                Is_Hurt_Dodge_BlockHit_ATK_After_Attack = true;
            }
            else if (!In_Check_Walk_Dodge) Is_Hurt_Dodge_BlockHit_ATK_After_Attack = false;

            // else if(State_E < Battle_Controller.S_ATK_BASIC_CLOSE
            //     || State_E > Battle_Controller.S_ATK_FIN_FAR) Is_Hurt_After_Attack = false;

            Last_Opponent_State = State_E;
        }
        if (Dodge_Count_Down > 0 || !(ZeroHurt_OneDodge_TwoBlock == 1))
        {
            //print("In Step 2");
            Vector3 D;
            float Multiplier;
            if (ZeroHurt_OneDodge_TwoBlock == 0)
            {
                /*
                D = transform.position - Enemy.position;
                Last_Dodge_Dir_Code = 0;
                Multiplier = 0 - ((float)Battle_Controller.Score[CharacterCode] / 2000);
                print("Score = " + Battle_Controller.Score + "    Multiplier = " + Multiplier);
                Spam_Block_Count -= 3f;
                if (Spam_Block_Count < 0) Spam_Block_Count = 0;
                */
            }

            else if (ZeroHurt_OneDodge_TwoBlock == 2)
            {
                Last_Dodge_Dir_Code = 0;
                D = (transform.position - Enemy.position) * 100;
                Multiplier = 0.8f - (Spam_Block_Count * 0.08f);
                Spam_Block_Count++;
            }
            else
            {
                D = Last_Position_Before_Dodge - Enemy.position;
                Multiplier = 1.6f;
                Spam_Block_Count -= 7f;
                if (Spam_Block_Count < 0) Spam_Block_Count = 0;
            }

            D.y = 0;
            float Distan = Compare_to_ENEMY.magnitude;

            if ((Battle_Controller.Attack_Range_Check(State_E) * 1f >= Distan))
            {
                
                
            }
            Dodge_Count_Down -= TIME;
        }

    }

    #endregion



  
    private float Dodge_Score_Multiplier()
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



}
#endregion


#endregion

