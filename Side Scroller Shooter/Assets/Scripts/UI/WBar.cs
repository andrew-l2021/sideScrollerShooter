using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WBar : MonoBehaviour
{
    private Image wBar;
    [SerializeField] private GameObject player;
    private float currentw;
    private float maxw;

    private void Start()
    {
        wBar = GetComponent<Image>();
    }

    private void Update()
    {
        currentw = player.GetComponent<Player>().currentw;
        maxw = player.GetComponent<Player>().wBar;
        wBar.fillAmount = currentw / maxw;
    }
}
