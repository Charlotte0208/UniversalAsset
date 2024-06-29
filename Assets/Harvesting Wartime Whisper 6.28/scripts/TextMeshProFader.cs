using System.Collections;
using UnityEngine;
using TMPro;

public class TextMeshProFader : MonoBehaviour
{
    public TextMeshProUGUI[] textMeshPros;
    public float fadeInTime = 1.0f;
    public float displayTime = 2.0f;
    public float fadeOutTime = 1.0f;
    public GameObject objectToShowOnFinish; // Assign this in the Inspector

    private void OnEnable()
    {
        StartCoroutine(FadeTextMeshProObjects());
    }

    private IEnumerator FadeTextMeshProObjects()
    {
        for (int i = 0; i < textMeshPros.Length; i++)
        {
            yield return StartCoroutine(FadeInText(textMeshPros[i], fadeInTime));
            yield return new WaitForSeconds(displayTime);

            if (i < textMeshPros.Length - 1)
            {
                yield return StartCoroutine(FadeOutText(textMeshPros[i], fadeOutTime));
            }
            else
            {
                // Last textMeshPro, start fading out and activate the specified object
                yield return StartCoroutine(FadeOutText(textMeshPros[i], fadeOutTime));
                if (objectToShowOnFinish != null)
                {
                    objectToShowOnFinish.SetActive(true);
                }
            }
        }
    }

    private IEnumerator FadeInText(TextMeshProUGUI textMeshPro, float duration)
    {
        float elapsedTime = 0f;
        Color color = textMeshPro.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / duration);
            textMeshPro.color = color;
            yield return null;
        }

        color.a = 1f;
        textMeshPro.color = color;
    }

    private IEnumerator FadeOutText(TextMeshProUGUI textMeshPro, float duration)
    {
        float elapsedTime = 0f;
        Color color = textMeshPro.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / duration));
            textMeshPro.color = color;
            yield return null;
        }

        color.a = 0f;
        textMeshPro.color = color;
    }
}
