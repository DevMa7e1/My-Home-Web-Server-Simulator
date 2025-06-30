using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public TMP_Text dialogueText;

    string[] texts = { "But..." , "Do you remember how you got here?", "A small, cheap, little ESP-01.", "Now, would you like...", "before finishing this for good...", "to give it a final goodbye?", "Do you happen to have an ESP-01", "or any of the following devices?", "Hmm? (click to select a device)"};
    public int textIndex = 0;
    public double fadeTimer = 0;
    public double delayTimer = 0;

    public bool fadeInProgress = false;
    public bool delayInProgress = true;

    Color startColor;
    Color middleColor;

    string[] deviceNames = {"ESP-01 (ESP8266)", "ESP32", "Raspberry Pi Pico W", "Raspberry Pi Zero/1/2/3/4/5 or a PC"};

    void Start()
    {
        startColor = dialogueText.color;
        middleColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }

    void Update()
    {
        if (delayInProgress) delayTimer += Time.deltaTime;
        else delayTimer = 0;
        if (fadeInProgress) fadeTimer += Time.deltaTime;
        else fadeTimer = 0;

        if (delayInProgress && delayTimer >= 7)
        {
            delayInProgress = false;
            fadeInProgress = true;
            if(textIndex >= texts.Count()-1)
                fadeInProgress = false;
            else
                textIndex++;
        }

        if (fadeInProgress && fadeTimer < 5)
        {
            float t = Mathf.Clamp01((float)fadeTimer / 5);
            dialogueText.color = Color.Lerp(startColor, middleColor, t);
        }
        if(fadeInProgress && fadeTimer > 5 && fadeTimer < 10)
        {
            dialogueText.text = texts[textIndex];
            float t = Mathf.Clamp01((float)(fadeTimer-5) / 5);
            dialogueText.color = Color.Lerp(middleColor, startColor, t);
        }
        if(fadeInProgress && fadeTimer >= 10)
        {
            fadeInProgress = false;
            delayInProgress = true;
        }
    }
}
