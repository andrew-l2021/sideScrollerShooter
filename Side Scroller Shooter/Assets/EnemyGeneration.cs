using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{

    public GameObject defaultEnemy;
    public float respawnTime = 1.0f;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(Wave());
    }

    private void SpawnEnemy()
    {
        print("Bruh");
        GameObject a = Instantiate(defaultEnemy) as GameObject;
        print(a);

        print(screenBounds.x);
        a.transform.position = new Vector2(screenBounds.x * 2, Random.Range(-screenBounds.y, screenBounds.y));
    }

    IEnumerator Wave()
    {
        while(true)
        {
            print("spawn");
            yield return new WaitForSeconds(respawnTime);
            SpawnEnemy();
        }
    }
}
