using UnityEngine;
using System.Collections.Generic;

public class ObjectSelector : MonoBehaviour
{
    public OrbitCamera orbitCamera;
    public ObjectMover objectMover;
    public List<Transform> excludedObjects;

    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f;

    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (excludedObjects.Contains(hit.transform))
                {
                    return; 
                }

                float timeSinceLastClick = Time.time - lastClickTime;
                lastClickTime = Time.time;

                if (timeSinceLastClick < doubleClickThreshold)
                {

                    orbitCamera.SetTarget(hit.transform);
                }
                else
                { 
                    objectMover.SetSelectedObject(hit.transform);

                }
            }
        }
    }
}
