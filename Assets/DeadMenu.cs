using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeadMenu : MonoBehaviour
{
	public static bool GameOver = false;

	public GameObject deadMenuUI;

	public string menuScene;

	public string pricipalScene;



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){ // Condicion para que el personaje muera
        	if(GameOver){
        		Restart();
        	}else{
        		Pause();
        	}
        }
    }

    public void Restart(){
    	deadMenuUI.SetActive(false);
        FindObjectOfType<AudioManager>().Stop("MatchSummary");
        FindObjectOfType<AudioManager>().Play("MatchTheme");
        
    	Time.timeScale = 1f;
    	GameOver = false;
    	SceneManager.LoadScene(pricipalScene);
    }

    public void Pause(){
    	deadMenuUI.SetActive(true);
    	GameOver = true;
    }


    public void LoadMenu(){
    	Time.timeScale = 1f;
    	SceneManager.LoadScene(menuScene);
    }

    public void QuitGame(){
    	Application.Quit();
    }
}
