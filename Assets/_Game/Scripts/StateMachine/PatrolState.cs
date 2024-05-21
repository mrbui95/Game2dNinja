using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float timer = 0;
    float randomTime;

    public void onEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(3f, 6f);
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

            if (timer < randomTime)
            {
                enemy.Moving();
            }
            else
            {
                enemy.ChangeState(new IdleState());
            }

    }

    public void onExit(Enemy enemy)
    {

    }
}
