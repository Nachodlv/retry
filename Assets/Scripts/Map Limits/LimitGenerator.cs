using UnityEngine;

/// <summary>
/// <para>Sets the collision points of the EdgeCollider2D taking into account the camera view bounds</para>
/// </summary>
public class LimitGenerator : MonoBehaviour
{
    [SerializeField] [Tooltip("Extra space from the camera edge")]
    private float extraSpace;

    private EdgeCollider2D edgeCollider2D;

    private void Awake()
    {
        edgeCollider2D = GetComponent<EdgeCollider2D>();
    }

    /// <summary>
    /// <para>If a main Camera is found, it sets the points of the EdgeCollider2D.</para>
    /// </summary>
    private void Start()
    {
        var myCamera = Camera.main;
        if (myCamera == null)
        {
            Debug.LogError("No camera found");
            return;
        }

        edgeCollider2D.points = GetCameraViewBounds(myCamera);
    }

    /// <summary>
    /// <para>Get the camera view bounds adding up the extra space</para>
    /// </summary>
    /// <param name="myCamera"></param>
    /// <returns>Returns the points of the bounds forming a square. The first and the last point are the same</returns>
    private Vector2[] GetCameraViewBounds(Camera myCamera)
    {
        var bounds = CameraBounds.GetCameraBounds(myCamera);
        var max = bounds.max;
        var min = bounds.min;
        return new[]
        {
            new Vector2(max.x + extraSpace, max.y + extraSpace),
            new Vector2(min.x - extraSpace, max.y + extraSpace),
            new Vector2(min.x - extraSpace, min.y - extraSpace),
            new Vector2(max.x + extraSpace, min.y - extraSpace),
            new Vector2(max.x + extraSpace, max.y + extraSpace),
        };
    }
}