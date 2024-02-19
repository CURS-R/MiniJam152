using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    [field: SerializeField] public RatController Controller { get; private set; }
    [field: SerializeField] public RatMovement Movement { get; private set; }
}
