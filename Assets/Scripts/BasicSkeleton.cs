using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSkeleton : Enemy
{
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        AbstractUpdate();

        switch (aiState)
        {
            case AiState.Move:
                Invoke("Move", 1);
                break;
            case AiState.Attack:
                Attack();
                break;
            case AiState.Death:
                Death();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            aiState = AiState.Attack;
            damage = true;
            InvokeRepeating("Damage", damageDelay / 2, damageDelay);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            CancelInvoke("Damage");
            damage = false;
            StartCoroutine(WaitAnim());
            IEnumerator WaitAnim()
            {
                yield return new WaitForSeconds(1f);
                aiState = AiState.Move;
            }
        }
    }
}
