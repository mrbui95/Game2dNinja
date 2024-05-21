using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExePlayer : MonoBehaviour
{
    [SerializeField] public int speed = 5;

    public Rigidbody2D rb;

    public MoveMethod moveMethod { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {
        moveMethod?.Move(this);
    }


    public void SetMove(MoveMethod moveMethod)
    {
        this.moveMethod = moveMethod;
    }
}
