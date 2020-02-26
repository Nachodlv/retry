using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Fire events whenever the touch controller is moved</para>
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : Controller
{
    [SerializeField][Tooltip("Circle that represents that the user is controlling the ship")] 
    private GameObject circle;
    
    private SimpleTouchController touchController;
    private SpriteRenderer spriteRenderer;
    private bool enable;

    public bool Enable
    {
        get => enable;
        set
        {
            if(value) ActivateCircle();
            else DeActivateCircle();
            enable = value;
        }
    }

    private void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        touchController.TouchEvent += Move;
        touchController.TouchStateEvent += TouchControllerState;
    }

    /// <summary>
    /// If present if false then it calls the method Move with a direction of (0, 0)
    /// </summary>
    /// <param name="present">If the user is currently touching the touch control</param>
    private void TouchControllerState(bool present)
    {
        if (!present && Enable) Move(Vector2.zero);
    }

    /// <summary>
    /// <para>If Enable is true, it calls the base method Move with the direction.</para>
    /// </summary>
    /// <param name="direction"></param>
    private new void Move(Vector2 direction)
    {
        if (Enable) base.Move(direction);
    }

    /// <summary>
    /// <para>Sets the value of the touchController</para>
    /// </summary>
    /// <param name="newTouchController"></param>
    public void SetTouchController(SimpleTouchController newTouchController)
    {
        touchController = newTouchController;
    }

    /// <summary>
    /// <para>Activates a circle around the player ship so the user can identify better the ship that
    /// is controlling</para>
    /// </summary>
    private void ActivateCircle()
    {
        circle.SetActive(true);
        var color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;
    }

    /// <summary>
    /// <para>Removes the circle around the player ship. Reduces the transparency of the sprite.</para>
    /// </summary>
    private void DeActivateCircle()
    {
        circle.SetActive(false);
        var color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;
    }
}