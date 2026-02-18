using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;//public
    public int health;
    public int damage;
    public float shootDelay;//public
    public int coins;


    private bool canShoot=true;

    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip hitSound;
    public Animator anim;

    public GameObject walls;
    Rigidbody2D rb;
    bool ok = false;
    private bool wallAppear;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && canShoot && GameManager.isPaused==false)
        {
            StartCoroutine(shootingDelay());
        }
        if (wallAppear)
        {
            ok = !ok;
            walls.SetActive(ok);
        }

    }
    void FixedUpdate()
    {
        playerMovement();
        
    }

    private void playerMovement()
    {
        Vector3 movInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
        rb.MovePosition(transform.position + movInput.normalized * speed * Time.deltaTime);
    }

    public GameObject shootingPos;
    public GameObject bullet;
    private void shooting()
    {
        audioSource.PlayOneShot(shootSound);
        Instantiate(bullet, shootingPos.transform.position, Quaternion.identity);
    }
    IEnumerator shootingDelay()
    {
        shooting();
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="BulletEnemy")
        {
            anim.SetTrigger("isHitted");
            health--;
            audioSource.PlayOneShot(hitSound);
            Destroy(collision.gameObject);
        }

    }
    
}
