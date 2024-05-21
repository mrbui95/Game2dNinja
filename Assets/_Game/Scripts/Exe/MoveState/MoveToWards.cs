using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveToWards : MoveMethod
{
    public MoveToWards(Action callback, params Transform[] points) : base(callback, points)
    {
    }

    public override void MoveAction(ExePlayer player, Transform target)
    {
        var step = player.speed * Time.deltaTime; // calculate distance to move
        player.transform.position = Vector3.MoveTowards(player.transform.position, target.position, step);
    }
}
