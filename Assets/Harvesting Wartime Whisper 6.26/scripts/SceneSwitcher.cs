using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToLoad; // 要加载的场景名称
    public float blackoutDuration = 0.5f; // 黑屏持续时间

    private Camera mainCamera;
    private Color originalColor;

    void Start()
    {
        mainCamera = Camera.main;
        originalColor = mainCamera.backgroundColor;
    }

    public void SwitchScene()
    {
        StartCoroutine(SwitchSceneCoroutine());
    }

    private IEnumerator SwitchSceneCoroutine()
    {
        // 黑屏
        mainCamera.backgroundColor = Color.black;
        yield return new WaitForSeconds(blackoutDuration);

        // 加载新场景
        SceneManager.LoadScene(sceneToLoad);

        // 恢复原来的颜色
        mainCamera.backgroundColor = originalColor;
    }
}