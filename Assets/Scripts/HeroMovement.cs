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

    // Update is called once per frame

    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("heroSpeed", Mathf.Abs(horizontalMove));

        isDead = animator.GetBool("isDead");

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
}
