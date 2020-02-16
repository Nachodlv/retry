using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds
{
    /// <summary>
    /// <para>Returns the bounds of the camera view</para>
    /// </summary>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static Bounds GetCameraBounds(Camera camera)
    {
        var screenAspect = Screen.width / Screen.height;
        var cameraHeight = camera.orthographicSize * 2;
        var bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
}
