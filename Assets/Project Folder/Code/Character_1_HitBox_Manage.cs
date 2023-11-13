using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_1_HitBox_Manage : MonoBehaviour
{
    

    public BoxCollider localCollider_S;
    private BoxCollider localCollider;
    /*
    public PlayerController playercontroller;
    public Player_HitBox player_hitbox;
    */
    public Character_1 characterController;
    public Character_1_HitBox character_hitbox;

    public BoxCollider NULL { get; private set; }
    // Use this for initialization


    public SoundEffect SFX;

    void Start()
    {

        localCollider = localCollider_S;
        localCollider = gameObject.AddComponent<BoxCollider>();
        // Create a polygon collider
        localCollider.size = new Vector3(0, 0, 0);

        localCollider.isTrigger = true; // Set as a trigger so it doesn't collide with our environment
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider col)
    {

        //Debug.Log("Collider hit something!");
        //Debug.Log("Collider hit something!");
        //print("Hit = " + localCollider.size);
        //EnemyHitboxHandler e = col.GetComponent<EnemyHitboxHandler>();

        // This Character IS PLAYER
        if (characterController.Is_Player_or_Enemy)
        {
            if (col.tag == "Enemy_Attack")
            {
                //if (character_hitbox.Is_Block)
                //print("Get in");
                if (col.GetComponent<Character_1_HitBox_Manage>().character_hitbox.Is_Block)
                {
                    //characterController.Set_Is_Block_Successful(1);
                    print("Get Block");
                    if(character_hitbox.characterStat.GetCurrentCommand().ArmorBreak >= col.GetComponentInParent<Character_Stat>().GetCurrentCommand().ArmorBreak)
                    {
                        print(" character_hitbox.characterStat.GetCurrentCommand().ArmorBreak = " + character_hitbox.characterStat.GetCurrentCommand().ArmorBreak);
                        print(" col.GetComponent<Character_Stat>().GetCurrentCommand().ArmorBreak = " + col.GetComponentInParent<Character_Stat>().GetCurrentCommand().ArmorBreak);
                        print("ININININININININI");
                        SFX.PlaySound_Effect(8);
                    }
                    else{
                        characterController.setKnockback_Multi(1.0f);


                        characterController.Activate_Hurt(1);
                    }
                }
                else if (character_hitbox.Is_Block)
                {
                    if (character_hitbox.characterStat.GetCurrentCommand().ArmorBreak <= col.GetComponentInParent<Character_Stat>().GetCurrentCommand().ArmorBreak)
                    {
                        print("Guard Break");
                        SFX.PlaySound_Effect(8);
                    }
                    else
                    {
                        characterController.Block_Successful_Activate(true);
                        print("Block");
                    }

                }
            }
            else if (col.tag == "Enemy_Hurt")
            {
                if (character_hitbox.Is_Block)
                {

                }
                else if (!character_hitbox.Hit_Disable && !character_hitbox.Is_Block)
                {

                    //Used for Data
                    characterController.Last_Attack_Code_Hit_Check = characterController.Last_Attack_Code;

                    print("Hit " + character_hitbox.characterStat.GetCurrentCommand().Potency
                                    + " " + character_hitbox.characterStat.GetCurrentCommand().KnockbackForce);

                    character_hitbox.Previous_Command_Hit_Number = character_hitbox.characterStat.GetCurrentCommand();
                    character_hitbox.Hit_Disable = true;
                }
                //if()
            }
        }


        // This Character IS ENEMY
        else if (!characterController.Is_Player_or_Enemy) { 
            //EnemyHitboxHandler e = col.GetComponent<EnemyHitboxHandler>();
            if (Battle_Controller.game_Mode[characterController.CharacterCode] == 1)
            {
                if (col.tag == "Player_Attack")
                {

                    if (col.GetComponent<Character_1_HitBox_Manage>().character_hitbox.Is_Block)
                    {
                        print("Get Block");
                        if (character_hitbox.characterStat.GetCurrentCommand().ArmorBreak >= col.GetComponentInParent<Character_Stat>().GetCurrentCommand().ArmorBreak)
                        {
                            print(" character_hitbox.characterStat.GetCurrentCommand().ArmorBreak = " + character_hitbox.characterStat.GetCurrentCommand().ArmorBreak);
                            print(" col.GetComponent<Character_Stat>().GetCurrentCommand().ArmorBreak = " + col.GetComponentInParent<Character_Stat>().GetCurrentCommand().ArmorBreak);

                        }
                        else
                        {
                            characterController.setKnockback_Multi(1.0f);


                            characterController.Activate_Hurt(1);
                        }
                    }
                    else if (character_hitbox.Is_Block)
                    {

                        if (character_hitbox.characterStat.GetCurrentCommand().ArmorBreak <= col.GetComponentInParent<Character_Stat>().GetCurrentCommand().ArmorBreak)
                        {
                            print("Guard Break");
                            print("ININININININININI");
                            SFX.PlaySound_Effect(8);
                        }
                        else
                        {
                            characterController.Block_Successful_Activate(true);
                            print("Block");
                        }
                    }
                }
                else if (col.tag == "Player_Hurt")
                {
                    if (character_hitbox.Is_Block)
                    {
                    }
                    else if (!character_hitbox.Hit_Disable && !character_hitbox.Is_Block)
                    {
                        characterController.Last_Attack_Code_Hit_Check = characterController.Last_Attack_Code;

                        print("Hit " + character_hitbox.characterStat.GetCurrentCommand().Potency
                                       + " " + character_hitbox.characterStat.GetCurrentCommand().KnockbackForce);
                        character_hitbox.Previous_Command_Hit_Number = character_hitbox.characterStat.GetCurrentCommand();
                        character_hitbox.Hit_Disable = true;
                    }
                    //if()
                }
            }
            else
            {
                if (col.tag == (characterController.Is_P_Enemy ? "Enemy_Attack" : "P_Enemy_Attack"))
                {
                    if (col.GetComponent<Character_1_HitBox_Manage>().character_hitbox.Is_Block)
                    {
                        print("Get Block");
                        if (character_hitbox.characterStat.GetCurrentCommand().ArmorBreak >= col.GetComponentInParent<Character_Stat>().GetCurrentCommand().ArmorBreak)
                        {
                            print(" character_hitbox.characterStat.GetCurrentCommand().ArmorBreak = " + character_hitbox.characterStat.GetCurrentCommand().ArmorBreak);
                            print(" col.GetComponent<Character_Stat>().GetCurrentCommand().ArmorBreak = " + col.GetComponentInParent<Character_Stat>().GetCurrentCommand().ArmorBreak);

                        }
                        else
                        {
                            characterController.setKnockback_Multi(1.0f);


                            characterController.Activate_Hurt(1);
                        }
                    }
                    else if (character_hitbox.Is_Block)
                    {

                        if (character_hitbox.characterStat.GetCurrentCommand().ArmorBreak <= col.GetComponentInParent<Character_Stat>().GetCurrentCommand().ArmorBreak)
                        {
                            print("Guard Break");
                            print("ININININININININI");
                            SFX.PlaySound_Effect(8);
                        }
                        else
                        {
                            characterController.Block_Successful_Activate(true);
                            print("Block");
                        }
                    }
                    /*
                    if (col.GetComponent<Character_1_HitBox_Manage>().character_hitbox.Is_Block)
                    {

                        print("BLOCKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK ");
                        //characterController.Set_Is_Block_Successful(1);
                        characterController.setKnockback_Multi(1.0f);


                        characterController.Activate_Hurt(1);
                    }
                    else if (character_hitbox.Is_Block)
                    {
                        characterController.Block_Successful_Activate(true);
                        //print("IS BLOCK? = " + enemy_hitbox.Is_Block );
                        //print("Block");
                    }
                    */
                }
                else if (col.tag == (characterController.Is_P_Enemy ? "Enemy_Hurt" : "P_Enemy_Hurt"))
                {
                    if (character_hitbox.Is_Block)
                    {

                    }
                    else if (!character_hitbox.Hit_Disable && !character_hitbox.Is_Block)
                    {
                        characterController.Last_Attack_Code_Hit_Check = characterController.Last_Attack_Code;
                        character_hitbox.Previous_Command_Hit_Number = character_hitbox.characterStat.GetCurrentCommand();
                        character_hitbox.Hit_Disable = true;
                          print("Hit " + character_hitbox.characterStat.GetCurrentCommand().Potency
                            + " " + character_hitbox.characterStat.GetCurrentCommand().KnockbackForce);
                    }
                    //if()
                }
            }
    }

        //Debug.Log(player.GetCommand(player.GetCurrentForm(), player.GetCurrentCommand()).Name);
        //Debug.Log(player.GetCommand(player.GetCurrentForm(), player.GetCurrentCommand()).GetPotency());

        /*
        if (e != null && !col.GetComponent<Enemy_Riku_AI>().ISinvinciable())
        {
            int damage = BattleControllers.CalDamage(player.GetAttack(BattleController.currentATB), player.GetMagic(BattleController.currentATB),
                                                    (int)player.GetCommand(player.GetCurrentForm(), player.GetCurrentCommand()).GetTYPE(),
                                                    player.GetCommand(player.GetCurrentForm(), player.GetCurrentCommand()).GetPotency(),

                                                    1,
                                                    0,
                                                    0,
                                                    0,
                                                    1,
                                                    1,
                                                    0);
        
            //Debug.Log(damage);

            e.KnockBack(0.05f, 20f, player.transform.position.x, e.transform.position.x, Vector2.Angle(player.transform.position, e.transform.position), player.CommandCount);
            e.DamageEnemy(damage);
            
        */
        //Debug.Log(player.transform.position.x + " " + player.transform.position.y + "   " + e.transform.position.x + " " + e.transform.position.y);
        //Debug.Log(Vector2.Angle(player.transform.position, e.transform.position)); 

        //}
    }

    public void setHitBox_Real(BoxCollider val)
    {
        
        characterController.Set_Is_HitBoxActivate(1);

        //localCollider = val;
        localCollider.center = new Vector3(val.center.x + val.transform.localPosition.x
                                                , val.center.y + val.transform.localPosition.y
                                                , val.center.z + val.transform.localPosition.z);
        //print(localCollider.center.x + " " + localCollider.center.y + " " + localCollider.center.z);
        //localCollider.center = colliders[(int)val].center ;
        localCollider.size = val.size;
        //print(localCollider.size + "    " + localCollider.center);

    }
    public void clearHitBox_Real()
    {
        characterController.Set_Is_HitBoxActivate(0);
        localCollider.center = new Vector3(0, 0, 0);
        localCollider.size = new Vector3(0, 0, 0);


    }
}
