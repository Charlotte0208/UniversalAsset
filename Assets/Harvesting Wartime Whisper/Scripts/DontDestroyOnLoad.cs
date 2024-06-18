using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    void Awake()
    {
        // Check if an instance with the same name already exists in the scene
        DontDestroyOnLoad[] existingInstances = FindObjectsOfType<DontDestroyOnLoad>();
        foreach (var instance in existingInstances)
        {
            if (instance != this && instance.gameObject.name == gameObject.name)
            {
                // If an instance with the same name exists, destroy this new instance
                Destroy(gameObject);
                return;
            }
        }
        
        // If no instance with the same name exists, mark this as DontDestroyOnLoad
        DontDestroyOnLoad(gameObject);
    }
}