using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageFadeIn : MonoBehaviour
{
    public CanvasGroup panelCanvasGroup;
    public Button buttonToShow;
    public float fadeInDuration = 2f;
    public float buttonDelay = 2f;

    void Start()
    {
        StartCoroutine(FadeInPanel());
    }

    IEnumerator FadeInPanel()
    {
        buttonToShow.gameObject.SetActive(false);

        float elapsed = 0f;
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            panelCanvasGroup.alpha = elapsed / fadeInDuration;
            yield return null;
        }
        panelCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(buttonDelay);

        buttonToShow.gameObject.SetActive(true);
    }
}
