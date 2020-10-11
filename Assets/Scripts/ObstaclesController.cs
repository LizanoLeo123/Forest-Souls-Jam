using Assets.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject prefabAnimalCage;
    public GameObject prefabRock;
    public GameObject prefabBullets;
    public GameObject prefabBoomerang;

    private List<CageController> cages = new List<CageController>();
    private Bounds cameraBounds;
    private Vector3 startPosition;
    private GameObject hunter = null;

    private HunterController hunterCt = null;
    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        cameraBounds = Camera.main.OrthographicBounds();
        startPosition = new Vector3(cameraBounds.max.x, 0, 0);
        
        hunter = GameObject.Find("Hunter");
        hunterCt = hunter.GetComponent<HunterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: temp keycode to instantiate a new cage prefab
        if (Input.GetKeyDown(KeyCode.O))
        {
            startPosition = hunter.transform.position + new Vector3(-0.2f, -0.25f, 0);
            NewObstacle();
        }
    }

    private void NewObstacle()
    {
        int obstacleCode = 2; // Random.Range(0, 3);
        switch (obstacleCode)
        {
            case 0:
                hunterCt.ThrowBox();
                Invoke("ThrowNewAnimalCage", 2.0f);
                break;
            case 1:
                hunterCt.ThrowRock();
                Invoke("ThrowRocks", 2.0f);
                break;
            case 2:
                hunterCt.Shoot();
                Invoke("FireShotgun", 0.85f);
                break;
            default:
                break;
        }

        //Invoke("NewObstacle", 3.0f);
    }

    private static List<string> animals = new List<string>() { "monkey", "snake", "sloth" };

    private void ThrowNewAnimalCage()
    {
        var newCage = Instantiate(prefabAnimalCage, startPosition, Quaternion.identity);
        var newCageController = newCage.GetComponent<CageController>();

        newCageController.animalName = animals[Random.Range(0, animals.Count)];
        
        // Add to array, to keep track of it
        cages.Add(newCageController);
    }

    private void ThrowRocks()
    {
        var rock1 = Instantiate(prefabRock, startPosition, Quaternion.identity);
        var rock2 = Instantiate(prefabRock, startPosition, Quaternion.identity);
        var rock3 = Instantiate(prefabRock, startPosition, Quaternion.identity);
    }

    private void FireShotgun()
    {
        // Create bullets
        var bullet1 = Instantiate(prefabBullets, startPosition, Quaternion.identity);
        var bullet2 = Instantiate(prefabBullets, startPosition, Quaternion.identity);
    }
}
