using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int gameTimeInSeconds = 30;
    private float timeInSeconds;
    public static int gameState = 0;
    private int gameOverState = 1;
    private int winState = 2;
    
    private int playerHP = 5;
    private TextMeshProUGUI timerText; 
    private TextMeshProUGUI hpText; 
    private TextMeshProUGUI stateText; 
    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        hpText = GameObject.Find("HPText").GetComponent<TextMeshProUGUI>();
        stateText = GameObject.Find("StateText").GetComponent<TextMeshProUGUI>();

        timeInSeconds = (float) gameTimeInSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == 0)
        {
            CountDown();
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

    void UpdateText()
    {
        switch (gameState)
        {
            case 0:
                timerText.text = Mathf.RoundToInt(timeInSeconds % 60).ToString();
                stateText.text = "";
                break;
            case 1:
                stateText.text = "YOU LOSE";
                stateText.color = Color.red;
                break;
            default:
               stateText.text = "YOU WIN";
               stateText.color = Color.green;
                break;
        }

        hpText.text = "HP:" + playerHP;
    }


}
