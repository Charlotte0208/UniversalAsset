using System.Collections;
using UnityEngine;

public class WorldMoverAlternate : MonoBehaviour
{
    public string worldTag = "world";  // 世界物体的Tag
    public AudioSource audioSource;    // 音频播放器
    public AudioClip audioClip;        // 音频片段
    public GameObject airWall;         // 空气墙

    private GameObject worldObject;    // 世界物体
    private Vector3 originalPosition;  // 世界物体的初始坐标

    void Start()
    {
        // 查找带有worldTag的物体
        worldObject = GameObject.FindGameObjectWithTag(worldTag);

        if (worldObject != null)
        {
            // 记录初始坐标
            originalPosition = worldObject.transform.position;

            // 立即将y轴坐标变为-100米
            Vector3 newPosition = originalPosition;
            newPosition.y = -100f;
            worldObject.transform.position = newPosition;

            // 隐藏空气墙
            if (airWall != null)
            {
                airWall.SetActive(false);
            }

            // 播放音频
            if (audioSource != null && audioClip != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }

            // 启动协程
            StartCoroutine(MoveWorldObject());
        }
        else
        {
            Debug.LogError("未找到带有Tag为'" + worldTag + "'的物体");
        }
    }

    IEnumerator MoveWorldObject()
    {
        // 坐标不变5秒
        yield return new WaitForSeconds(5f);

        // 以每秒10米的速度匀速上升直到y轴坐标为0米
        while (worldObject.transform.position.y < originalPosition.y)
        {
            Vector3 position = worldObject.transform.position;
            position.y += 10f * Time.deltaTime;
            if (position.y > originalPosition.y)
            {
                position.y = originalPosition.y;
            }
            worldObject.transform.position = position;

            yield return null;
        }

        // 等待2秒
        yield return new WaitForSeconds(2f);

        // 显现空气墙
        if (airWall != null)
        {
            airWall.SetActive(true);
        }

        // 停止音频播放
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
