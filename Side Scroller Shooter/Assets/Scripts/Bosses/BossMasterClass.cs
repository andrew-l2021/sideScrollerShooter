using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMasterClass : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    public float phase { get; private set; } = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void decrementHealth(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, currentHealth);
    }

    public void changePhase()
    {
        phase++;
    }

    public void destroyObject()
    {
        Destroy(gameObject);
    }
}
