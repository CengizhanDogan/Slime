using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Enemy
{
    Vector3 moveDir;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        
        moveDir = (target.transform.position - transform.position).normalized;
    }

    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, fTarget.position, speed * Time.deltaTime);

        transform.position += moveDir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke("Damage", 0);
            Destroy(gameObject);
        }
    }
}
