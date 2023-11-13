using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_1_HurtBox_Manage : MonoBehaviour {

    public BoxCollider localCollider_S;
    //public EnemyController enemycontroller;
    public Character_1 characterController;

    //public EnemyStat enemystat;
    public Character_Stat characterStat;

    private BoxCollider localCollider;
    //public EnemyHitBox enemy_hitbox;
    public Character_1_HitBox characterHitbox;

    public BoxCollider NULL { get; private set; }
    public Character_1 character;

    private int HitDuringBlock_Count = 0;
    private Command lastCommandDuringBlock = null;
    private float lastAttackStatDuringBlock = 0f;
    // Use this for initialization


    public SoundEffect SFX;


    void Start()
    {

        localCollider = localCollider_S;
        localCollider = gameObject.AddComponent<BoxCollider>();
        // Create a polygon collider
        localCollider.size = localCollider_S.size;
        localCollider.center = localCollider_S.center;
        localCollider.isTrigger = true; // Set as a trigger so it doesn't collide with our environment
    }

    // Update is called once per frame
    void Update()
    {
        if (character.Get_Is_Block_SuccessfulCountDecay())
        {
            HitDuringBlock_Count = 0;
        }
        else if(HitDuringBlock_Count >= 2 )
        {
            Getting_Hit_Activate(lastCommandDuringBlock, lastAttackStatDuringBlock);
            HitDuringBlock_Count = 0;
        }
        else if(HitDuringBlock_Count >= 1)
        {
            print("COUNT = " + HitDuringBlock_Count);
            HitDuringBlock_Count++;
        }
        /*
        if (characterHitbox.Is_Block == false) HitDuringBlock_Count = 0;
        else if (characterHitbox.Is_Block == true && HitDuringBlock_Count >= 0)
        {
            HitDuringBlock_Count++;
        }
        */
    }


    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Collider hit something!");
        //Debug.Log("Collider hit something!");
        //print("Hit = " + localCollider.size);
        //EnemyHitboxHandler e = col.GetComponent<EnemyHitboxHandler>();
        if (!(characterController.Get_Is_Level_up() || characterStat.Cur_HP_Per <= 0))
        {
            //Player Case
            if (characterController.Is_Player_or_Enemy)
            {
                if (col.tag == "Enemy_Attack" && !characterController.IS_Invinciable_Real())
                {
                    //print("Block COunt " + HitDuringBlock_Count);
                    if (/*characterController.Get_Is_Block() == true */ characterController.Get_Is_Block() )
                    {
                        lastCommandDuringBlock = col.GetComponentInParent<Character_Stat>().GetCurrentCommand();
                        lastAttackStatDuringBlock = col.GetComponentInParent<Character_Stat>().Attack;
                        HitDuringBlock_Count = 1;
                        
                    }
                    else if (col.GetComponent<Character_1_HitBox_Manage>().character_hitbox.Is_Block)
                    {

                    }
                    else if (!col.GetComponentInParent<Character_1_HitBox>().Is_BlockHitBox_Actived())
                    {
                        Getting_Hit_Activate(col.GetComponentInParent<Character_Stat>().GetCurrentCommand()
                           , col.GetComponentInParent<Character_Stat>().Attack);
                    }


                    //print("Ouch e Ouch " );
                    // playercontroller.Activate_Hurt();
                    //player_hitbox.Previous_Command_Hit_Number = player_hitbox.playerstat.GetCurrentCommand();
                    //player_hitbox.Hit_Disable = true;

                }
            }
            //Enemy Case
            else
            {
                if (Battle_Controller.game_Mode[character.CharacterCode] == 1)
                {
                    if (col.tag == "Player_Attack" && !characterController.IS_Invinciable_Real())
                    {
                        if (/*characterController.Get_Is_Block() == true */ characterController.Get_Is_Block())
                        {
                            lastCommandDuringBlock = col.GetComponentInParent<Character_Stat>().GetCurrentCommand();
                            lastAttackStatDuringBlock = col.GetComponentInParent<Character_Stat>().Attack;
                            HitDuringBlock_Count = 1;
                        }
                        else if (!col.GetComponentInParent<Character_1_HitBox>().Is_BlockHitBox_Actived())
                            Getting_Hit_Activate(col.GetComponentInParent<Character_Stat>().GetCurrentCommand()
                            , col.GetComponentInParent<Character_Stat>().Attack);
                        //player_hitbox.Previous_Command_Hit_Number = player_hitbox.playerstat.GetCurrentCommand();
                        //player_hitbox.Hit_Disable = true;
                    }
                }
                else
                {
                    if (col.tag == (characterController.Is_P_Enemy ? "Enemy_Attack" : "P_Enemy_Attack") && !characterController.IS_Invinciable_Real())
                    {
                        if (/*characterController.Get_Is_Block() == true */ characterController.Get_Is_Block())
                        {
                            lastCommandDuringBlock = col.GetComponentInParent<Character_Stat>().GetCurrentCommand();
                            lastAttackStatDuringBlock = col.GetComponentInParent<Character_Stat>().Attack;
                            HitDuringBlock_Count = 1;
                        }
                        else if (!col.GetComponentInParent<Character_1_HitBox>().Is_BlockHitBox_Actived())
                            Getting_Hit_Activate(col.GetComponentInParent<Character_Stat>().GetCurrentCommand() 
                            , col.GetComponentInParent<Character_Stat>().Attack);
                        //player_hitbox.Previous_Command_Hit_Number = player_hitbox.playerstat.GetCurrentCommand();
                        //player_hitbox.Hit_Disable = true;
                    }
                }
            }
            
        }
        
    }
    private void Getting_Hit_Activate(Command C , float Attack)
    {

        float damage = 0;
        

       // Command C;
       // print("Hurt " + characterController.Is_Player_or_Enemy);
        /*
        if (Battle_Controller.game_Mode[character.CharacterCode] == 1)
        {
            C = col.GetComponentInParent<Character_Stat>().GetCurrentCommand();
            characterController.setKnockback_Multi(C.Knockback);
            damage = (C.Potency * col.GetComponentInParent<Character_Stat>().Attack) * 1f;
        }
        else
        {*/
           // C = col.GetComponentInParent<Character_Stat>().GetCurrentCommand();
            characterController.setKnockback_Multi(C.KnockbackForce);
            damage = (C.Potency * Attack) * 1f;

        //}
        if (!character.Is_Player_or_Enemy)
        {
            characterStat.IncreaseRevengeValue(C.revengeValue);
        }

        //print("ArmorBreak = " + C.ArmorBreak + "   Get_SuperArmorLevel = " + characterController.Get_SuperArmorLevel());
        print("Enemy Ouch e Ouch | Knockback = " + C.GetKnockbackType() + " Damage = " + damage);
        characterStat.DamageDeal((int)damage);
        if ( C.ArmorBreak >= characterController.Get_SuperArmorLevel() && !characterStat.IsHitRVvalue())
            characterController.Activate_Hurt(C.GetKnockbackType());
        

        //characterStat.AddRevengeValue(C.revengeValue);

        /////////////////// Sound Effect part ////////////////////////
        SFX.PlayHit(C.sfxHitCode);

    }

}