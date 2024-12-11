using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject joystick;
    public GameObject joystickBG;

    public Vector2 joystickVec;
    public Vector2 joystickTouchPos;
    public Vector2 joystickOriginalPos;

    private float joystickRadius;
    private bool isDragging = false;

    void Start()
    {
        // Initialize joystick position
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.x / 1.1f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Check if the touch is within the joystick radius
        if (Vector2.Distance(eventData.position, joystickOriginalPos) <= joystickRadius)
        {
            isDragging = true;
            joystickTouchPos = joystickOriginalPos;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        // Calculate the direction and position
        Vector2 dragPos = eventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float distance = Vector2.Distance(dragPos, joystickTouchPos);
        if (distance < joystickRadius)
        {
            joystick.transform.position = dragPos;
        }
        else
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging) return;

        // Reset joystick to the original position
        joystick.transform.position = joystickOriginalPos;
        joystickVec = Vector2.zero;
        isDragging = false;
    }
}
