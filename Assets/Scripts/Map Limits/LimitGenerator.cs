using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitGenerator : MonoBehaviour
{
    [SerializeField] [Tooltip("Extra space from the camera edge")]
    private float extraSpace;

    private EdgeCollider2D edgeCollider2D;

    private void Awake()
    {
        edgeCollider2D = GetComponent<EdgeCollider2D>();
    }

    private void Start()
    {
        var camera = Camera.main;
        if (camera == null)
        {
            Debug.LogError("No camera found");
            return;
        }

        var bounds = CameraBounds.GetCameraBounds(camera);
        var max = bounds.max;
        var min = bounds.min;
        edgeCollider2D.points = new[]
        {
            new Vector2(max.x + extraSpace, max.y + extraSpace),
            new Vector2(min.x - extraSpace, max.y + extraSpace),
            new Vector2(min.x - extraSpace, min.y - extraSpace),
            new Vector2(max.x + extraSpace, min.y - extraSpace),
            new Vector2(max.x + extraSpace, max.y + extraSpace),
        };
    }
}
