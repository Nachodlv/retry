using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public abstract event Action OnFireAbility;

    public abstract event Action<Vector2> OnMove;

    public abstract void FireAbility();
    public abstract void Move(Vector2 direction);
}
