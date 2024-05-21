using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMove : MoveMethod
{
    public VelocityMove(Action callback, params Transform[] points) : base(callback, points)
    {
    }

    public override void MoveAction(ExePlayer player, Transform target)
    {
        player.rb.bodyType = RigidbodyType2D.Dynamic;

        if (Math.Abs(player.rb.velocity.x) < 0.1f)
        {
            bool isLeft = (target.position.x - player.transform.position.x) < 0;
            Vector2 offset = isLeft ? Vector2.left : Vector2.right;
            player.rb.velocity = offset * player.speed;
        }
    }

    public override void MoveDone(ExePlayer player)
    {
        base.MoveDone(player);
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        player.rb.velocity = Vector2.zero;
        player.rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
