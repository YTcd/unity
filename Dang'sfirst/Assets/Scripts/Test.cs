using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour

    
{
    private Rigidbody playerRigidbody;
    public float speed = 500f;
    public float gravity = -9.8f;
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
            


            float xInput = Input.GetAxis("Horizontal");
            float zInput = Input.GetAxis("Vertical");

            float xSpeed = xInput * speed;
            float zSpeed = zInput * speed;

            Vector3 newVelocity;
            newVelocity = new Vector3(playerRigidbody.velocity.x * xSpeed, 0f , playerRigidbody.velocity.z * zSpeed);
        
        //transform.forward는 월드 좌표 기준 오브젝트의 회전 값을 반영한 normalized된 값을 반환합니다.
        //현재의 forward와 입력받은 값(방향)을 곱하여, 캐릭터를 기준(로컬 좌표)으로 캐릭터를 이동합니다.


        playerRigidbody.velocity = newVelocity * Time.deltaTime;
        playerRigidbody.AddForce(Vector3.up * gravity);
    }
}
