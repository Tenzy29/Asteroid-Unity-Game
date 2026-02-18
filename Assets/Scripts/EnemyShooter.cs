using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bullet;
    public GameObject firePos;

    private PlayerController playerController;
    private GameObject player;

    public int health;
    public float speed;

    private bool isDying = false;
    private float deathTimer = 0.5f;
    bool canShoot = true;

    public Animator anim;
    Rigidbody2D rb;
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
        rb.velocity = -transform.up * speed;
        if(canShoot)
            StartCoroutine(shootingDelay());
        if (transform.position.y <= -6)
            Destroy(gameObject);
        if (isDying)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0)
                Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (health >= 1)
                anim.SetTrigger("isHitted");
            health -= playerController.damage;
            if (health <= 0)
            {
                isDying = true;
                anim.SetTrigger("isDead");
                playerController.coins++;
            }
            Destroy(collision.gameObject);
        }
    }
    private void shooting()
    {
        Instantiate(bullet, firePos.transform.position, Quaternion.identity);
    }
    
    public float shootDelayEnemy;
    IEnumerator shootingDelay()
    {
        shooting();
        canShoot = false;
        yield return new WaitForSeconds(shootDelayEnemy);
        canShoot = true;

    }

}
