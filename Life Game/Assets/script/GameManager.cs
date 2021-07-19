using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //싱글턴

    public bool isGameover = false; // 게임오버 체크

    public Text time;
    public GameObject gameoverUI;
    public Text turn;

    private int Turn = 0;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다.");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isGameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameover && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (!isGameover && Turn == 0)
        {
            Turn++;
            turn.text = Turn + " 번째 턴";
        }

    }

    public void EndGame()
    {

    }
}
