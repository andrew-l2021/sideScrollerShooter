using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField] private GameObject health;
    [SerializeField] private Text comboWindow;

    // Update is called once per frame
    void Update()
    {
        comboWindow.text = ((int)health.GetComponent<Health>().currentHealth).ToString();
    }
}
