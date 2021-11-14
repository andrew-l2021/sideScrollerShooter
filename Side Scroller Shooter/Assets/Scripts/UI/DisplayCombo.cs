using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCombo : MonoBehaviour
{
    public static DisplayCombo instance;
    
    [SerializeField] private GameObject player;
    [SerializeField] private Text comboWindow;
    [SerializeField] private Text currentScore;
    [SerializeField] private Text highScore;
    private string comboString;
    private int numberOfQWEPowerups;

    int score = 0;
    int highscore = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentScore.text = score.ToString() + " pts";
        highscore = PlayerPrefs.GetInt("highscore", 0);
        highScore.text = "HIGH: " + highscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        comboString = "";
        for (int i = 0; i < player.GetComponent<Player>().comboLetters.Length; i++)
        {
            comboString += player.GetComponent<Player>().comboLetters[i];
        }
        comboWindow.text = "Current Combo: " + comboString;

        if (score - numberOfQWEPowerups * 1000 > 1000) //Every 1000 points, the PermanentlyIncreaseRandomQWE function is called
        {
            numberOfQWEPowerups++;
            player.GetComponent<Player>().PermanentlyIncreaseRandomQWE();
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        currentScore.text = score.ToString() + " pts";
        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
            highScore.text = "HIGH: " + score.ToString();
        }
    }
}
