using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Shoot : NetworkBehaviour
{
    [SerializeField]
    private GameObject[] laserShooters;

    [SerializeField]
    private GameObject laser;

    [SerializeField]
    private float laserSpeed = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isLocalPlayer && Input.GetKey(KeyCode.Space)) {
            CmdShoot();
        }
    }

    [Command]
    void CmdShoot() {
        foreach (var sp in laserShooters)
        {
            GameObject b = Instantiate(laser, sp.transform.position, sp.transform.rotation);
            b.GetComponent<Rigidbody>().velocity = transform.forward * laserSpeed;
            NetworkServer.Spawn(b);
            Destroy(b, 5.0f);
        }
    }
}
