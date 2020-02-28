﻿using System;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    public event Action<Vector2> OnMove;

    [SerializeField] private KeyCode up = KeyCode.UpArrow;
    [SerializeField] private KeyCode down = KeyCode.DownArrow;
    [SerializeField] private KeyCode left = KeyCode.LeftArrow;
    [SerializeField] private KeyCode right = KeyCode.RightArrow;

    private bool keyPressed;
    
    private void Update()
    {
        var hadAKeyPressed = keyPressed;
        keyPressed = false;
        var direction = Vector2.zero;
        if (Input.GetKey(up)) direction += new Vector2(0, 1);
        if (Input.GetKey(down)) direction += new Vector2(0, -1);
        if (Input.GetKey(left)) direction += new Vector2(-1, 0);
        if (Input.GetKey(right)) direction += new Vector2(1, 0);
        if (direction != Vector2.zero) keyPressed = true;
        if(hadAKeyPressed || keyPressed) OnMove?.Invoke(direction);
    }
}