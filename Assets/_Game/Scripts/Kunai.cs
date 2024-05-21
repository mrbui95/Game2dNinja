using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    [SerializeField] public GameObject hitVFX;
    public Rigidbody2D rb;

    public Character character { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        rb.velocity = transform.right * 5f;


        Invoke(nameof(OnDespawn), 4f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);

        character.MinusKunaiCount();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Character c = collision.GetComponent<Character>();
            if (c != null)
            {
                c.OnHit(30f);
                Instantiate(hitVFX, transform.position, transform.rotation);
                OnDespawn();
            }
        }
    }
}
