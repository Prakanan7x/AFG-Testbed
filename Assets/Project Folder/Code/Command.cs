using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : MonoBehaviour {

    public string Name;
    public string Des;
    public int type; // 1 = Physical, 2 = Defense
    //public int ATB;
    //public int ATB_Hold;
    public float Potency;
    public int KnockbackType;
    public float KnockbackForce;
    public float ArmorBreak;
    //public int Guard_Defense;
    //public int Combo; // Charge = 0, Infinite = -1

    public int code;
    public int stylePoint;
    public int styleRequirement; // 0 = land, 1 = just used, 2 = dodge,
    public float revengeValue;
    public int sfxHitCode = 0;

    //private int Combo_Count;
    public float GetPotency() { return Potency; }
    public int GetKnockbackType() { return KnockbackType; }
    public float GetKnockbackForce() { return KnockbackForce; }
    public float GetTYPE() { return type; }
    public void UseCommand(int i)
    {

    }

    public void Attack(int i)
    {

    }
}
