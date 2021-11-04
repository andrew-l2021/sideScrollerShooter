using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayQ : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Text comboWindow;

    // Update is called once per frame
    void Update()
    {
        comboWindow.text = ((int) player.GetComponent<Player>().currentq).ToString();
    }
}
