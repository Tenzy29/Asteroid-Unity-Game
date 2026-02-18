using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private GameObject player;
    public float speed = 1f; // Speed of the wall

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        // Move towards the player's position
        if (player != null)
        {
            //Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }
}
