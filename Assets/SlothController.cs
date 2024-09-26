using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlothController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        FindObjectOfType<AudioManager>().Play("Sloth");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
