using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_StyleBar_Controller : MonoBehaviour {

    public Character_1_Data characterData;
    public Text styleRankLetter;
    static int stylePoint = 0;
    float stylePercent = 0;
    static int styleRank = 0; // D0 C1 B2 A3 S4 SS5 SSS6
    public Character_1 character;
    // public PlayerStat player;
    //public EnemyStat P_enemy;
    public Character_Stat characterStat;
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
        //MAX_HP = (int)characterStat.GetHP();
    }

    // Update is called once per frame
    void Update()
    {
        stylePercent = GetPercentage();
        //stylePercent = character.GetStylePoint();
        styleRankLetter.text = GetStyleRankFromPoint();

        objectRectTransform.sizeDelta = new Vector2(orix * stylePercent, objectRectTransform.sizeDelta.y);

        if (rpercent <= stylePercent)
        {
            rpercent = stylePercent;
            f = 0;
        }
        else
        {
            f++;
            if (f > 15)
            {
                rpercent = stylePercent;
                f = 0;
            }
        }

        residueRectTransform.sizeDelta = new Vector2(orix * rpercent, residueRectTransform.sizeDelta.y);
    }


    public string GetStyleRankFromPoint()
    {
        switch (characterData.styleRank)
        {
            case 0:
                return "D";
                break;
            case 1:
                return "C";
                break;
            case 2:
                return "B";
                break;
            case 3:
                return "A";
                break;
            case 4:
                return "S";
                break;
            case 5:
                return "SS";
                break;
            case 6:
                return "SSS";
                break;
            default:
                return "Er";
                break;
        }

    }
    private float GetPercentage()
    {
        switch (characterData.styleRank)
        {
            case 0:
                return characterData.StylePoint/100;
                break;
            case 1:
                return (characterData.StylePoint-100) / 120;
                break;
            case 2:
                return (characterData.StylePoint - 220) / 140;
                break;
            case 3:
                return (characterData.StylePoint - 360) / 160;
                break;
            case 4:
                return (characterData.StylePoint - 520) / 180;
                break;
            case 5:
                return (characterData.StylePoint - 700) / 220;
                break;
            case 6:
                return (characterData.StylePoint - 920) / 300;
                break;
            default:
                return 0;
                break;
        }
    }
}
