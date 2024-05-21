using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateMove : MoveMethod
{
    public TranslateMove(Action callback, params Transform[] points) : base(callback, points)
    {
    }

    public override void MoveAction(ExePlayer player, Transform target)
    {
        Vector3 offset = target.position - player.transform.position;
        offset = offset.normalized;

        player.transform.Translate(player.speed * Time.deltaTime * offset);
    }
}
