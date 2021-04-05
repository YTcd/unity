using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotater : MonoBehaviour
{
    public float rotateSpeed = 200f;
    float mh;
    float mv;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //playerRigidbody.AddForce(new Vector3(0, -30f * Time.deltaTime , 0)); //낙하 속도가 느려서 AddForce 해줌

        
        CamRotate();
        

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

        transform.eulerAngles = new Vector3(-mv, mh, 0);    //오일러 각으로 변환해서 캠 회전
    }
   

}
