using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_1_HitBox : MonoBehaviour
{

    // Set these in the editor
    #region HitBox list
    private BoxCollider clear;
    public BoxCollider F_Atk1_1;
    public BoxCollider F_Atk1_2;
    public BoxCollider F_Atk1_3;
    public BoxCollider F_Atk1_4;
    public BoxCollider F_Atk_Pre1_1;
    public BoxCollider F_Atk_Pre1_2;
    public BoxCollider F_Atk_Fin1_1;
    public BoxCollider F_Atk3_1;
    public BoxCollider F_Atk8_1;
    public BoxCollider F_Atk8_2;
    public BoxCollider F_Atk_Pre3_1;
    public BoxCollider F_Atk_Fin2_1;
    public BoxCollider HitBox_20011_Slash_1__1;
    public BoxCollider HitBox_20012_Slash_2__1;
    public BoxCollider HitBox_20013_Slash_3__1;
    public BoxCollider HitBox_20014_Slash_4F__1;
    public BoxCollider HitBox_20111_Stab_1__1;
    public BoxCollider HitBox_20112_Stab_2__1;
    public BoxCollider HitBox_20113_Stab_3__1;
    public BoxCollider HitBox_20114_Stab_4F__1;
    public BoxCollider HitBox_22011_Smash_1__1;
    public BoxCollider HitBox_22012_Smash_2__1;
    public BoxCollider HitBox_22013_Smash_3F__1;
    public BoxCollider HitBox_22111_Slam_1__1;
    public BoxCollider HitBox_22112_Slam_2__1;
    public BoxCollider HitBox_22113_Slam_3F__1;
    public BoxCollider HitBox_24011_Punch_1__1;
    public BoxCollider HitBox_24012_Punch_2__1;
    public BoxCollider HitBox_24013_Punch_3__1;
    public BoxCollider HitBox_24014_Punch_4__1;
    public BoxCollider HitBox_24015_Punch_5F__1;
    public BoxCollider HitBox_24111_Kick_1__1;
    public BoxCollider HitBox_24112_Kick_2__1;
    public BoxCollider HitBox_24113_Kick_3__1;
    public BoxCollider HitBox_24114_Kick_4__1;
    public BoxCollider HitBox_24115_Kick_5F__1;


    public BoxCollider frame16;
    public BoxCollider frame17;
    public BoxCollider frame18;
    public BoxCollider frame19;
    public BoxCollider frame20;
    public BoxCollider HitBox_40010_Block__1;
    public BoxCollider HitBox_40011_WeakBlock__1;
    public BoxCollider HitBox_42201_ChargeBlock__1;
    #endregion
    // Used for organization
    private BoxCollider[] colliders;

    // Collider on this game object
    private BoxCollider localCollider;

    public BoxCollider NULL { get; private set; }

    public bool Is_Block = false;


    /*
    public PlayerStat playerstat;
    public Player_Hit_Box_Mange HitBox_Manage;

    public Command Previous_Command_Hit_Number;
    public bool Hit_Disable = false;
    */

    public Character_Stat characterStat;
    public Character_1_HitBox_Manage HitBox_Manage;

    public Command Previous_Command_Hit_Number;
    public bool Hit_Disable = false;

    //public BattleController BattleControllers;

    public enum hitBoxes
    {
        clear ,// special case to remove all boxes
        frame_Attack1_1_Box,
        frame_Attack1_2_Box,
        frame_Attack1_3_Box,
        frame_Attack1_4_Box,
        //frame0Box,
        //frame1Box,
        //frame2Box,
        //frame3Box,
        frame_Attack_Pre1_1_Box,
        frame_Attack_Pre1_2_Box,
        frame_Attack_Fin1_1_Box,
        frame_Attack3_1_Box,
        frame_Attack8_1_Box,
        frame_Attack8_2_Box,
        frame_Attack_Pre3_1_Box,
        frame_Attack_Fin2_1_Box,

        HitBox_20011_Slash_1__1,
        HitBox_20012_Slash_2__1,
        HitBox_20013_Slash_3__1,
        HitBox_20014_Slash_4F__1,
        HitBox_20111_Stab_1__1,
        HitBox_20112_Stab_2__1,
        HitBox_20113_Stab_3__1,
        HitBox_20114_Stab_4F__1,
        HitBox_22011_Smash_1__1,
        HitBox_22012_Smash_2__1,
        HitBox_22013_Smash_3F__1,
        HitBox_22111_Slam_1__1,
        HitBox_22112_Slam_2__1,
        HitBox_22113_Slam_3F__1,
        HitBox_24011_Punch_1__1,
        HitBox_24012_Punch_2__1,
        HitBox_24013_Punch_3__1,
        HitBox_24014_Punch_4__1,
        HitBox_24015_Punch_5F__1,
        HitBox_24111_Kick_1__1,
        HitBox_24112_Kick_2__1,
        HitBox_24113_Kick_3__1,
        HitBox_24114_Kick_4__1,
        HitBox_24115_Kick_5F__1,

    frame16Box,
        frame17Box,
        frame18Box,
        frame19Box,
        frame20Box,
        HitBox_40010_Block__1,
        HitBox_40011_WeakBlock__1,
        HitBox_42201_ChargeBlock__1
    }
    // Use this for initialization


    void Start()
    {

        // Set up an array so our script can more easily set up the hit boxes

        colliders = new BoxCollider[] {        clear ,// special case to remove all boxes
                                        F_Atk1_1, F_Atk1_2, F_Atk1_3, F_Atk1_4,
                                        F_Atk_Pre1_1, F_Atk_Pre1_2,
                                        F_Atk_Fin1_1,
                                        F_Atk3_1,
                                        F_Atk8_1, F_Atk8_2,
                                        F_Atk_Pre3_1,
                                        F_Atk_Fin2_1,


        HitBox_20011_Slash_1__1,
        HitBox_20012_Slash_2__1,
        HitBox_20013_Slash_3__1,
        HitBox_20014_Slash_4F__1,
        HitBox_20111_Stab_1__1,
        HitBox_20112_Stab_2__1,
        HitBox_20113_Stab_3__1,
        HitBox_20114_Stab_4F__1,
        HitBox_22011_Smash_1__1,
        HitBox_22012_Smash_2__1,
        HitBox_22013_Smash_3F__1,
        HitBox_22111_Slam_1__1,
        HitBox_22112_Slam_2__1,
        HitBox_22113_Slam_3F__1,
        HitBox_24011_Punch_1__1,
        HitBox_24012_Punch_2__1,
        HitBox_24013_Punch_3__1,
        HitBox_24014_Punch_4__1,
        HitBox_24015_Punch_5F__1,
        HitBox_24111_Kick_1__1,
        HitBox_24112_Kick_2__1,
        HitBox_24113_Kick_3__1,
        HitBox_24114_Kick_4__1,
        HitBox_24115_Kick_5F__1,



            frame16, frame17, frame18, frame19, frame20,
                                        HitBox_40010_Block__1,
                                        HitBox_40011_WeakBlock__1,
                                        HitBox_42201_ChargeBlock__1};

        localCollider = gameObject.AddComponent<BoxCollider>();
        localCollider = colliders[1];
        // Create a polygon collider

        localCollider.isTrigger = true; // Set as a trigger so it doesn't collide with our environment

        //print(localCollider.center);
        //localCollider.enabled = false; // Clear auto-generated polygons
    }



    void OnTriggerEnter(Collider col)
    {

    }

    void Update()
    {

    }

    public hitBoxes currentHitBox;
    public int BlockHitBox_Actived_Type()
    {
        if(currentHitBox == hitBoxes.HitBox_40010_Block__1 
        || currentHitBox == hitBoxes.HitBox_42201_ChargeBlock__1){
            return 2;
        }
        else if(currentHitBox == hitBoxes.HitBox_40011_WeakBlock__1){
            return 1;
        }
        return 0;
    }
    public bool Is_BlockHitBox_Actived(){
        return(currentHitBox == hitBoxes.HitBox_40010_Block__1 
        || currentHitBox == hitBoxes.HitBox_42201_ChargeBlock__1
        ||currentHitBox == hitBoxes.HitBox_40011_WeakBlock__1);
    }
    public bool Is_HitBox_Actived()
    {
        return currentHitBox != hitBoxes.clear;
    }

    public void setHitBox(hitBoxes val)
    {
        currentHitBox = val;
        if (val != hitBoxes.clear)
        {
           // print("Set hitbox = " + val);
            if (val == hitBoxes.HitBox_40010_Block__1
             || val == hitBoxes.HitBox_42201_ChargeBlock__1
             || val == hitBoxes.HitBox_40011_WeakBlock__1)
            {
                
                Is_Block = true;
            }
            else Is_Block = false;
            //HitBox_Manage.transform.SetPositionAndRotation(colliders[(int)val].transform.position, colliders[(int)val].transform.rotation);
            HitBox_Manage.setHitBox_Real(colliders[(int)val]);

            /*
            //Debug.Log("Hitbox active");
            //localCollider = colliders[(int)val];
            //localCollider = colliders[1];
            //print(localCollider.size);
            localCollider.center = new Vector3(colliders[(int)val].center.x + colliders[(int)val].transform.localPosition.x
                                                , colliders[(int)val].center.y + colliders[(int)val].transform.localPosition.y
                                                , colliders[(int)val].center.z + colliders[(int)val].transform.localPosition.z  );
            //print(localCollider.center.x + " " + localCollider.center.y + " " + localCollider.center.z);
            //localCollider.center = colliders[(int)val].center ;
            localCollider.size = colliders[(int)val].size;
            return;
            */

            return;

        }
        HitBox_Manage.clearHitBox_Real();

        /*
        localCollider.center = new Vector3(0, 0, 0);
        localCollider.size = new Vector3(0, 0, 0);
        */
        //localCollider = NULL;
    }
}
