using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlimeMovement : MonoBehaviour
{
    [Header("Game Components")]
    public Animator anim;

    [Header("Gameplay Settings")]
    public float speed = 40f;

    [Header("Fields")]
    private bool isDashing = false;
    private Vector3 target;
    private Vector3 mousePos;

    private void Update()
    {
        switch (GameManager.Instance.currentState)
        {
            case GameState.Idle:
                break;

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
        if (!isDashing)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    private void Dash()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDashing = true;
            this.gameObject.transform.DOMove(target, 0.5f).OnComplete(() =>
            {
                isDashing = false;
            });
        }
    }
}
