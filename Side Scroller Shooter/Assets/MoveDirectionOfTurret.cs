using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDirectionOfTurret : MonoBehaviour
{
    Vector2 targetLocation;
    Vector2 pos;
    [SerializeField] float moveSpeed = 5;
    Rigidbody2D enemyComponent;

    // Start is called before the first frame update
    void Start()
    {
        enemyComponent = GetComponent<Rigidbody2D>();
        pos = transform.position;
        targetLocation = findClosestProjectileTurret().transform.position; //Finds the closest turret to shoot from
        enemyComponent.velocity = (targetLocation - pos).normalized * moveSpeed;
    }

    private GameObject findClosestProjectileTurret()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("ProjectileDirection");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
