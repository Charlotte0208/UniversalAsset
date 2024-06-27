using UnityEngine;
using System.Collections;

public class CabinToFarm : MonoBehaviour
{
    public GameObject CabinAirWall;
    public GameObject FarmAirWall;

    // Public audio clip to play during the transition
    public AudioClip transitionClip;

    // AudioSource to play the clip
    private AudioSource audioSource;

    private GameObject cameraObject;
    private Color originalBackgroundColor;

    private void Start()
    {
        // Add an AudioSource component to the game object this script is attached to
        audioSource = gameObject.AddComponent<AudioSource>();

        // Find the main camera
        cameraObject = Camera.main.gameObject;
        if (cameraObject != null)
        {
            // Save the original background color
            originalBackgroundColor = cameraObject.GetComponent<Camera>().backgroundColor;
        }
        else
        {
            Debug.LogError("Main Camera is not found.");
        }
    }

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
        // Make the "world" object and camera disappear
        world.SetActive(false);
        SetCameraVisibility(false);

        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        CabinAirWall.SetActive(false);

        // Set the position and rotation
        world.transform.position += new Vector3(4.8f, 0, 24);
        world.transform.rotation = Quaternion.Euler(0, 90, 0);

        FarmAirWall.SetActive(true);

        // Play the audio clip
        if (transitionClip != null)
        {
            audioSource.clip = transitionClip;
            audioSource.Play();

            // Wait for the audio clip to finish playing
            yield return new WaitForSeconds(transitionClip.length);
        }
        else
        {
            Debug.LogWarning("Transition clip is not set.");
        }

        // Make the "world" object and camera reappear
        world.SetActive(true);
        SetCameraVisibility(true);
    }

    private void SetCameraVisibility(bool isVisible)
    {
        if (cameraObject != null)
        {
            Camera cameraComponent = cameraObject.GetComponent<Camera>();
            if (isVisible)
            {
                // 恢复相机视野并使用新场景中的背景颜色
                cameraComponent.cullingMask = ~0; // 显示所有图层
                cameraComponent.backgroundColor = originalBackgroundColor;
            }
            else
            {
                // 保存新场景中的背景颜色
                originalBackgroundColor = cameraComponent.backgroundColor;

                // 设置相机视野为黑色
                cameraComponent.cullingMask = 0; // 不显示任何图层
                cameraComponent.backgroundColor = Color.black;
            }
        }
        else
        {
            Debug.LogError("Main Camera is not found.");
        }
    }
}
