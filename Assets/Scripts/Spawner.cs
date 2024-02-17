using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utils;

public class Spawner : MonoBehaviour
{
    [field: SerializeField] public GameObject ObjectToSpawn { get; private set; }
    [field: SerializeField] public List<Transform> SpawnPositions { get; private set; }
    [field: SerializeField] private float spawnRadius { get; set; } = 3;
    public float SpawnRadius => Mathf.Abs(spawnRadius);
    [field: SerializeField] private bool isSphere { get; set; } = false;
    [field: SerializeField] private bool onOutside { get; set; } = false;
    [field: SerializeField] private bool randomRotation { get; set; } = false;
    [field: Header("Debug")]
    [field: SerializeField] private Color debugColor { get; set; }
    public Color DebugColor => new(debugColor.r, debugColor.g, debugColor.b, 1);

    private void Start()
    {
        SpawnObjects(15); // TODO: call this in a manager (or something else)
    }

    public void SpawnObjects(int amount)
    {
        for (int i = 0; i < amount; i++)
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
            Quaternion rot = randomRotation ? Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)) : Quaternion.identity;
            Instantiate(ObjectToSpawn, pos, rot);
        }
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