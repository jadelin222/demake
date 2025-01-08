using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    public Image mask;
    public float fadeDuration = 1f;

    private void Awake()
    {
            Instance = this;
    }

    public void FadeIn(System.Action onComplete = null)
    {
        StartCoroutine(Fade(1, onComplete));
    }

    public void FadeOut(System.Action onComplete = null)
    {
        StartCoroutine(Fade(0, onComplete));
    }

    private IEnumerator Fade(float targetAlpha, System.Action onComplete)
    {
        //mask.gameObject.SetActive(true);
        float startAlpha = mask.color.a;
        float timePassed = 0f;
        while (timePassed < fadeDuration)
        {
            timePassed += Time.deltaTime;
            float lerpA = Mathf.Lerp(startAlpha, targetAlpha, timePassed / fadeDuration);

            mask.color = new Color(mask.color.r, mask.color.g, mask.color.b, lerpA);
            yield return null;
        }
        mask.color = new Color(mask.color.r, mask.color.g, mask.color.b, targetAlpha);
        onComplete?.Invoke();
    }
}
