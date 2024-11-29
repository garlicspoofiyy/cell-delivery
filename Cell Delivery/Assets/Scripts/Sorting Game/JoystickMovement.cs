using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class JoystickMovement : MonoBehaviour
{

    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    public Vector2 joystickTouchPos;
    public Vector2 joystickOriginalPos;
    private float joystickRadius;
    private bool isDragging = false;


    // Start is called once per frame
    void Start()
    {
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y;
    }

    public void PointerDown() 
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(
            joystickBG.GetComponent<RectTransform>(), 
            Input.mousePosition, 
            null))
        {
            isDragging = true;
            joystick.transform.position = Input.mousePosition;
            joystickBG.transform.position = Input.mousePosition;
            joystickTouchPos = Input.mousePosition;
        }
    }

    public void Drag(BaseEventData baseEventData) 
    {
        if (!isDragging) return;

        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if (joystickDist < joystickRadius) 
        {
            // move joystick within the joystick bg
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
        } 
        else 
        {
            // else only up to the radius of the bg
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
        }
    }

    public void PointerUp() 
    {
        isDragging = false;
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
        joystickBG.transform.position = joystickOriginalPos;
    }
}

