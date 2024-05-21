using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveMethod
{

    public Transform[] points;

    protected int index;

    protected Action callback;

    public bool IsLoop { get; set; }
    public bool IsRandomRoute { get; set; }
    public bool IsSleep { get; set; }

    public MoveMethod(Action callback, params Transform[] points)
    {
        this.points = points;
        this.callback = callback;
        IsLoop = false;
        IsRandomRoute = false;
        IsSleep = false;
        index = 0;
    }




    public void Move(ExePlayer player)
    {
        if (!IsSleep)
        {
            if (points != null && points.Length > 0 && index < points.Length)
            {
                Transform target = points[index];

                float dis = Vector3.Distance(target.position, player.transform.position);

                if (dis < 0.5f)
                {
                    if (!IsRandomRoute)
                    {
                        index++;
                        if (IsLoop && index >= points.Length)
                        {
                            index = 0;
                        }
                    }
                    else
                    {
                        int length = points.Length;

                        int nextIndex = index;

                        while (nextIndex == index)
                        {
                            nextIndex = new System.Random().Next(length);
                        }

                        index = nextIndex;
                    }
                }
                else
                {
                    MoveAction(player, target);

                }

            }
            else
            {
                MoveDone(player);
            }
        }
    }

    public abstract void MoveAction(ExePlayer player, Transform target);

    public virtual void MoveDone(ExePlayer player)
    {
        player.SetMove(null);
        callback();
    }
}
