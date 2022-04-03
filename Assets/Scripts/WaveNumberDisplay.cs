using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveNumberDisplay : MonoBehaviour
{
    private TextMeshProUGUI display;
    private Color textColor;
    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<TextMeshProUGUI>();
        textColor = display.color;
    }

    public void DisplayWave(int waveNum)
    {
        //TODO: Change this to display roman numeral if there is time
        ChangeDisplay($"Wave {waveNum}");
    }

    public void DisplayWave(string text)
    {
        ChangeDisplay(text);
    }

    private void ChangeDisplay(string text)
    {
        display.color = textColor;
        display.text = text;
        StartCoroutine(TextFade());
    }

    private IEnumerator TextFade()
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
}
