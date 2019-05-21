using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int gameTimeInSeconds = 30;
    private float timeInSeconds;
    public static int gameState = 3;
    private int gameOverState = 1;
    private int winState = 2;

    private Button startButton;
    
    public int playerHP = 5;
    private TextMeshProUGUI timerText; 
    private TextMeshProUGUI hpText; 
    private TextMeshProUGUI stateText; 
    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        hpText = GameObject.Find("HPText").GetComponent<TextMeshProUGUI>();
        stateText = GameObject.Find("StateText").GetComponent<TextMeshProUGUI>();
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        timeInSeconds = (float) gameTimeInSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == 0)
        {
           startButton.gameObject.SetActive(false);
            CountDown();
        }
        else
        {
            startButton.gameObject.SetActive(true); 
        }
       
        UpdateText();
    }

    public void damagePlayer()
    {
        print("playerDamaged");
        if (playerHP > 0)
            playerHP--;
        else
        {
            gameState = 1;
        }

        
    }

    void CountDown()
    {
        if (timeInSeconds >= 0)
            timeInSeconds -= Time.deltaTime;
        else
            gameState = winState;
       
     
    }

    public void StartGame()
    {
        if (gameState == 3)
            gameState = 0;
        else
        {
            SceneManager.LoadScene(0);
            AgentBehavior.InitSpeed = 0.6f;
            gameState = 3;
        }

        
        
        
    }

    void UpdateText()
    {
        switch (gameState)
        {
            case 0:
                timerText.text = Mathf.RoundToInt(timeInSeconds % 60).ToString();
                stateText.text = "";
                break;
            case 1:
                stateText.text = "you lose";
                stateText.color = Color.red;
                
                break;
            case 2:
                stateText.text = "you win";
                stateText.color = new Color(0.11f, 0.56f, 0.11f);
                break;
            default:
               stateText.text = "start!";
               stateText.color = Color.black;
                break;
        }

        hpText.text = "HP:" + playerHP;
    }


}
