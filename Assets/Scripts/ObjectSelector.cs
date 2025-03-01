using UnityEngine;
using System.Collections.Generic;

public class ObjectSelector : MonoBehaviour
{
    public OrbitCamera orbitCamera;
    public List<Transform> excludedObjects;


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
                orbitCamera.SetTarget(hit.transform);
            }
        }
    }
}
