using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour {

    public static bool GameIsPause = false;

    public GameObject pauseMenuUI;

    //public AudioSource BGM;
    //public AudioSource audioSource;
   // public AudioClip pauseSFX;

    // Update is called once per frame
    void Update()
    {
        //print("PAUSE");
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button9) )
        {
            
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
      //  BGM.Play();
        //audioSource.PlayOneShot(pauseSFX);
        Time.timeScale = 1f;
        GameIsPause = false;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
     //   BGM.Pause();
        //audioSource.PlayOneShot(pauseSFX);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPause = false;
        SceneManager.LoadScene("title");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting menu....");
        Application.Quit();
    }
    public string previousscene;
    public void Quit_Game()
    {
        SceneManager.LoadScene(previousscene, LoadSceneMode.Single);
    }

}
