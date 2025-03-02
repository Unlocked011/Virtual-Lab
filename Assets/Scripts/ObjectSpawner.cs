using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{ 
    public GameObject objectPrefab;
    public Transform spawnPoint;
    public ObjectMover objectMover;

    public void SpawnObject()
    {
        if (objectPrefab != null && spawnPoint != null)
        {
            GameObject newObject = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);
            objectMover.SetSelectedObject(newObject.transform);
        }
        else
        {
            Debug.LogError("Coœ posz³o nie tak");
        }
    }

}
