using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject itemPrifeb;
    public Transform cam1;
    Vector3 temp = Vector3.zero;
    bool zoom = false;          //줌 풀기 가능
    
    // Update is called once per frame
    

    private void Awake()
    {
        cam1 = GetComponent<Transform>();
    }
    void Update()
    {

        if (zoom)
        {
            Click();

        }
        else
            Zoom();
    }

    void Zoom()
    {
        if (Input.GetMouseButtonDown(0))
        {
            temp = cam1.position;
            temp.y = temp.y / 2;
            cam1.position = temp;
            zoom = true;
        }
    }

void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layerMask = 1 << LayerMask.NameToLayer("plane"); //클릭 위치 바닥만 인식하도록 레이어 추가

            if (Physics.Raycast(ray, out hit,100f, layerMask))
            {
                Debug.Log("클릭위치 "+ hit.point);
                Vector3 hitPos = hit.point;

                GameObject item = Instantiate(itemPrifeb);
                hitPos.y = hitPos.y + 0.5f;

                // 애매한 클릭을 보정해준다
                if (hitPos.x % 1 != 0)
                {
                    if ((hitPos.x % 1) > 0.5f)
                        hitPos.x = hitPos.x - (hitPos.x % 1) + 1;
                    if ((hitPos.x % 1) <= 0.5f)
                        hitPos.x = hitPos.x - (hitPos.x % 1);
                }

                // Z 좌표는 기본적으로 음수이기 때문에 반올림 공식이 다름
                if (hitPos.z % 1 != 0)
                {
                    if ((hitPos.z % 1) < -0.5f)
                        hitPos.z = hitPos.z - (hitPos.z % 1) - 1;
                    else if ((hitPos.z % 1) >= -0.5f)
                        hitPos.z = hitPos.z - (hitPos.z % 1);
                }

                item.transform.position = hitPos;
                //Debug.Log(hitPos);
            }


        }

        if (Input.GetMouseButtonDown(1))
        {
            temp.x = 25f;
            temp.y = 50f;
            temp.z = -108.5f;
            cam1.position = temp;
            zoom = false;
        }
    }

}

