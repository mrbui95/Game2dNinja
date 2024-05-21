using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] protected Animator anim;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected CombatText combatTextPreb;

    [SerializeField] protected GameObject attackArea;

    protected string currentAnimName = "idle";

    protected float hp;

    protected int kunaiCount;
    protected bool IsDied => hp < 0;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        hp = 100;
        healthBar.OnInit(100);

        kunaiCount = 0;
    }

    public virtual void OnDespawn()
    {

    }

    public virtual void OnDeath()
    {
        ChangeAnim("death");
        Invoke(nameof(OnDespawn), 2f);
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            //Debug.Log("Change Anim: " + animName);
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void OnHit(float damage)
    {
        Debug.Log("OnHit: " + damage + ", " + IsDied);
        if (!IsDied)
        {
            hp -= damage;
            Instantiate(combatTextPreb, healthBar.transform.position + 0.5f * Vector3.up, Quaternion.identity).OnInit((-1) * damage);
            healthBar.SetNewHp(hp);

            if (IsDied)
            {
                OnDeath();
            }
        }
    }

    public void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    public void DeactiveAttack()
    {
        attackArea.SetActive(false);
    }

    public void AddKunaiCount()
    {
        kunaiCount++;
    }

    public void MinusKunaiCount()
    {
        if (kunaiCount >= 1)
        {
            kunaiCount--;
        }
        else
        {
            kunaiCount = 0;
        }
    }
}
