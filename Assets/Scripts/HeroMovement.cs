using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool isDead = false;


    public int health = 100;

    public Text healthLabel;

    public GameManager gameManager;

    private GameObject hero;

    void Awake()
    {
        hero = GameObject.Find("Hero");

    }

    // Update is called once per frame

    void Update()
    {
        if(hero.transform.position.x <= -4.5)
            hero.transform.position = new Vector3(-4.5f, hero.transform.position.y, hero.transform.position.z);
        if(hero.transform.position.x >= 1.5)
            hero.transform.position = new Vector3(1.5f, hero.transform.position.y, hero.transform.position.z);

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

    public void takeHit(int damage)
    {

        health -= damage;
        FindObjectOfType<AudioManager>().Play("kidDamage");
        
        healthLabel.text = "Health: "+health.ToString(); 

        if(health <= 0){

            FindObjectOfType<AudioManager>().Stop("MatchTheme");
            FindObjectOfType<AudioManager>().Play("MatchSummary");
            healthLabel.text = "Health: 0"; 
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        FindObjectOfType<AudioManager>().Play("kidDefeat");
        isDead = true;
        animator.SetBool("isDead", true);
        gameManager.gameFinished = true;
    }
}

