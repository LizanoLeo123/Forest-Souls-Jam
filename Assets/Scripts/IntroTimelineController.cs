using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTimelineController : MonoBehaviour
{
    private GameObject sloth;
    private GameObject animalCageIntro;

    void Awake()
    {
        sloth = GameObject.Find("IntroTimelineManager/Sloth");
        animalCageIntro = GameObject.Find("IntroTimelineManager/AnimalCageIntro");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sloth.transform.position = animalCageIntro.transform.position;
    }
}
