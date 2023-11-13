using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_RV_Bar_Controller : MonoBehaviour {

    //public Text mytext;

    static float MAX_RV;
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

        MAX_RV = character.Cur_RV;
    }

    // Update is called once per frame
    void Update()
    {
        percent = character.Cur_RV / character.RevengeValue;
       // mytext.text = (int)(percent * MAX_RV) + "/" + MAX_RV;

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

    public void UpdateMaxRV()
    {
        MAX_RV = character.Cur_RV;
    }
}