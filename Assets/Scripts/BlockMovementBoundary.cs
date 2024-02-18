using Base;
using UnityEngine;

/// <summary>
/// This class doesn't work!
/// </summary>
public class BlockMovementBoundary : Boundary
{
    protected override void DoBoundaryHit(GameObject gameObject)
    {
        var directionFromCenter = (gameObject.transform.position - anchor.position).normalized;

        var newPosition = anchor.position + directionFromCenter * Radius;

        float offset = 0.1f;
        newPosition -= directionFromCenter * offset;

        gameObject.transform.position = newPosition;
    }
}