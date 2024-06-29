using System.Collections;
using TMPro;
using UnityEngine;

public class TextFader : MonoBehaviour
{
    public TMP_Text textComponent;
    public GameObject objectToHide;
    public GameObject objectToShow;
    public float fadeDuration = 1.0f;
    public float visibleDuration = 2.0f;
    public float waitBeforeFadeIn = 0.0f; // 新增的变量，默认为0秒

    public void FadeInAndOut()
    {
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        // 确保文本初始时完全透明
        SetAlpha(0f);

        // 等待淡入前的时间
        yield return new WaitForSeconds(waitBeforeFadeIn);

        // 淡入
        yield return StartCoroutine(FadeTo(1f, fadeDuration));

        // 等待指定的可见时间
        yield return new WaitForSeconds(visibleDuration);

        // 淡出
        yield return StartCoroutine(FadeTo(0f, fadeDuration));

        // 隐藏指定对象
        if (objectToHide != null)
        {
            objectToHide.SetActive(false);
        }
        
        // 显示指定对象
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = textComponent.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            SetAlpha(newAlpha);
            yield return null;
        }

        SetAlpha(targetAlpha);
    }

    private void SetAlpha(float alpha)
    {
        Color color = textComponent.color;
        color.a = alpha;
        textComponent.color = color;
    }
}