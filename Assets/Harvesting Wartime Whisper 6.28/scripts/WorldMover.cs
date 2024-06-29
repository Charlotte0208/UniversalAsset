using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class WorldMover : MonoBehaviour
{
    public float moveDuration = 5.0f; // 持续时间
    public AudioClip moveAudioClip; // 音频剪辑
    public string nextSceneName; // 下一个场景的名称
    private AudioSource audioSource;
    private GameObject worldObject;
    private Camera mainCamera;
    private Color originalBackgroundColor;
    private bool isMoving = false;
    private List<AudioSource> allAudioSources = new List<AudioSource>();

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = moveAudioClip;
        mainCamera = Camera.main;
        originalBackgroundColor = mainCamera.backgroundColor;
        worldObject = GameObject.FindWithTag("world");

        if (worldObject == null)
        {
            Debug.LogError("No GameObject found with tag 'world'");
        }

        // 获取场景中所有的 AudioSource 组件
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        allAudioSources.AddRange(audioSources);
    }

    public void MoveWorld()
    {
        // 检查是否已经在播放音频
        if (audioSource.isPlaying) return;
        if (isMoving) return;
        isMoving = true;

        StartCoroutine(MoveWorldCoroutine());
    }

    private IEnumerator MoveWorldCoroutine()
    {
        // 开始播放音频
        audioSource.Play();

        // 暂停场景内所有其他的音频
        foreach (AudioSource source in allAudioSources)
        {
            if (source != audioSource) // 排除当前移动音频源
            {
                source.Pause();
            }
        }

        // 初始化移动时间
        float elapsedTime = 0f;
        Vector3 originalPosition = worldObject.transform.position;

        while (elapsedTime < moveDuration)
        {
            float randomX = Random.Range(0.01f, 0.1f) * (Random.value > 0.5f ? 1 : -1);
            float randomY = Random.Range(0.01f, 0.1f) * (Random.value > 0.5f ? 1 : -1);
            float randomZ = Random.Range(0.01f, 0.1f) * (Random.value > 0.5f ? 1 : -1);

            Vector3 newPosition = worldObject.transform.position + new Vector3(randomX, randomY, randomZ);

            // 确保总偏移不超过0.2米
            if (Vector3.Distance(originalPosition, newPosition) <= 0.2f)
            {
                worldObject.transform.position = newPosition;
            }

            elapsedTime += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        // 停止音频播放
        audioSource.Stop();

        // 黑屏效果并让 world 对象消失
        StartCoroutine(BlackScreenAndSwitchScene());
    }

    private IEnumerator BlackScreenAndSwitchScene()
    {
        mainCamera.backgroundColor = Color.black;
        worldObject.SetActive(false); // 让 world 对象消失
        yield return new WaitForSeconds(0.5f);

        // 加载下一个场景
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);

        // 等待新场景加载完成后恢复相机背景颜色
        yield return new WaitForSeconds(0.1f); // 等待一帧以确保场景切换完成

        mainCamera = Camera.main; // 重新获取相机引用，因为场景切换后相机会变化
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = originalBackgroundColor;
        }
    }
}
