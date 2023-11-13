using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour {

    public static int GameMode = 0;
    public static int ScoreMode = 0;
    public static int LoadFile_P = 0;
    public static int SaveFile_P = 0;
    public static int LoadFile_E = 0;
    public static int SaveFile_E = 0;

    public string nextscene;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Start_Game_PlayerVAI_A()
    {
        GameMode = 1;
        ScoreMode = 1;
        LoadFile_P = 0;
        SaveFile_P = 0;
        LoadFile_E = 205;
        SaveFile_E = 305;
            
        Time.timeScale = 1f;
        print("ASD");
        SceneManager.LoadScene(nextscene, LoadSceneMode.Single);

    }
    public void Start_Game_PlayerVAI_D()
    {/*
        GameMode = 1;
        ScoreMode = 2;
        LoadFile_P = 0;
        SaveFile_P = 0;
        LoadFile_E = 205;
        SaveFile_E = 305;
        Time.timeScale = 1f;

        SceneManager.LoadScene(nextscene, LoadSceneMode.Single);
*/
    }
    public void Start_Game_AIVAI_A()
    {
        GameMode = 2;
        ScoreMode = 1;
        LoadFile_P = 205;
        SaveFile_P = 305;
        LoadFile_E = 205;
        SaveFile_E = 305;
        Time.timeScale = 1f;

        SceneManager.LoadScene(nextscene, LoadSceneMode.Single);

    }
    public void Start_Game_AIVAI_D()
    {/*
        GameMode = 2;
        ScoreMode = 2;
        LoadFile_P = 205;
        SaveFile_P = 305;
        LoadFile_E = 205;
        SaveFile_E = 305;
        Time.timeScale = 1f;

        SceneManager.LoadScene(nextscene, LoadSceneMode.Single);
*/
    }

    public void Quit_Game()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
