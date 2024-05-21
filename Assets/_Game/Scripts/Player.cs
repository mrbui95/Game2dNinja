using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 350;

    [SerializeField] private Kunai KunaiPrefab;
    [SerializeField] private Transform throwPoint;



    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isDoubleJump = false;
    private bool isAttacking = false;

    private float horizontal;


    private int coin = 0;

    private Vector3 savePoint;


    public override void OnInit()
    {
        base.OnInit();

        isAttacking = false;

        transform.position = savePoint;

        coin = PlayerPrefs.GetInt("coin", 0);
        UIManager.instance.SetCoin(coin);

        DeactiveAttack();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        OnInit();
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }

    // Start is called before the first frame update
    void Start()
    {
        SavePoint();
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {

        if (IsDied)
        {
            return;
        }

        isGrounded = CheckGround();

        //horizontal = Input.GetAxisRaw("Horizontal");





        if (rb.velocity.y < 0)
        {
            if (isGrounded == false)
            {
                ChangeAnim("fall");
            }
            else
            {
                isJumping = false;
                isDoubleJump = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Throw();
        }


        if (!isAttacking && Mathf.Abs(horizontal) != 0)
        {
            Moving();
        }
        else if (isGrounded == true)
        {
            if (!isJumping && !isAttacking)
            {
                rb.velocity = Vector2.zero;
                ChangeAnim("idle");
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;

        switch (tag)
        {
            case "coin":
                {
                    coin += 1;
                    PlayerPrefs.SetInt("coin", coin);
                    UIManager.instance.SetCoin(coin);
                    Destroy(collision.gameObject);
                    break;
                }
            case "death-zone":
                {
                    ChangeAnim("death");

                    Invoke(nameof(OnInit), 1f);
                    break;
                }
        }
    }

    private bool CheckGround()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.03f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.03f, groundLayer);
        return (hit.collider != null);
    }

    private void Moving()
    {
        rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);

        transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));

        if (!isJumping)
        {
            ChangeAnim("run");
        }
    }

    public void Attack()
    {
        if (isAttacking)
        {
            return;
        }
        isGrounded = CheckGround();
        if (isGrounded == true)
        {
            isAttacking = true;
            ChangeAnim("attack");

            Invoke(nameof(ResetAttack), 0.5f);

            ActiveAttack();
            Invoke(nameof(DeactiveAttack), 0.5f);
        }
    }

    public void Throw()
    {
        isGrounded = CheckGround();
        if (isGrounded == true)
        {
            isAttacking = true;
            Debug.Log("OnThrow");
            rb.velocity = Vector2.zero;
            ChangeAnim("throw");
            if (kunaiCount < 5)
            {
                Kunai kunai = Instantiate(KunaiPrefab, throwPoint.position, throwPoint.rotation);
                kunai.character = this;

                AddKunaiCount();

                Invoke(nameof(ResetAttack), 0.5f);
            }
            else
            {
                Debug.Log("current kunai " + kunaiCount);
            }


        }
    }

    private void ResetAttack()
    {
        isAttacking = false;
        ChangeAnim("idle");
    }

    public void Jump()
    {
        isGrounded = CheckGround();
        if (isGrounded == true || isDoubleJump == false)
        {
            isJumping = true;
            ChangeAnim("jump");
            rb.AddForce(jumpForce * Vector2.up);

            if (isGrounded == false)
            {
                isDoubleJump = true;
            }
        }
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }

    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;
    }
}
