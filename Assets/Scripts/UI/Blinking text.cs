using System.Collections;
using UnityEngine;
using TMPro;

public class TextBlink : MonoBehaviour
{
    TextMeshProUGUI text;
    CanvasGroup canvasGroup;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        StartBlinking();
    }

    IEnumerator Blink()
    {
        while (true)
        {
            StartCoroutine(FadeText(1f, 0f, 1f));//fade in
            yield return new WaitForSeconds(1f);
            StartCoroutine(FadeText(0f, 1f, 1f));//fade out
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator FadeText(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }

    void StartBlinking()
    {
        StartCoroutine(Blink());
    }
    void StopBlinking()
    {
        StopAllCoroutines();
    }
}
