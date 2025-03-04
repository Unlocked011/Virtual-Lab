using Unity.Mathematics;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public float rotationSpeed = 0.2f;
    public float zoomSpeed = 0.5f;
    public float transitionSpeed = 1f;
    public float baseDistance = 10f;
    public float sizeMultiplier = 1.5f;

    public Vector3 defaultPosition;
    public Quaternion defaultRotation;
    private Transform target;
    private float targetDistance;
    private bool isTransitioning = false;
    private bool isObjectBeingMoved = false; 

    void Start()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
    }

    void Update()
    {
        if (isObjectBeingMoved) return; 

        if (isTransitioning && target != null)
        {
            SmoothTransitionToTarget();
            return;
        }

        if (Input.touchCount == 1 && target != null)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float rotX = touch.deltaPosition.y * rotationSpeed;
                float rotY = -touch.deltaPosition.x * rotationSpeed;

                transform.RotateAround(target.position, Vector3.up, rotY);
                transform.RotateAround(target.position, transform.right, rotX);
            }
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        targetDistance = CalculateDistanceBasedOnSize(target);
        isTransitioning = true;
    }

    public void SetObjectBeingMoved(bool isMoving)
    {
        isObjectBeingMoved = isMoving;
    }

    private float CalculateDistanceBasedOnSize(Transform obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Vector3 size = renderer.bounds.size;
            float maxSize = Mathf.Max(size.x, size.y, size.z);
            return baseDistance + (maxSize * sizeMultiplier);
        }

        return baseDistance;
    }

    private void SmoothTransitionToTarget()
    {
        if (target == null) return;

        Vector3 direction = (transform.position - target.position).normalized;
        Vector3 desiredPosition = target.position + direction * targetDistance;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * transitionSpeed);
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * transitionSpeed);

        if (Vector3.Distance(transform.position, desiredPosition) < 0.1f)
        {
            isTransitioning = false;
        }
    }

    public void ResetCamera()
    {
        target = null;
        isObjectBeingMoved = false;
        isTransitioning = false;

        transform.position = defaultPosition;
        transform.rotation = defaultRotation;
    }
}
