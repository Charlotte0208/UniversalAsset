using UnityEngine;
using UnityEngine.UI;

public class SceneSwitchButton : MonoBehaviour
{
    public string targetSceneName; // Public variable to specify the target scene name

    void Start()
    {
        // Find the GameObject with the "SceneManager" tag
        GameObject sceneManagerObject = GameObject.FindWithTag("SceneManager");

        if (sceneManagerObject != null)
        {
            // Get the SceneLoader component from the SceneManager GameObject
            SceneLoader sceneLoader = sceneManagerObject.GetComponent<SceneLoader>();

            if (sceneLoader != null)
            {
                // Get the Button component attached to this GameObject
                Button button = GetComponent<Button>();

                if (button != null)
                {
                    // Add the SwitchToScene function to the button's onClick event
                    button.onClick.AddListener(() => sceneLoader.SwitchToScene(targetSceneName));
                }
                else
                {
                    Debug.LogError("No Button component found on this GameObject.");
                }
            }
            else
            {
                Debug.LogError("No SceneLoader component found on the SceneManager GameObject.");
            }
        }
        else
        {
            Debug.LogError("No GameObject found with the 'SceneManager' tag.");
        }
    }
}