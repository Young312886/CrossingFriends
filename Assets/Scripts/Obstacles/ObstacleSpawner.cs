using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    private float maxX = 5f;
    private float minY = -5f;
    [SerializeField]
    private GameObject[] cars;
    // int posY;
    [SerializeField]
    private float spawnInterval = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartObstacleRoutine();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.x > maxX || transform.position.y < minY) {
            Destroy(gameObject); 
        }
    }

    void StartObstacleRoutine() {
        // 병렬처리 해주는 함수
        StartCoroutine("ObstacleRoutine"); 
    }
    

    IEnumerator ObstacleRoutine() {
        yield return new WaitForSeconds(1f);
        while (true) {
            int index = Random.Range(0, cars.Length);
            SpawnEnemy(index);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void SpawnEnemy(int index) {
        Vector3 spawnPos = new Vector3( transform.position.x , transform.position.y, transform.position.z );
        Instantiate( cars[index], spawnPos, Quaternion.identity );
    }
}
