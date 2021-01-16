using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject skipIntroButton;

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
    private ObstaclesController _obstaclesController;

    public GameObject intro;

    private DeadMenu _deadMenu;

    private bool _gameStarted;
    // Start is called before the first frame update
    void Start()
    {
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
        _obstaclesController = GameObject.Find("Obstacles").GetComponent<ObstaclesController>();

        //Activate hunter
        _hand.SetActive(false);
        //_hunter.SetActive(false);
        _hunterSprite.enabled = false;
        _car.SetActive(false);

        StartCoroutine(StartGameAfterIntro());
        _deadMenu = GameObject.Find("Canvas").GetComponent<DeadMenu>();

        skipIntroButton = GameObject.Find("SkipIntro");
    }

    // Update is called once per frame
    void Update()
    {
        //There should not be anything in the update because this class functions are big and they should not be called once per frame
    }

    public void StartGame()
    {
        _gameStarted = true;
        skipIntroButton.SetActive(false);
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
        intro.SetActive(false);

        //StartThrowing obstacles
        _obstaclesController.StartGame();

        //Play game music
        FindObjectOfType<AudioManager>().Play("MatchTheme");
    }

    public void FinishGame()
    {
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
        _obstaclesController._gameFinished = true;

        _hand.SetActive(false);

        //Show death menu
        //_hero.KillPlayer();
        StartCoroutine(ShowGameOver());

        var obstacles = GameObject.Find("Obstacles")?.GetComponent<ObstaclesController>();
        obstacles.StopObstacles();
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(1.5f);
        
        _deadMenu.Pause();
    }

    private IEnumerator StartGameAfterIntro()
    {
        yield return new WaitForSeconds(18f);
        if(!_gameStarted) //If not skipped intro
            StartGame();
    }
}
