using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossII : MonoBehaviour
{
    private PlayerController player;
    public int cooldown = 3;
    public float cooldowning = 3;
    public int damage;
    private Animator anim;
    public float enemyHealth = 500;
    public float startEnemySpeed = 25;
    public float enemySpeed = 25;
    private Rigidbody2D rb;
    private Vector2 moveVector;
    public bool isPlayerVisible;
    public Camera winCamera;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (enemyHealth <= 0)
        {
            winCamera.GetComponent<Camera>().enabled = true;
            winCamera.GetComponentInChildren<Canvas>().enabled = true;
            Destroy(gameObject);
        }
        if (isPlayerVisible)
        {
            moveVector = player.gameObject.transform.position - gameObject.transform.position;
            rb.MovePosition(rb.position + moveVector * enemySpeed * Time.deltaTime);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            isPlayerVisible = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            isPlayerVisible = false;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {

            if (cooldowning <= 0)
            {
                anim.SetTrigger("Attack");
            }
            else
            {
                cooldowning -= Time.deltaTime;
                enemySpeed = startEnemySpeed;
            }
        }
    }

    public void StartAttack()
    {
        enemySpeed = 25;
    }

    public void OnEnemyAttack()
    {
        if (isPlayerVisible)
        {
            player.playerHealth -= damage;
            cooldowning = cooldown;
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
    }
}
