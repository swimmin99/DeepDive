using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    Animator animator;
    public GameObject damageText;
    public bool invincible = false;
    public float invincibilityTime = 1f;
    public float invincibilityTimeElapsed = 0f;
    public float Health
    {
        set
        {
            health = value;

            if (Health <= 0)
            {
                print("die");
                animator.SetBool("isDie", true);

            }
        }
        get
        {
            return health;
        }
    }
    public float health;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            print(GetComponent<BoxCollider2D>().offset);
            invincibilityTimeElapsed += Time.deltaTime;

            if(invincibilityTimeElapsed > invincibilityTime)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                invincible = false;
                invincibilityTimeElapsed = 0f;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        //text ugui
        // .GetComponent<RectTransform>()




        //damage
        if (!animator.GetBool("isDie"))
        {
            GameObject texttemp = Instantiate(damageText);
            RectTransform textTransform = texttemp.GetComponent<RectTransform>();
            TextMeshProUGUI textsetting = texttemp.GetComponent<TextMeshProUGUI>();
            textsetting.text = damage.ToString();
            textsetting.color = Color.red;
            textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            textTransform.SetParent(canvas.transform);


            Health -= damage;
        }
        //invincible timer
        invincible = true;
    }
}
