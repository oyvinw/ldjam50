using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    private Button restartButton;
    private Image buttonImage;

    // Start is called before the first frame update
    void Start()
    {
        restartButton = GetComponentInChildren<Button>();
        buttonImage = restartButton.GetComponent<Image>();
    }

    public void DisplayLose()
    {
        text.enabled = true;
        buttonImage.enabled = true;
        restartButton.enabled = true;
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        Color imageColor = buttonImage.color;
        Color textColor = text.color;
        for (float alpha = 0f; alpha <= 1; alpha += 0.1f)
        {
            imageColor.a = alpha;
            textColor.a = alpha;

            buttonImage.color = imageColor;
            text.color = textColor;
            yield return new WaitForSeconds(.2f);
        }

        imageColor.a = 1;
        textColor.a = 1;

        buttonImage.color = imageColor;
        text.color = textColor;

        restartButton.enabled = true;
    }

    public void HideDisplay()
    {
        text.enabled = false;
        buttonImage.enabled = false;
        restartButton.enabled = false;
    }

    /*
    private IEnumerator FadeOut()
    {
        Color color = display.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            color.a = alpha;
            display.color = color;
            yield return new WaitForSeconds(.2f);
        }

        color.a = 0;
        display.color = color;
    }
    */
}
