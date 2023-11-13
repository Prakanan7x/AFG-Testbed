using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStat : MonoBehaviour {

    //public List<BaseStats>[] stats;



    public int HP;
    public int Attack;
    public int Magic;
    //public int ATB_Init;
    // public int ATB_Max;
    //public float ATB_Speed;
    public int Basic_Attack_count = 1;
    public int Pre_Finisher_count = 1;
    public int Finisher_count = 1;
    public float Cur_HP_Per = 1;

    //public BattleController BC;

    public int CommandCount;

    public bool m_FacingRight = true;

    public float Guard_DMG_Reduct_Percent = 0f;
    public int Guard_DMG_Reduct_Minus = 0;

    public Command[] CommandList;

    public bool Is_Reset;
    //public Form[] Form_Slot;
    //public Form Form_Slot_1;
    //public Form Form_Slot_2;
    // public Form Form_Slot_3;

    //private int[] _HP = new int[3];
    //private int[] _Attack = new int[3];
    //private int[] _Magic = new int[3];
    //private int[] _ATB_Init = new int[3];
    //private int[] _ATB_Max = new int[3];
    //private float[] _ATB_Speed = new float[3];
    //private Command[][] _Command = { new Command[4], new Command[4], new Command[4] };

    //private int[] _ATB_Current;

    //private int currentForm = 0;
    private int currentCommand = 0;

    //public Form[] Form_reserve_Blue;
    //public Form[] Form_reserve_Red;
    //public Form[] Form_reserve_Yellow;

    void Start()
    {


        /*stats[1].Add(new BaseStats(HP, "HP", "Hit point"));
        stats[1].Add(new BaseStats(Attack, "Strength", "Physical Power."));
        stats[1].Add(new BaseStats(Magic, "Magic", "Magical Power."));
        stats[1].Add(new BaseStats(ATB_Init, "Initial ATB", "ATB value at the start of the battle"));
        stats[1].Add(new BaseStats(ATB_Max, "Maximum ATB", "ATB Capacity"));
        stats[1].Add(new BaseStats(ATB_Speed, "ATB Speed", "ATB regeneration speed"));

        stats[2].Add(new BaseStats(HP, "HP", "Hit point"));
        stats[2].Add(new BaseStats(Attack, "Strength", "Physical Power."));
        stats[2].Add(new BaseStats(Magic, "Magic", "Magical Power."));
        stats[2].Add(new BaseStats(ATB_Init, "Initial ATB", "ATB value at the start of the battle"));
        stats[2].Add(new BaseStats(ATB_Max, "Maximum ATB", "ATB Capacity"));
        stats[2].Add(new BaseStats(ATB_Speed, "ATB Speed", "ATB regeneration speed"));

        stats[3].Add(new BaseStats(HP, "HP", "Hit point"));
        stats[3].Add(new BaseStats(Attack, "Strength", "Physical Power."));
        stats[3].Add(new BaseStats(Magic, "Magic", "Magical Power."));
        stats[3].Add(new BaseStats(ATB_Init, "Initial ATB", "ATB value at the start of the battle"));
        stats[3].Add(new BaseStats(ATB_Max, "Maximum ATB", "ATB Capacity"));
        stats[3].Add(new BaseStats(ATB_Speed, "ATB Speed", "ATB regeneration speed"));
        // stats[0].AddStatBonus(new StatBonus(5));

        Debug.Log(stats[1][1].GetCalculatedStatValue());*/

        //AddFormStat(0, Form_Slot[0]);
        //AddFormStat(1, Form_Slot[1]);
        //AddFormStat(2, Form_Slot[2]);

    }
    void Update()
    {
        if (Cur_HP_Per <= 0)
        {
            //SceneManager.LoadScene("gameover", LoadSceneMode.Single);
        }
        //print(Cur_HP_Per);
    }

   /* public void AddFormStat(int slot, Form form)
    {

        _HP[slot] = HP + form.HP;
        //print(_HP[slot]);
        _Attack[slot] = Attack + form.Attack;
        _Magic[slot] = Magic + form.Magic;
        _ATB_Init[slot] = ATB_Init + form.ATB_Init;
        _ATB_Max[slot] = ATB_Max + form.ATB_Max;
        _ATB_Speed[slot] = ATB_Speed + form.ATB_Speed;

        _Command[slot][0] = form.Command_1;
        _Command[slot][1] = form.Command_2;
        _Command[slot][2] = form.Command_3;
        _Command[slot][3] = form.Command_4;

        BC.CommandUpdate(currentForm);

    }*/
    //public Form GetForm(int slot) { return Form_Slot[slot]; }
    //public void SetForm(int slot, Form F) { Form_Slot[slot] = F; }

    public int GetHP() { return HP; }
    public float GetCur_HP() { return Cur_HP_Per; }
    //public int GetAttack(int slot) { return _Attack[slot]; }
    //public int GetMagic(int slot) { return _Magic[slot]; }
    //public int GetATB_Init(int slot) { return _ATB_Init[slot]; }
    //public int GetATB_Max(int slot) { return _ATB_Max[slot]; }
    //public float GetATB_Speed(int slot) { return _ATB_Speed[slot]; }
    //public int GetATB_Current(int slot) { return _ATB_Current[slot]; }
    //public void SetATB_Current(int ATB, int slot) { _ATB_Current[slot] = ATB; }

    //public Command GetCommand_1(int slot) { return _Command[slot][0]; }
    //public Command GetCommand_2(int slot) { return _Command[slot][1]; }
    //public Command GetCommand_3(int slot) { return _Command[slot][2]; }
    //public Command GetCommand_4(int slot) { return _Command[slot][3]; }

    //public Command GetCommand(int slot, int com) { return _Command[slot][com]; }

    //public void SetCurrentForm(int slot) { currentForm = slot; }
    public void SetCurrentCommand(int slot) { currentCommand = slot; }
    //public int GetCurrentForm() { return currentForm; }
    public Command GetCurrentCommand() { return CommandList[ currentCommand]; }

    public float GetCurrentHPPercentage() { return Cur_HP_Per; }
    public void SetCurrentHPPercentage(float n) { Cur_HP_Per = n; }


    

    /*  public void AddStatBonus(List<BaseStats> statBonuses, int slot)
      {
          foreach (BaseStats statBonus in statBonuses)
          {
              stats[slot].Find(x => x.StatName == statBonus.StatName).AddStatBonus(new StatBonus(statBonus.BaseValue));
          }
      }

      public void RemoveStatBonus(List<BaseStats> statBonuses, int slot)
      {
          foreach (BaseStats statBonus in statBonuses)
          {
              stats[slot].Find(x => x.StatName == statBonus.StatName).RemoveStatBonus(new StatBonus(statBonus.BaseValue));
          }
      }

      public void ChangeFirstSlot(int newslot)
      {
          print("Form in the First Slot changed to");
          SetForm(0, Form_reserve_Blue[newslot]);
          //Form_Slot[0] = newslot;
          AddFormStat(0, Form_Slot[0]);
      }

      public void ChangeSecondSlot(int newslot)
      {
          print("Form in the Second Slot changed to");
          SetForm(1, Form_reserve_Red[newslot]);
          AddFormStat(1, Form_Slot[1]);
          //Form_Slot[1] = newslot;
      }

      public void ChangeThirdSlot(int newslot)
      {
          print("Form in the Third Slot changed to");
          SetForm(2, Form_reserve_Yellow[newslot]);
          AddFormStat(2, Form_Slot[2]);
          //Form_Slot[2] = newslot;

      }
      */

    public void DamageDeal(int d)
    {
       // print((float)d / (float)HP);
        Cur_HP_Per -= (float)d / (float)HP;
        //print("OUCHHHHHHHHHHHHHHHH " + Cur_HP_Per*HP);
    }
}