using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               // UI 관련 라이브러리
using UnityEngine.SceneManagement;  // 씬 관리 라이브러리

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText; //게임 오버시 활성화 텍스트
    public Text timeText;           // 시간 표시 텍스트
    public Text recordText;         // 최고기록 표시 텍스트

    private float surviveTime;
    private bool isGameover;


    void Start()
    {
        surviveTime = 0f;
        isGameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameover)
        {
            surviveTime += Time.deltaTime;
            timeText.text = "Time : " + (int)surviveTime; 
        }
    }

    public void EndGame()
    {

    }
}
