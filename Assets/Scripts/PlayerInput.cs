using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerInput : MonoBehaviour
{
    public static event OnSwipeInput SwipeEvent;
    public delegate void OnSwipeInput(Vector2 direction);
    
    private Vector2 _tapPosition;
    private Vector2 _swipePosition;

    private bool _isSwiping;
    private bool _isMobile;
    private float _deadzone = 70f;
    
    private void Update()
    {
        CheckTouchPosition();
        CheckInput();
    }

    private void Start()
    {
        _isMobile = Application.isMobilePlatform;
    }

    private void CheckTouchPosition()
    {
        if (!_isMobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isSwiping = true;
                _tapPosition = (Vector2)Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
                ResetSwipeParameters();
        }
        else
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _isSwiping = true;
                _tapPosition = Input.GetTouch(0).position;
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                ResetSwipeParameters();
        }   
    }
    
    private void CheckInput()
    {
        _swipePosition = Vector2.zero;
        if (_isSwiping)
        {
            if (!_isMobile && Input.GetMouseButton(0))
                _swipePosition = (Vector2)Input.mousePosition - _tapPosition;
            else if (Input.touchCount > 0)
                _swipePosition = Input.GetTouch(0).position - _tapPosition;
        }

        if (_swipePosition.sqrMagnitude > _deadzone)
        {
            if (Math.Abs(_swipePosition.x) > Math.Abs(_swipePosition.y))
                SwipeEvent?.Invoke(_swipePosition.x > 0 ? Vector2.right : Vector2.left);
            else
                SwipeEvent?.Invoke(_swipePosition.y > 0 ? Vector2.up : Vector2.down);
            ResetSwipeParameters();
        }
    }

    private void ResetSwipeParameters()
    {
        _isSwiping = false;
        
        _tapPosition = Vector2.zero;
        _swipePosition = Vector2.zero;
    }
}
