using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool isDead = false;

    private GameObject hero;

    void Awake()
    {
        hero = GameObject.Find("Hero");
    }

    // Update is called once per frame

    void Update()
    {

        if (Input.GetAxisRaw("Horizontal") == -1 && hero.transform.position.x <= -2.5)
        {
            hero.transform.position = new Vector3(-2.5f, hero.transform.position.y, hero.transform.position.z);
        } else if (Input.GetAxisRaw("Horizontal") == 1 && hero.transform.position.x >= 2.5)
        {
            hero.transform.position = new Vector3(2.5f, hero.transform.position.y, hero.transform.position.z);
        }

        isDead = animator.GetBool("isDead");

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("heroSpeed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") && !isDead)
        {
            jump = true;
            
            animator.SetBool("isJumping", true);

        }

    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, isDead);
            
        jump = false;
    }

    public void takeHit()
    {
        Debug.LogWarning("Took hit...");
    }
}

