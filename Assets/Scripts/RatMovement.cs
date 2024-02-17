using System;
using UnityEngine;
using UnityEngine.Serialization;

public class RatMovement : MonoBehaviour
{
    [SerializeField] public float Speed { get; private set; } = 5f;
    
    private Vector3 targetPos;

    [HideInInspector] public bool IsWaitingForNewDestination { get; private set; }
    [HideInInspector] private float distanceFromTarget;

    private void Update()
    {
        if (targetPos == null || IsWaitingForNewDestination)
            return;
        
        MoveTowardsAndFaceDestination();
        
        if (distanceFromTarget < 0.1f)
        {
            IsWaitingForNewDestination = true;
        }
    }

    public void SetTarget(Vector3 newTargetPos)
    {
        targetPos = new(newTargetPos.x, 0, newTargetPos.z); // TODO: some way of setting the floor level
        IsWaitingForNewDestination = false;
    }

    private void MoveTowardsAndFaceDestination()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);

        var direction = (targetPos - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            var lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * Speed);
        }
        
        distanceFromTarget = Vector3.Distance(transform.position, targetPos);
    }
}