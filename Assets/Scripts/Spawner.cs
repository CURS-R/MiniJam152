using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [field: SerializeField] public GameObject ObjectToSpawn { get; private set; }
    [field: SerializeField] public List<Transform> SpawnPositions { get; private set; }
    [field: SerializeField] public uint NumberOfObjects { get; private set; } = 10;
    [field: SerializeField] private float spawnRadius { get; set; } = 3;
    public float SpawnRadius => Mathf.Abs(spawnRadius);

    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < NumberOfObjects; i++)
        {
            var randomT = SpawnPositions[Random.Range(0, SpawnPositions.Count)];
            var randomPos = Random.insideUnitSphere * SpawnRadius + randomT.position;
            var randomRot = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            Instantiate(ObjectToSpawn, randomPos, randomRot);
        }
    }
    
    private void OnDrawGizmos()
    {
        foreach (var pos in SpawnPositions)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(pos.position, SpawnRadius);
        }
    }
}