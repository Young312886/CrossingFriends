using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpanwerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ObstacleSpwaner;
    // int posY;
    [SerializeField]
    private float spawnInterval = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
       StartSpawnerRoutine();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartSpawnerRoutine() {
        // 병렬처리 해주는 함수
        StartCoroutine("SpawnerRoutine"); 
    }
    

    IEnumerator SpawnerRoutine() {
        yield return new WaitForSeconds(1f);
        while (true) {
            SpawnSpawner();

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void SpawnSpawner() {
        Vector3 spawnPos = new Vector3( transform.position.x , transform.position.y, transform.position.z );
        Instantiate( ObstacleSpwaner, spawnPos, Quaternion.identity );
    }
}
