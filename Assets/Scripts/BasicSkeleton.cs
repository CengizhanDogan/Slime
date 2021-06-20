using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSkeleton : Enemy
{
    void Start()
    {
        if (gameObject.tag == "Nurse")
        {
            es = GameObject.FindGameObjectWithTag("Spawn").GetComponent<EnemySpawn>();
            target = GameObject.FindGameObjectWithTag("Bone");
            if (target == null)
            {
                return;
            }
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
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
        if (other.gameObject.tag == "Player" && gameObject.tag != "Nurse")
        {
            aiState = AiState.Attack;
            CancelInvoke("Move");
            if (gameObject.tag == "SkeletonSpear")
            {
                spearInt++;
                if (spearInt > 1)
                {
                    target.GetComponent<SlimeMovement>().healthPoint--;
                    anim.SetBool("Attack", false);
                    StartCoroutine(WaitAnim());
                    IEnumerator WaitAnim()
                    {
                        yield return new WaitForSeconds(1.5f);
                        aiState = AiState.Move;
                    }
                    once = true;
                    spearInt = 0;
                }
            }
            else if (gameObject.tag == "Mage" && gameObject.tag != "SkeletonMage")
            {
                InvokeRepeating("Fireball", damageDelay / 5, damageDelay);
            }
            else
            {
                InvokeRepeating("Damage", damageDelay / 2, damageDelay);
            }
        }
        if (gameObject.tag == "Nurse" && other.gameObject.tag == "Bone")
        {
            aiState = AiState.Attack;
            Invoke("Heal", 0);
            Instantiate(es.Skeletons[Random.Range(0, es.Skeletons.Count)], other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && gameObject.tag != "Nurse" && gameObject.tag != "SkeletonSpear")
        {
            CancelInvoke("Damage");
            CancelInvoke("Fireball");

            StartCoroutine(WaitAnim());
            IEnumerator WaitAnim()
            {
                yield return new WaitForSeconds(1f);
                aiState = AiState.Move;
            }
        }
    }
}
