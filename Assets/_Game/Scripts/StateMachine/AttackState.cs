using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer = 0;

    public void onEnter(Enemy enemy)
    {
        if (enemy.Target != null)
        {
            enemy.StopMoving();
            enemy.Attack();
        }
    }

    public void onExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void onExit(Enemy enemy)
    {

    }
}
