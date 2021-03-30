using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotater : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public float rotateSpeed = 200f;
    float mh;
    float mv;
    public float speed = 10f;
    private int jumpCount = 0;
    public float jumpForce = 50f;
    private bool isGrounded; //바닥과 접촉 상태인지

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerRigidbody.AddForce(new Vector3(0, -30f * Time.deltaTime , 0)); //낙하 속도가 느려서 AddForce 해줌

        jump();
        Move();
        CamRotate();
        

    }
    void jump()
    {
        if (Input.GetKeyDown("space") && jumpCount < 2 )
        {
            playerRigidbody.velocity = Vector3.zero;
            jumpCount++;
            playerRigidbody.AddForce(new Vector3(0, jumpForce * Time.deltaTime , 0));
        }
    }

    void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);

        newVelocity = transform.forward * newVelocity.z + transform.right * newVelocity.x;
        newVelocity.y = 0f;
        //Y좌표로도 움직일 수도 있는 상태여서 이동시 위아래로는 영향이 가지 않도록 0으로 초기화
        //transform.forward는 월드 좌표 기준 오브젝트의 회전 값을 반영한 normalized된 값을 반환합니다.
        //현재의 forward와 입력받은 값(방향)을 곱하여, 캐릭터를 기준(로컬 좌표)으로 캐릭터를 이동합니다.


                playerRigidbody.velocity = newVelocity * Time.deltaTime * 500f;
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

        transform.eulerAngles = new Vector3(-mv, mh, 0);
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
