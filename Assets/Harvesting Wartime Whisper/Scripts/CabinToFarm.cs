using UnityEngine;
using System.Collections;

public class CabinToFarm : MonoBehaviour
{
    public GameObject CabinAirWall;

    public GameObject FarmAirWall;
    
    // Public method to adjust the "world" object
    public void AdjustWorldObject()
    {
        // Find the object with tag "world"
        GameObject world = GameObject.FindWithTag("world");

        // Check if the "world" object is found
        if (world != null)
        {
            // Start the coroutine to adjust the world object
            StartCoroutine(AdjustWorldCoroutine(world));
        }
        else
        {
            Debug.LogWarning("No object with tag 'world' found in the scene.");
        }
    }

    private IEnumerator AdjustWorldCoroutine(GameObject world)
    {
        // Make the "world" object disappear
        world.SetActive(false);

        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);
        
        CabinAirWall.SetActive(false);
        
        // Set the position and rotation
        world.transform.position += new Vector3(5, 0, 23);
        world.transform.rotation = Quaternion.Euler(0, 180, 0);
        
        FarmAirWall.SetActive(true);

        // Make the "world" object reappear
        world.SetActive(true);
    }
}