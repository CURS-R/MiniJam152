using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
using Utils;

public class Spawner : MonoBehaviour
{
    [field: SerializeField] public List<Transform> SpawnPositions { get; private set; }
    [field: SerializeField] private float spawnRadius = 3;
    public float SpawnRadius => Mathf.Abs(spawnRadius);
    [field: SerializeField] private bool isSphere;
    [field: SerializeField] private bool onOutside;
    [field: SerializeField] private bool randomRotation;
    [field: Header("Debug")]
    [field: SerializeField] private Color debugColor;
    public Color DebugColor => new(debugColor.r, debugColor.g, debugColor.b, Mathf.Clamp(debugColor.a, 0.1f,1f));

    public List<GameObject> Spawn(GameObject prefab, uint amount)
    {
        List<GameObject> objectsSpawned = new();
        for (int i = 0; i < amount; i++)
            objectsSpawned.Add(Spawn(prefab));
        return objectsSpawned;
    }
    public GameObject Spawn(GameObject prefab)
    {
        var pos = GetRandomPosition();
        Quaternion rot = randomRotation ? Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)) : Quaternion.identity;
        return Instantiate(prefab, pos, rot);
    }

    public Vector3 GetRandomPosition()
    {
        var randomT = SpawnPositions[Random.Range(0, SpawnPositions.Count)];
        Vector3 pos;
        if (isSphere)
        {
            pos = (onOutside ? Random.onUnitSphere : Random.insideUnitSphere) * SpawnRadius + randomT.position;
        }
        else
        {
            var circle = (onOutside ? Random.insideUnitCircle.normalized : Random.insideUnitCircle) * SpawnRadius;
            pos = new Vector3(circle.x, 0, circle.y) + randomT.position;
        }
        return pos;
    }
    
    private void OnDrawGizmos()
    {
        foreach (var pos in SpawnPositions)
        {
            Gizmos.color = DebugColor;
            if (isSphere)
            {
                Gizmos.DrawWireSphere(pos.position, SpawnRadius);
            }
            else
            {
                var mesh = MeshUtils.GetUnityPrimitiveMesh(PrimitiveType.Cylinder);
                Gizmos.DrawWireMesh(mesh, pos.position, Quaternion.identity, new(spawnRadius*2, 0 ,spawnRadius*2));
            }
        }
    }
}