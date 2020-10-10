using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRespawner : MonoBehaviour
{
    public float fallingThreshold;

    void FixedUpdate()
    {
        if (transform.position.y < fallingThreshold)
        {
            gameObject.SetActive(false);
            transform.position = new Vector3(-2.31405f, -0.83f, 0f);
        }
        
    }
}
