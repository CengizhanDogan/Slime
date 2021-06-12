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
    public GameObject itemDropClone;
    public SpriteRenderer sp;

    [Header("Fields")]
    public float speed;
    public float attackDamage;
    public float damageDelay;
    public float healthPoint;
    public bool damage;

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
        if (!itemDropClone)
        {
            itemDropClone = GameObject.FindGameObjectWithTag("Sword");
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
        target.GetComponent<SlimeMovement>().healthPoint--;
        if (damage)
        {
            damage = false;
        }
        Debug.Log("Damage");
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
            }
            //yield return new WaitForSeconds(0.8f);
            Destroy(gameObject, 0.8f);
            poofAnim.SetBool("Poof", true);
        }
    }


}