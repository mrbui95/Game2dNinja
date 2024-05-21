using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMove : MoveMethod
{
    public LerpMove(Action callback, params Transform[] points) : base(callback, points)
    {
    }

    public override void MoveAction(ExePlayer player, Transform target)
    {
        float journeyLength = Vector3.Distance(player.transform.position, target.position);
        float distCovered = Time.deltaTime * player.speed;
        float fracJourney = distCovered / journeyLength;
        player.transform.position = Vector3.Lerp(player.transform.position, target.position, fracJourney);

    }
}
