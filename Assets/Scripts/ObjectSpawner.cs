using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{ 
    public GameObject objectPrefab;
    public Transform spawnPoint;

    public void SpawnObject()
    {
        if (objectPrefab != null && spawnPoint != null)
        {
            GameObject newObject = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);
            newObject.AddComponent<DraggableObject>();
        }
        else
        {
            Debug.LogError("Coœ posz³o nie tak");
        }
    }

}
