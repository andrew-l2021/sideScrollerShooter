using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{

    //easy enemies
    public GameObject defaultEnemy;
    public GameObject enemyIntervalToPos;
    public GameObject enemyLinear;

    //medium enemies
    public GameObject enemyLinearToPOs;
    public GameObject enemyShooterBasic;
    public GameObject enemyShooterHoming;

    //hard enemies
    public GameObject enemySplitter;
    public GameObject enemyZigZag;

    public float timePeriodforEasyDifficulty = 60;
    public float timePeriodforMediumDifficulty = 120;
    public float timePeriodforHardDifficulty = 180;
    public float respawnTime = 1.0f; //
    private float difficultyTimer = 0.0f; // timer
    private float currentDifficulty = 0; // current difficulty
    private int enemiesAllowedOnScreen = 8; //default amount of enemies allowed on screen
    private Vector2 screenBounds;


    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        timePeriodforMediumDifficulty += timePeriodforEasyDifficulty;
        StartCoroutine(Wave());

    }

    private void Update()
    {
        difficultyTimer += Time.deltaTime;


        // 0 - 60 seconds = easy;
        // 60 - 120 seconds = medium
        // 120 - 180 seconds = hard
        // 180 - infinity seconds = veryhard

        if(difficultyTimer < timePeriodforEasyDifficulty && currentDifficulty != 0)
        {
            //easy difficulty
            currentDifficulty = 0;
            AdjustDifficulty(10, 2);
        }
        if( (difficultyTimer > timePeriodforEasyDifficulty && difficultyTimer < timePeriodforMediumDifficulty) && currentDifficulty != 1)
        {
            //medium difficulty
            AdjustDifficulty(16, 4);
            currentDifficulty = 1;
        }
        else if( (difficultyTimer > timePeriodforMediumDifficulty && difficultyTimer < timePeriodforHardDifficulty) && currentDifficulty != 2 )
        {
            //hard difficulty
            currentDifficulty = 2;
            AdjustDifficulty(20, 6);
        }
        else if( (difficultyTimer > timePeriodforHardDifficulty) && currentDifficulty != 3)
        {
            //very hard difficulty
            currentDifficulty = 3;
            AdjustDifficulty(25, 8);
        }

    }

    private void SpawnEnemy()
    {
        GameObject enemy = null;
        if (currentDifficulty == 0)
        {
            //Easy Enemies 1 in3

            int random = Random.Range(1, 3);

            if(random == 1)
            {
                enemy = Instantiate(defaultEnemy) as GameObject;
            }
            else if(random == 2)
            {
                enemy = Instantiate(enemyIntervalToPos) as GameObject;
            }
            else
            {
                enemy = Instantiate(enemyLinear) as GameObject;
            }

        }
        else if(currentDifficulty == 1)
        {
            //Medium Enemies

            int random = Random.Range(1, 3);

            if (random == 1)
            {
                enemy = Instantiate(enemyLinearToPOs) as GameObject;
            }
            else if (random == 2)
            {
                enemy = Instantiate(enemyShooterBasic) as GameObject;
            }
            else
            {
                enemy = Instantiate(enemyShooterHoming) as GameObject;
            }
        }
        else if(currentDifficulty == 2)
        {
            //Hard Enemies

            int random = Random.Range(1, 2);

            if (random == 1)
            {
                enemy = Instantiate(enemySplitter) as GameObject;
            }
            else if (random == 2)
            {
                enemy = Instantiate(enemyZigZag) as GameObject;
            }
        }
        else
        {
            int random = Random.Range(1, 2);

            if (random == 1)
            {
                enemy = Instantiate(enemySplitter) as GameObject;
            }
            else if (random == 2)
            {
                enemy = Instantiate(enemyZigZag) as GameObject;
            }
        }
        GameObject a = Instantiate(defaultEnemy) as GameObject;
        enemy.transform.position = new Vector2(screenBounds.x * 2.1f, Random.Range(-screenBounds.y, screenBounds.y));
    }

    private void AdjustDifficulty(int enemyOnScreenIncrease, int enemyDamageIncrease)
    {
        defaultEnemy.GetComponent<EnemyDamage>().SetDamage(enemyDamageIncrease);
        enemiesAllowedOnScreen = enemyOnScreenIncrease;
    }

    IEnumerator Wave()
    {
        
        while(true)
        {
            //print("number: " + GameObject.FindGameObjectsWithTag("Enemy").Length);
            //print("spawn");
            if(GameObject.FindGameObjectsWithTag("Enemy").Length <= enemiesAllowedOnScreen)
            {
                yield return new WaitForSeconds(respawnTime / 8);
                SpawnEnemy();
            }

            yield return new WaitForSeconds(respawnTime / 8);
            
        }
    }
}
