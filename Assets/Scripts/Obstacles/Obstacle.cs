using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{   
    [SerializeField]
    private float moveSpeed = 10f;
    private float maxX = 5f;
    private float minY = -5f;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Random.Range(0, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        if (transform.position.x > maxX || transform.position.y < minY) {
            Destroy(gameObject); 
        }
    }
}
