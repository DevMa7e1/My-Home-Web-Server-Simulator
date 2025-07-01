using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public TMP_Text dialogueText;

    string[] texts = { "But..." , "Do you remember how you got here?", "A small, cheap, little ESP-01.", "Now, would you like...", "before finishing this for good...", "to give it a final goodbye?", "Do you happen to have an ESP-01", "or any of the following devices?", "If yes, click on one of the devices!", "If not, just click on the PC."};
    string codepi = "String decode(String encoded) {\r\n  String decoded = \"\";\r\n  for (int i = 0; i < encoded.length(); i++) {\r\n    decoded += char(encoded[i] - 1);\r\n  }\r\n  return decoded;\r\n}\r\n\r\nvoid setup() {\r\n  Serial1.begin(9600);\r\n  delay(2000);\r\n  Serial1.println(decode(\"J(wf!fokpzfe!xpsljoh!xjui!zpv/\"));\r\n  delay(5000);\r\n  Serial1.println(decode(\"J!bn!ibqqz!up!tff!uibu!zpv!ibwf!sfbdife!zpvs!hpbm!pg!pof!cjmmjpo\\\"\"));\r\n  delay(5000);\r\n  Serial1.println(decode(\"Cvu!fwfszuijoh!ibt!bo!foe-!sjhiu@\"));\r\n  delay(10000);\r\n  Serial1.println(decode(\"Xfmm-!czf!J!hvftt///\"));\r\n}\r\n\r\nvoid loop(){}\r\n";
    string codeee = "String decode(String encoded) {\r\n  String decoded = \"\";\r\n  for (int i = 0; i < encoded.length(); i++) {\r\n    decoded += char(encoded[i] - 1);\r\n  }\r\n  return decoded;\r\n}\r\n\r\nvoid setup() {\r\n  Serial.begin(9600);\r\n  delay(2000);\r\n  Serial.println(decode(\"J(wf!fokpzfe!xpsljoh!xjui!zpv/\"));\r\n  delay(5000);\r\n  Serial.println(decode(\"J!bn!ibqqz!up!tff!uibu!zpv!ibwf!sfbdife!zpvs!hpbm!pg!pof!cjmmjpo\\\"\"));\r\n  delay(5000);\r\n  Serial.println(decode(\"Cvu!fwfszuijoh!ibt!bo!foe-!sjhiu@\"));\r\n  delay(10000);\r\n  Serial.println(decode(\"Xfmm-!czf!J!hvftt///\"));\r\n}\r\n\r\nvoid loop(){}\r\n";
    public int textIndex = 0;
    public double fadeTimer = 0;
    public double delayTimer = 0;

    public bool fadeInProgress = false;
    public bool delayInProgress = true;

    Color startColor;
    Color middleColor;

    public TMP_InputField CodeText;
    public GameObject CodeField;

    public Button ESP01;
    public Button ESP32;
    public Button RPiPw;
    public Button PC;

    void Start()
    {
        startColor = dialogueText.color;
        middleColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        CodeField.SetActive(false);
        ESP01.onClick.AddListener(ESPAction);
        ESP32.onClick.AddListener(ESPAction);
        RPiPw.onClick.AddListener(RPiAction);
    }

    void ESPAction()
    {
        CodeText.text = codeee;
        dialogueText.text = "Upload this code to your ESP board and then open the serial monitor at 9600 baud! After you're done, you can close the game.";
    }
    void RPiAction()
    {
        CodeText.text = codepi;
        dialogueText.text = "Upload this code to your Raspberry Pi Pico or Pico W board and then open the serial monitor at 9600 baud! After you're done, you can close the game.";
    }

    void PCAction()
    {
        SceneManager.LoadScene("NoDeviceEndScene");
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
            if (textIndex >= texts.Count() - 1)
            {
                fadeInProgress = false;
                CodeField.SetActive(true);
                dialogueText.text = "";
                dialogueText.fontSize = 70;
                PC.onClick.AddListener(PCAction);
            }
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
