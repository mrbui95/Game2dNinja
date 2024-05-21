using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timer = 0;
    float randomTime;

    public void onEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timer = 0;
        randomTime = Random.Range(2f, 4f);
    }

    public void onExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (enemy.Target != null)
        {
            bool isDirectionRight = enemy.Target.transform.position.x > enemy.transform.position.x;
            enemy.ChangeDirection(isDirectionRight);

            if (enemy.IsTargetInRange())
            {
                enemy.ChangeState(new AttackState());
            }
        }

            if (timer > randomTime)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void onExit(Enemy enemy)
    {

    }
}
