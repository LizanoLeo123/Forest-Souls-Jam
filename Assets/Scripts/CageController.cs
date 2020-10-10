﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageController : MonoBehaviour
{
    public string animalName = "";
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello " + animalName);
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        throwIt();
    }

    private void throwIt()
    {
        // Force when thrown
        var velocityX = Random.Range(-2, -6);
        var velocityY = Random.Range(2, 6);
        rb2D.velocity = new Vector2(velocityX, velocityY);

        // Angle
        var angle = Random.Range(-20, 20);
        rb2D.SetRotation(angle);
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