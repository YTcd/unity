using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hellocode : MonoBehaviour
{

    int a = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("helloworld");
    }

    // Update is called once per frame
    void Update()
    {
        a++;
        if(a%60==0)
        {
            Debug.Log(a / 60);
        }
    }
}
