using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;

    void Start()
    {
        // Cache the main camera for screen-to-world point conversions
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Handle touch input for mobile or mouse input for desktop
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            if (IsPointerOverShip(mousePosition))
            {
                offset = transform.position - mousePosition;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            if (offset != Vector3.zero)
            {
                transform.position = mousePosition + offset;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Reset the offset when releasing the drag
            offset = Vector3.zero;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Convert screen position to world position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }

    private bool IsPointerOverShip(Vector3 pointerPosition)
    {
        // Check if the pointer is over the ship's collider
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            return collider.OverlapPoint(pointerPosition);
        }

        // Optional: Return true if no collider is attached
        return true;
    }
}