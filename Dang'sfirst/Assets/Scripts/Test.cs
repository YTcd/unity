using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour

    
{
    private Rigidbody playerRigidbody;

    public float jumpForce = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.AddForce(Vector3.up * jumpForce);
            }
    }
}
