using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    private GameObject cameraObject;
    private Vector3 initialCameraPosition;
    private Vector3 initialCameraRotation;
    private GameObject worldObject;
    private Vector3 positionOffset;
    private Vector3 rotationOffset;

    public void SwitchToScene(string sceneName)
    {
        // 保存相机位置和旋转
        SaveCameraPositionAndRotation();
        // 切换场景
        SceneManager.LoadScene(sceneName);
    }

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void SaveCameraPositionAndRotation()
    {
        cameraObject = Camera.main.gameObject;
        if (cameraObject != null)
        {
            initialCameraPosition = cameraObject.transform.position;
            initialCameraRotation = cameraObject.transform.eulerAngles;
            Debug.Log("old camera position: " + initialCameraPosition);
            Debug.Log("old camera rotation: " + initialCameraRotation);
        }
        else
        {
            Debug.LogError("Main Camera is not found.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 获取新的相机对象
        cameraObject = Camera.main.gameObject;
        if (cameraObject != null)
        {
            // 计算相机的位移和旋转变化
            positionOffset += cameraObject.transform.position - initialCameraPosition;
            rotationOffset += cameraObject.transform.eulerAngles - initialCameraRotation;

            Debug.Log("positionOffset: " + positionOffset);
            Debug.Log("rotationOffset: " + rotationOffset);

            // 获取新的world对象
            worldObject = GameObject.FindGameObjectWithTag("world");

            if (worldObject != null)
            {
                // 调整位置和旋转
                worldObject.transform.position += positionOffset;
                Vector3 newRotation = worldObject.transform.eulerAngles + rotationOffset;
                newRotation.z = 0f;
                worldObject.transform.eulerAngles = newRotation;

                // 启动协程隐藏world对象0.5秒
                StartCoroutine(HideWorldForDelay(0.5f));
            }
            else
            {
                Debug.LogError("No GameObject with tag 'world' found in the scene.");
            }
        }
        else
        {
            Debug.LogError("Main Camera is not found in the new scene.");
        }
    }

    private IEnumerator HideWorldForDelay(float delay)
    {
        // 隐藏world对象
        SetWorldVisibility(false);

        // 等待指定的延迟时间
        yield return new WaitForSeconds(delay);

        // 显示world对象
        SetWorldVisibility(true);
    }

    private void SetWorldVisibility(bool isVisible)
    {
        Renderer[] renderers = worldObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = isVisible;
        }
    }
}