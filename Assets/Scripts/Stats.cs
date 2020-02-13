using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] [Tooltip("Movement speed")]
    private float speed;

    public float Speed => speed;
}
