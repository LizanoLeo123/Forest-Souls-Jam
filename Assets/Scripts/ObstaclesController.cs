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
    public GameObject prefabCrate;
    public GameObject prefabBoomerang;

    private Bounds cameraBounds;
    private Vector3 startPosition;
    private GameObject hunter = null;

    private HunterController hunterCt = null;

    private List<KeyValuePair<ObstacleType, int>> obstaclesWeights = new List<KeyValuePair<ObstacleType, int>>(4);

    private bool _throwNewObstacle;
    
    [HideInInspector]
    public  bool _gameFinished;

    void Start()
    {
        cameraBounds = Camera.main.OrthographicBounds();
        startPosition = new Vector3(cameraBounds.max.x, 0, 0);

        hunter = GameObject.Find("Hunter");
        hunterCt = hunter.GetComponent<HunterController>();

        _gameFinished = false;

        // Obstacles weights
        obstaclesWeights.Add(new KeyValuePair<ObstacleType, int>(ObstacleType.Rocks, 30));
        obstaclesWeights.Add(new KeyValuePair<ObstacleType, int>(ObstacleType.Crate, 30));
        obstaclesWeights.Add(new KeyValuePair<ObstacleType, int>(ObstacleType.Gun, 30));
        obstaclesWeights.Add(new KeyValuePair<ObstacleType, int>(ObstacleType.AnimalCage, 10));

        StartCoroutine(DropFirstObstacle());

        //InvokeRepeating("NewObstacle", 20.0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: temp keycode to instantiate a new cage prefab
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    NewObstacle();
        //}
        if (_throwNewObstacle && !_gameFinished)
        {
            NewObstacle();
            _throwNewObstacle = false;
            StartCoroutine(WaitForNewObstacle());
        }
    }

    // New obstacle
    private enum ObstacleType
    {
        Crate,
        Rocks,
        Gun,
        AnimalCage
    }

    private bool obstaclePending = false;
    private void NewObstacle()
    {
        if (obstaclePending)
            return;

        obstaclePending = true;
        ObstacleType obstacleCode = GetNewObstacleRandom();

        switch (obstacleCode)
        {
            case ObstacleType.AnimalCage:
                hunterCt.ThrowBox();
                Invoke("ThrowNewAnimalCage", 1.0f);
                break;
            case ObstacleType.Crate:
                hunterCt.ThrowBox();
                Invoke("ThrowNewCrate", 1.0f);
                break;
            case ObstacleType.Rocks:
                hunterCt.ThrowRock();
                Invoke("ThrowRocks", 1.0f);
                break;
            case ObstacleType.Gun:
                hunterCt.Shoot();
                Invoke("FireShotgun", 0.85f);
                break;
            default:
                break;
        }

        //Invoke("NewObstacle", 3.0f);
    }

    public void StopObstacles()
    {
        CancelInvoke();
    }

    private ObstacleType GetNewObstacleRandom()
    {
        int val = Random.Range(0, 100);
        int lower = 0;
        int upper = 0;
        foreach (var ob in obstaclesWeights)
        {
            upper += ob.Value;
            if (lower <= val && val < upper)
                return ob.Key;
            lower += ob.Value;
        }
        return ObstacleType.Rocks;
    }

    private static List<string> animals = new List<string>() { "frog", "snake", "toucan", "spider"};


    // Offsets for each obstacle starting point
    private static readonly Vector3 offset_cage = new Vector3(-0.4f, 0, 0);
    private static readonly Vector3 offset_rocks = new Vector3(-0.4f, -0.25f, 0);
    private static readonly Vector3 offset_gun = new Vector3(-0.2f, -0.25f, 0);

    private void ThrowNewAnimalCage()
    {
        startPosition = hunter.transform.position + offset_cage;
        var newCage = Instantiate(prefabAnimalCage, startPosition, Quaternion.identity);
        var newCageController = newCage.GetComponent<CageController>();

        newCageController.animalName = animals[Random.Range(0, animals.Count)];

        obstaclePending = false;
    }

    private void ThrowNewCrate()
    {
        startPosition = hunter.transform.position + offset_cage;
        var newCage = Instantiate(prefabCrate, startPosition, Quaternion.identity);
        //var newCageController = newCage.GetComponent<CageController>();
        obstaclePending = false;
    }

    private void ThrowRocks()
    {
        // Instantiate rocks
        startPosition = hunter.transform.position + offset_rocks;
        var rock1 = Instantiate(prefabRock, startPosition, Quaternion.identity);
        //var rock2 = Instantiate(prefabRock, startPosition, Quaternion.identity);
        //var rock3 = Instantiate(prefabRock, startPosition, Quaternion.identity);

        obstaclePending = false;
    }

    private void FireShotgun()
    {
        startPosition = hunter.transform.position + offset_gun;

        var bullet1 = Instantiate(prefabBullets, startPosition, Quaternion.identity);
        var bullet2 = Instantiate(prefabBullets, startPosition, Quaternion.identity);

        obstaclePending = false;
    }

    public IEnumerator DropFirstObstacle()
    {
        yield return new WaitForSeconds(5);
        _throwNewObstacle = true;
    }

    public IEnumerator WaitForNewObstacle()
    {
        yield return new WaitForSeconds(3f);
        _throwNewObstacle = true;
    }
}
