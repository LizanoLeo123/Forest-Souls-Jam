using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterController : MonoBehaviour
{
    private GameObject hunter;
    private GameObject hunterArm;
    
    void Awake()
    {
        hunter = GameObject.Find("Hunter");
        hunterArm = GameObject.Find("HunterArm");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hunterArm.transform.position = hunter.transform.position + new Vector3(0,0,1);
    }
}
