
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_1_AIControl : MonoBehaviour {

    #region Variable

        #region AI Related Var

    public Character_1 character;
    public Character_Stat characterStat;
    public Character_1_Data characterData;

    private float TIME;
    private float TIME_IN_THIS_10thFRAME;
    private float TIME_ACCU;
    public float wait = 0;
    private float MoveCount;

    public AI_Mode_List AI_mode;
    public enum AI_Mode_List
    {
        SimpleAI_1
        ,ReactiveAI_1
        ,PlayerReactiveAI
    }

    public float moveX = 0f;
    public float moveZ = 0f;


    public bool Is_UsingPreset = false;

    private bool Is_Waiting_For_Action = false;

    public int Actioncode = 101;


    public float Action_Count = -1.5f;


    // public float TIME;

    #endregion

    #region Pattern/Command Variable

    public class AILogic{
        public List<Combo> Combo_Libery = new List<Combo>();
        public List<Pattern> Pattern_Libery = new List<Pattern>();

        public List<Combo> ReactCombo_Libery = new List<Combo>();
        public List<React_Command> React_Command_Libery = new List<React_Command>();

        public List<Combo> RevengeCombo_Libery = new List<Combo>();
        public List<Pattern> Revenge_Command_Libery = new List<Pattern>();
        public PatternSet curPatternSet = new PatternSet();
        public float RV;

        public AILogic(){}  
        public AILogic(List<int[]> Combo_Libery_Code,
                        List<List<float[]>> Pattern_Libery_Code,
                        List<int[]> ReactCombo_Libery_Code,
                        List<List<float[]>> React_Command_Libery_Code,
                        List<float> React_Command_Libery_Time,
                        List<int[]> React_Command_Libery_Action_Code,
                        List<int[]> RevengeCombo_Libery_Code,
                        float revengeValue,
                        List<float[]> Revenge_Command_Libery_Code,
                        List<int> CurrentPatternSet_Code                        
        ){
            //for(int i = 0 ; i < Combo_Libery_Code.Count ; i++){
                //Combo_Libery.Add(new List<Combo>());
                foreach(var frame in Combo_Libery_Code){
                    Combo_Libery.Add(new Combo(frame));  
                }
            //}
            //for(int i = 0 ; i < ReactCombo_Libery_Code.Count ; i++){
                //ReactCombo_Libery.Add(new List<Combo>());
                foreach(var frame in ReactCombo_Libery_Code){
                    ReactCombo_Libery.Add(new Combo(frame));  
                }
            //}
            //for(int i = 0 ; i < RevengeCombo_Libery_Code.Count ; i++){
                //RevengeCombo_Libery.Add(new List<Combo>());
                foreach(var frame in RevengeCombo_Libery_Code){
                    RevengeCombo_Libery.Add(new Combo(frame));  
                }
            //}
            
            foreach(var frame in Pattern_Libery_Code){
                float[][] a = CreateRectangularArray<float>(frame);
                Pattern_Libery.Add(new Pattern(a));
                //print("Pattern_Libery_Code[0] = "  + (frame[0][0]));
            }
            print("React_Command_Libery_Code Count = "  + React_Command_Libery_Code.Count);
            for(int i = 0 ; i < React_Command_Libery_Code.Count ; i++){
                List<float[]> bList = React_Command_Libery_Code[i];
                float[][] b = CreateRectangularArray<float>(bList);
                Pattern patB = new Pattern(b);
                PatternSet patsetB = new PatternSet(new Pattern[] {patB});
                print(i);
                React_Command_Libery.Add(new React_Command(
                    patsetB,
                    React_Command_Libery_Action_Code[i],
                    React_Command_Libery_Time[i],
                    25.0f));

                print("React_Command_Libery_Code Num = "  + i);
            }

            float[][] c = Revenge_Command_Libery_Code.ToArray();
            Revenge_Command_Libery.Add(new Pattern(c));

            RV = revengeValue;

            int patSetListLength= CurrentPatternSet_Code.Count;
            Pattern[] patternListForSet = new Pattern[patSetListLength];
            int[] patternListForSetCode = new int[patSetListLength];
            for(int i = 0 ; i < patSetListLength ; i++){
                patternListForSet[i] = Pattern_Libery[CurrentPatternSet_Code[i]];
                patternListForSetCode[i] = CurrentPatternSet_Code[i];
                print("CurrentPatternSet_Code Count = "  + i);
            }
            curPatternSet = new PatternSet(patternListForSet,patternListForSetCode);
            
            print("Write Success");
        }

        public PatternSet ChangePatSetSlot(int patCode, int slot){
            PatternSet newPatternSet = curPatternSet;
            newPatternSet.Set_patternList_Slot(Pattern_Libery[patCode], slot);
            newPatternSet.Set_patternListCode_Slot(patCode,slot);
            return newPatternSet;
        }
       
        
    }

    static T[][] CreateRectangularArray<T>(IList<T[]> arrays)
    {
        // TODO: Validation and special-casing for arrays.Count == 0
        if(arrays == null) return null;
        else{
            int minorLength = arrays[0].Length;
            T[][] ret = new T[arrays.Count][];
            for (int i = 0; i < arrays.Count; i++)
            {
                ret[i] = new T[minorLength];
                var array = arrays[i];
                if (array.Length != minorLength)
                {
                    //throw new ArgumentException
                    print("!!!!!!!!!!!All arrays must be the same length");
                }
                for (int j = 0; j < minorLength; j++)
                {
                    //print(i + "  " + j );
                    ret[i][j] = array[j];
                }
            }
            return ret;
        }
        
    }

    public class PatternSet
    {
        public Pattern[] patternList;
        public int[] patternListCode;

        public PatternSet()
        {
            patternList = null;
            patternListCode = null;
        }
        public PatternSet(Pattern[] x)
        {
            patternList = x;
            patternListCode = null;
        }
        public PatternSet(Pattern[] x, int[] y)
        {
            patternList = x;
            patternListCode = y;
        }
        public PatternSet( int[] y)
        {
            patternList = null;
            patternListCode = y;
        }
        public Pattern[] Get_patternList()
        {
            return patternList;
        }
        public int[] Get_patternListCode()
        {
            return patternListCode;
        }
        public void Set_patternList(Pattern[] x){
            patternList = x;
        }
        public void Set_patternListCode(int[] x){
            patternListCode = x;
        }
        public void Set_patternList_Slot(Pattern value,int slot){
            patternList[slot] = value;
        }
        public void Set_patternListCode_Slot(int value,int slot){
            patternListCode[slot] = value;
        }
    }

    public PatternSet setPatternListFromCode(Pattern[] b, PatternSet a ){
        Pattern[] nP = new Pattern[(a.Get_patternListCode()).Length];
        for(int i = 0;i<(a.Get_patternListCode()).Length;i++){
            nP[i] = b[(a.Get_patternListCode())[i]];
        }
        a.Set_patternList(nP);
        return a;
    }

    public class Pattern
    {
        public Pattern(float[][] x)
        {
            movePattern = x;
        }

        public float[][] movePattern;
        /*
         * [ActionCode1, Detailed.... ]
         * [ActionCode2, Detailed.... ]
         */
        public float[][] Get_movePattern()
        {
            return movePattern;
        }

        public void Set_movePattern(float[][] x)
        {
            movePattern = x;
        }
    }

    public class Combo
    {
        public Combo( int[] x)
        {
            comboPattern = x;

        }

        public int[] comboPattern;


        public int[] Get_ComboPattern()
        {
            return comboPattern;
        }
        public int Get_ComboPatternLenght(){
            return comboPattern.Length;
        }
        public void Set_ComboPattern(int[] x)
        {
            comboPattern = x;

        }
    }

    public class React_Command
    {
        private PatternSet patternSet;
        private int[] oppo_Action;
        private float reaction_Speed;
        private float range;
        
        public React_Command(PatternSet patternSet,int[] oppo_Action,float reaction_Speed, float range)
        {
            this.patternSet = patternSet;
            this.reaction_Speed=reaction_Speed;
            this.oppo_Action = oppo_Action;
            this.range = range;
            //print(patternSet);
            //print(patternSet.Get_patternList());
        }
        public Pattern[] Get_patternList()
        {
            return patternSet.Get_patternList();
        }
        public int[] Get_oppo_Action(){
            return oppo_Action;
        }
        public float Get_reaction_Speed(){
            return reaction_Speed;
        }
        public float Get_range(){
            return range;
        }
    }

/*
    public Combo[] Combo_Libery = new Combo[1000000];
    public Pattern[] Pattern_Libery = new Pattern[1000000];
    public PatternSet[] PatternSet_Libery = new PatternSet[1000000];
    public React_Command[] React_Command_Libery = new React_Command[1000000];
    public Pattern[] Revenge_Command_Libery = new Pattern[1000000];
*/
    public AILogic CurrentAI = new AILogic(); 
    public void SetCurrentAIfromWrite(List<int[]> Combo_Libery_Code,
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
                             
        CurrentAI = new AILogic( Combo_Libery_Code,
                         Pattern_Libery_Code,
                         ReactCombo_Libery_Code,
                         React_Command_Libery_Code,
                         React_Command_Libery_Time,
                         React_Command_Libery_Action_Code,
                         RevengeCombo_Libery_Code,
                         revengeValue,
                         Revenge_Command_Libery_Code,
                        CurrentPatternSet_Code);

                        characterStat.RevengeValue = revengeValue;
                        Choose_Pattern_FromSystem();
    }
    /*
    public List<Combo> Combo_Libery = new List<Combo>();
    public List<Pattern> Pattern_Libery = new List<Pattern>();
    
    public List<React_Command> React_Command_Libery = new List<React_Command>();
    public List<Pattern> Revenge_Command_Libery = new List<Pattern>();
    public PatternSet NormalPatternSet;
    */
    public List<PatternSet> PatternSet_Libery = new List<PatternSet>();

    public Combo[] Combo_Libery_Preset = new Combo[1000000];
    public Pattern[] Pattern_Libery_Preset = new Pattern[1000000];
    public PatternSet[] PatternSet_Libery_Preset = new PatternSet[1000000];
    public React_Command[] React_Command_Libery_Preset = new React_Command[1000000];

    public void InitializeReact_CommandStart()
    {
        //public React_Command reactCommandList1 = new React_Command(PatternSet_Libery[200001]);
        //React_Command_Libery[0] = new React_Command(PatternSet_Libery[200001],new int[] {20011,20111}, 0.01f,5f);
        if(Is_UsingPreset){
             CurrentAI.React_Command_Libery.Add(new React_Command(PatternSet_Libery[1],new int[] {20011,20111}, 0.01f,5f)) ;
        }
        else{
            //CurrentAI.React_Command_Libery.Add(new React_Command(PatternSet_Libery[200001],new int[] {20011,20111}, 0.01f,5f)) ;
        }
        //CurrentAI.React_Command_Libery = React_Command_Libery;
    }
        public void InitializeRevenge_CommandStart()
    {
        //public React_Command reactCommandList1 = new React_Command(PatternSet_Libery[200001]);
        CurrentAI.Revenge_Command_Libery = new List<Pattern>(RVB200003_DodgeBackHN1);
        //CurrentAI.Revenge_Command_Libery = Revenge_Command_Libery;
    }
////////
    #endregion

        #region ComboList

    

    /* C 
        1(Difficulty 0 = Basic, 1 = Intermediate, 2 = Expert)
        2
        3
        4 Mode (Balance = 0-1 , Heavy = 2-3, Quick = 4-5)
        5 Attack command (Even = Neutral, Odd = Forward)
        6 Combo Count
        */


    public Combo C000001_BasicCom_BalN1 = new Combo(new int[] {20011});
    public Combo C000002_BasicCom_BalN2 = new Combo(new int[] { 20011, 20012  });
    public Combo C000003_BasicCom_BalN3 = new Combo(new int[] { 20011, 20012, 20013 });
    public Combo C000004_BasicCom_BalN4 = new Combo(new int[] { 20011, 20012, 20013, 20014 });

    public Combo C000011_BasicCom_BalF1 = new Combo(new int[] { 20111 });
    public Combo C000012_BasicCom_BalF2 = new Combo(new int[] { 20111, 20112 });
    public Combo C000013_BasicCom_BalF3 = new Combo(new int[] { 20111, 20112, 20113 });
    public Combo C000014_BasicCom_BalF4 = new Combo(new int[] { 20111, 20112, 20113, 20114 });

    public Combo C000201_BasicCom_HeaN1 = new Combo(new int[] { 22011 });
    public Combo C000202_BasicCom_HeaN2 = new Combo(new int[] { 22011, 22012 });
    public Combo C000203_BasicCom_HeaN3 = new Combo(new int[] { 22011, 22012, 22013 });

    public Combo C000211_BasicCom_HeaF1 = new Combo(new int[] { 22111 });
    public Combo C000212_BasicCom_HeaF2 = new Combo(new int[] { 22111, 22112 });
    public Combo C000213_BasicCom_HeaF3 = new Combo(new int[] { 22111, 22112, 22113 });

    public Combo C000401_BasicCom_QukN1 = new Combo(new int[] { 24011 });
    public Combo C000402_BasicCom_QukN2 = new Combo(new int[] { 24011, 24012 });
    public Combo C000403_BasicCom_QukN3 = new Combo(new int[] { 24011, 24012, 24013 });

    public Combo C000411_BasicCom_QukF1 = new Combo(new int[] { 24111 });
    public Combo C000412_BasicCom_QukF2 = new Combo(new int[] { 24111, 24112 });
    public Combo C000413_BasicCom_QukF3 = new Combo(new int[] { 24111, 24112, 24113 });

    public Combo C090001_BasicCom_TelBalN1 = new Combo(new int[] { 52040, 20011 });
    public Combo C040001_BasicCom_BalN1Dodge = new Combo(new int[] { 20011, 40205 });
    public Combo C040002_BasicCom_DodgeBalN1 = new Combo(new int[] { 40205, 20011 });


    public Combo C040101_BasicCom_BlockBalN1 = new Combo(new int[] { 40010, 20011 });
    public Combo C040102_BasicCom_BlockBalN2 = new Combo(new int[] { 40010, 20011,20012 });
    public Combo C040201_BasicCom_BalN1Block = new Combo(new int[] { 20011, 40010 });

    public Combo C040105_BasicCom_DodgeBack = new Combo(new int[] { 40205 });
    public Combo C040115_BasicCom_DodgeBackBN4 = new Combo(new int[] { 40205,20014 });
    public Combo C040121_BasicRev_DodgeBackHN1 = new Combo(new int[] { 40205, 22011 });

    public Combo C100001_InterCom_BalN01 = new Combo(new int[] {40202,20011}); // Dodge diagonal left -> NAttack once
    public Combo C100101_InterCom_BalN02 = new Combo(new int[] { 40207, 20011 }); // Dodge diagonal right -> NAttack once
    public Combo C100011_InterCom_BalN03 = new Combo(new int[] { 40203, 20111 }); // Dodge left -> FAttack once
    public Combo C100111_InterCom_BalN04 = new Combo(new int[] { 40206, 20111 }); // Dodge right -> FAttack once



    
    #endregion

        #region Combo Called

  

    public void InitializeComboStart()
    {
        Combo_Libery_Preset[0] = C000001_BasicCom_BalN1;
        Combo_Libery_Preset[1] = C000001_BasicCom_BalN1;
        Combo_Libery_Preset[2] = C000002_BasicCom_BalN2;
        Combo_Libery_Preset[3] = C000003_BasicCom_BalN3;
        Combo_Libery_Preset[4] = C000004_BasicCom_BalN4;
        Combo_Libery_Preset[11] = C000011_BasicCom_BalF1;
        Combo_Libery_Preset[12] = C000012_BasicCom_BalF2;
        Combo_Libery_Preset[13] = C000013_BasicCom_BalF3;
        Combo_Libery_Preset[14] = C000014_BasicCom_BalF4;

        Combo_Libery_Preset[201] = C000201_BasicCom_HeaN1;
        Combo_Libery_Preset[202] = C000202_BasicCom_HeaN2;
        Combo_Libery_Preset[203] = C000203_BasicCom_HeaN3;
        Combo_Libery_Preset[211] = C000211_BasicCom_HeaF1;
        Combo_Libery_Preset[212] = C000212_BasicCom_HeaF2;
        Combo_Libery_Preset[213] = C000213_BasicCom_HeaF3;

        Combo_Libery_Preset[401] = C000401_BasicCom_QukN1;
        Combo_Libery_Preset[402] = C000402_BasicCom_QukN2;
        Combo_Libery_Preset[403] = C000403_BasicCom_QukN3;
        Combo_Libery_Preset[411] = C000411_BasicCom_QukF1;
        Combo_Libery_Preset[412] = C000412_BasicCom_QukF2;
        Combo_Libery_Preset[413] = C000413_BasicCom_QukF3;

        Combo_Libery_Preset[90001] = C090001_BasicCom_TelBalN1;
        Combo_Libery_Preset[40001] = C040001_BasicCom_BalN1Dodge;
        Combo_Libery_Preset[40002] = C040002_BasicCom_DodgeBalN1;
        Combo_Libery_Preset[40101] = C040101_BasicCom_BlockBalN1;
        Combo_Libery_Preset[40102] = C040102_BasicCom_BlockBalN2;
        Combo_Libery_Preset[40201] = C040201_BasicCom_BalN1Block;
        Combo_Libery_Preset[40105] = C040105_BasicCom_DodgeBack;
        Combo_Libery_Preset[40115] = C040115_BasicCom_DodgeBackBN4;
        Combo_Libery_Preset[40121] = C040121_BasicRev_DodgeBackHN1;
        Combo_Libery_Preset[100001] = C100001_InterCom_BalN01;
        Combo_Libery_Preset[100101] = C100101_InterCom_BalN02;
        Combo_Libery_Preset[100011] = C100011_InterCom_BalN03;
        Combo_Libery_Preset[100111] = C100111_InterCom_BalN04;
        if(Is_UsingPreset){

        }
        else{

        }

    }

    #endregion

    #region PatternList

    public Pattern P000000_StandStill = new Pattern(new float[][] { // Run up, Nattack once, and Run away

        new float[] {  901, 0, 0      ,0      ,5      ,0}
    });
    public Pattern P000000_SuperBasicPat_BalN1 = new Pattern(new float[][] { // Run up, Nattack once, and Run away

        new float[] { 101, 0,   0, 000001 ,0.3f      ,0 }
    });
    public Pattern P000001_BasicPat_BalN1 = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] {991    ,0      , 1      ,0      ,0      ,0},
        new float[] { 901, 0, 2      ,4      ,4      ,0},
        new float[] { 101, 0,0       ,000001 ,0.3f         ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });
    public Pattern P000002_BasicPat_BalN2 = new Pattern(new float[][] { // Run up, Nattack twice, and Run away
        new float[] { 991, 0, 1      ,0      ,0      ,0},
        new float[] { 901, 0, 1      ,4      ,4      ,0},
        new float[] { 101, 0, 0      ,000002 ,0.4f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });
    public Pattern P000003_BasicPat_BalN3 = new Pattern(new float[][] { // Run up, Nattack thirce, and Run away
        new float[] { 991, 0, 1      ,0      ,0      ,0},
        new float[] { 901, 0, 3      ,0      ,1      ,0},
        new float[] { 901, 0, 8      ,4      ,4      ,0},
        new float[] { 101, 0, 0      ,000003 ,0.4f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });

    public Pattern P010001_BasicPat_BlockBalN1 = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] {991    ,0      , 1      ,0      ,0      ,0},
        new float[] { 901, 0, 2      ,4      ,4      ,0},
        new float[] { 101, 0, 0      ,000001 ,0.3f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });

    public Pattern P090012_BasicPat_BalN2Twice = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 1      ,0      ,0      ,0},
        new float[] { 901, 0, 2      ,4      ,4      ,0},
        new float[] { 101, 0, 0      ,000002 ,0.3f   ,0 },
        new float[] { 101, 0, 0      ,000002 ,0.05f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });

    public Pattern P000011_BasicPat_HevN1 = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 2      ,0      ,0      ,0},
        new float[] { 901, 0, 2      ,4      ,4      ,0},
        new float[] { 101, 0, 0      ,000201 ,0.3f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });
    public Pattern P000012_BasicPat_HevN2 = new Pattern(new float[][] { // Run up, Nattack twice, and Run away
        new float[] { 991, 0, 2      ,0      ,0      ,0},
        new float[] { 901, 0, 1      ,4      ,4      ,0},
        new float[] { 101, 0, 0      ,000202 ,0.4f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });
    public Pattern P000013_BasicPat_HevN3 = new Pattern(new float[][] { // Run up, Nattack thirce, and Run away
        new float[] { 991, 0, 2      ,0      ,0      ,0},
        new float[] { 901, 0, 3      ,0      ,1      ,0},
        new float[] { 901, 0, 8      ,4      ,4      ,0},
        new float[] { 101, 0, 0      ,000203 ,0.5f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });

    public Pattern P000021_BasicPat_QukN1 = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 3      ,0      ,0      ,0},
        new float[] { 901, 0, 2      ,4      ,4      ,0},
        new float[] { 101, 0, 0      ,000401 ,0.1f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });
    public Pattern P000022_BasicPat_QukN2 = new Pattern(new float[][] { // Run up, Nattack twice, and Run away
        new float[] { 991, 0, 3      ,0      ,0      ,0},
        new float[] { 901, 0, 1      ,4      ,4      ,0},
        new float[] { 101, 0, 0      , 000402 ,0.09f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });
    public Pattern P000023_BasicPat_QukN3 = new Pattern(new float[][] { // Run up, Nattack thirce, and Run away
        new float[] { 991, 0, 3      ,0      ,0      ,0},
        new float[] { 901, 0, 3      ,0      ,1      ,0},
        new float[] { 901, 0, 8      ,4      ,4      ,0},
        new float[] { 101, 0, 0      ,000403 ,0.05f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });

    public Pattern P090001_BasicPat_TeleportBalN1 = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 1      ,0      ,0      ,0},
        new float[] { 101, 0, 0      ,090001 ,0.1f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });
    public Pattern P040001_BasicPat_BalN1Dodge = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 1      ,0      ,0      ,0},
        new float[] { 901, 0, 2      ,4      ,4      ,0},
        new float[] { 101, 0, 0      ,040001 ,0.1f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });





    public Pattern P040002_BasicPat_DodgeBalN1 = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 1      ,0      ,0      ,0},
        new float[] { 901, 0, 8      ,4      ,4      ,0},
        new float[] { 901, 0, 0      ,10000     , 0.05f, 0},
        new float[] { 101, 0, 0      ,040002 ,0.1f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });

    public Pattern P040101_BasicPat_BlockBalN1 = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 1      ,0      ,0      ,0},
        new float[] { 901, 0, 2      ,4      ,4      ,0},
        new float[] { 101, 0, 0      ,040101, 0.1f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });

    public Pattern P040102_BasicPat_BlockBalN2 = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 1      ,0      ,0      ,0},
        new float[] { 901, 0, 2      ,4      ,4      ,0},
        new float[] { 101, 0, 0      ,040102, 0.1f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });

    public Pattern P100002_InterPat_Bal1 = new Pattern(new float[][] { // Run up, Nattack twice, and Run away
        new float[] { 901, 0, 2      ,3.5f     ,0.5f      ,0}, // Move Zig zag toward opponent
        new float[] { 901, 0, 8      ,3.5f      , 0.5f, 0},
        new float[] { 901, 0, 2      ,3.5f      , 0.5f, 0},
        new float[] { 901, 0, 8      ,3.5f      , 0.5f, 0},
        new float[] { 901, 0, 2      ,3.5f      , 0.5f, 0},
        new float[] { 901, 0, 8      ,3.5f      , 0.5f, 0},

        new float[] { 101, 0, 0      ,100001 ,0.1f   ,0 },
        new float[] { 101, 0, 0      ,100111, 0.1f   ,0},
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
    });

    public Pattern P010001_BasicPat_BalAtkMoveinBetw1 = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 1      ,0      ,0      ,0},
        new float[] { 901, 0, 2      ,3      ,4      ,0},
        new float[] { 101, 0, 0      ,000001 ,0.2f   ,0 },
        new float[] { 901, 0, 7      ,0      ,1.0f      ,0},
          new float[] { 101, 0, 0      ,000002 ,0.2f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
         });

    public Pattern P010002_BasicPat_BalAtkMoveinBetw2 = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 1      ,0      ,0      ,0},
        new float[] { 901, 0, 8      ,3      ,4      ,0},
        new float[] { 101, 0, 0      ,000001 ,0.2f   ,0 },
        new float[] { 901, 0, 3      ,0      ,1.0f      ,0},
          new float[] { 101, 0, 0      ,000002 ,0.2f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
         });
    public Pattern P010201_BasicPat_HevAtkMoveinBetw1 = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 2      ,0      ,0      ,0},
        new float[] { 901, 0, 1      ,3      ,4      ,0},
        new float[] { 101, 0, 0      ,000201 ,0.3f   ,0 },
        new float[] { 901, 0, 6      ,0      ,1.0f      ,0},
          new float[] { 101, 0, 0      ,000202 ,0.3f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
         });
    public Pattern P010202_BasicPat_HevAtkMoveinBetw2 = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 2      ,0      ,0      ,0},
        new float[] { 901, 0, 1      ,3      ,4      ,0},
        new float[] { 101, 0, 0      ,000201 ,0.3f   ,0 },
        new float[] { 901, 0, 3      ,0      ,1.0f      ,0},
          new float[] { 101, 0, 0      ,000202 ,0.3f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
         });
    public Pattern P010401_BasicPat_QukAtkMoveinBetw1 = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 3      ,0      ,0      ,0},
        new float[] { 901, 0, 3      ,3      ,1f      ,0},
         new float[] { 901, 0, 2      ,3      ,4      ,0},
        new float[] { 101, 0, 0      ,000402 ,0.2f   ,0 },
        new float[] { 901, 0, 1     ,0      ,1.0f      ,0},
          new float[] { 101, 0, 0      ,000403 ,0.2f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
         });
    public Pattern P010402_BasicPat_QukAtkMoveinBetw2 = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 3      ,0      ,0      ,0},
        new float[] { 901, 0, 7      ,3      ,1f      ,0},
         new float[] { 901, 0, 8      ,3      ,4      ,0},
        new float[] { 101, 0, 0      ,000402 ,0.2f   ,0 },
        new float[] { 901, 0, 1     ,0      ,1.0f      ,0},
          new float[] { 101, 0, 0      ,000403 ,0.2f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
         });

    public Pattern P011001_BasicPat_BalAtkMoveinBetw1Fast = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 1      ,0      ,0      ,0},
        new float[] { 901, 0, 2      ,3      ,4      ,0},
        new float[] { 101, 0, 0      ,000001 ,0.05f   ,0 },
        new float[] { 901, 0, 7      ,0      ,1.0f      ,0},
          new float[] { 101, 0, 0      ,000002 ,0.05f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
         });

    public Pattern P011002_BasicPat_BalAtkMoveinBetw2Fast = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 1      ,0      ,0      ,0},
        new float[] { 901, 0, 8      ,3      ,4      ,0},
        new float[] { 101, 0, 0      ,000001 ,0.05f   ,0 },
        new float[] { 901, 0, 3      ,0      ,1.0f      ,0},
          new float[] { 101, 0, 0      ,000002 ,0.05f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
         });
    public Pattern P011201_BasicPat_HevAtkMoveinBetw1Fast = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 2      ,0      ,0      ,0},
        new float[] { 901, 0, 1      ,3      ,4      ,0},
        new float[] { 101, 0, 0      ,000201 ,0.1f   ,0 },
        new float[] { 901, 0, 6      ,0      ,1.0f      ,0},
          new float[] { 101, 0, 0      ,000202 ,0.1f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
         });
    public Pattern P011202_BasicPat_HevAtkMoveinBetw2Fast = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 2      ,0      ,0      ,0},
        new float[] { 901, 0, 1      ,3      ,4      ,0},
        new float[] { 101, 0, 0      ,000201 ,0.1f   ,0 },
        new float[] { 901, 0, 3      ,0      ,1.0f      ,0},
          new float[] { 101, 0, 0      ,000202 ,0.1f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
         });
    public Pattern P011401_BasicPat_QukAtkMoveinBetw1Fast = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 3      ,0      ,0      ,0},
        new float[] { 901, 0, 3      ,3      ,1f      ,0},
         new float[] { 901, 0, 2      ,3      ,4      ,0},
        new float[] { 101, 0, 0      ,000402 ,0.03f   ,0 },
        new float[] { 901, 0, 1     ,0      ,1.0f      ,0},
          new float[] { 101, 0, 0      ,000403 ,0.03f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
         });
    public Pattern P011402_BasicPat_QukAtkMoveinBetw2Fast = new Pattern(new float[][] { // Run up, Nattack once, and Run away
        new float[] { 991, 0, 3      ,0      ,0      ,0},
        new float[] { 901, 0, 7      ,3      ,1f      ,0},
         new float[] { 901, 0, 8      ,3      ,4      ,0},
        new float[] { 101, 0, 0      ,000402 ,0.03f   ,0 },
        new float[] { 901, 0, 1     ,0      ,1.0f      ,0},
          new float[] { 101, 0, 0      ,000403 ,0.03f   ,0 },
        new float[] { 900, 0, 5      ,0      ,1.5f   ,0}
         });


    public Pattern P090701_Running_Left = new Pattern(new float[][] { // Run up, Nattack twice, and Run away

        new float[] { 900, 0, 3      ,0     ,1.5f     ,0}
    });

    public Pattern P090000_StandStill = new Pattern(new float[][] { // Run up, Nattack twice, and Run away

        new float[] { 900, 0, 0      ,0     ,1.5f     ,0}
    });
    public Pattern P090001_StandStill0_75sec = new Pattern(new float[][] { // Run up, Nattack twice, and Run away

        new float[] { 900, 0, 0      ,0     ,0.75f     ,0}
    });

    public Pattern P200001_DodgeBackward = new Pattern(new float[][] {

        new float[] { 101, 1, 0      ,40105 ,0.1f   ,0 }
    });
    public Pattern P200002_DodgeBackwardBN4 = new Pattern(new float[][] {
        new float[] { 101, 1, 0      ,40115 ,0.3f   ,0 }
    });
    public Pattern P200003_DodgeBackwardHN1 = new Pattern(new float[][] {
        new float[] { 101, 1, 0      ,40121 ,0.05f   ,0 }
    });
    public Pattern P200021_DodgeBackwardVer2 = new Pattern(new float[][] {

        new float[] { 101, 1, 0      ,40105 ,0.1f   ,0 }
    });



    public void InitializePatternStart()
    {
        Pattern_Libery_Preset[0] = P000000_StandStill;
        Pattern_Libery_Preset[1] = P000001_BasicPat_BalN1;
        Pattern_Libery_Preset[2] = P000002_BasicPat_BalN2;
        Pattern_Libery_Preset[3] = P000003_BasicPat_BalN3;
        Pattern_Libery_Preset[11] = P000011_BasicPat_HevN1;
        Pattern_Libery_Preset[12] = P000012_BasicPat_HevN2;
        Pattern_Libery_Preset[13] = P000013_BasicPat_HevN3;
        Pattern_Libery_Preset[21] = P000021_BasicPat_QukN1;
        Pattern_Libery_Preset[22] = P000022_BasicPat_QukN2;
        Pattern_Libery_Preset[23] = P000023_BasicPat_QukN3;
        Pattern_Libery_Preset[10001] = P010001_BasicPat_BalAtkMoveinBetw1;
        Pattern_Libery_Preset[10002] = P010002_BasicPat_BalAtkMoveinBetw2;
        Pattern_Libery_Preset[10201] = P010201_BasicPat_HevAtkMoveinBetw1;
        Pattern_Libery_Preset[10202] = P010202_BasicPat_HevAtkMoveinBetw2;
        Pattern_Libery_Preset[10401] = P010401_BasicPat_QukAtkMoveinBetw1;
        Pattern_Libery_Preset[10402] = P010402_BasicPat_QukAtkMoveinBetw2;
        Pattern_Libery_Preset[11001] = P011001_BasicPat_BalAtkMoveinBetw1Fast;
        Pattern_Libery_Preset[11002] = P011002_BasicPat_BalAtkMoveinBetw2Fast;        
        Pattern_Libery_Preset[11201] = P011201_BasicPat_HevAtkMoveinBetw1Fast;
        Pattern_Libery_Preset[11202] = P011202_BasicPat_HevAtkMoveinBetw2Fast;
        Pattern_Libery_Preset[11401] = P011401_BasicPat_QukAtkMoveinBetw1Fast;
        Pattern_Libery_Preset[11402] = P011402_BasicPat_QukAtkMoveinBetw2Fast;
        Pattern_Libery_Preset[90000] = P090000_StandStill;
        Pattern_Libery_Preset[90001] = P090001_StandStill0_75sec;
        Pattern_Libery_Preset[90701] = P090701_Running_Left;
        Pattern_Libery_Preset[200001] = P200001_DodgeBackward;
        Pattern_Libery_Preset[200002] = P200002_DodgeBackwardBN4;
        Pattern_Libery_Preset[200003] = P200003_DodgeBackwardHN1;



        if(Is_UsingPreset){
            CurrentAI.Pattern_Libery = new List<Pattern>(Pattern_Libery_Preset);
        }
        else{
            //CurrentAI.Pattern_Libery = new List<Pattern>(Pattern_Libery_Preset);
            /*
            CurrentAI.Pattern_Libery[0] = P000000_StandStill;
            CurrentAI.Pattern_Libery[1] = P000001_BasicPat_BalN1;
            CurrentAI.Pattern_Libery[2] = P000002_BasicPat_BalN2;
            CurrentAI.Pattern_Libery[3] = P000003_BasicPat_BalN3;
            */
        }
        //CurrentAI.Pattern_Libery = Pattern_Libery;
    }
    ////////////////////////////////////// Current Pattern List /////////////////////////////////////
    public Pattern[] Pattern_List;
    public React_Command[] React_Command_List = new React_Command[1];
    public Pattern[] RVBPattern_List;

    public enum AIPattern
    {
        P0_StandStill,
        P01_SuperBasic,
        P11_RunUpAndAttackOnce,
        P12_RunUpAndAttackOnceAndTwice,
        P13_TeleportTest,
        P14_TwoPatternTest,
        P15_DodgeTest,
        P16_BlockTest,
        P1001_Beginer_Pattern1_B,
        P1002_Beginer_Pattern2_H,
        P1003_Beginer_Pattern3_Q,
        P1011_Beginer_PatternPureOff,
        P1101_Basic_1_SlowCombo,
        P1102_Basic_2_FastCombo,
        P9001_TEST_RunLeft,
        P9002_TEST_RunLeftStop,

        RVB200001_DodgeBack,
        RVB200002_DodgeBackBN4,
        RVB200003_DodgeBackHN1,
        P10001_Inter_1_AllAround_Off_Block_Dodge
    }
    public bool UseEditor;
    public AIPattern Chosen_AI = AIPattern.P11_RunUpAndAttackOnce;
    public AIPattern Chosen_RCT = AIPattern.RVB200002_DodgeBackBN4;
    public AIPattern Chosen_RVB = AIPattern.RVB200001_DodgeBack;

    private Pattern[] P0_StandStill;
    private Pattern[] P01_SuperBasic;
    private Pattern[] P11_RunUpAndAttackOnce;
    private Pattern[] P12_RunUpAndAttackOnceAndTwice;
    private Pattern[] P13_TeleportTest; // Still broken when cancel speed is too low
    private Pattern[] P14_TwoPatternTest;
    private Pattern[] P15_DodgeTest;
    private Pattern[] P16_BlockTest;
    private Pattern[] P1001_Beginer_Pattern1_B;
    private Pattern[] P1002_Beginer_Pattern2_H;
    private Pattern[] P1003_Beginer_Pattern3_Q;
    private Pattern[] P1011_Beginer_PatternPureOff;
    private Pattern[] P1101_Basic_1_SlowCombo;
    private Pattern[] P1102_Basic_2_FastCombo;
    private Pattern[] P9001_TEST_RunLeft;
    private Pattern[] P9002_TEST_RunLeftStop;
    private Pattern[] RVB200001_DodgeBack;
    private Pattern[] RVB200002_DodgeBackBN4;
    private Pattern[] RVB200003_DodgeBackHN1;

    private Pattern[] P10001_Inter_1_AllAround_Off_Block_Dodge;

   
    public void InitializePatternSetStart()
    {
        PatternSet_Libery_Preset[0] = new PatternSet(P0_StandStill);
        
        PatternSet_Libery_Preset[1] = new PatternSet(P01_SuperBasic);
        PatternSet_Libery_Preset[11] = new PatternSet(P11_RunUpAndAttackOnce);
        PatternSet_Libery_Preset[12] = new PatternSet(P12_RunUpAndAttackOnceAndTwice);
        PatternSet_Libery_Preset[13] = new PatternSet(P13_TeleportTest);
        PatternSet_Libery_Preset[14] = new PatternSet(P14_TwoPatternTest);
        PatternSet_Libery_Preset[15] = new PatternSet(P15_DodgeTest);
        PatternSet_Libery_Preset[16] = new PatternSet(P16_BlockTest);
        PatternSet_Libery_Preset[1001] = new PatternSet(P1001_Beginer_Pattern1_B);
        PatternSet_Libery_Preset[1002] = new PatternSet(P1002_Beginer_Pattern2_H);
        PatternSet_Libery_Preset[1003] = new PatternSet(P1003_Beginer_Pattern3_Q);
        PatternSet_Libery_Preset[1011] = new PatternSet(P1011_Beginer_PatternPureOff);
        PatternSet_Libery_Preset[1101] = new PatternSet(P1101_Basic_1_SlowCombo);
        PatternSet_Libery_Preset[1102] = new PatternSet(P1102_Basic_2_FastCombo);
        PatternSet_Libery_Preset[9001] = new PatternSet(P9001_TEST_RunLeft);
        PatternSet_Libery_Preset[9002] = new PatternSet(P9002_TEST_RunLeftStop);
        PatternSet_Libery_Preset[200001] = new PatternSet(RVB200001_DodgeBack);
        PatternSet_Libery_Preset[200002] = new PatternSet(RVB200002_DodgeBackBN4);
        PatternSet_Libery_Preset[200003] = new PatternSet(RVB200003_DodgeBackHN1);
        PatternSet_Libery_Preset[10001] = new PatternSet(P10001_Inter_1_AllAround_Off_Block_Dodge);
        

        if(Is_UsingPreset){
            PatternSet_Libery =  new List<PatternSet>(PatternSet_Libery_Preset);
        }
        else{
            //PatternSet_Libery =  new List<PatternSet>(PatternSet_Libery_Preset);
            /*
            PatternSet_Libery[0] = new PatternSet(P0_StandStill,new int[] {0});
            PatternSet_Libery[1] = new PatternSet(P01_SuperBasic,new int[] {1});
            PatternSet_Libery[2] = new PatternSet(P11_RunUpAndAttackOnce,new int[] {11});
            PatternSet_Libery[3] = new PatternSet(P12_RunUpAndAttackOnceAndTwice,new int[] {12});
            PatternSet_Libery[3] = new PatternSet(P12_RunUpAndAttackOnceAndTwice,new int[] {12});
            */
        }
    }
    
    public void Pattern_List_Set()
    {

        P0_StandStill = new Pattern[] { P000000_StandStill };

        P01_SuperBasic = new Pattern[] { P000000_SuperBasicPat_BalN1 }; 
        P11_RunUpAndAttackOnce = new Pattern[] { P000001_BasicPat_BalN1};
        P12_RunUpAndAttackOnceAndTwice = new Pattern[] { P000001_BasicPat_BalN1, P000002_BasicPat_BalN2 };
        P13_TeleportTest = new Pattern[] { P090001_BasicPat_TeleportBalN1 };
        P14_TwoPatternTest = new Pattern[] { P090012_BasicPat_BalN2Twice };
        P15_DodgeTest = new Pattern[] { P040001_BasicPat_BalN1Dodge, P040002_BasicPat_DodgeBalN1 };
        P16_BlockTest = new Pattern[] { P040101_BasicPat_BlockBalN1, P040102_BasicPat_BlockBalN2 };
        P1001_Beginer_Pattern1_B = new Pattern[] { P000001_BasicPat_BalN1, P000002_BasicPat_BalN2, P000003_BasicPat_BalN3 };
        P1002_Beginer_Pattern2_H = new Pattern[] { P000011_BasicPat_HevN1, P000012_BasicPat_HevN2, P000013_BasicPat_HevN3 };
        P1003_Beginer_Pattern3_Q = new Pattern[] { P000023_BasicPat_QukN3, P000021_BasicPat_QukN1, P000022_BasicPat_QukN2, P000023_BasicPat_QukN3 };
        P1011_Beginer_PatternPureOff = new Pattern[] { P000002_BasicPat_BalN2, P000003_BasicPat_BalN3, P000012_BasicPat_HevN2, P000013_BasicPat_HevN3, P000022_BasicPat_QukN2, P000023_BasicPat_QukN3 };

        P1101_Basic_1_SlowCombo = new Pattern[] {
                P010001_BasicPat_BalAtkMoveinBetw1, P090001_StandStill0_75sec
                , P010002_BasicPat_BalAtkMoveinBetw2 , P090001_StandStill0_75sec
                ,P010201_BasicPat_HevAtkMoveinBetw1 , P090001_StandStill0_75sec
                ,P010202_BasicPat_HevAtkMoveinBetw2, P090001_StandStill0_75sec
                ,P010401_BasicPat_QukAtkMoveinBetw1, P090001_StandStill0_75sec
                ,P010402_BasicPat_QukAtkMoveinBetw2, P090001_StandStill0_75sec};
        P1102_Basic_2_FastCombo = new Pattern[] {
                P011001_BasicPat_BalAtkMoveinBetw1Fast, P090001_StandStill0_75sec
                , P011002_BasicPat_BalAtkMoveinBetw2Fast , P090001_StandStill0_75sec
                ,P011201_BasicPat_HevAtkMoveinBetw1Fast , P090001_StandStill0_75sec
                ,P011202_BasicPat_HevAtkMoveinBetw2Fast, P090001_StandStill0_75sec
                ,P011401_BasicPat_QukAtkMoveinBetw1Fast, P090001_StandStill0_75sec
                ,P011402_BasicPat_QukAtkMoveinBetw2Fast, P090001_StandStill0_75sec};
        P9001_TEST_RunLeft = new Pattern[] { P090701_Running_Left };
        P9002_TEST_RunLeftStop = new Pattern[] { P090701_Running_Left, P090000_StandStill };

        RVB200001_DodgeBack = new Pattern[] { P200001_DodgeBackward };
        RVB200002_DodgeBackBN4 = new Pattern[] { P200002_DodgeBackwardBN4 /*, P200001_DodgeBackward, P200002_DodgeBackwardBN4*/ };
        RVB200003_DodgeBackHN1 = new Pattern[] { P200003_DodgeBackwardHN1 };
        //Pattern_List = new Pattern[] { P090000_StandStill };

        P10001_Inter_1_AllAround_Off_Block_Dodge = new Pattern[] {
                P010001_BasicPat_BalAtkMoveinBetw1, P090001_StandStill0_75sec
                , P010002_BasicPat_BalAtkMoveinBetw2 , P090001_StandStill0_75sec
                ,P010201_BasicPat_HevAtkMoveinBetw1 , P090001_StandStill0_75sec
                ,P010202_BasicPat_HevAtkMoveinBetw2, P090001_StandStill0_75sec
                ,P010401_BasicPat_QukAtkMoveinBetw1, P090001_StandStill0_75sec
                ,P010402_BasicPat_QukAtkMoveinBetw2, P090001_StandStill0_75sec};
    }
    public void Choose_Pattern_InEditor(int AI0_RCT1_RVB2)
    {
        Pattern[] choose = P11_RunUpAndAttackOnce;
        AIPattern chooseCase = Chosen_AI;
        if (AI0_RCT1_RVB2 == 0)
            chooseCase = Chosen_AI;
        else if (AI0_RCT1_RVB2 == 1)
            chooseCase = Chosen_RCT;

            
        else if (AI0_RCT1_RVB2 == 2)
            chooseCase = Chosen_RVB;

        switch (chooseCase)
        {
            case AIPattern.P0_StandStill:
                choose = P0_StandStill;
                break;
                
            case AIPattern.P01_SuperBasic:
                choose = P01_SuperBasic;
            break;
            case AIPattern.P11_RunUpAndAttackOnce:
                choose = P11_RunUpAndAttackOnce;
                break;
            case AIPattern.P12_RunUpAndAttackOnceAndTwice:
                choose = P12_RunUpAndAttackOnceAndTwice;
                break;
            case AIPattern.P13_TeleportTest:
                choose = P13_TeleportTest;
                break;
            case AIPattern.P14_TwoPatternTest:
                choose = P14_TwoPatternTest;
                break;
            case AIPattern.P15_DodgeTest:
                choose = P15_DodgeTest;
                break;
            case AIPattern.P16_BlockTest:
                choose = P16_BlockTest;
                break;
            case AIPattern.P1001_Beginer_Pattern1_B:
                choose = P1001_Beginer_Pattern1_B;
                break;
            case AIPattern.P1002_Beginer_Pattern2_H:
                choose = P1002_Beginer_Pattern2_H;
                break;
            case AIPattern.P1003_Beginer_Pattern3_Q:
                choose = P1003_Beginer_Pattern3_Q;
                break;
            case AIPattern.P1011_Beginer_PatternPureOff:
                choose = P1011_Beginer_PatternPureOff;
                break;
            case AIPattern.P1101_Basic_1_SlowCombo:
                choose = P1101_Basic_1_SlowCombo;
                break;
            case AIPattern.P1102_Basic_2_FastCombo:
                choose = P1102_Basic_2_FastCombo;
                break;
            case AIPattern.P9001_TEST_RunLeft:
                choose = P9001_TEST_RunLeft;
                break;
            case AIPattern.P9002_TEST_RunLeftStop:
                choose = P9002_TEST_RunLeftStop;
                break;
            case AIPattern.RVB200001_DodgeBack:
                choose = RVB200001_DodgeBack;
                break;
            case AIPattern.RVB200002_DodgeBackBN4:
                choose = RVB200002_DodgeBackBN4;
                break;
            case AIPattern.RVB200003_DodgeBackHN1:
                choose = RVB200003_DodgeBackHN1;
                break;
            case AIPattern.P10001_Inter_1_AllAround_Off_Block_Dodge:
                choose = P10001_Inter_1_AllAround_Off_Block_Dodge;
                break;
        }
        if (AI0_RCT1_RVB2 == 0){
            Pattern_List = choose;
            CurrentAI.curPatternSet = new PatternSet(Pattern_List, new int[] {1,2,1,1} );
            
            //NormalPatternSet = new PatternSet(Pattern_List);
        }

        else if (AI0_RCT1_RVB2 == 1)
            //ReactPattern_List = choose;
            React_Command_List[0] = new React_Command(new PatternSet(choose),new int[] {20011,20111,22011}, 0.1f,5f);
        else if (AI0_RCT1_RVB2 == 2)
            //RVBPattern_List = choose;
            CurrentAI.Revenge_Command_Libery=  new List<Pattern>(choose);
    }
    public void Choose_Pattern_FromSystem()
    {
        //CurrentAI.curPatternSet = setPatternListFromCode(Pattern_Libery_Preset, CurrentAI.curPatternSet);
        Pattern_List = CurrentAI.curPatternSet.Get_patternList();
        //NormalPatternSet = new PatternSet(Pattern_List);
        RVBPattern_List = CurrentAI.Revenge_Command_Libery.ToArray();
        React_Command_List = CurrentAI.React_Command_Libery.ToArray();
        ReactiveCooldownTimerList = new float[React_Command_List.Length];
    }




    #endregion

    #region Revenge Value Mechanic // RVB = Revenge Value Break 
    

   

    #endregion

    // Use this for initialization
    // Use this for initialization

    #region Start

    void Start()
    {
        
        if(!character.Is_Player_or_Enemy || character.Is_P_Enemy){
                Pattern_List_Set();
            InitializeComboStart();
            InitializePatternStart();
            InitializePatternSetStart();
            //Pattern_List_Set();
            InitializeReact_CommandStart();
            InitializeRevenge_CommandStart();
            if(UseEditor){
                Choose_Pattern_InEditor(0);
                Choose_Pattern_InEditor(1);
                Choose_Pattern_InEditor(2);
                print("FIN");
            }
            else{
                //Choose_Pattern_FromSystem();
            }
            //SystemToCurrentAI();
            /*
            if (character.Is_P_Enemy)
            {
                if (Main_Menu.LoadFile_P > 0)
                {
                    fileLoadNumber = Main_Menu.LoadFile_P;
                }
                if (Main_Menu.SaveFile_P > 0)
                {
                    fileSaveNumber = Main_Menu.SaveFile_P;
                }

            }
            else
            {
                if (Main_Menu.LoadFile_E > 0)
                {
                    fileLoadNumber = Main_Menu.LoadFile_E;
                }
                if (Main_Menu.SaveFile_E > 0)
                {
                    fileSaveNumber = Main_Menu.SaveFile_E;
                }
            }
            if (Main_Menu.GameMode == 1)
            {
                if (!character.Is_P_Enemy)
                {
                    if (Main_Menu.ScoreMode == 1) AI_mode = AI_Mode_List.True_Dynamic_4_Train_Against_Dodge;
                    else if (Main_Menu.ScoreMode == 2) AI_mode = AI_Mode_List.Enemy_Training_Dodge_1;
                }
            }
            if (Main_Menu.GameMode == 2)
            {
                if (!character.Is_P_Enemy)
                {
                    if (Main_Menu.ScoreMode == 1)
                    {
                        AI_mode = AI_Mode_List.True_Dynamic_4_Train_Against_Dodge;
                        print("YESSSSSSSSSs");
                    }
                    else if (Main_Menu.ScoreMode == 2) AI_mode = AI_Mode_List.Enemy_Training_Dodge_1;
                }
                else
                {
                    if (Main_Menu.ScoreMode == 1) AI_mode = AI_Mode_List.Enemy_Training_Dodge_1;
                    else if (Main_Menu.ScoreMode == 2) AI_mode = AI_Mode_List.True_Dynamic_4_Train_Against_Dodge;
                }
            }
            Round_Number = 0;
            i = 0;
            MovetimeMax = 1.9f;
            MovetimeMin = 1.4f;
            if (fileLoadNumber == 0)
                Dynamic_State_AI_Start();
            //Write_Save_File();
            ///////////////////////////////// Load Part ///////////////////
            else
                Read_Load_File();
            ///////////////////////////////// Load Part ///////////////////
            ///
            */
        } 
       
    }

    #endregion

    #region Update

    

    // Update is called once per frame
    void Update()
    {
        /*
            foreach(var frame in CurrentAI.Pattern_Libery[0].Get_movePattern()[0]){
                print(frame);
            }
        */
        if(!character.Is_Player_or_Enemy || character.Is_P_Enemy){
                //TIME = Time.deltaTime;
            TIME_ACCU = Time.realtimeSinceStartup;
            float TIME_IN_THIS_FRAME = 0;
            int i = 1;
            TIME_IN_THIS_10thFRAME = 0;
            float TIME_SAVE_FROM1TO10 = 0;
            while(i <= 10)
            {
                if(Attack_Time_Compare_cancel_time != 0) print("Attack_Time_Compare_cancel_time  =  " + Attack_Time_Compare_cancel_time);
                TIME_IN_THIS_FRAME = Time.realtimeSinceStartup - TIME_ACCU;
                //print("TIME_IN_THIS_FRAME = " + TIME_IN_THIS_FRAME);
                if ( (TIME_IN_THIS_FRAME) > (1f * i / 600f))
                {
                    TIME_IN_THIS_10thFRAME = TIME_IN_THIS_FRAME - TIME_SAVE_FROM1TO10;
                    TIME_SAVE_FROM1TO10 += TIME_IN_THIS_10thFRAME;
                    //print("Divide = " + i + "  TIME_IN_THIS_10thFRAME = " + TIME_IN_THIS_10thFRAME) ;
                    OneUpdateCycle();
                    i++;
                }
            }
        }
        




    }

    public float current_Reactive_Timer = 0;       
    public float ReactiveCooldownValue= 2.0f;  
    public float ReactiveCooldownTimer; 
    public float[]  ReactiveCooldownTimerList = new float[3];
    public float TimerCallReactiveAction;
    private bool IsCallReactiveAction;
    private int IsCallReactiveActionCode;
    private int[,] ReactiveActionToOppoActList = new int[100,10] ;
    private float[] ReactiveActionOppoActRange;
    private void OneUpdateCycle()
    {
        character.Call_Movement_For_AIControl(moveX, moveZ);
        //if (AI_mode == AI_Mode_List.SimpleAI_1) Fixed_AI_1();
         if (AI_mode == AI_Mode_List.ReactiveAI_1) Reactive_AI_1();
        /// When Character Died/Level up case, reset everything and count action to 2 second
        if (character.Get_Is_Disable_Control())
        {
            wait = 0;
            Current_Cancel_Wait = 0;
            //Action_Count = -2.0f;
            Action_Count = -0.8f;
            Move_Code_0_to_8_9isRandom(0);
            Call_Action_Sequence = false;
            character.Set_Is_InCombo(false);
            character.Get_Is_ComboAble_CheckFromActionCalled();
            IsCallReactiveAction = false;
            TimerCallReactiveAction = 0;
            ReactiveCooldownTimer= 0;
        }
        // When Revenge Value Hit, interrupt and perform revenge action
        if (characterStat.IsHitRVvalue())
        {
            print("RESET RV");
            EnterRevengePattern = true;
            InRevengePattern = true;
            HitRvSignal = true;
            characterStat.ResetRV();
            Call_Next_Pattern = true;
            Action_Count = 0;
            //SavePatternCount();
        }
        // When Reactive action is called, Delay timer hit 0, character isn't hurt, and not performing action. Start reactive action.
        else if (IsCallReactiveAction && TimerCallReactiveAction <=0 && !character.Get_Is_Hurt_Total() && !character.Is_Performing_Action)
        {
            print("REACT");
            Cancel();
            wait = 0;
            EnterReactivePattern = true;
            IsCallReactiveAction = false;
            TimerCallReactiveAction = 0;
            InReactivePattern = true;
            InReactiveAction = true;
            HitReactiveSignal = true;
            Call_Next_Pattern = true;
            //Action_Count = 0;

            //SavePatternCount();
        }



        //// When Performing action, stop the clock to not called next action
        if (character.Is_Performing_Action)
        {
            Is_Waiting_For_Action = false;
        }

       //if(character.Get_Is_ComboAble_Real()) print("character.Get_Is_ComboAble_Real() =  " + character.Get_Is_ComboAble_Real());
        if (Current_Cancel_Wait > 0 && ( character.Get_Is_ComboAble_Real() || (!character.Get_Is_ComboAble_Real() && !character.Is_Performing_Action && ActionInCombo_Count_Signal > 0)))
        {
            //Current_Cancel_Wait -= TIME;
            Current_Cancel_Wait -= TIME_IN_THIS_10thFRAME;
            //print("Current_Cancel_Wait" + Current_Cancel_Wait + "   Get_Is_ComboAble_Real = " + character.Get_Is_ComboAble_Real());
        }
        else if (wait > 0.00 && !character.Is_Performing_Action && !character.Get_Is_Disable_Control())
        {

            //wait -= TIME;
            wait -= TIME_IN_THIS_10thFRAME;
            
        }

        else if (!character.Is_Performing_Action && !Is_Waiting_For_Action && !character.Get_Is_InCombo() && !character.Get_Is_Disable_Control())
        {
            //Action_Count += TIME;
            Action_Count += TIME_IN_THIS_10thFRAME;

        }
        else
        {
            bool checkEnter = false;
        }

        ////Reactive Timer
        if(TimerCallReactiveAction > 0 && IsCallReactiveAction ){
            TimerCallReactiveAction -= TIME_IN_THIS_10thFRAME;
        }
        if(ReactiveCooldownTimer > 0 ){
            ReactiveCooldownTimer -= TIME_IN_THIS_10thFRAME;
        }
        


        if (character.Get_Is_Hurt_Total())
        {
            //Countdown_Check(false);
            //if(ActionCode ==)
            Actioncode = 0;
        }


        AI_Called_Action();


    }


    #endregion

    private int phase_Number;


    public int current_Pattern_Number = 0;             // Check Pattern Number
    public int current_ActionInPattern_Number = 99999;    // Check Action count in Pattern Number
    public bool EnterRevengePattern = false;
    public bool InRevengePattern = false;
    public bool HitRvSignal = false;
    public bool HitReactiveSignal = false;
    public bool EnterReactivePattern = false;
    public bool InReactivePattern = false;
    public bool InReactiveAction = false;
    public int current_RVBPattern_Number = 0;             // Check Pattern Number (for Revenge Attack)
    public int current_RVBActionInPattern_Number = 0;    // Check Action count in Pattern Number (for Revenge Attack)

    public int current_ReactivePattern_Number = 0;             // Check Pattern Number (for Reactive Attack)
    public int current_ReactiveActionInPattern_Number = 0;    // Check Action count in Pattern Number (for Reactive Attack)

    private void ResetPatternVariable(){
        current_Pattern_Number = 0;             // Check Pattern Number
        current_ActionInPattern_Number = 99999;    // Check Action count in Pattern Number
        EnterRevengePattern = false;
        InRevengePattern = false;
        HitRvSignal = false;
        HitReactiveSignal = false;
        EnterReactivePattern = false;
        InReactivePattern = false;
        InReactiveAction = false;
        current_RVBPattern_Number = 0;             // Check Pattern Number (for Revenge Attack)
        current_RVBActionInPattern_Number = 0;    // Check Action count in Pattern Number (for Revenge Attack)
        current_ReactivePattern_Number = 0;             // Check Pattern Number (for Reactive Attack)current_ReactiveActionInPattern_Number = 0;    // Check Action count in Pattern Number (for Reactive Attack)
    }

    private void Reactive_AI_1()
    {

        RVBPattern_List = CurrentAI.Revenge_Command_Libery.ToArray();
        Pattern[] current_Pattern_List = InRevengePattern ? RVBPattern_List : 
                                        (InReactivePattern ? React_Command_List[0].Get_patternList() : Pattern_List);
        List<Combo> current_Combo_List = InRevengePattern ? CurrentAI.RevengeCombo_Libery : 
                                        (InReactivePattern ? CurrentAI.ReactCombo_Libery : CurrentAI.Combo_Libery);
        
        

        Pattern current_Pattern;
        float[] current_ActionInPattern;

        if (Call_Next_Action || Call_Next_Pattern)
        {
           
            HitRvSignal = false;
            HitReactiveSignal = false;
            RVBPattern_List = CurrentAI.Revenge_Command_Libery.ToArray();
            current_Pattern_List = InRevengePattern ? RVBPattern_List : 
                                        (InReactivePattern ?  React_Command_List[0].Get_patternList()  : Pattern_List);

            current_Combo_List = InRevengePattern ? CurrentAI.RevengeCombo_Libery : 
                                        (InReactivePattern ? CurrentAI.ReactCombo_Libery : CurrentAI.Combo_Libery);

            // Pattern List Assign
            for (int i = 0; i < current_Pattern_List.Length; i++)
            {
                current_Pattern = current_Pattern_List[0];

            }
            int current_Pat_Number = InRevengePattern ? current_RVBPattern_Number: 
                                          (InReactivePattern ? current_ReactivePattern_Number  : current_Pattern_Number);
            int current_InPat_Number = InRevengePattern ? current_RVBActionInPattern_Number:
                                        (InReactivePattern? current_ReactiveActionInPattern_Number : current_ActionInPattern_Number);
            print("current_Pat_Number = " + current_Pat_Number);
            print(InRevengePattern + "  " + InReactivePattern);
            print(Pattern_List.Length);
            print("current_Pattern_List[current_Pat_Number].Get_movePattern().Length-1 = " + (current_Pattern_List[current_Pat_Number].Get_movePattern().Length-1));
            print("current_InPat_Number = " + current_InPat_Number);
            if (current_Pattern_List[current_Pat_Number].Get_movePattern().Length-1 
                <= (current_InPat_Number)
                || Call_Next_Pattern)
            {
                //////////////////////////// Add Data Here ////////////////////////////
                characterData.DATA_Pattern_Count += 1;
                //////////////////////////////////////////////////////////////////////

                print("current_InPat_Number = " + current_InPat_Number + " IsReactivePattern = " + InReactivePattern);
                // Call next pattern cases.
                // Case 1: current_InPat_Number 99999 is set to be at the start of the game, or switch from React or Revenege Case to Standard Pattern
                if (
                    //(current_InPat_Number) == 99999
                current_InPat_Number >= (current_Pattern_List[current_Pat_Number].Get_movePattern()).Length)
                {
                    print("CASE 1");
                    if (InRevengePattern) {

                        if(EnterRevengePattern){
                            //Cancel();
                            EnterRevengePattern = false;
                            current_RVBActionInPattern_Number = 0;
                            current_InPat_Number=0; 
                            current_ActionInPattern_Number = 0;
                        }
                        else {
                            InRevengePattern = false;
                            current_RVBActionInPattern_Number = 0;
                            current_InPat_Number=0; 
                            current_ActionInPattern_Number = 0;
                        }
                    }
                    else if (InReactivePattern) {
                        
                        //InReactivePattern = false;
                        
                        if(EnterReactivePattern){
                            //Cancel();
                            EnterReactivePattern = false;
                            current_ReactiveActionInPattern_Number = 0;
                            current_InPat_Number=0;
                            current_ActionInPattern_Number = 0;                  
                        }
                        else {
                            InReactivePattern = false;
                            current_ReactiveActionInPattern_Number = 0;
                            current_InPat_Number=0;
                            current_ActionInPattern_Number = 0;    
                        }
                        
                    }
                    else {
                        InRevengePattern = false;
                        InReactivePattern = false;
                        current_InPat_Number=0;
                        current_ActionInPattern_Number = 0;
                    }

                }
                // Case 2: If reach the end of pattern list, Loop Standard Pattern back to 0, while set React & RV pat to -1 to exit
                else if (current_Pattern_List.Length - 1 <= (current_Pat_Number))
                {
                    print("CASE 2");
                    if (InRevengePattern) current_RVBPattern_Number = -1;
                    else if (InReactivePattern) current_ReactivePattern_Number = -1;

                    else current_Pattern_Number = 0;

                }
                // Case 3: Nomral case, go to next pattern
                else
                {
                    print("CASE 3");
                    if (InRevengePattern) current_RVBPattern_Number++;
                    else if (InReactivePattern) current_ReactivePattern_Number++;
                    else current_Pattern_Number++;
                }

                if (InRevengePattern) current_RVBActionInPattern_Number = 0;
                else if (InReactivePattern) current_ReactiveActionInPattern_Number = 0;
                else current_ActionInPattern_Number = 0;

            }
            else
            {
                if (InRevengePattern) current_RVBActionInPattern_Number++;
                else if (InReactivePattern) current_ReactiveActionInPattern_Number++;
                else current_ActionInPattern_Number++;

            }

            // Called AI to do the action in the pattern
            current_Pattern = current_Pattern_List[current_Pat_Number];
            current_ActionInPattern = current_Pattern.Get_movePattern()[current_InPat_Number];

            //Call Action here here
            Call_AI_Command_Method(current_ActionInPattern , current_Combo_List);

            Call_Next_Action = false;
            Call_Next_Pattern = false;

             if (InRevengePattern && current_RVBPattern_Number <0){
                current_RVBPattern_Number = 0;
                current_RVBActionInPattern_Number = 99999;

                    //InRevengePattern = false;
                }
            else if (InReactivePattern && current_ReactivePattern_Number <0){

                current_ReactivePattern_Number = 0;
                current_ReactiveActionInPattern_Number = 99999;

                    //InReactivePattern = false;
                }
           /*
            if (InRevengePattern &&(current_Pattern_List.Length-1  <=  current_RVBPattern_Number) 
                && current_Pattern_List[current_RVBPattern_Number].Get_movePattern().Length-1 <= current_RVBActionInPattern_Number)
                {
                print("Hit the thing");
                print("current_Pattern_List.Length-1 = " + (current_Pattern_List.Length - 1));
                print("current_RVBPattern_Number = " + current_RVBPattern_Number);
                print("current_Pattern_List[current_RVBPattern_Number].Get_movePattern().Length-1 = " + (current_Pattern_List[current_RVBPattern_Number].Get_movePattern().Length - 1));
                print("current_RVBActionInPattern_Number = " + current_RVBActionInPattern_Number);
                current_RVBPattern_Number = 0;
                current_RVBActionInPattern_Number = 99999;

                    //InRevengePattern = false;
                }
            else if (InReactivePattern &&(current_Pattern_List.Length-1  <=  current_ReactivePattern_Number) 
                && current_Pattern_List[current_ReactivePattern_Number].Get_movePattern().Length-1 <= current_ReactiveActionInPattern_Number)
                {
                print("Reactive");
                print("current_Pattern_List.Length-1 = " + (current_Pattern_List.Length - 1));
                print("current_ReactivePattern_Number = " + current_ReactivePattern_Number);
                print("current_Pattern_List[current_ReactivePattern_Number].Get_movePattern().Length-1 = " + (current_Pattern_List[current_ReactivePattern_Number].Get_movePattern().Length - 1));
                print("current_ReactiveActionInPattern_Number = " + current_ReactiveActionInPattern_Number);
                current_ReactivePattern_Number = 0;
                current_ReactiveActionInPattern_Number = 99999;

                    //InReactivePattern = false;
                }
        */
        }
    }



    
    #region AI Control

    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% AI Function %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

    void Move(int RorL, int UorD, float time)
    {
        if (RorL > 0 && time > 0)
        {
            moveX = 1.0f;
        }
        if (RorL < 0 && time > 0)
        {
            moveX = -1.0f;
        }
        if (UorD > 0 && time > 0)
        {
            moveZ = 1.0f;
        }
        if (UorD < 0 && time > 0)
        {
            moveZ = -1.0f;
        }
        MoveCount = time;
    }
    public void face_opponents()
    {
        if (true)
            transform.LookAt(new Vector3(character.Enemy.transform.position.x, transform.position.y, character.Enemy.transform.position.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(character.transform.position.x
                                                                                                        , transform.position.y
                                                                                                        , character.transform.position.z) - transform.position), Time.time * 10 * 100);

    }
    

    #region Signal Variable

    public bool Call_Next_Action = false;       // Called next action in current Pattern
    public bool Call_Next_Pattern = false;       // Skip to next pattern
    private int Dodge_Signal = 0;
    private float Dodge_Time_AccordingToWait = 0;
    private int Block_Signal = 0;
    private float Block_Time_AccordingToWait = 0;
    private int Jump_Signal = 0;
    private float Jump_Time_AccordingToWait = 0;
    private float Jump2_Time_AccordingToWait = 0;
    private int Teleport_Signal = 0;
    private float Teleport_Time_AccordingToWait = 0f;
    private int Dodge_or_Block_After_Attack = 0;


    private bool Call_Action_Sequence = false;
    private bool Range_Condition_Met_After_Move = false;
    public int ActionInCombo_Count_Signal = 0;                        // Count Number of action in combo
    private int[] Action_Code_List = new int[200];
    private float[] Attack_Cancel_Speed_List = new float[200];
    public float Attack_Time_Compare_cancel_time = 0;
    public float Current_Cancel_Wait = 0;
    
    private int Is_Run_RangeCheck1Under2Over = 0;
    private float Cur_time_Move_to_Attack = 100f;
    private float Cur_Dis_Move_to_Attack = 0f;
    private int Cur_Combo_Attack = 0;
    //private int[] Cur_Added_Combo_List = ;
    private bool Is_Hurt_Before = false;

    private bool Is_Break_Pattern;

    #endregion

    #region Movement Function

    private void Move_Code_0_to_8_9isRandom(int d)
    {
        int code = d;
        if (code == 9) code = (int)Random.Range(1, 8.999999f);
        print((character.Is_P_Enemy ? "P_Enemy : " : "  Enemy : ") + "d = " + d);
        switch (d)
        {
            case 0:
                moveX = 0.0f;
                moveZ = 0.0f;
                break;
            case 1:
                moveX = 0.0f;
                moveZ = 1.0f;
                break;
            case 2:
                moveX = 1.0f;
                moveZ = 1.0f;
                break;
            case 3:
                moveX = 1.0f;
                moveZ = 0.0f;
                break;
            case 4:
                moveX = 1.0f;
                moveZ = -1.0f;
                break;
            case 5:
                moveX = 0.0f;
                moveZ = -1.0f;
                break;
            case 6:
                moveX = -1.0f;
                moveZ = -1.0f;
                break;
            case 7:
                moveX = -1.0f;
                moveZ = 0.0f;
                break;
            case 8:
                moveX = -1.0f;
                moveZ = 1.0f;
                break;
        }
    }
    #endregion

    #region Attack_Combo_Function
    private void Attack_Code_Push()
    {
        print("CALL__PUSH");
        int V = ActionInCombo_Count_Signal >= 200 ? 200 : ActionInCombo_Count_Signal;
        print("V = " + V);
        for (int i = 0; i <= V; i++)
        {

            print("Attack Code Push");
            print(Action_Code_List[i]);

            Action_Code_List[i] = Action_Code_List[i + 1];
            Attack_Cancel_Speed_List[i] = Attack_Cancel_Speed_List[i + 1];
        }
        ActionInCombo_Count_Signal -= 1;
    }

    private void Attack_Code_Clear()
    {

        for (int i = 0; i < ActionInCombo_Count_Signal; i++)
        {
            Action_Code_List[i] = 0;
            Attack_Cancel_Speed_List[i] = 0;
        }
        ActionInCombo_Count_Signal = 0;
    }

    #endregion


    #region Function Called

    public void Call_AI_Command_Method(float[] pattern, List<Combo> current_Combo_List)
        {
        //character.Set_Is_Invinciable_FromAI((int)pattern[1] == 1 ? true : false);
        characterStat.ResetRV();
        if(InReactiveAction && !InReactivePattern) InReactiveAction = false;
        print( (character.Is_Player_or_Enemy?"Player Call Command = ":"Enemy Call Command = ") + (int)pattern[0]);
        print(pattern[0] + " "+ pattern[1] + " " + pattern[2] + " "+ pattern[3]+ " "+ pattern[4]);
        
            switch ((int)pattern[0])
            {
            case 101:
                print((int)pattern[2]);
                //print(CurrentAI.Combo_Libery[(int)pattern[2]]);
                //AI_101_ATK_P_GetC(Call_Combo((int)pattern[2]).Get_ComboPattern(),pattern[3]);
                AI_101_ATK_P_GetC(  current_Combo_List[(int)pattern[2]].Get_ComboPattern(),pattern[3]);
                //AI_101_ATK_P_GetC((  (current_Combo_List[(int)pattern[2]])[(int)pattern[3]]).Get_ComboPattern(),pattern[4]);
                break;
                /*
            case 102:
                AI_102_ATK_P_GetC_Rand(Call_Combo((int)pattern[2]).Get_ComboPattern(),
                                    pattern[3]);
                break;   
                */  
            case 201:
               // AI_201_SRT_P_GetC( CurrentAI.Combo_Libery[(int)pattern[2]]).Get_ComboPattern(),pattern[3], pattern[4], int[,] Oppo_Act_List, pattern[6]);
                break;
                
            case 900:
                AI_900_MOV_P_Free((int)pattern[2], pattern[4]);
                break;
            case 901:
                AI_901_MOV_Toward_Target((int)pattern[2], pattern[3], pattern[4], (int)pattern[5]>=1?true:false);
                break;
            case 911:
                AI_911_MOV_Toward_Target_RandomRange((int)pattern[2], pattern[3], pattern[4], pattern[5], false);
                break;
                
            case 931:
                AI_931_TEL_P((int)pattern[2], pattern[3]);
                break;



            case 991:
                AI_991_STANCE_CHG((int)pattern[2]);
                break;
            case 1000:
                AI_1000_CHG_PAT_SET((int)pattern[2], (int)pattern[3]);
                break;
        }
        }

    #endregion


    #region AI_Command_Method  

    public void AI_101_ATK_P_GetC( int[] Combo, float Cancel_Speed)   // DorB_cancel = 0 None, 1 Dodge, 2 Block
    {
        /*
         *  Pattern Example{}
         1.Perform Attack Combo/Action from (Action) list, with each of them with Cancel_Speed of (Cancel_Speed)
         */
        //Dodge_or_Block_After_Attack = DorB_cancel;

        //float T = Random.Range(time1, time2);
        /*
        Move_Code_0_to_8_9isRandom(1);
        print((character.Is_P_Enemy ? "P_Enemy : " : "  Enemy : ") + "AI FUNCTION CALL = AI_101_ATK_P_GetC(" + Dis + " , " + time + " , " + ComNum + " , " + Action + " , " + Cancel_Speed + ")");

        Cur_time_Move_to_Attack = time;
        Cur_Dis_Move_to_Attack = Dis;
        print("Dis =" + Dis);
        */
        foreach (var i in Combo){
            print(i);
        }
        print((character.Is_P_Enemy ? "P_Enemy : " : "  Enemy : ") + "AI FUNCTION CALL = AI_101_ATK_P_GetC(" + Combo + " , " + Cancel_Speed + ")    ActionCount = "  + Action_Count);
        print("ComNum is = " + Combo.Length);
        Cur_Combo_Attack = Combo.Length;
        Call_Action_Sequence = true;
        ActionInCombo_Count_Signal = Combo.Length;
        for (int i = 0; i < Combo.Length; i++)
        {
            if(Combo[i] == 0) break;
            
            Action_Code_List[i] = Combo[i];
            Attack_Cancel_Speed_List[i] = Cancel_Speed;

        }
        wait = -0.0001f;
    }
    public void AI_102_ATK_P_GetC_Rand( Combo[] ComboList, float Cancel_Speed)   // DorB_cancel = 0 None, 1 Dodge, 2 Block
    {

        int Cur_List_Combo_Used = (int)Random.Range(1, ComboList.Length);
        int[] Combo = ComboList[Cur_List_Combo_Used].Get_ComboPattern();

        print((character.Is_P_Enemy ? "P_Enemy : " : "  Enemy : ") + "AI FUNCTION CALL = AI_102_ATK_P_GetC_Rand(" + ComboList + " , " + Cancel_Speed + ")    ActionCount = "  + Action_Count);
        print("ComNum is = " + Combo.Length);
        Cur_Combo_Attack = Combo.Length;
        Call_Action_Sequence = true;
        ActionInCombo_Count_Signal = Combo.Length;
        for (int i = 0; i < Combo.Length; i++)
        {
            if(Combo[i] == 0) break;
            Action_Code_List[i] = Combo[i];
            Attack_Cancel_Speed_List[i] = Cancel_Speed;

        }
        wait = -0.0001f;
    }
    public void AI_201_SRT_P_GetC( int[] Combo,float reaction_Speed, float Cancel_Speed, int[,] Oppo_Act_List, float[] range)   // DorB_cancel = 0 None, 1 Dodge, 2 Block
    {

        current_Reactive_Timer = reaction_Speed;
        ReactiveActionToOppoActList = Oppo_Act_List;
        ReactiveActionOppoActRange = range;
        /*
        int Cur_List_Combo_Used = (int)Random.Range(1, ComboList.Length);
        int[] Combo = ComboList[Cur_List_Combo_Used].Get_ComboPattern();

        print((Is_P_Enemy ? "P_Enemy : " : "  Enemy : ") + "AI FUNCTION CALL = AI_102_ATK_P_GetC_Rand(" + ComboList + " , " + Cancel_Speed + ")    ActionCount = "  + Action_Count);
        print("ComNum is = " + Combo.Length);
        Cur_Combo_Attack = Combo.Length;
        Call_Action_Sequence = true;
        ActionInCombo_Count_Signal = Combo.Length;
        for (int i = 0; i < Combo.Length; i++)
        {
            Action_Code_List[i] = Combo[i];
            Attack_Cancel_Speed_List[i] = Cancel_Speed;

        }
        wait = -0.0001f;
        */
    }

    public void AI_401_DOG_S(int code)
    {
        print((character.Is_P_Enemy ? "P_Enemy : " : "  Enemy : ") + "AI FUNCTION CALL = AI_401_DOG_S(" + code + ")");
        //wait += T + 0.5f;
        Move_Code_0_to_8_9isRandom(code % 10);
        Dodge_Signal = code % 10;
        wait = 0.1f;
        Dodge_Time_AccordingToWait = 0.05f;
        Move_Code_0_to_8_9isRandom(code % 10);

    }
    public void AI_402_BOK_S()
    {
        print((character.Is_P_Enemy ? "P_Enemy : " : "  Enemy : ") + "AI FUNCTION CALL = AI_402_BOK_S(" + " )");
        //wait += T + 0.5f;
        Block_Signal = 1;
        wait = 0.05f;
        Block_Time_AccordingToWait = 0.05f;
        //Move_Code_0_to_8(code % 10);

    }

    public void AI_900_MOV_P_Free(int code, float time)
    {
        /*
        *  Pattern Example{}
        1.Move around at direction
           8    1   2
           7    0   3    
           6    5   4
        */
        //float T = Random.Range(time1, time2);
        //int C = (int)Random.Range(903, 907.99999f);
        print((character.Is_P_Enemy ? "P_Enemy : " : "  Enemy : ") + "AI FUNCTION CALL = AI_900_MOV_P_FREE(" + time + " " + code + ")");
        wait = time;
        Move_Code_0_to_8_9isRandom(code);
    }

    public void AI_901_MOV_Toward_Target(int code, float Dis, float time, bool breakPat)
    {
        /*
         1.Get Close to the target until.....
            a. Distance between character and target reach (Dis)
            b. Reach (time1) limit
        */
        Move_Code_0_to_8_9isRandom(code);
        print((character.Is_P_Enemy ? "P_Enemy : " : "  Enemy : ") + "AI FUNCTION CALL = AI_901_MOV_Toward_Target(" + Dis + " , " + time  + ")          " + Action_Count);
        wait = time;
        Cur_time_Move_to_Attack = time;
        if(Dis >= 0){
            Is_Run_RangeCheck1Under2Over = 1;
            Cur_Dis_Move_to_Attack = Dis;
        }
        else{
            Is_Run_RangeCheck1Under2Over = 2;
            Cur_Dis_Move_to_Attack = -Dis;
        }
        
        //print("Dis =" + Dis);

    }
    
    public void AI_911_MOV_Toward_Target_RandomRange(int code, float DisMin, float DisMax, float time, bool breakPat)
    {
        /*
         1.Get Close to the target until.....
            a. Distance (Random in range of Dis Min & Dis Max) between character and target reach (Dis)
            b. Reach (time1) limit
        */
        Move_Code_0_to_8_9isRandom(code);
        print((character.Is_P_Enemy ? "P_Enemy : " : "  Enemy : ") + "AI FUNCTION CALL = AI_911_MOV_Toward_Target_RandomRange(" + DisMin + " - " + DisMax  + " , " + time  + ")          " + Action_Count);
        wait = time;
        Cur_time_Move_to_Attack = time;
        float Dis = RandomFloat(DisMin,DisMax);
        if(Dis >= 0){
            Is_Run_RangeCheck1Under2Over = 1;
            Cur_Dis_Move_to_Attack = Dis;
        }
        else{
            Is_Run_RangeCheck1Under2Over = 2;
            Cur_Dis_Move_to_Attack = -Dis;
        }
        
        //print("Dis =" + Dis);

    }
    
    public void AI_931_TEL_P(int dir, float distance)
    {
       // Set_Teleport_Pos(Pos);
        print((character.Is_P_Enemy ? "P_Enemy : " : "  Enemy : ") + "AI FUNCTION CALL = AT_931_TEL_P(" + dir + ", " + distance + ")");
        wait = 1.00f;
        Teleport_Signal = 50000 + dir*1000 + (int)(distance*10);
        Teleport_Time_AccordingToWait = 0.04f;
    }

    public void AI_991_STANCE_CHG(int code) //1B 2H 3Q
    {
        character.Set_CharacterMode(code);
    }

    public void AI_1000_CHG_PAT_SET(int patCode, int slotNum) {
        if(Pattern_List.Length > slotNum){
            if(CurrentAI.Pattern_Libery.Count > patCode )
                Pattern_List =  CurrentAI.ChangePatSetSlot(patCode,slotNum).Get_patternList();
            else print("AI_1000_CHG_PAT_SET ?????? patNum out of Bound");
        }
        else print("AI_1000_CHG_PAT_SET ?????? slotNum out of Bound");
    }

    #region Teleport Code
    public Vector3 Teleport_Pos_CompareToPlayer = new Vector3(3.5f, 0f, 0f);

    #endregion

   
    public void AI_971_JUMP_S(float Dir, int Count, float time1, float time2)
    {
        //float T = Random.Range(time1, time2);
        int C = (int)Dir % 10;
        if (Dir % 10 == 9)
        {
            C = (int)Random.Range(930, 938.99999f);
        }

        print((character.Is_P_Enemy ? "P_Enemy : " : "  Enemy : ") + "AI FUNCTION CALL = AI_971_JUMP_S(" + Dir + ", " + Count + ", " + time1 + ", " + time2 + ")");
        //wait += T + 0.5f;
        if (Count == 1)
        {
            wait = time1 + 0.1f;
            Jump_Time_AccordingToWait = time1 + 0.07f;
            Jump2_Time_AccordingToWait = -0.1f;
            Jump_Signal = 1;
        }
        else
        {
            wait = time1 + time2 + 0.1f;
            Jump_Time_AccordingToWait = time2 + 0.05f;
            Jump2_Time_AccordingToWait = time1 + time2 + 0.08f;
            Jump_Signal = 2;
        }
        Move_Code_0_to_8_9isRandom(C % 10);

    }
    /////////////////////////////////////////////////////////////////////////////////////////////

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

    #endregion

    public void Cancel()
    {
        print("Cancel");
        character.Movement_All_Reset();
        face_opponents();
        // print(currentKnockback_Multi);
        character.animator.SetTrigger("Cancel");
        character.Movement_All_Reset();
        wait = 0;
        Current_Cancel_Wait = 0;
        Action_Count = 0.02f;
        //Call_Action_Sequence = false;
        character.Set_Is_InCombo(false);
        Call_Action_Sequence = false;
        //character.Is_ComboAble_CheckFromActionCalled = false;
    }

    void AI_Called_Action()
    {


        //-------Check Distance against Player-----------//
        if(Is_Run_RangeCheck1Under2Over != 0)
        {
            if (Is_Run_RangeCheck1Under2Over == 1 && character.Compare_to_ENEMY.magnitude <= Cur_Dis_Move_to_Attack)
            {
                //print("Condition Met");
                Is_Run_RangeCheck1Under2Over = 0;
                Range_Condition_Met_After_Move = true;
                Cur_Dis_Move_to_Attack = 0f;
                Cur_time_Move_to_Attack = 0f;
                wait = 0.001f;
                Call_Next_Action = true;
            }
            else if (Is_Run_RangeCheck1Under2Over == 2 && character.Compare_to_ENEMY.magnitude >= Cur_Dis_Move_to_Attack)
            {
                //print("Condition Met");
                Is_Run_RangeCheck1Under2Over = 0;
                Range_Condition_Met_After_Move = true;
                Cur_Dis_Move_to_Attack = 0f;
                Cur_time_Move_to_Attack = 0f;
                wait = 0.001f;
                Call_Next_Action = true;
            }
        }
            
        
        //if(Call_Action_Sequence) print("Call_Action_Sequence = " + Call_Action_Sequence);
        if (/*Range_Condition_Met_After_Move &&*/ Call_Action_Sequence)
        {
            //print("CALLLLLLL");
            character.Set_Is_InCombo(true);

            /////////////////////////////////////////////////////////// FIXED HERE /////////////////////////////////////////
            if ((ActionInCombo_Count_Signal > 0) && (Current_Cancel_Wait <= Attack_Time_Compare_cancel_time) && !Is_Waiting_For_Action)
            /////////////////////////////////////////////////////////// FIXED HERE /////////////////////////////////////////
            {
                print("CALLLLLLL1   moveX = " + moveX + "   moveZ = " + moveZ);
                //character.Call_Movement_For_AIControl(moveX, moveZ);
                characterStat.ResetRV();
                print("CALLLLLLL2   moveX = " + moveX + "   moveZ = " + moveZ);
                Attack_Time_Compare_cancel_time = 0f;
                Is_Waiting_For_Action = true;
                

                
                Actioncode = Action_Code_List[0];
                print("ACTION = " + Actioncode);
                if(Actioncode >= 40200 && Actioncode <= 40209){
                    //Cancel();
                    //Action_Count = -2f;
                    Move_Code_0_to_8_9isRandom(Actioncode-40200);
                    character.Call_Movement_For_AIControl(moveX, moveZ);
                }
                character.Call_Action_For_AIControl(AbgActCodeToFull(Actioncode));
                Current_Cancel_Wait = Attack_Cancel_Speed_List[0];
                print("Current_Cancel_Wait = " + Current_Cancel_Wait);
                Attack_Code_Push();

            }
            if (ActionInCombo_Count_Signal == 0)
            {
                character.Call_Movement_For_AIControl(moveX, moveZ);
                character.Set_Is_InCombo(false);
                print("INATTACK");
                Attack_Code_Clear();
                //Range_Condition_Met_After_Move = false;
                if (Dodge_or_Block_After_Attack == 0)
                {
                    wait = 0;
                }
                else if (Dodge_or_Block_After_Attack == 1)
                {
                    //AI_904_MOV_P_D_OFF(0.1f, 0.11f);
                }
                else if (Dodge_or_Block_After_Attack == 2)
                {
                    //AI_961_BOK_S();
                }

                Call_Action_Sequence = false;
                Action_Count = 0;
            }
        }



        else if (Dodge_Signal > 0 && wait <= Dodge_Time_AccordingToWait)
        {
            print("IN");
            Move_Code_0_to_8_9isRandom(Dodge_Signal);

            Dodge_Signal = 0;
            Is_Waiting_For_Action = true;

            Actioncode = 910;

        }
        else if (Block_Signal > 0 && wait <= Block_Time_AccordingToWait)
        {
            //print("IN");
            //Move_Code_0_to_8(Dodge_Signal);
            Block_Signal = 0;
            Is_Waiting_For_Action = true;

            Actioncode = 920;

        }
        else if (Jump_Signal > 1 && wait <= Jump2_Time_AccordingToWait)
        {

            Jump_Signal = 1;

            print("IN");
            //Move_Code_0_to_8(Dodge_Signal);
            //Jump_Signal = 0;
            Is_Waiting_For_Action = false;

            Actioncode = 930;

        }

        else if (Jump_Signal > 0 && wait <= Jump_Time_AccordingToWait)
        {

            Jump_Signal = 0;

            print("IN");
            //Move_Code_0_to_8(Dodge_Signal);
            //Jump_Signal = 0;
            Is_Waiting_For_Action = false;

            Actioncode = 930;

        }
        else if (Teleport_Signal > 0 && wait <= Teleport_Time_AccordingToWait)
        {
            Actioncode = Teleport_Signal;
            Teleport_Signal = 0;
            Is_Waiting_For_Action = true;
            
        }



        if (character.Get_Is_Disable_Control())
        {
            wait = 0;
            Current_Cancel_Wait = 0;
            Action_Count = -0.7f;
            Call_Action_Sequence = false;
            character.Set_Is_InCombo(false);
            //character.Is_ComboAble_CheckFromActionCalled = false;
        }

        if (character.Get_Is_Hurt_Total() && !HitRvSignal)
        {
            Is_Hurt_Before = true;
            wait = 0;
            Current_Cancel_Wait = 0;
            //print("HURTTTTT");
            Action_Count = -0.12f;
            Call_Action_Sequence = false;
            character.Set_Is_InCombo(false);

            //////////////// Reset to Normal Pattern after //////////////
            InRevengePattern = false;
            InReactivePattern = false;

            EnterReactivePattern = false;
            IsCallReactiveAction = false;
            TimerCallReactiveAction = 0;
            InReactivePattern = false;
            InReactiveAction = false;
            HitReactiveSignal = false;
            TimerCallReactiveAction = 0;
            //Call_Next_Pattern = false;

        }

        else if (Action_Count >= 0.01f)
        {
            print(moveX + "  " + moveZ);
            moveX = 0.0f;
            moveZ = 0.0f;
            Call_Action_Sequence = false;
            Attack_Code_Clear();
            if (Is_Hurt_Before)
            {
                Is_Hurt_Before = false;
                Call_Next_Pattern = true;
                Call_Next_Action = false;
            }
            else {
                //print("Is this the CAUSE");
                Call_Next_Action = true;
                Call_Next_Pattern = false;
            }
            character.Set_Is_InCombo(false);

            Action_Count = 0;
        }
        else
        {
            Call_Next_Action = false;
            Call_Next_Pattern = false;

        }

/////////////////////////// Reactive Check ///////////////////////////////
        if(!InReactiveAction && ReactiveCooldownTimer <= 0){
            ReactiveCheck();
        }

    }
    private void ReactiveCheck(){
        bool is_Player_or_Enemy = character.Is_Player_or_Enemy;
        int opponentState = is_Player_or_Enemy?(Battle_Controller.E_STATE[character.CharacterCode])
        :(Battle_Controller.P_STATE[character.CharacterCode]);
        int opponentMode = is_Player_or_Enemy?(Battle_Controller.E_MODE[character.CharacterCode])
        :(Battle_Controller.P_MODE[character.CharacterCode]);
    //-------------------
        bool isReactive = false;
        float timeToReact = 0;

        //print(  (is_Player_or_Enemy?"Player":"Enemy") + "  React_Command_List.Length = "+  React_Command_List.Length);
        for(int j = 0 ; j < React_Command_List.Length ; j++){

            //print(  (is_Player_or_Enemy?"Player":"Enemy") + "  React_Command_List[j].Get_oppo_Action().Length = "+ React_Command_List[j].Get_oppo_Action().Length);

            for(int i = 0 ; i < React_Command_List[j].Get_oppo_Action().Length ; i++){
                int actionCheck = (React_Command_List[j].Get_oppo_Action())[i]; 
                /// Mode Check
                if(actionCheck >= 1001 && (actionCheck-1000) == opponentMode){
                    print("Activate Reactive Mode: Action = " + opponentState +" : Distance = " + character.Compare_to_ENEMY.magnitude);
                    timeToReact = React_Command_List[j].Get_reaction_Speed();
                    isReactive = true;
                    IsCallReactiveActionCode = j;
                    break;
                } 
                /// Action Check
                else if(opponentState == (React_Command_List[j].Get_oppo_Action())[i] 
                    && React_Command_List[j].Get_range() >= character.Compare_to_ENEMY.magnitude){
                    print("Activate Reactive Action: Action = " + opponentState +" : Distance = " + character.Compare_to_ENEMY.magnitude);
                    timeToReact = React_Command_List[j].Get_reaction_Speed();
                    isReactive = true;
                    IsCallReactiveActionCode = j;
                    break;
                }
                if((React_Command_List[j].Get_oppo_Action())[i] == 0) break;
            }
            if(isReactive) break;
        }
        
/*
        for(int j = 0 ; j < ReactiveActionToOppoActList.GetLength(0) ; j++){
            for(int i = 0 ; i < ReactiveActionToOppoActList.GetLength(1) ; i++){
                if(opponentState == ReactiveActionToOppoActList[j,i]){
                    isReactive = true;
                    IsCallReactiveActionCode = j;
                    break;
                }
                if(ReactiveActionToOppoActList[j,i] == 0) break;
            }
            if(isReactive) break;
        }
        */
        
        //if(isReactive)    
        //IsCallReactiveAction = isReactive;
        if(isReactive && !InReactivePattern){
            ReactiveCooldownTimer = ReactiveCooldownValue;
            TimerCallReactiveAction = timeToReact;
            IsCallReactiveAction = true;
            InReactivePattern = true;
            InReactiveAction = true;
            print("IsCallReactiveAction = " + IsCallReactiveAction);
        }
        
           
    }
     public int AbgActCodeToFull(int i){
        switch(i){
            //Movement
            case 000: return 10010;
            case 001: return 10011;
            case 002: return 10012;
            case 003: return 10013;
            case 004: return 10014;
            case 005: return 10015;
            case 006: return 10016;
            case 007: return 10017;
            case 008: return 10018;
            
            //Attack
            case 101: return 20011;
            case 102: return 20012;
            case 103: return 20013;
            case 104: return 20014;
            case 201: return 20111;
            case 202: return 20112;
            case 203: return 20113;
            case 204: return 20114;
            case 301: return 22011;
            case 302: return 22012;
            case 303: return 22013;
            case 401: return 22111;
            case 402: return 22112;
            case 403: return 22113;
            case 501: return 24011;
            case 502: return 24012;
            case 503: return 24013;
            case 504: return 24014;
            case 505: return 24015;
            case 601: return 24111;
            case 602: return 24112;
            case 603: return 24113;
            case 604: return 24114;
            case 605: return 24115;

            //Block
            case 800: return 40010;

            //Dodge
            case 901: return 40201;
            case 902: return 40202;
            case 903: return 40203;
            case 904: return 40204;
            case 905: return 40205;
            case 906: return 40206;
            case 907: return 40207;
            case 908: return 40208;
            case 909: return 40209;
            
            default: return i;
        }
    }
    #endregion

/*
    private pattern CreatePatternFromCode(){

    }
    */
}

#endregion