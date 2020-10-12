using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageController : MonoBehaviour
{
    public string animalName = null;
    public GameObject[] animals;

    private Rigidbody2D rb2D;
    private AudioSource sound = null;

    private int damage = 10;

    private bool _onGound = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        sound = gameObject.GetComponent<AudioSource>();
        throwIt();
        StartCoroutine(MoveAfterLanding());
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
        if (_onGound)
        {
            
            //Debug.Log(transform.eulerAngles.z);
            var angle = Mathf.Abs(transform.eulerAngles.z);
            if (angle > -5 && angle < 10 || angle > 350 && angle < 370)
                transform.Translate(new Vector3(-1f, 0, 0) * Time.deltaTime);
            else if (angle > 80 && angle < 100)
                transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);
            else if (angle > 170 && angle < 190)
                transform.Translate(new Vector3(1f, 0, 0) * Time.deltaTime);
            else if (angle > 260 && angle < 280)
                transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            sound?.Play();
            if (!string.IsNullOrEmpty(animalName))
            {
                // Its an animal cage
                Debug.Log("Collision with Hero with " + animalName); // should 'open' and release the animal inside
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

    public IEnumerator MoveAfterLanding()
    {
        yield return new WaitForSeconds(1.3f);
        _onGound = true;
        //Debug.Log(transform.eulerAngles.z);
    }
    private void Dispose()
    {
        Destroy(this.gameObject);
    }

}
