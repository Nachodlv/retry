using System;
using UnityEngine;

/// <summary>
/// <para>Animates the ShinyMaterial</para>
/// </summary>
public class ShinyShaderAnimation: MonoBehaviour
{
    [SerializeField] [Tooltip("Renderer with the shiny material")]
    private SpriteRenderer renderer;
    
    private Material shinyMaterial;
    
    private void Awake()
    {
        shinyMaterial = renderer.material;
    }

    /// <summary>
    /// <para>Changes the shine location of the shinyMaterial</para>
    /// </summary>
    private void Update()
    {
        var shineLocation = Mathf.PingPong(Time.time, 1.0f);
        shinyMaterial.SetFloat("_ShineLocation", shineLocation);
    }
}
