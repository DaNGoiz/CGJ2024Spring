using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowClearUI : MonoBehaviour
{
    public Image firstImage; // 第一个 Image 组件
    public Image secondImage; // 第二个 Image 组件
    public Sprite firstSprite; // 第一个 Sprite
    public Sprite secondSprite; // 第二个 Sprite
    public Button buttonToShow; // 要显示的按钮

    void Start()
    {
        StartCoroutine(FadeColorsAndChangeSprite());
    }

    IEnumerator FadeColorsAndChangeSprite()
    {
        // 初始化
        firstImage.sprite = firstSprite;
        secondImage.sprite = secondSprite;
        secondImage.color = new Color(1f, 1f, 1f, 0f); // 第二个 Image 完全透明

        // 渐变到白色
        yield return FadeToColor(firstImage.color, Color.white, 2f, firstImage);

        // 渐变到饱和色
        yield return FadeToColor(Color.white, firstImage.color, 1f, firstImage);

        // 等待 5 秒
        yield return new WaitForSeconds(5f);

        // 交叉淡化到第二个 Sprite
        yield return CrossfadeSprites(3f);

        // 显示按钮
        buttonToShow.gameObject.SetActive(true);
    }

    IEnumerator FadeToColor(Color startColor, Color endColor, float duration, Image image)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            image.color = Color.Lerp(startColor, endColor, t / duration);
            yield return null;
        }
        image.color = endColor;
    }

    IEnumerator CrossfadeSprites(float duration)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = t / duration;
            firstImage.color = new Color(1f, 1f, 1f, 1f - alpha);
            secondImage.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }
        firstImage.color = new Color(1f, 1f, 1f, 0f);
        secondImage.color = new Color(1f, 1f, 1f, 1f);
    }
}
