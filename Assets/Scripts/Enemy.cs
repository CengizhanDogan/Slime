using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    public AiState aiState;

    [Header("Game Components")]
    public Animator anim;
    public Animator poofAnim;
    public GameObject target;
    public GameObject itemDrop;
    public GameObject bone;
    public GameObject itemDropClone;
    public GameObject fireBall;
    public EnemySpawn es;
    public SpriteRenderer sp;

    [Header("Fields")]
    public float speed;
    public float attackDamage;
    public float damageDelay;
    public float healthPoint;

    private void Awake()
    {
        Instance = this;
    }

    public enum AiState
    {
        Move,
        Attack,
        Death
    }

    public void AbstractUpdate()
    {
        sp.sortingOrder = -(int)transform.position.y;

        if (healthPoint <= 0)
        {
            aiState = AiState.Death;
            CancelInvoke("Move");
        }
        else if (gameObject.tag == "Nurse")
        {
            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("Bone");
                if (target == null)
                {
                    aiState = AiState.Move;
                    anim.SetBool("Walk", false);
                    anim.SetBool("Attack", false);
                    return;
                }
            }
        }
        if (!itemDropClone)
        {
            itemDropClone = GameObject.FindGameObjectWithTag("Sword");
        }
    }

    public virtual void Move()
    {
        if (target != null)
        {
            if (transform.position.x < (target.transform.position.x - transform.position.x - 0.01f))
            {
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180, transform.rotation.z));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0, transform.rotation.z));
            }
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            anim.SetBool("Walk", true);
            anim.SetBool("Attack", false);
        }
    }
    public virtual void Attack()
    {
        anim.SetBool("Attack", true);
        anim.SetBool("Walk", false);

        if (gameObject.tag == "SkeletonSpear")
        {
            StartCoroutine(SpearDash());
            IEnumerator SpearDash()
            {
                yield return new WaitForSeconds(0.7f);

                Vector3 moveDir = (target.transform.position - transform.position).normalized;
                transform.position += moveDir * speed * 5 * Time.deltaTime;

            }
        }
    }
    public virtual void Damage()
    {
        target.GetComponent<SlimeMovement>().healthPoint--;
        Debug.Log("Damage");
    }

    public virtual void Heal()
    {
        StartCoroutine(Timer());
        IEnumerator Timer()
        {
            yield return new WaitForSeconds(1);
            aiState = AiState.Move;
            CancelInvoke("Attack");
            CancelInvoke("Heal");
        }
    }

    public virtual void Fireball()
    {
        Instantiate(fireBall, transform.position, Quaternion.identity);
    }
    public virtual void Death()
    {
        anim.SetBool("Death", true);
        StartCoroutine(DestroyObject());
        IEnumerator DestroyObject()
        {
            yield return new WaitForSeconds(0.8f);
            sp.enabled = false;
            if (!itemDropClone && GameManager.Instance.currentState == GameState.Slime)
            {
                itemDropClone = Instantiate(itemDrop, transform.position, itemDrop.transform.rotation);
                Instantiate(bone, transform.position, itemDrop.transform.rotation);
            }
            Destroy(gameObject, 0.8f);
            poofAnim.SetBool("Poof", true);
        }
    }


}