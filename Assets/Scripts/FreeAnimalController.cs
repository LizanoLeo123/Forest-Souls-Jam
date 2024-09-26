using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeAnimalController : MonoBehaviour
{
    public bool fly;

    void Update()
    {
        Move();
        if (transform.position.x == -10)
            Destroy(gameObject);
    }

    void Move()
    {
        if (!fly) //If the animal is not a bird
        {
            if (transform.position.y > -1.8f)
            {
                transform.Translate(new Vector3(-2f, -2f, 0) * Time.deltaTime);
            }
            else
            {
                transform.Translate(new Vector3(-2f, 0, 0) * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(new Vector3(-2f, 0, 0) * Time.deltaTime);
        }
    }
}
