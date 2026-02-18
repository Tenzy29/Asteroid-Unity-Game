using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int health;


    Rigidbody2D rb;
    public Animator anim;
    private PlayerController playerController;
    private GameObject player;
    public GameObject smoke;

    public AudioClip hitSound;

    private bool isDying = false;
    private float deathTimer = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * -speed;

        if(isDying)
        {
            deathTimer -= Time.deltaTime;
            if(deathTimer<=0)
                Destroy(gameObject);
        }
        if (transform.position.y <= -6)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if(health>=1)
                anim.SetTrigger("isHitted");
            health-=playerController.damage;
            if (health <= 0)
            {
                isDying = true;
                anim.SetTrigger("isDead");
                playerController.coins++;
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("PlayerHitted");
            anim.SetTrigger("isDead");
            playerController.anim.SetTrigger("isHitted");
            playerController.health--;
            playerController.audioSource.PlayOneShot(hitSound);
            Destroy(gameObject);
        }
    }

}
