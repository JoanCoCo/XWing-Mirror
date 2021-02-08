using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Flight : NetworkBehaviour
{
    [SerializeField]
    private float speed = 10.0f;

    [SerializeField]
    private float torque = 10.0f;

    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isLocalPlayer) {
            body.AddForce(transform.forward * Input.GetAxis("Vertical") * speed);
            body.AddTorque(transform.up * Input.GetAxis("Horizontal") * torque);
        }
    }
}
