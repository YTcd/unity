using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Transform playerRotate;
    public Transform cameraRotate;
    public float speed = 10f;
    private int jumpCount = 0;
    public float jumpForce = 500f;
    private bool isGrounded; //바닥과 접촉 상태인지

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRotate = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        playerRotate.rotation = cameraRotate.rotation;
        jump();
        Move();
    }

    void jump()
    {
        if (Input.GetKeyDown("space") && jumpCount < 2)
        {
            playerRigidbody.velocity = Vector3.zero;
            jumpCount++;
            playerRigidbody.AddForce(new Vector3(0f, jumpForce * Time.deltaTime, 0f));
        }
    }
    void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);
        //Y좌표로도 움직일 수도 있는 상태여서 이동시 위아래로는 영향이 가지 않도록 0으로 초기화

        newVelocity = transform.forward * newVelocity.z + transform.right * newVelocity.x;
        newVelocity.y = 0f;
        //transform.forward는 월드 좌표 기준 오브젝트의 회전 값을 반영한 normalized된 값을 반환합니다.
        //현재의 forward와 입력받은 값(방향)을 곱하여, 캐릭터를 기준(로컬 좌표)으로 캐릭터를 이동합니다.


        playerRigidbody.velocity = newVelocity * Time.deltaTime * 500f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        jumpCount = 0;
        // 바닥에 닿았음을 감지하는 처리
    }


    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        //바닥에서 떨어졌음을 감지하는 처리
    }
}
