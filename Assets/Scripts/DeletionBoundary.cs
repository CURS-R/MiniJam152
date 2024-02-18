using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Base;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

public class DeletionBoundary : MonoBehaviour
{
    [field: SerializeField] public List<GameObject> prefabBlacklist { get; private set; }
    [field: SerializeField] public Transform anchor { get; private set; }
    [field: SerializeField] private float radius = 3;
    public float Radius => Mathf.Abs(radius);
    [field: Header("Debug")]
    [field: SerializeField] private Color debugColor;
    public Color DebugColor => new(debugColor.r, debugColor.g, debugColor.b, Mathf.Clamp(debugColor.a, 0.1f,1f));

    private List<List<Type>> typeBlacklists = new();
    
    private void Start()
    {
        foreach (var prefab in prefabBlacklist)
        {
            AddPrefabComponentsToABlacklist(prefab);
        }
    }
    
    private void Update()
    {
        List<GameObject> objectsMarkedForDeletion = new();
        foreach (var go in (GameObject[])FindObjectsOfType(typeof(GameObject)))
        {
            var types = go.GetComponents<Component>().Select(component => component.GetType()).ToList();

            bool isInBlacklist =false;
            foreach (var typeBlacklist in typeBlacklists)
            {
                if (types.Count == typeBlacklist.Count && !types.Except(typeBlacklist).Any())
                {
                    isInBlacklist = true;
                }
            }

            if (!isInBlacklist) continue;
            
            if (Vector3.Distance(go.transform.position, Vector3.zero) > radius)
                objectsMarkedForDeletion.Add(go);
        }
        foreach (var obj in objectsMarkedForDeletion)
            Destroy(obj);
    }
    
    private void AddPrefabComponentsToABlacklist(GameObject prefab)
    {
        var newBlacklist = prefab.GetComponents<Component>().Select(component => component.GetType()).ToList();
        typeBlacklists.Add(newBlacklist);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = DebugColor;
        var mesh = MeshUtils.GetUnityPrimitiveMesh(PrimitiveType.Cylinder);
        Gizmos.DrawWireMesh(mesh, anchor.position, Quaternion.identity, new(Radius*2, 0 ,Radius*2));
    }
}