using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class NextWaveButton : MonoBehaviour
{
    private Button button;
    private Image buttonImage;
    private TextMeshProUGUI text;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        buttonImage = button.GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void DisplayNextWaveButton()
    {
        buttonImage.enabled = true;
        text.enabled = true;
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        Color imageColor = buttonImage.color;
        for (float alpha = 0f; alpha <= 1; alpha += 0.1f)
        {
            imageColor.a = alpha;

            buttonImage.color = imageColor;
            yield return new WaitForSeconds(.2f);
        }

        imageColor.a = 1;

        buttonImage.color = imageColor;
        button.enabled = true;
    }

    public void HideDisplay()
    {
        buttonImage.enabled = false;
        button.enabled = false;
        text.enabled = false;
    }
}
