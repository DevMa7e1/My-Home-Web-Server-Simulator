using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PanelFadeOut : MonoBehaviour
{
    public Image WelPanel;
    public TMP_Text WelText;
    public GameObject MainPanel;

    [Range(0.1f, 10.0f)]
    public float fadeDuration = 2.0f;

    void Start()
    {
        WelText.gameObject.SetActive(false);
        MainPanel.SetActive(false);
        StartFade();
    }
    public void StartFade()
    {
        StopAllCoroutines();
        StartCoroutine(FadePanelCoroutine());
    }
    private IEnumerator FadePanelCoroutine()
    {
        Color startColor = WelPanel.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            if(elapsedTime / fadeDuration >= .5)
                WelText.gameObject.SetActive(true);
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            WelPanel.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        WelPanel.color = endColor;
        WelText.gameObject.SetActive(false);
        MainPanel.gameObject.SetActive(true);
    }
}
