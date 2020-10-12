using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeAnimalController : MonoBehaviour
{
    public BoxCollider2D collider;
    public Rigidbody2D rb;

    private void Start()
    {
        StartCoroutine(TurnOffPhysics());
    }

    void Update()
    {
        transform.Translate(new Vector3(-2f, 0, 0) * Time.deltaTime);
        if (transform.position.y == -10)
            Destroy(gameObject);
    }

    private IEnumerator TurnOffPhysics()
    {
        yield return new WaitForSeconds(1.5f);
        collider.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY & RigidbodyConstraints2D.FreezeRotation;
    }
}
