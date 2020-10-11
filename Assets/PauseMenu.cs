using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
	public static bool GameisPause = false;

	public GameObject pauseMenuUI;

	public string menuScene;



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
        	if(GameisPause){
        		Resume();
        	}else{
        		Pause();
        	}
        }
    }

    public void Resume(){
    	pauseMenuUI.SetActive(false);
    	Time.timeScale = 1f;
    	GameisPause = false;
    }

    void Pause(){
    	pauseMenuUI.SetActive(true);
    	Time.timeScale = 0f;
    	GameisPause = true;
    }


    public void LoadMenu(){
    	Time.timeScale = 1f;
    	SceneManager.LoadScene(menuScene);
    }

    public void QuitGame(){
    	Application.Quit();
    }
}
