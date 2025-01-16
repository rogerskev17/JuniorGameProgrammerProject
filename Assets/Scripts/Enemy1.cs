using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float speed = 0.025f;
    private Rigidbody enemyRb;
    private GameObject player;
    private float timeToLive = 5f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (transform.position - player.transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        enemyRb.AddForce((player.transform.position - transform.position).normalized * speed, ForceMode.Impulse);
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }
}
