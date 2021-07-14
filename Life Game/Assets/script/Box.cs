using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public void OnTriggerEnter(Collider other){
        if (other.tag == "Box")
            {
            Debug.Log("충돌");
            Destroy(other);
            }
    }
}
