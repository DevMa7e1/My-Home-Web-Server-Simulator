using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoDeviceDialogueScript : MonoBehaviour
{
    public GameObject ESP01;
    public TMP_Text Text;

    public double time = 0;

    string[] texts = { "I've enjoyed working with you.", "I am happy to see that you have reached your goal of one billion!", "But everything has an end, right?", "Well, bye I guess..." };
    int textIndex;

    public Sprite off;
    public Sprite on;
    
    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time < 7 && time > 6.8 && ESP01.GetComponent<Image>().sprite != on)
        {
            ESP01.GetComponent<Image>().sprite = on;
        }
        if(time >= 7 && textIndex < 4)
        {
            ESP01.GetComponent <Image>().sprite = off;
            Text.text = texts[textIndex];
            textIndex++;
            time = 0;
        }
        if(time >= 15)
        {
            Application.Quit();
        }
    }
}
