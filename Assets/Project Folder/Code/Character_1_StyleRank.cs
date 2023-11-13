using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_1_StyleRank : MonoBehaviour {


    static int stylePoint = 0;
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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*
        stylePercent = character.GetS();
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
        */
    }




}
