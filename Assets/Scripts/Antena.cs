using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antena : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    public GameObject wallPrefab; // Prefab of the wall to spawn
    public List<GameObject> activeWalls = new List<GameObject>(); // Track active walls

    public AudioSource audioSrc;
    public AudioClip antenaSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnWalls()); // Start spawning walls
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Walls are moving toward the player handled by their own script
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player is the one colliding with the antenna
        if (collision.gameObject == player)
        {
            anim.SetBool("isOn", true);
            audioSrc.PlayOneShot(antenaSound);
            StartCoroutine(wait());
        }
    }

    IEnumerator SpawnWalls()
    {
        while (true) // Loop to keep spawning walls
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f)); // Random spawn delay

            // Spawn a wall at a random position
            Vector3 spawnPos = new Vector3(Random.Range(-10f, 10f), 6f, 0f); // Adjust the spawn range
            GameObject newWall = Instantiate(wallPrefab, spawnPos, Quaternion.identity);

            activeWalls.Add(newWall); // Add the new wall to the active list
        }
    }
    private void DestroyAllWalls()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("InvisibleWall");

        foreach (GameObject wall in walls)
        {
            Destroy(wall);
        }
    }
    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        DestroyAllWalls();
        anim.SetBool("isOn", false);
    }
}
