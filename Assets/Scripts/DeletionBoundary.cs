using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Base;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

public class DeletionBoundary : Boundary
{
    protected override void DoBoundaryHit(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}