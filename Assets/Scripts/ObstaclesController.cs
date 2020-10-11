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
    public GameObject prefabGun;
    public GameObject prefabBoomerang;

    private List<CageController> cages = new List<CageController>();
    private Bounds cameraBounds;
    private Vector3 startPosition;
    private GameObject gun = null;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        cameraBounds = Camera.main.OrthographicBounds();
        startPosition = new Vector3(cameraBounds.max.x, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: temp keycode to instantiate a new cage prefab
        if (Input.GetKeyDown(KeyCode.O))
        {
            NewObstacle();
        }
    }

    private void NewObstacle()
    {
        int obstacleCode = Random.Range(0, 3);
        switch (obstacleCode)
        {
            case 0:
                ThrowNewAnimalCage();
                break;
            case 1:
                ThrowRocks();
                break;
            case 2:
                FireShotgun();
                break;
            default:
                break;
        }

        //Invoke("NewObstacle", 3.0f);
    }

    private void ThrowNewAnimalCage()
    {
        // New instance
        var newCage = Instantiate(prefabAnimalCage, startPosition, Quaternion.identity);
        var newCageController = newCage.GetComponent<CageController>();

        // TODO: should pop from a stack or queue of the animals list
        newCageController.animalName = "monkey";

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
        if (gun == null)
            gun = Instantiate(prefabGun, startPosition, Quaternion.identity);
        else
            gun.SetActive(true);

        // Create bullets
        var bullet1 = Instantiate(prefabBullets, startPosition, Quaternion.identity);
        var bullet2 = Instantiate(prefabBullets, startPosition, Quaternion.identity);
        var bullet3 = Instantiate(prefabBullets, startPosition, Quaternion.identity);

        // Hide gun after 1 second
        Invoke("HideGun", 1.0f);
    }

    private void HideGun()
    {
        gun.SetActive(false);
    }
}
