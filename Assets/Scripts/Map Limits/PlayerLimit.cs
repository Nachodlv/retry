using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerLimit : MonoBehaviour
{
    private Collider2D myCollider;
    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(other.collider, myCollider);
        }
    }
}
