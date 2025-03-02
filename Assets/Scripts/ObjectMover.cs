using UnityEngine;
using UnityEngine.UI;

public class ObjectMover : MonoBehaviour
{
    public Slider sliderX;
    public Slider sliderY;
    public Slider sliderZ;
    public OrbitCamera orbitCamera;

    private Transform selectedObject;

    void Start()
    {
        sliderX.onValueChanged.AddListener(UpdatePosition);
        sliderY.onValueChanged.AddListener(UpdatePosition);
        sliderZ.onValueChanged.AddListener(UpdatePosition);
    }

    public void SetSelectedObject(Transform newObject)
    {
        selectedObject = newObject;

        if (selectedObject != null)
        {
            Vector3 pos = selectedObject.position;
            sliderX.value = pos.x;
            sliderY.value = pos.y;
            sliderZ.value = pos.z;
        }
    }

    private void UpdatePosition(float value)
    {
        if (selectedObject != null)
        {
            if (orbitCamera != null)
            {
                orbitCamera.SetObjectBeingMoved(true);
            }

            selectedObject.position = new Vector3(sliderX.value, sliderY.value, sliderZ.value);

            CancelInvoke(nameof(EnableCameraMovement));
            Invoke(nameof(EnableCameraMovement), 0.5f);
        }
    }

    private void EnableCameraMovement()
    {
        if (orbitCamera != null)
        {
            orbitCamera.SetObjectBeingMoved(false);
        }
    }
}
