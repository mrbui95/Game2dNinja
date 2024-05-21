using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExeCamera : MonoBehaviour
{
    [SerializeField] ExePlayer player;

    private Vector3 offset = new Vector3(0, 0, -10);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = player.transform.position + offset;
        target.y = transform.position.y;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * player.speed);
    }
}
