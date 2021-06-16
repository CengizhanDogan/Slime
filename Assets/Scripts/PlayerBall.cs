using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public float speed = 3;
    Vector3 moveDir;

    void Start()
    {
        moveDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        moveDir.z = 0;
        Destroy(gameObject, 3);
    }


    void Update()
    {
        transform.position += moveDir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SkeletonBasic")
        {
            collision.gameObject.GetComponent<BasicSkeleton>().healthPoint--;
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "SkeletonMage")
        {

            collision.transform.Find("Mage").gameObject.GetComponent<BasicSkeleton>().healthPoint--;

            if (collision.transform.Find("Mage").gameObject.GetComponent<BasicSkeleton>().healthPoint <= 0)
            {
                Destroy(collision.gameObject, 3);
            }

            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Nurse")
        {

            collision.gameObject.GetComponent<BasicSkeleton>().healthPoint--;
            Destroy(gameObject);
        }
    }
}
