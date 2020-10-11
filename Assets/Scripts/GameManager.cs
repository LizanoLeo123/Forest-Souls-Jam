using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameFinished;

    private BG_Controller _bgFar;
    private BG_Controller2 _bgMid;
    private BG_Controller3 _bgClose;

    private GameObject _ground;
    private GameObject _groundAnimated;

    private Score _score;

    private HeroMovement _hero;

    private DeadMenu _deadMenu;
    // Start is called before the first frame update
    void Start()
    {
        gameFinished = false;
        _bgFar = GameObject.Find("BackgroundFar").GetComponent<BG_Controller>();
        _bgMid = GameObject.Find("BackgroundMid").GetComponent<BG_Controller2>();
        _bgClose = GameObject.Find("BackgroundClose").GetComponent<BG_Controller3>();

        _ground = GameObject.Find("Ground");
        _ground.SetActive(false);
        _groundAnimated = GameObject.Find("GroundAnimated");

        _score = GameObject.Find("Timer").GetComponent<Score>();

        //This should not be here
        _hero = GameObject.Find("Hero").GetComponent<HeroMovement>();

        _deadMenu = GameObject.Find("Canvas").GetComponent<DeadMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameFinished == true)
        {
            //Stop ground movement
            _ground.SetActive(true);
            _groundAnimated.SetActive(false);

            _bgFar.gameStart = false;
            _bgMid.gameStart = false;
            _bgClose.gameStart = false;

            //Stop timer
            _score.start = false;

            //Show death menu
            _hero.KillPlayer();
            StartCoroutine(ShowGameOver());
        }
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(1.5f);
        
        _deadMenu.Pause();
    }
}
