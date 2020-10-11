using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterController : MonoBehaviour
{
    private GameObject hunter;
    private GameObject hunterArm;
    private GameObject car;
    private Animator hunterAnimator;
    private Animator hunterArmAnimator;
    //private GameObject obstacles;
    //private ObstaclesController obstaclesController;

    void Awake()
    {
        hunter = GameObject.Find("Hunter");
        hunterArm = GameObject.Find("HunterArm");
        hunterAnimator = hunter.GetComponent<Animator>();
        hunterArmAnimator = hunterArm.GetComponent<Animator>();
        car = GameObject.Find("Car");

        //obstacles = GameObject.Find("Obstacles");
        //obstaclesController = obstacles.GetComponent<ObstaclesController>();
    }

    // Update is called once per frame
    void Update()
    {
        hunterArm.transform.position = hunter.transform.position;
        car.transform.position = hunter.transform.position + new Vector3(1.5f,-0.5f,0);

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    Shoot();
        //}
    }

    public void Shoot()
    {
        hunterAnimator.SetTrigger("shoot");
        //obstaclesController.FireShotgun();
        //obstaclesController.Invoke("FireShotgun", 1.0f);
    }

    public void ThrowBox()
    {
        hunterArmAnimator.SetTrigger("throwBox");
    }

    public void ThrowRock()
    {
        hunterArmAnimator.SetTrigger("throwRock");
    }
}
