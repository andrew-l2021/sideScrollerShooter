using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    [SerializeField] private GameObject health;
    private float currentHealth;
    private float maxHealth;

    private void Start()
    {
        healthBar = GetComponent<Image>();
    }

    private void Update()
    {
        currentHealth = health.GetComponent<Health>().currentHealth;
        maxHealth = health.GetComponent<Health>().startingHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
