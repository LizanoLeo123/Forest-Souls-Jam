using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageController : MonoBehaviour
{
    public string animalName = "";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello " + animalName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision " + animalName);
        if (collision.gameObject.name == "Hero")
        {
            Debug.Log("Collision with Hero with " + animalName); // should 'open' and release the animal inside

            // TODO: Should sendMessage upwards to notify this animal was "saved" before destroying it
            Object.Destroy(this.gameObject);
        }
    }

}
