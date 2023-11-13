using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_1_ModeSwitchUI : MonoBehaviour {

	// Use this for initialization
	public GameObject Background_Default;
	public GameObject Background_Yellow;
	public GameObject Background_Red;
	public GameObject Background_Blue;

	public GameObject Text_Default;
	public GameObject Text_Yellow;
	public GameObject Text_Red;
	public GameObject Text_Blue;

	private int curCharMode = 0;
	public void UIModeSwitch(int mode)
    {
		curCharMode = mode;
        switch(curCharMode){
			case 0:
				Background_Default.SetActive(true);
				Background_Yellow.SetActive(false);
				Background_Red.SetActive(false);
				Background_Blue.SetActive(false);

				Text_Default.SetActive(true);
				Text_Yellow.SetActive(false);
				Text_Red.SetActive(false);
				Text_Blue.SetActive(false);
				break;
			case 1:
				Background_Default.SetActive(false);
				Background_Yellow.SetActive(true);
				Background_Red.SetActive(false);
				Background_Blue.SetActive(false);
				
				Text_Default.SetActive(false);
				Text_Yellow.SetActive(true);
				Text_Red.SetActive(false);
				Text_Blue.SetActive(false);
				break;
			case 2:
				Background_Default.SetActive(false);
				Background_Yellow.SetActive(false);
				Background_Red.SetActive(true);
				Background_Blue.SetActive(false);
				
				Text_Default.SetActive(false);
				Text_Yellow.SetActive(false);
				Text_Red.SetActive(true);
				Text_Blue.SetActive(false);
				break;
			case 3:
				Background_Default.SetActive(false);
				Background_Yellow.SetActive(false);
				Background_Red.SetActive(false);
				Background_Blue.SetActive(true);

				Text_Default.SetActive(false);
				Text_Yellow.SetActive(false);
				Text_Red.SetActive(false);
				Text_Blue.SetActive(true);
				break;
		}
    }
}
