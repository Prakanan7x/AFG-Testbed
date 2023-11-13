using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_HP_Bar_Controller : MonoBehaviour
{
    public Text mytext;

    static int MAX_HP;
    float percent;
    public Character_1 character;
    // public PlayerStat player;
    //public EnemyStat P_enemy;
    public Character_Stat characterStat;
    public static bool Is_Player_or_Enemy;
    public static int characterCode;

    RectTransform objectRectTransform;
    RawImage myimage;
    float orix;

    public GameObject residue;
    RawImage residueimg;
    RectTransform residueRectTransform;

    private int f = 0; //frame delay
    float rpercent = -1; //residue's percent
    // Use this for initialization
    void Start()
    {
        objectRectTransform = GetComponent<RectTransform>();
        myimage = GetComponent<RawImage>();
        orix = objectRectTransform.sizeDelta.x;

        residueimg = residue.GetComponent<RawImage>();
        residueRectTransform = residue.GetComponent<RectTransform>();

        //print(character.CharacterCode);
        //print(Battle_Controller.game_Mode[0] + " " + Battle_Controller.game_Mode[1]);
        /*
        if (Battle_Controller.game_Mode[character.CharacterCode] == 1)
            MAX_HP = (int)characterStat.GetHP();
        else
        {
            MAX_HP = (int)characterStat.GetHP();
        }
        */
        MAX_HP = (int)characterStat.GetHP();
    }

    // Update is called once per frame
    void Update()
    {
        Is_Player_or_Enemy = character.Is_Player_or_Enemy;
        characterCode = character.CharacterCode;
        /*
        percent = Is_Player_or_Enemy ?
            Battle_Controller.Player_percentHP[characterCode]
            :
            Battle_Controller.Enemy_percentHP[characterCode]

            ;
            */
        percent = characterStat.GetCur_HP();
        mytext.text = (int)(percent * MAX_HP) + "/" + MAX_HP;

        objectRectTransform.sizeDelta = new Vector2(orix * percent, objectRectTransform.sizeDelta.y);

        if (rpercent <= percent)
        {
            rpercent = percent;
            f = 0;
        }
        else
        {
            f++;
            if (f > 30)
            {
                rpercent = percent;
                f = 0;
            }
        }

        residueRectTransform.sizeDelta = new Vector2(orix * rpercent, residueRectTransform.sizeDelta.y);
    }

    public static void UpdateMaxHP()
    {
        MAX_HP = (int)( Is_Player_or_Enemy ?
            Battle_Controller.Player_percentHP[characterCode]
            :
            Battle_Controller.Enemy_percentHP[characterCode])

            ;
    }
}
