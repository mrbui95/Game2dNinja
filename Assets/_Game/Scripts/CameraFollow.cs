using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float speed = 20;


    // Start is called before the first frame update
    void Start()
    {
        target = FindAnyObjectByType<Player>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x > 2 ? target.position.x : 2, target.position.y > 0 ? target.position.y : 0);
        transform.position = Vector3.Lerp(transform.position, targetPosition + offset, Time.deltaTime * speed);
    }
}
