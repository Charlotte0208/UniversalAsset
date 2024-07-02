using UnityEngine;
using System.Collections;

public class HideGameObjectOnStart : MonoBehaviour
{
    void Start()
    {
        // 在 Start 方法执行后的第一帧隐藏游戏对象
        StartCoroutine(HideAfterOneFrame());
    }

    IEnumerator HideAfterOneFrame()
    {
        // 等待当前帧结束
        yield return new WaitForEndOfFrame();

        // 隐藏游戏对象
        gameObject.SetActive(false);
    }
}
