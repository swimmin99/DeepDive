using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : MonoBehaviour
{
    //basic preferances
    public SpriteRenderer spriteRenderer;
    public Transform playerpos;
    Animator animator;
    public float speed = 0.1f;
    //for persuit
    public bool animFollow;
    public bool end;
    //for patroliing
    public Vector2 moveSpot;
    public Vector2 originalSpot;
    public float range = 0.5f;

    public float waitTime;
    public float startwaitTime = 3;

    //for knockback
    Rigidbody2D rb;
    //for attack
    public float damage = 2;
    public float force = 500f;
    //for damage text
    public GameObject damageText;
    //for invincible
    public bool invincible = false;
    public float invincibilityTime = 1f;
    public float invincibilityTimeElapsed = 0f;
    //
    public float Health
    {
        set
        {
            health = value;

            if (Health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }

    public float health;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerpos = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSpot = transform.position;
        end = false;

        waitTime = startwaitTime;
        moveSpot = new Vector2(UnityEngine.Random.Range(originalSpot.x - range, originalSpot.x + range), UnityEngine.Random.Range(originalSpot.y - range, originalSpot.y + range));

    }

    private void Update()
    {
        animFollow = animator.GetBool("isFollow");
        
        if (animFollow == true && !end)
        {
            if (Vector2.Distance(playerpos.position, transform.position) > 2)
            {
                animator.SetBool("isFollow", false);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, playerpos.position, speed * Time.deltaTime);
                if (playerpos.position.x < transform.position.x)
                    spriteRenderer.flipX = true;
                else
                    spriteRenderer.flipX = false;

            }
        }
        else if (!end)
        {
            if (Vector2.Distance(transform.position, moveSpot) < 0.1f)
            {
                if (waitTime <= 0)
                {
                    animator.SetBool("isPatrol", true);
                    moveSpot = new Vector2(UnityEngine.Random.Range(originalSpot.x - range, originalSpot.x + range), UnityEngine.Random.Range(originalSpot.y - range, originalSpot.y + range));
                    waitTime = startwaitTime;
                }
                else
                {
                    animator.SetBool("isPatrol", false);
                    waitTime -= Time.deltaTime;
                }


                }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, moveSpot, speed * Time.deltaTime);
                if (moveSpot.x < transform.position.x)
                    spriteRenderer.flipX = true;
                else
                    spriteRenderer.flipX = false;
            }
        }

        if (invincible)
        {
            print("invincible on" + invincibilityTimeElapsed);
            invincibilityTimeElapsed += Time.deltaTime;

            if (invincibilityTimeElapsed > invincibilityTime)
            {
                print("invincible down");
                invincible = false;
                invincibilityTimeElapsed = 0f;
            }
        }

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{                
    //    if(collision.gameObject.tag == "Player")
    //    {

    //        GameObject player = collision.gameObject;
    //        print("checking!");
    //        if ((player.GetComponent<PlayerStatus>().invincible)==false)
    //        {
    //            print("invincible false");
    //            Vector3 parentPosition = player.GetComponentInParent<Transform>().position;
    //            player.GetComponent<PlayerStatus>().TakeDamage(damage);
    //            player.GetComponent<PlayerController>().Knockback(parentPosition, transform.position, force);
    //        }
    //    }

    //}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            GameObject player = collision.gameObject;
            print("checking!");
            if ((player.GetComponent<PlayerStatus>().invincible) == false)
            {
                print("invincible false");
                Vector3 parentPosition = player.GetComponentInParent<Transform>().position;
                player.GetComponent<PlayerStatus>().TakeDamage(damage);
                player.GetComponent<PlayerController>().Knockback(parentPosition, transform.position, force);
            }
        }

    }


    public void TakeDamage(float damage, Vector2 parentPosition, Vector2 colliderPosition, float range)
    {
        if (!invincible)
        {
            animator.SetTrigger("isHit");
            //
            RectTransform textTransform = Instantiate(damageText).GetComponent<RectTransform>();
            TextMeshProUGUI temptext = damageText.GetComponent<TextMeshProUGUI>();
            temptext.text = damage.ToString();
            temptext.color = Color.white;
            textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            textTransform.SetParent(canvas.transform);
            //

            Health -= damage;
            Vector2 direction = (colliderPosition - parentPosition).normalized;

            rb.AddForce(direction * range, ForceMode2D.Impulse);
        }
    }

    public void Defeated()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        animator.SetTrigger("Defeated");
        end = true;
    }
    
    public void RemoveEnemy()
    {
        Destroy(gameObject);
        //DestroySelf();
    }
    
}
