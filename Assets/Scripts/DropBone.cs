using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBone : MonoBehaviour
{
    float random;
    public float speed = 2;
    public GameObject poof;
    public GameObject damageColl;
    GameObject damageCollClone;
    void Start()
    {
        random = Random.Range(2f, 5f);
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, 5));
        StartCoroutine(Drop());
    }

    IEnumerator Drop()
    {
        yield return new WaitForSeconds(random);
        Instantiate(poof, transform.position, Quaternion.identity);
        damageCollClone = Instantiate(damageColl, transform.position, Quaternion.identity);
        Destroy(damageCollClone, 0.2f);
        Destroy(gameObject);
    }

}
