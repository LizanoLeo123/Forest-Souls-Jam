using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadMenu : MonoBehaviour
{
	public static bool GameOver = false;

	public GameObject deadMenuUI;

	public string menuScene;

	public string pricipalScene;

    public Text coinsLabel;
    public Text coinsWonLabel;

    private Score _timer;

    private float animationTime = 0.8f;
    private float currentNumber;
    private float initialNumber;
    private float desiredNumber;

    private void Start()
    {
        _timer = GameObject.Find("Timer").GetComponent<Score>();
        currentNumber = Game.Instance.coins;
        coinsLabel.text = Game.Instance.coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.F)){ // Condicion para que el personaje muera
        //	if(GameOver){
        //		Restart();
        //	}else{
        //		Pause();
        //	}
        //}
        if(currentNumber != desiredNumber)
        {
            if(initialNumber < desiredNumber)
            {
                currentNumber += (animationTime * Time.deltaTime) * (desiredNumber - initialNumber);
                if (currentNumber >= desiredNumber)
                    currentNumber = desiredNumber;
            }
        }
        coinsLabel.text = currentNumber.ToString("0");
    }

    private void SetNumber(float value)
    {
        initialNumber = currentNumber;
        desiredNumber = value;
    }

    public void Restart(){
    	deadMenuUI.SetActive(false);
        FindObjectOfType<AudioManager>().Stop("MatchSummary");   
        FindObjectOfType<AudioManager>().Play("People");        
    	Time.timeScale = 1f;
    	GameOver = false;
    	SceneManager.LoadScene(pricipalScene);
    }

    public void Pause(){
    	deadMenuUI.SetActive(true);

        //Add coins to player
        AddCoins((int)_timer.time);

    	GameOver = true;
    }


    public void LoadMenu(){
    	Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Stop("MatchSummary");
    	SceneManager.LoadScene(menuScene);
    }

    public void QuitGame(){
    	Application.Quit();
    }

    void AddCoins(int time)
    {
        currentNumber = Game.Instance.coins;
        int coins = time / 2; //Each two seconds equals to one coin
        Game.Instance.AddCoins(coins);
        coinsWonLabel.text = "+" + coins.ToString();

        //Call the animation
        StartCoroutine(ShowCoinsWon());
    }

    private IEnumerator ShowCoinsWon()
    {
        yield return new WaitForSeconds(1f);
        SetNumber(Game.Instance.coins);

        //Wait until animation finished
        yield return new WaitForSeconds(animationTime);
        coinsWonLabel.text = "";
    }
}
