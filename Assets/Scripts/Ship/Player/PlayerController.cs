using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Fire events whenever the touch controller is moved</para>
/// </summary>
public class PlayerController : Controller
{
    private SimpleTouchController touchController;
    [NonSerialized] public bool Enable;
    
    private void Awake()
    {
        base.Awake();
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
        if(!present && Enable) Move(Vector2.zero);
    }

    /// <summary>
    /// <para>If Enable is true, it calls the base method Move with the direction.</para>
    /// </summary>
    /// <param name="direction"></param>
    private new void Move(Vector2 direction)
    {
        if(Enable) base.Move(direction);
    }

    /// <summary>
    /// <para>Sets the value of the touchController</para>
    /// </summary>
    /// <param name="newTouchController"></param>
    public void SetTouchController(SimpleTouchController newTouchController)
    {
        touchController = newTouchController;
    }
}