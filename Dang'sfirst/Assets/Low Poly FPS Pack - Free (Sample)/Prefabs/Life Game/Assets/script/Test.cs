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
    bool zoom = false;
    // Update is called once per frame
    

    private void Awake()
    {
        cam1 = GetComponent<Transform>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            temp = cam1.position;
            temp.y = temp.y / 2;
            cam1.position= temp;
        }
        
        Click();
    }
    void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Vector3 hitPos = hit.point;

                GameObject item = Instantiate(itemPrifeb);
                hitPos.y = hitPos.y + 0.5f;

                if (hitPos.x % 1 != 0)
                {
                    if ((hitPos.x % 1) > 0.5f)
                        hitPos.x = hitPos.x - (hitPos.x % 1);
                    if ((hitPos.x % 1) <= 0.5f)
                        hitPos.x = hitPos.x - (hitPos.x % 1) + 1;
                }

                if (hitPos.z % 1 != 0)
                {
                    if ((hitPos.z % 1) > 0.5f)
                        hitPos.z = hitPos.z - (hitPos.z % 1);
                    if ((hitPos.z % 1) <= 0.5f)
                        hitPos.z = hitPos.z - (hitPos.z % 1) + 1;
                }

                item.transform.position = hitPos;

                Debug.Log(hitPos);
            }


        }

    }
}
