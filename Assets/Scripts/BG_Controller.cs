using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Controller : MonoBehaviour
{
    private GameObject _backgroundContainer;
    private GameObject[] _backgroundsArray;

    public float speed;
    public bool gameStart;
    public bool gameFinished;

    private int _bgCounter = 0;
    private int _bgSelector;

    private GameObject _previousBG;
    private GameObject _currentBG;

    private float _bgSize;
    private Vector3 _screenSize;
    private bool _outOfScreen;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStart==true && gameFinished==false)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        
        if(_previousBG.transform.position.x + _bgSize < _screenSize.x && _outOfScreen == false)
        {
            //Debug.Log("A");
            _outOfScreen = true;
            // Destroy previous background
            DestroyPrevious();
        }
    }

    void MeazureScreen()
    {
        _screenSize = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x +7f, 0, 0);
    }

    void DestroyPrevious()
    {
        Destroy(_previousBG);
        _bgSize = 0;
        _previousBG = null;
        CreateBackground();
    }

    void BGControllerSpeed()
    {
        speed = 2.5f;
    }

    void FindBackground()
    {
        _backgroundsArray = GameObject.FindGameObjectsWithTag("Background");
        for (int i = 0; i < _backgroundsArray.Length; i++)
        {
            _backgroundsArray[i].gameObject.transform.parent = _backgroundContainer.transform;
            _backgroundsArray[i].gameObject.SetActive(false);
            _backgroundsArray[i].gameObject.name = "BackgroundOFF_" + i.ToString();
        }
        CreateBackground();
    }

    void CreateBackground()
    {
        _bgCounter +=  1;
        _bgSelector = Random.Range(0, _backgroundsArray.Length);
        GameObject BG = Instantiate(_backgroundsArray[_bgSelector]);
        BG.SetActive(true);
        BG.name = "Background_" + _bgCounter.ToString();
        BG.transform.parent = gameObject.transform;
        placeBackground();

        _outOfScreen = false;
    }

    void placeBackground()
    {
        string previous = "Background_" + (_bgCounter - 1).ToString(); //Background_0
        _previousBG = GameObject.Find(previous);
        string current = "Background_" + _bgCounter.ToString(); //Background_1
        _currentBG = GameObject.Find(current);

        MeazureBackground();
        _currentBG.transform.position =
            new Vector3(_previousBG.transform.position.x + _bgSize,
            _previousBG.transform.position.y,
            _previousBG.transform.position.z);
    }

    void MeazureBackground()
    {
        _bgSize += _previousBG.gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void StartGame()
    {
        MeazureScreen();
        _backgroundContainer = GameObject.Find("BackgroundContainer");
        FindBackground();
        BGControllerSpeed();
    }
}
