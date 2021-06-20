using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Animator>().SetBool("Trap", true);
            collision.gameObject.GetComponent<SlimeMovement>().healthPoint--;
            StartCoroutine(AnimFalse());
            IEnumerator AnimFalse()
            {
                yield return new WaitForSeconds(0.1f);
                GetComponent<Animator>().SetBool("Trap", false);
            }
        }
    }
}
