using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject TemporaryShield;

    // Start is called before the first frame update
    void Start()
    {
        TemporaryShield = GameObject.Find("TemporaryShield");
        TemporaryShield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ActivateShield()
    {
        TemporaryShield.SetActive(true);
    }


    public void DestroyShield()
    {
        TemporaryShield.SetActive(false);
    }
}
