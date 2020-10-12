using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageController : MonoBehaviour
{
    public string animalName = null;
    private Rigidbody2D rb2D;
    private AudioSource sound = null;

    private int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        sound = gameObject.GetComponent<AudioSource>();
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
        if (collision.gameObject.name == "Hero")
        {
            sound?.Play();
            if (!string.IsNullOrEmpty(animalName))
            {
                // Its an animal cage
                //Debug.Log("Collision with Hero with " + animalName); // should 'open' and release the animal inside
                switch (animalName)
                {
                    case "frog":
                        //Instantiate frog
                        Instantiate(animals[0], transform.position, Quaternion.identity);
                        break;
                    case "snake":
                        //Instantiate snake
                        Instantiate(animals[1], transform.position, Quaternion.identity);
                        break;
                    case "toucan":
                        //Instantiate toucan
                        Instantiate(animals[2], transform.position, Quaternion.identity);
                        break;
                    case "spider":
                        //Instantiate spider
                        Instantiate(animals[3], transform.position, Quaternion.identity);
                        break;
                }

                // TODO: Should sendMessage upwards to notify this animal was "saved" before destroying it
                //Invoke("Dispose", 1.0f);
                Destroy(gameObject);
            }
            else
            {
                // Its a normal crate, then hit the Hero
                var hero = collision.gameObject.GetComponent<HeroMovement>();
                hero?.takeHit(damage);
            }
        }
    }

    private void Dispose()
    {
        Destroy(this.gameObject);
    }

}
