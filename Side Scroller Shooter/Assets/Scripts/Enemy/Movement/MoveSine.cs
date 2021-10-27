using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSine : MonoBehaviour
{
    float sinCenterY;
    [SerializeField] float amplitude;
    [SerializeField] float frequency;
    [SerializeField] bool inverted;
    [SerializeField] float moveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        sinCenterY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        //y position calculation
        float sin = Mathf.Sin(pos.x * frequency) * amplitude;
        if (inverted)
        {
            sin *= -1;
        }
        pos.y = sinCenterY + sin;

        //x position calculation
        pos.x -= moveSpeed * Time.deltaTime;
        if (pos.x < -15)
        {
            Destroy(gameObject);
        }

        transform.position = pos;
    }
}
