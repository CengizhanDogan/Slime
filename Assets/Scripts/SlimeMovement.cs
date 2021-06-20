using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlimeMovement : MonoBehaviour
{
    [Header("Game Components")]
    public Animator anim;
    public SpriteRenderer sp;
    public GameObject poof;
    public GameObject playerBall;
    public Collider2D slimeColl;
    public Collider2D swordColl;

    [Header("Gameplay Settings")]
    public float speed = 40f;
    public float dashSpeed = 5f;
    public int healthPoint = 3;
    private int lastHp;

    [Header("Fields")]
    private bool isDashing = false;
    private bool isSlashing = false;
    private bool isFireball = false;
    private bool fire = true;
    private bool speedMod = false;
    private bool isDashingSound = false;
    private Vector3 target;
    private float possDiff;

    public float _originalalpha;

    private void Start()
    {
        lastHp = healthPoint;
    }
    private void Update()
    {
        //sp.sortingOrder = -(int)transform.position.y;

        switch (GameManager.Instance.currentState)
        {            
            case GameState.Slime:
                if (!speedMod)
                {
                    speed = 2;
                }
                dashSpeed = 5f;
                InputControl();
                Dash();
                break;
            case GameState.Sword:
                Invoke("Timer", 7);
                InputControl();
                Slash();
                break;
            case GameState.Mage:
                Invoke("Timer", 7);
                InputControl();
                Fireball();
                break;
            case GameState.Nurse:
                if (!speedMod)
                {
                    speed = 3.5f;
                }
                dashSpeed = 5f;
                Invoke("Timer", 7);
                InputControl();
                Dash();
                break;
            case GameState.Spear:
                dashSpeed = 10f;
                Invoke("Timer", 7);
                InputControl();
                Dash();
                break;
            case GameState.End:
                break;
        }

        #region Animation Layers
        if (GameManager.Instance.currentState == GameState.Slime)
        {
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(4, 0);
            anim.SetLayerWeight(5, 0);
            anim.SetLayerWeight(6, 0);
            anim.SetLayerWeight(7, 0);
            anim.SetLayerWeight(8, 0);
            anim.SetLayerWeight(9, 0);
            anim.SetLayerWeight(10, 0);
            anim.SetLayerWeight(11, 0);
            anim.SetLayerWeight(12, 0);
            anim.SetLayerWeight(13, 0);
            anim.SetLayerWeight(14, 0);
        }
        if (GameManager.Instance.currentState == GameState.Sword)
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(6, 0);
            anim.SetLayerWeight(7, 0);
            anim.SetLayerWeight(8, 0);
            anim.SetLayerWeight(9, 0);
            anim.SetLayerWeight(10, 0);
            anim.SetLayerWeight(11, 0);
            anim.SetLayerWeight(12, 0);
            anim.SetLayerWeight(13, 0);
            anim.SetLayerWeight(14, 0);
        }
        if (GameManager.Instance.currentState == GameState.Spear)
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
            anim.SetLayerWeight(5, 0);
            anim.SetLayerWeight(9, 0);
            anim.SetLayerWeight(10, 0);
            anim.SetLayerWeight(11, 0);
            anim.SetLayerWeight(12, 0);
            anim.SetLayerWeight(13, 0);
            anim.SetLayerWeight(14, 0);
        }
        if (GameManager.Instance.currentState == GameState.Nurse)
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
            anim.SetLayerWeight(5, 0);
            anim.SetLayerWeight(6, 0);
            anim.SetLayerWeight(7, 0);
            anim.SetLayerWeight(8, 0);
            anim.SetLayerWeight(12, 0);
            anim.SetLayerWeight(13, 0);
            anim.SetLayerWeight(14, 0);
        }
        if (GameManager.Instance.currentState == GameState.Mage)
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
            anim.SetLayerWeight(5, 0);
            anim.SetLayerWeight(6, 0);
            anim.SetLayerWeight(7, 0);
            anim.SetLayerWeight(8, 0);
            anim.SetLayerWeight(9, 0);
            anim.SetLayerWeight(10, 0);
            anim.SetLayerWeight(11, 0);
        }
        if (healthPoint == 3)
        {
            if (GameManager.Instance.currentState == GameState.Slime)
            {
                anim.SetLayerWeight(0, 1);
                anim.SetLayerWeight(2, 0);
                anim.SetLayerWeight(3, 0);

            }
            if (GameManager.Instance.currentState == GameState.Sword)
            {
                anim.SetLayerWeight(1, 1);
                anim.SetLayerWeight(4, 0);
                anim.SetLayerWeight(5, 0);
            }
            if (GameManager.Instance.currentState == GameState.Spear)
            {
                anim.SetLayerWeight(6, 1);
                anim.SetLayerWeight(7, 0);
                anim.SetLayerWeight(8, 0);
            }
            if (GameManager.Instance.currentState == GameState.Nurse)
            {
                anim.SetLayerWeight(9, 1);
                anim.SetLayerWeight(10, 0);
                anim.SetLayerWeight(11, 0);
            }
            if (GameManager.Instance.currentState == GameState.Mage)
            {
                anim.SetLayerWeight(12, 1);
                anim.SetLayerWeight(13, 0);
                anim.SetLayerWeight(14, 0);
            }
        }
        if (healthPoint == 2)
        {
            if (GameManager.Instance.currentState == GameState.Slime)
            {
                anim.SetLayerWeight(0, 0);
                anim.SetLayerWeight(2, 1);
                anim.SetLayerWeight(3, 0);
            }
            if (GameManager.Instance.currentState == GameState.Sword)
            {
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(4, 1);
                anim.SetLayerWeight(5, 0);
            }
            if (GameManager.Instance.currentState == GameState.Spear)
            {
                anim.SetLayerWeight(6, 0);
                anim.SetLayerWeight(7, 1);
                anim.SetLayerWeight(8, 0);
            }
            if (GameManager.Instance.currentState == GameState.Nurse)
            {
                anim.SetLayerWeight(9, 0);
                anim.SetLayerWeight(10, 1);
                anim.SetLayerWeight(11, 0);
            }
            if (GameManager.Instance.currentState == GameState.Mage)
            {
                anim.SetLayerWeight(12, 0);
                anim.SetLayerWeight(13, 1);
                anim.SetLayerWeight(14, 0);
            }
        }
        if (healthPoint <= 1)
        {
            if (GameManager.Instance.currentState == GameState.Slime)
            {
                anim.SetLayerWeight(0, 0);
                anim.SetLayerWeight(2, 0);
                anim.SetLayerWeight(3, 1);
            }
            if (GameManager.Instance.currentState == GameState.Sword)
            {
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(4, 0);
                anim.SetLayerWeight(5, 1);
            }
            if (GameManager.Instance.currentState == GameState.Spear)
            {
                anim.SetLayerWeight(6, 0);
                anim.SetLayerWeight(7, 0);
                anim.SetLayerWeight(8, 1);
            }
            if (GameManager.Instance.currentState == GameState.Nurse)
            {
                anim.SetLayerWeight(9, 0);
                anim.SetLayerWeight(10, 0);
                anim.SetLayerWeight(11, 1);
            }
            if (GameManager.Instance.currentState == GameState.Mage)
            {
                anim.SetLayerWeight(12, 0);
                anim.SetLayerWeight(13, 0);
                anim.SetLayerWeight(14, 1);
            }
        }
        #endregion
    }

    private void InputControl()
    {
        possDiff = transform.position.x - target.x;
        if (!SoundManager.Instance.movementSlimeSound.isPlaying)
        {
            SoundManager.Instance.movementSlimeSound.Play();
        }

        if (transform.position.x < possDiff)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180, transform.rotation.z));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0, transform.rotation.z));
        }
        if (!isDashing && !isSlashing && !isFireball)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (target != transform.position)
            {
                anim.SetBool("Walk", true);
            }
        }
        if (isDashing)
        {
            SoundManager.Instance.slimeDashAndAttack.Play();
            anim.SetBool("Attack", true);
            anim.SetBool("Walk", false);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * dashSpeed * Time.deltaTime);
            StartCoroutine(DashFalse());
            IEnumerator DashFalse()
            {
                yield return new WaitForSeconds(0.3f);
                isDashing = false;
                anim.SetBool("Attack", false);
            }
        }
        if (isSlashing)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            anim.SetBool("Attack", true);
            anim.SetBool("Walk", false);
            slimeColl.enabled = false;
            swordColl.enabled = true;
            SoundManager.Instance.slimeSwordAttack.Play(); //CHECK LATER

            transform.position = Vector3.MoveTowards(transform.position, target, speed / 2 * Time.deltaTime);
            StartCoroutine(SlashFalse());
            IEnumerator SlashFalse()
            {
                yield return new WaitForSeconds(1f);
                isSlashing = false;
                slimeColl.enabled = true;
                swordColl.enabled = false;
                anim.SetBool("Attack", false);
            }
        }

        if (isFireball)
        {
            if (fire)
            {
                Instantiate(playerBall, transform.position, Quaternion.identity);
                SoundManager.Instance.fireballSound.Play();
                fire = false;
            }
            anim.SetBool("Attack", true);
            anim.SetBool("Walk", false);
            StartCoroutine(SlashFalse());
            IEnumerator SlashFalse()
            {
                yield return new WaitForSeconds(0.5f);
                isFireball = false;
                fire = true;
                anim.SetBool("Attack", false);
            }
        }

        if (target == transform.position)
        {
            anim.SetBool("Walk", false);
        }

        if (lastHp != healthPoint)
        {
            Invoke("DoTransparent", 0.3f);
            Invoke("CancelTransparent", 0.6f);
            lastHp = healthPoint;
        }
    }

    private void Dash()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDashing = true;
        }
    }

    private void Slash()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSlashing = true;
        }
    }

    private void Fireball()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isFireball = true;
        }
    }

    private void Timer()
    {
        GameManager.Instance.currentState = GameState.Slime;
        CancelInvoke("Timer");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SkeletonBasic" || collision.gameObject.tag == "SkeletonSpear")
        {
            if (isDashing)
            {
                collision.gameObject.GetComponent<BasicSkeleton>().healthPoint--;
            }
            if (isSlashing)
            {
                collision.gameObject.GetComponent<BasicSkeleton>().healthPoint--;
            }
        }

        if (collision.gameObject.tag == "SkeletonMage")
        {
            if (isDashing)
            {
                collision.transform.Find("Mage").gameObject.GetComponent<BasicSkeleton>().healthPoint--;
            }
            if (isSlashing)
            {
                collision.transform.Find("Mage").gameObject.GetComponent<BasicSkeleton>().healthPoint--;
            }
            if (collision.transform.Find("Mage").gameObject.GetComponent<BasicSkeleton>().healthPoint <= 0)
            {
                Destroy(collision.gameObject, 3);
            }
        }

        if (collision.gameObject.tag == "Nurse")
        {
            if (isDashing)
            {
                collision.gameObject.GetComponent<BasicSkeleton>().healthPoint--;
            }
            if (isSlashing)
            {
                collision.gameObject.GetComponent<BasicSkeleton>().healthPoint--;
            }
        }
        if (GameManager.Instance.currentState == GameState.Slime)
        {
            if (collision.gameObject.tag == "Sword")
            {
                SoundManager.Instance.eatSound.Play();
                GameManager.Instance.currentState = GameState.Sword;
                Instantiate(poof, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject, 0.4f);
            }
            if (collision.gameObject.tag == "Spear")
            {
                SoundManager.Instance.eatSound.Play();
                GameManager.Instance.currentState = GameState.Spear;
                Instantiate(poof, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject, 0.4f);
            }
            if (collision.gameObject.tag == "Medkit")
            {
                SoundManager.Instance.eatSound.Play();
                GameManager.Instance.currentState = GameState.Nurse;
                Instantiate(poof, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject, 0.4f);
            }
            if (collision.gameObject.tag == "Staff")
            {
                SoundManager.Instance.eatSound.Play();
                GameManager.Instance.currentState = GameState.Mage;
                Instantiate(poof, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject, 0.4f);
            }
        }

        if (collision.gameObject.tag == "BoneDamage")
        {
            healthPoint--;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Web")
        {
            speedMod = true;
            speed = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Web")
        {
            speedMod = false;
            speed = 2;
        }
    }

    private void DoTransparent()
    {
        this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        healthPoint = lastHp;
    }

    private void CancelTransparent()
    {
        this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
        CancelInvoke("DoTransparent");
        CancelInvoke("CancelTransparent");
    }
}
