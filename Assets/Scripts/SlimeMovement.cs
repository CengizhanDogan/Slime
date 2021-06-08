using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlimeMovement : MonoBehaviour
{
    [Header("Game Components")]
    public Animator anim;
    public SpriteRenderer sp;

    [Header("Gameplay Settings")]
    public float speed = 40f;

    [Header("Fields")]
    private bool isDashing = false;
    private Vector3 target;
    private float possDiff;

    private SpriteRenderer _sRenderer;
    private Color _spriteColour;
    public float _originalalpha;


    private void Update()
    {
        sp.sortingOrder = -(int)transform.position.y;

        switch (GameManager.Instance.currentState)
        {
            case GameState.Slime:
                InputControl();
                Dash();
                break;
            case GameState.End:
                break;
        }
    }

    private void InputControl()
    {
        possDiff = transform.position.x - target.x;

        if (transform.position.x < possDiff)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180, transform.rotation.z));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0, transform.rotation.z));
        }
        if (!isDashing)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (target != transform.position)
            {
                anim.SetBool("Walk", true);
            }
        }
        else if (isDashing)
        {
            anim.SetBool("Dash", true);
            anim.SetBool("Walk", false);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * 5 * Time.deltaTime);
            StartCoroutine(DashFalse());
            IEnumerator DashFalse()
            {
                yield return new WaitForSeconds(0.3f);
                isDashing = false;
                anim.SetBool("Dash", false);
            }
        }
        if (target == transform.position)
        {
            anim.SetBool("Walk", false);
        }

        if (Enemy.Instance.damage)
        {
            Debug.Log("Hit");
            Invoke("DoTransparent", 0.3f);
            Invoke("CancelTransparent", 0.6f);
        }
    }

    private void Dash()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDashing = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SkeletonBasic")
        {
            if (isDashing)
            {
                collision.gameObject.GetComponent<BasicSkeleton>().healthPoint--;
            }
        }
    }

    private void DoTransparent()
    {
        this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    }

    private void CancelTransparent()
    {
        this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
    }
}
