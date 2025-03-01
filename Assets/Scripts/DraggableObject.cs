using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private OrbitCamera orbitCamera;
    void Start()
    {
        cam = Camera.main;
        orbitCamera = cam.GetComponent<OrbitCamera>();
    }

    void OnMouseDown()
    {
        offset = transform.position - GetMousePosition();

        if (orbitCamera != null)
        {
            orbitCamera.SetDragging(true);
        }
    }

    void OnMouseDrag()
    {
        transform.position = GetMousePosition() + offset;
    }

    void OnMouseUp()
    {
        if (orbitCamera != null)
        {
            orbitCamera.SetDragging(false);
        }
    }

    Vector3 GetMousePosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }
}
