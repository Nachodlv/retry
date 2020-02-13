using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Fire events whenever the touch controller is moved</para>
/// </summary>
public class PlayerController : Controller
{
    public override event Action OnFireAbility;

    [SerializeField] [Tooltip("Touch controller to move the player ship")]
    private SimpleTouchController touchController;

    private void Awake()
    {
        touchController.TouchEvent += Move;
        touchController.TouchStateEvent += TouchControllerState;
    }

    
    public override void FireAbility()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// If present if false then it calls the method Move with a direction of (0, 0)
    /// </summary>
    /// <param name="present">If the user is currently touching the touch control</param>
    private void TouchControllerState(bool present)
    {
        if(!present) Move(Vector2.zero);
    }
}