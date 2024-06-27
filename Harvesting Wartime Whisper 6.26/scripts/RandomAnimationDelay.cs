using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationDelay : MonoBehaviour
{
    public float minDelay = 0.1f;
    public float maxDelay = 2.0f;

    void Start()
    {
        // 获取场景中所有带有 Animator 组件的对象
        Animator[] animators = FindObjectsOfType<Animator>();

        // 为每个 Animator 添加一个随机延迟
        foreach (Animator animator in animators)
        {
            StartCoroutine(DelayAnimation(animator));
        }
    }

    IEnumerator DelayAnimation(Animator animator)
    {
        // 随机生成一个延迟时间
        float delay = Random.Range(minDelay, maxDelay);

        // 关闭 Animator
        animator.enabled = false;

        // 等待延迟时间
        yield return new WaitForSeconds(delay);

        // 启动 Animator
        animator.enabled = true;
    }
}
