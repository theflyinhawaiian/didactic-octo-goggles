using System.Collections.Generic;
using UnityEngine;

public enum SpawnMethod
{
    Random,
    Wave,
    Pattern,
    Staggered,
    Lattice
}

public class SpawnerBehavior : MonoBehaviour
{
    public GameObject player;
    public Transform endPoint;
    public float velocity = 2000;

    public GameObject obstacle;
    public SpawnMethod spawnMethod = SpawnMethod.Random;

    float distanceFromLastSpawn = 0;
    float lastPosition = 0f;
    public float spawnFrequency = 50;
    public float offsetFromPlayer = 50;

    List<Vector3> objectPositions = new List<Vector3>();

    private void Start()
    {
        lastPosition = transform.position.z;

        switch (spawnMethod) {
            case SpawnMethod.Random:
                GenerateRandomObstacles();
                break;
            case SpawnMethod.Pattern:
                break;
            case SpawnMethod.Wave:
                GenerateWaves();
                break;
            case SpawnMethod.Staggered:
                GenerateStaggeredWaves();
                break;
            case SpawnMethod.Lattice:
                GenerateLatticedWaves();
                break;
        }
    }

    void FixedUpdate()
    {
        var newZVal = player.transform.position.z + offsetFromPlayer;
        transform.position = new Vector3(transform.position.x, transform.position.y, newZVal);

        SpawnObstacles(newZVal);
    }

    void SpawnObstacles(float currZ)
    {
        if (currZ > objectPositions[0].z) {
            Instantiate(obstacle, objectPositions[0], Quaternion.identity);
            objectPositions.RemoveAt(0);
            SpawnObstacles(currZ);
        }
    }

    void GenerateRandomObstacles()
    {
        var leftLimit = (float)(transform.position.x - (transform.localScale.x / 2.0) + (obstacle.transform.localScale.x / 2.0));
        var rightLimit = (float)(transform.position.x + (transform.localScale.x / 2.0) - (obstacle.transform.localScale.x / 2.0));

        for (var i = spawnFrequency; i < endPoint.position.z; i += spawnFrequency) {
            var xval = Random.Range(leftLimit, rightLimit);
            GenerateObstacle(xval, i);
        }
    }

    void GenerateWaves()
    {
        var leftLimit = (float)(transform.position.x - (transform.localScale.x / 2.0) + (obstacle.transform.localScale.x / 2.0));
        var rightLimit = (float)(transform.position.x + (transform.localScale.x / 2.0) - (obstacle.transform.localScale.x / 2.0));

        var wavesSpawned = 0;

        for (var i = spawnFrequency; i < endPoint.position.z - 100; i += spawnFrequency) {
            for (var j = 0; j < 10; j++) {
                float xval;

                if (wavesSpawned % 2 == 0) {
                    xval = leftLimit + j * (obstacle.transform.localScale.x / 2);
                } else {
                    xval = rightLimit - j * (obstacle.transform.localScale.x / 2);
                }
                GenerateObstacle(xval, i);
                i += 5;
            }

            wavesSpawned += 1;
        }
    }

    void GenerateStaggeredWaves()
    {
        var leftLimit = (float)(transform.position.x - (transform.localScale.x / 2.0) + (obstacle.transform.localScale.x * 3.5));
        var rightLimit = (float)(transform.position.x + (transform.localScale.x / 2.0) - (obstacle.transform.localScale.x * 3.5));

        for (var i = spawnFrequency; i < endPoint.position.z - 100; i += spawnFrequency) {
            var centralXVal = Random.Range(leftLimit, rightLimit);
            var obstacleWidth = obstacle.transform.localScale.x;

            //Iterative attempt at the following uncommented code
            /*for(var j = 0; j < 3; j++)
            {
                for(var k = 0; k < j+1; k++)
                {

                }
            }*/

            GenerateObstacle(centralXVal, i);
            GenerateObstacle(centralXVal + 1.5f * obstacleWidth, i + 10);
            GenerateObstacle(centralXVal - 1.5f * obstacleWidth, i + 10);
            GenerateObstacle(centralXVal, i + 20);
            GenerateObstacle(centralXVal + 2.5f * obstacleWidth, i + 20);
            GenerateObstacle(centralXVal - 2.5f * obstacleWidth, i + 20);

            i += 20;
        }
    }

    void GenerateLatticedWaves()
    {
        var leftLimit = (float)(transform.position.x - (transform.localScale.x / 2.0) + (obstacle.transform.localScale.x/2));
        var rightLimit = (float)(transform.position.x + (transform.localScale.x / 2.0) - (obstacle.transform.localScale.x/2));

        var obstacleWidth = obstacle.transform.localScale.x;

        for (var i = spawnFrequency; i < endPoint.position.z - 100; i += spawnFrequency) {
            for(var numRows = 0; numRows < 5; numRows++) {
                if(numRows % 2 == 0) {
                    for(var j = leftLimit; j < rightLimit; j += 3 * obstacleWidth) {
                        GenerateObstacle(j, i);
                    }
                }else {
                    for (var j = rightLimit; j >= leftLimit; j -= 3 * obstacleWidth) {
                        GenerateObstacle(j, i);
                    }
                }
                i += 15;
            }
        }
    }

    void GenerateObstacle(float x, float z)
    {
        var newObstaclePos = new Vector3(x, 1, z);
        objectPositions.Add(newObstaclePos);
    }
}
