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


    public int health = 10;

    public Text healthLabel;

    public GameManager gameManager;

    private GameObject hero;

    private Renderer rend;
    private Color c;

    private bool _invulnerable;
    private bool _dead;

    void Awake()
    {
        hero = GameObject.Find("Hero");
        rend = GetComponent<Renderer>();
        c = rend.material.color;
        _dead = false;
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
        if (!_invulnerable)
        {
            health -= damage;
            FindObjectOfType<AudioManager>().Play("kidDamage");

            healthLabel.text = "Health: " + health.ToString();

            if (health <= 0)
            {
                FindObjectOfType<AudioManager>().Stop("MatchTheme");
                FindObjectOfType<AudioManager>().Play("MatchSummary");
                healthLabel.text = "Health: 0";
                KillPlayer();
            }
            else
            {
                StartCoroutine(MakeInvulnerable());
            }
        }
    }

    IEnumerator MakeInvulnerable()
    {
        _invulnerable = true;
        int inmunitySeconds = 3;
        for (int i = 0; i < inmunitySeconds; i++)
        {
            c.a = 0.5f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.5f);
            c.a = 1f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.5f);
        }

        //Once three seconds has passed
        _invulnerable = false;
    }

    public void KillPlayer()
    {   
        if (!_dead)
        {
            FindObjectOfType<AudioManager>().Play("kidDefeat");
            isDead = true;
            animator.SetBool("isDead", true);
            gameManager.FinishGame();
        }
        _dead = true;
    }
}

