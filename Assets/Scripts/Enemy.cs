using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Game Components")]
    public Animator anim;
    public GameObject target;
    public SpriteRenderer sp;

    [Header("Fields")]
    public float speed;
    public float attackDamage;
    public float damageDelay;
    public float healthPoint;
    public bool damage;

    public void AbstractUpdate()
    {
        sp.sortingOrder = -(int)transform.position.y;

        if (healthPoint <= 0)
        {
            GameManager.Instance.aiState = AiState.Death;
        }
    }

    public virtual void Move()
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
    public virtual void Attack()
    {
        anim.SetBool("Attack", true);
        anim.SetBool("Walk", false);

    }
    public virtual void Damage()
    {
        if (damage)
        {
            Debug.Log("Damage");
        }
    }
    public virtual void Death()
    {
        anim.SetBool("Death", true);
    }


}