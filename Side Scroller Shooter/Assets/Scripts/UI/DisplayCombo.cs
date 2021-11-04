using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCombo : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Text comboWindow;
    private string comboString;

    // Update is called once per frame
    void Update()
    {
        comboString = "";
        for (int i = 0; i < player.GetComponent<Player>().comboLetters.Length; i++)
        {
            comboString += player.GetComponent<Player>().comboLetters[i];
        }
        comboWindow.text = "Current Combo: " + comboString;
    }
}
