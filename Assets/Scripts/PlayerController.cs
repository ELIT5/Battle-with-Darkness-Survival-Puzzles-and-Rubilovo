using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 0.5f;
    private Vector2 moveVector;
    public float playerHealth = 100;
    public Camera gameOverCamera;
    private Animator anim;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    public int damage;
    public bool haveSword = false;
    private Inventory inv;
    public AudioSource audio;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        timeBtwAttack = startTimeBtwAttack;
        inv = GetComponentInChildren<Inventory>();
    }

    void Update()
    {
        for (int i = 0; i < inv.maxCount; i++)
        {
            if (inv.items[i].id == 4)
            {
                haveSword = true;
            }
        }
        if (haveSword)
        {
            anim.SetBool("Have_sword", true);
        }
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = Input.GetAxis("Vertical");
        if (moveVector.x < 0 || moveVector.y < 0)
        {
            if (!haveSword)
            {
                anim.SetBool("Is_walking", true);
            }
            else
            {
                anim.SetBool("Is_walking_with_sword", true);
            }
            GetComponent<SpriteRenderer>().flipX = true;
            
        }
        else if (moveVector.x > 0 || moveVector.y > 0)
        {
            if (!haveSword)
            {
                anim.SetBool("Is_walking", true);
            }
            else
            {
                anim.SetBool("Is_walking_with_sword", true);
            }
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            anim.SetBool("Is_walking", false);
            anim.SetBool("Is_walking_with_sword", false);
        }
        rb.MovePosition(rb.position + moveVector * speed * Time.deltaTime);
        if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButtonDown(0) && haveSword)
            {
                audio.Play();
                anim.SetTrigger("Attack");
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<BossII>().TakeDamage(damage);
                }
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        if (playerHealth <= 0)
        {
            gameOverCamera.GetComponent<Camera>().enabled = true;
            gameOverCamera.GetComponentInChildren<Canvas>().enabled = true;
            Destroy(gameObject);
        }
    }
}