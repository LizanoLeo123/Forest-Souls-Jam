using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameFinished;
    public bool startGame;

    private GameObject _heroPrefab;

    private BG_Controller _bgFar;
    private BG_Controller2 _bgMid;
    private BG_Controller3 _bgClose;

    private GameObject _ground;
    private GameObject _groundAnimated;

    private Score _score;

    private HeroMovement _hero;

    private GameObject _car;
    private GameObject _hunter;
    private GameObject _hand;
    private Animator _carAnimator;
    private Animator _hunterAnimator;
    private SpriteRenderer _hunterSprite;

    public GameObject intro;

    private DeadMenu _deadMenu;
    // Start is called before the first frame update
    void Start()
    {
        gameFinished = false;
        _bgFar = GameObject.Find("BackgroundFar").GetComponent<BG_Controller>();
        _bgMid = GameObject.Find("BackgroundMid").GetComponent<BG_Controller2>();
        _bgClose = GameObject.Find("BackgroundClose").GetComponent<BG_Controller3>();

        _ground = GameObject.Find("Ground");
        _groundAnimated = GameObject.Find("GroundAnimated");
        _groundAnimated.SetActive(false);
        

        _score = GameObject.Find("Timer").GetComponent<Score>();

        //This should not be here
        //_hero = GameObject.Find("Hero").GetComponent<HeroMovement>();
        _heroPrefab = GameObject.Find("Hero");
        _heroPrefab.SetActive(false);

        _car = GameObject.Find("Car");
        _hunter = GameObject.Find("Hunter");
        _hunterSprite = _hunter.GetComponent<SpriteRenderer>();
        _hand = GameObject.Find("HunterArm");
        _carAnimator = GameObject.Find("Car").GetComponent<Animator>();
        _hunterAnimator = GameObject.Find("Hunter").GetComponent<Animator>();
        //Activate hunter
        _hand.SetActive(false);
        //_hunter.SetActive(false);
        _hunterSprite.enabled = false;
        _car.SetActive(false);

        StartCoroutine(StartGameAfterIntro());
        _deadMenu = GameObject.Find("Canvas").GetComponent<DeadMenu>();


    }

    // Update is called once per frame
    void Update()
    {
        if(gameFinished == true)
        {
            FinishGame();
        }
        if (startGame)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        _heroPrefab.SetActive(true);
        _ground.SetActive(false);
        _groundAnimated.SetActive(true);

        _bgFar.gameStart = true;
        _bgMid.gameStart = true;
        _bgClose.gameStart = true;

        //Activate hunter
        _hand.SetActive(true);

        //_hunter.SetActive(true);
        _hunterSprite.enabled = true;
        _car.SetActive(true);

        //Start timer
        _score.start = true;
        intro.SetActive(true);
    }

    private void FinishGame()
    {
        startGame = false;
        //Stop ground movement
        _ground.SetActive(true);
        _groundAnimated.SetActive(false);

        _bgFar.gameStart = false;
        _bgMid.gameStart = false;
        _bgClose.gameStart = false;

        //Stop timer
        _score.start = false;

        //Animations of the car and hunter
        _carAnimator.SetTrigger("GameFinished");
        _hunterAnimator.SetTrigger("GameFinished");

        _hand.SetActive(false);

        //Show death menu
        //_hero.KillPlayer();
        StartCoroutine(ShowGameOver());
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(1.5f);
        
        _deadMenu.Pause();
    }

    private IEnumerator StartGameAfterIntro()
    {
        yield return new WaitForSeconds(15f);
        StartGame();
    }
}
