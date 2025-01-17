﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandeler : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Transform playerRotate;
    public Transform cameraRotate;
    float mh;   //캠 회전축
    float mv;
    public float speed = 1200f;
    private int jumpCount = 0;
    public float rotateSpeed = 200f;
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

        jump();
        Move();
        CamRotate();
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            playerRigidbody.velocity = Vector3.zero;
            jumpCount++;
            playerRigidbody.AddForce(Vector3.up * jumpForce);
        }
    }

    void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed * Time.deltaTime;
        float zSpeed = zInput * speed * Time.deltaTime;

        Vector3 newVelocity;
        //앞 뒤 버튼을 동시에 눌러 대각선 이동을 한다면
        if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
        {
            newVelocity = transform.right * xSpeed + transform.forward * zSpeed;
            newVelocity *= 1.4142135f / 2; //2분의 루트2의 근사값을 곱해 속도 보정
            newVelocity += transform.up * playerRigidbody.velocity.y;
        }
        else
        {
            newVelocity =
                transform.right * xSpeed + transform.forward * zSpeed + transform.up * playerRigidbody.velocity.y;
        }
        //transform.forward는 월드 좌표 기준 오브젝트의 회전 값을 반영한 normalized된 값을 반환합니다.
        //현재의 forward와 입력받은 값(방향)을 곱하여, 캐릭터를 기준(로컬 좌표)으로 캐릭터를 이동합니다.
        
        playerRigidbody.velocity = newVelocity;
        
    }

    void CamRotate()
    {
        float h = Input.GetAxis("Mouse X"); //마우스의 X축 움직임 감지
        float v = Input.GetAxis("Mouse Y"); //마우스의 Y축 움직임 감지


        mh += h * rotateSpeed * Time.deltaTime;
        mv += v * rotateSpeed * Time.deltaTime;

        if (mv >= 90)
        {
            mv = 90;
        }
        else if (mv <= -90)
        {
            mv = -90;
        }

        playerRotate.transform.eulerAngles = new Vector3(0, mh, 0);

        cameraRotate.transform.eulerAngles = new Vector3(-mv, mh, 0);    //오일러 각으로 변환해서 캠 회전
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
