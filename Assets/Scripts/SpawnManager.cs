using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public GameObject zombiePrefabs;
    public int number;
    public float spawnRadius;
    public bool spawnOnStart = true;
    Vector3 result;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnOnStart)
        {
            CreateAllZombies();
        }

    }

    private void CreateAllZombies()
    {
        for (int i = 0; i < number; i++)
        {
            print("Transform.position = " + transform.position);
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * spawnRadius;
            print("Random point = " + randomPoint);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                result = hit.position;
                //print("Result=" +result);
                Instantiate(zombiePrefabs, result, Quaternion.identity);
            }
            else
                i--;
        }
    }
}
