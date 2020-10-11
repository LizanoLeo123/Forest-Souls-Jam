using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private bool destroying = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        throwIt();
    }

    // Update is called once per frame
    void Update()
    {
        if (!destroying)
        {
            destroying = true;
            Destroy(this.gameObject, 2.0f);
        }
    }

    private void throwIt()
    {
        // Force when thrown
        var velocityX = Random.Range(-5, -10);
        var velocityY = Random.Range(2, 6);
        rb2D.velocity = new Vector2(velocityX, velocityY);

        // Angle
        var angle = Random.Range(-20, 20);
        rb2D.SetRotation(angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            Debug.Log("Rock collision");
            // TODO: Should "kill" the hero
        }
    }
}
