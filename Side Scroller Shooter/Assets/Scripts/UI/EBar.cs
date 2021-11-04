using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EBar : MonoBehaviour
{
    private Image eBar;
    [SerializeField] private GameObject player;
    private float currente;
    private float maxe;

    private void Start()
    {
        eBar = GetComponent<Image>();
    }

    private void Update()
    {
        currente = player.GetComponent<Player>().currente;
        maxe = player.GetComponent<Player>().eBar;
        eBar.fillAmount = currente / maxe;
    }
}
