using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterControllerIntro : MonoBehaviour
{
    private GameObject hunterIntro;
    private GameObject hunterArmIntro;
    private GameObject carIntro;

    void Awake()
    {
        hunterIntro = GameObject.Find("IntroTimelineManager/HunterIntro");
        hunterArmIntro = GameObject.Find("IntroTimelineManager/HunterArmIntro");
        carIntro = GameObject.Find("IntroTimelineManager/CarIntro");
    }

    // Update is called once per frame
    void Update()
    {
        hunterArmIntro.transform.position = hunterIntro.transform.position;
        carIntro.transform.position = hunterIntro.transform.position + new Vector3(1.5f, -0.5f, 0);
    }
}
