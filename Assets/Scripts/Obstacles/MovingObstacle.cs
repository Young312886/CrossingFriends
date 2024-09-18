using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    private float maxX = 4.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        if (transform.position.x > maxX) {
            Destroy(gameObject);
        }
    }
}
