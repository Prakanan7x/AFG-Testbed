using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_AI_Text_Manage : MonoBehaviour {

	public Text AI_Description_Text;
    public Text AI_CurrentWeightList_Text;

    static string AI_Description;
    static string AI_CurrentWeightList;
    static List<int> currentWeightList;
    public Character_1_AIWrite characterAIWrite;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        AI_Description = characterAIWrite.lastSave_LoadAIString;
        AI_Description_Text.text = AI_Description;

        //currentWeightList = characterAIWrite.currentWeightList;

       
    }


}
