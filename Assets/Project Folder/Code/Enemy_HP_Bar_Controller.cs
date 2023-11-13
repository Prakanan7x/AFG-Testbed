using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_HP_Bar_Controller : MonoBehaviour {

    public Text mytext;

    static int MAX_HP;
    float percent;
    //public EnemyStat enemy;
    public Character_Stat character;

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

        MAX_HP = (int)character.GetHP();
    }

    // Update is called once per frame
    void Update()
    {
        MAX_HP = (int)character.GetHP();
        percent = Battle_Controller.Enemy_percentHP[0];
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
        MAX_HP = (int)Battle_Controller.Enemy_HPMax[0];
    }
}