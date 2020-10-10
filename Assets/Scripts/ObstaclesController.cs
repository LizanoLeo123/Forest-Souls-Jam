using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;

    private List<CageController> cages = new List<CageController>();

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            // TODO: temp keycode to instantiate a new cage prefab
            SpawnNewCage();
        }
    }

    /// <summary>
    /// Instantiate a new cage
    /// </summary>
    private void SpawnNewCage()
    {
        // New instance
        // TODO: should start from right side of screen, and add a velocity vector
        var newCage = Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        var newCageController = newCage.GetComponent<CageController>();

        // TODO: should pop from a stack or queue of the animals list
        newCageController.animalName = "monkey";

        // Add to array, to keep track of it
        cages.Add(newCageController);
    }
}
