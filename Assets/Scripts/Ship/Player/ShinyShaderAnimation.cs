using System;
using UnityEngine;

public class ShinyShaderAnimation: MonoBehaviour
{
    [SerializeField] [Tooltip("Renderer with the shiny material")]
    private SpriteRenderer renderer;
    
    private Material shinyMaterial;
    
    private void Awake()
    {
        shinyMaterial = renderer.material;
    }

    private void Update()
    {
        var shineLocation = Mathf.PingPong(Time.time, 1.0f);
        shinyMaterial.SetFloat("_ShineLocation", shineLocation);
    }
}
