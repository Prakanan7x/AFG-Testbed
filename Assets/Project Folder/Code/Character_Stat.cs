using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Stat : MonoBehaviour {
    public int HP;
    public int Attack;
    public int Magic;


    public int Basic_Attack_count = 1;
    public int Pre_Finisher_count = 1;
    public int Finisher_count = 1;
    public float Cur_HP_Per = 1;


    public int CommandCount;

    public bool m_FacingRight = true;

    public float Guard_DMG_Reduct_Percent = 0f;
    public int Guard_DMG_Reduct_Minus = 0;

    public Command[] CommandList;

    public bool Is_Reset;

    private int currentCommand = 0;
    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        if (Cur_HP_Per <= 0)
        {
            //SceneManager.LoadScene("gameover", LoadSceneMode.Single);
        }
    }
    public void SetCurrentCommand(int slot) { currentCommand = slot; }
    //public int GetCurrentForm() { return currentForm; }
    public Command GetCurrentCommand() { return CommandList[currentCommand]; }

    public float GetCurrentHPPercentage() { return Cur_HP_Per; }
    public void SetCurrentHPPercentage(float n) { Cur_HP_Per = n; }

    public int GetHP() { return HP; }
    public float GetCur_HP() { return Cur_HP_Per; }
    public void DamageDeal(int d)
    {
        //print("Character  is == " + this.GetComponent<CharacterController>().name + "   Damage = " + d);
        //print((float)d / (float)HP);
        Cur_HP_Per -= (float)d / (float)HP;
        //print("OUCHHHHHHHHHHHHHHHH " + Cur_HP_Per * HP);
        //print("Character HP is == " + Cur_HP_Per);
    }

    public float RevengeValue = 8;
    public float Cur_RV = 0;

    public bool IsHitRVvalue()
    {
        return RevengeValue > 0 && RevengeValue <= Cur_RV;   
    }
    public void IncreaseRevengeValue(float d)
    {
        Cur_RV += d;

    }
    public void ResetRV()
    {
        Cur_RV = 0;
    }



}
