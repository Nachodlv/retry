using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds
{
    public static Bounds GetCameraBounds(Camera camera)
    {
        var screenAspect = Screen.width / Screen.height;
        var cameraHeight = camera.orthographicSize * 2;
        var bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }

//    public static Vector2[] GetCameraBoundPoints(Camera camera)
//    {
//        var bounds = GetCameraBounds(camera);
//        var max = bounds.max;
//        var min = bounds.min;
//        
//        return new[]
//        {
//            new Vector2(max.x, max.y),
//            new Vector2(min.x, max.y),
//            new Vector2(min.x, min.y),
//            new Vector2(max.x, min.y), 
//        };
//    }
}
