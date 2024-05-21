using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float movingSpeed;
    [SerializeField] private Rigidbody2D rb;

    private bool isRightDirection = true;

    public IState currentState;

    public Character Target;

    public override void OnInit()
    {
        base.OnInit();

        ChangeState(new IdleState());

        DeactiveAttack();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        ChangeState(null);

        Destroy(gameObject);
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null && !IsDied)
        {
            currentState.onExecute(this);
        }
    }



    public void ChangeState(IState newState)
    {
        //#Debug.Log("ChangeState" + newState);
        if (currentState != null)
        {
            currentState.onExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.onEnter(this);
        }
    }

    public void Moving()
    {
        ChangeAnim("run");
        rb.velocity = transform.right * movingSpeed;
    }

    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        Debug.Log("Enemy Attacking");
        ChangeAnim("attack");

        Invoke(nameof(ResetAttack), 0.5f);

        Invoke(nameof(ActiveAttack), 0.2f);
        Invoke(nameof(DeactiveAttack), 0.5f);
    }

    private void ResetAttack()
    {
        ChangeAnim("idle");
    }

    public bool IsTargetInRange()
    {
        if (this.Target != null)
        {
            return Vector3.Distance(this.Target.transform.position, transform.position) < attackRange;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy-wall")
        {
            ChangeDirection(!isRightDirection);
        }
    }

    public void ChangeDirection(bool isRightDirection)
    {
        this.isRightDirection = isRightDirection;

        transform.rotation = isRightDirection ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    public void SetTarget(Character character)
    {
        Target = character;

        if (IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else if (Target != null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());
        }
    }
}
