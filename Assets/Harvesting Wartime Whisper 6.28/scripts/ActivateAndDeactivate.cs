using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAndDeactivate : MonoBehaviour
{
    // 公共变量，允许在Inspector中设置物体列表
    public List<GameObject> objectsToActivate;

    // 设置等待的帧数
    public int waitFrames = 1;

    private void Start()
    {
        // 激活列表中的每个物体
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
            // 启动协程来处理物体的非激活
            StartCoroutine(DeactivateAfterFrames(obj));
        }
    }

    private IEnumerator DeactivateAfterFrames(GameObject obj)
    {
        // 等待指定的帧数
        for (int i = 0; i < waitFrames; i++)
        {
            yield return null; // 等待下一帧
        }

        // 将物体设置为非激活状态
        obj.SetActive(false);
    }
}