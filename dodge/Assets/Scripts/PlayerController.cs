using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRigidbody; //이동에 사용할 리지드바디 컴포넌트
    public float speed = 10f; //이동속력
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        playerRigidbody.AddForce(new Vector3(0, -300f, 0));

        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);

        playerRigidbody.velocity = newVelocity;
    }
    
    public void die()
    {
        gameObject.SetActive(false);

        GameManager gameManager = FindObjectOfType<GameManager>();

        gameManager.EndGame();


    }

}
