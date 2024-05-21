using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" ||  collision.tag == "Enemy")
        {
            Character c = collision.GetComponent<Character>();
            c.OnHit(30f);
        }
    }
}
