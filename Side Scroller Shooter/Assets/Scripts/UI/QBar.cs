using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QBar : MonoBehaviour
{
    private Image qBar;
    [SerializeField] private GameObject player;
    private float currentq;
    private float maxq;

    private void Start()
    {
        qBar = GetComponent<Image>();
    }

    private void Update()
    {
        currentq = player.GetComponent<Player>().currentq;
        maxq = player.GetComponent<Player>().qBar;
        qBar.fillAmount = currentq / maxq;
    }
}
