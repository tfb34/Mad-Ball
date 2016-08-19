using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {
    public GameObject PauseUI;
    AudioSource music;
	// Use this for initialization
	void Start () {
        PauseUI.SetActive(false);
        music = GetComponent<AudioSource>();
        
       // Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
      
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseUI.activeInHierarchy)
            {
                Resume();
            }
            else
            {

                PauseUI.SetActive(true);
                music.Pause();
                Time.timeScale = 0;
            }
        }
 
	}
    public void Resume()
    {
        PauseUI.SetActive(false);
        music.Play();
        Time.timeScale = 1;

    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
   
}
