using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageControllerIntro : MonoBehaviour
{
    public string animalName = "";
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        throwIt();
    }

    private void throwIt()
    {
        // Force when thrown
        var velocityX = -2;
        var velocityY = 2;
        rb2D.velocity = new Vector2(velocityX, velocityY);

        // Angle
        var angle = -20;
        rb2D.SetRotation(angle);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
