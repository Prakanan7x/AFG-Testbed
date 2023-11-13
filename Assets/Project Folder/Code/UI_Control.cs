using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Control : MonoBehaviour {

	
	readonly public KeyCode HideUI1 = KeyCode.Z;
    readonly public KeyCode HideUI2 = KeyCode.Joystick1Button8;

    public static bool ShowControlMap = true;

	public static bool ShowPatternDesc = false;

	private int uiModeNumber = 0;
    public GameObject ControlMap;
	public GameObject PatternDesc;

    //public AudioSource BGM;
    //public AudioSource audioSource;
   // public AudioClip pauseSFX;

    // Update is called once per frame
    void Update()
    {
        //print("PAUSE");
        if (Input.GetKeyDown(HideUI1) || Input.GetKeyDown(HideUI2))
        {
            
			UISwitch();
        }
    }

    public void UISwitch()
    {
        switch(uiModeNumber){
			case 0:
				ShowControlMap = false;
				ShowPatternDesc = true;
				uiModeNumber = 1;
				break;
			case 1:
				ShowControlMap = false;
				ShowPatternDesc = false;
				uiModeNumber = 2;
				break;
			case 2:
				ShowControlMap = true;
				ShowPatternDesc = false;
				uiModeNumber = 0;
				break;
		}
		ControlMap.SetActive(ShowControlMap);
		PatternDesc.SetActive(ShowPatternDesc);
    }

  

}


