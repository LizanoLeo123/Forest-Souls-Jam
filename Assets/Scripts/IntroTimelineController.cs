using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTimelineController : MonoBehaviour
{
    //private GameObject sloth;
    //private GameObject animalCageIntro;
    private GameObject _groundAnimated;

    private BG_Controller _bgFar;
    private BG_Controller2 _bgMid;
    private BG_Controller3 _bgClose;

    void Awake()
    {
        //sloth = GameObject.Find("IntroTimelineManager/Sloth");
        //animalCageIntro = GameObject.Find("IntroTimelineManager/AnimalCageIntro");
        _groundAnimated = GameObject.Find("Grid/GroundAnimated");
        
        FindObjectOfType<AudioManager>().Play("People");
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _bgFar = GameObject.Find("BackgroundFar").GetComponent<BG_Controller>();
        _bgMid = GameObject.Find("BackgroundMid").GetComponent<BG_Controller2>();
        _bgClose = GameObject.Find("BackgroundClose").GetComponent<BG_Controller3>();
        StartCoroutine(moveGround());
    }

    // Update is called once per frame
    void Update()
    {
        //sloth.transform.position = animalCageIntro.transform.position;
    }

    private IEnumerator moveGround()
    {
        yield return new WaitForSeconds(13.33f);
        _groundAnimated.SetActive(true);

        _bgFar.gameStart = true;
        _bgMid.gameStart = true;
        _bgClose.gameStart = true;
    }
}
