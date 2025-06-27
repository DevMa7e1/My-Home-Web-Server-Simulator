using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdsScript : MonoBehaviour
{
    public Button AdButton;
    public TMP_Text AdText;
    public TMP_Text AdBtnText;

    public double beforeVPS;

    int adNo = 0;

    public double adEffectSeconds;
    public bool adsInAction = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public static AdsScript Instance { get; private set; }

    void Start()
    {
        AdButton.onClick.AddListener(Click);
    }

    public string padTimeString(string timeString)
    {
        if(timeString.Length == 1)
        {
            return "0" + timeString;
        }
        else
        {
            return timeString;
        }
    }

    void UpdatePrice()
    {
        AdBtnText.text = "Buy ads! Price: " + 10*Math.Pow(2, adNo);
    }

    void Click()
    {
        if (GameManager.Instance.currentRevenue >= 10 * Math.Pow(2, adNo))
        {
            adEffectSeconds += 61;
            GameManager.Instance.visitorsPerSecond += Math.Ceiling(GameManager.Instance.maxVisitorsPerSecond / (2*(adNo+1)));
            GameManager.Instance.currentRevenue -= 10 * Math.Pow(2,adNo);
            adNo++;
            UpdatePrice();
            adsInAction = true;
            GameManager.Instance.CheckOverload();
        }
    }

    void Update()
    {
        if(adEffectSeconds > 0)
            adEffectSeconds -= Time.deltaTime;
        if(adEffectSeconds < 0)
        {
            adEffectSeconds = 0;
        }
        AdText.text = padTimeString(Math.Floor(adEffectSeconds / 60).ToString()) + ":" + padTimeString(Math.Round(adEffectSeconds - Math.Floor(adEffectSeconds / 60)*60).ToString());
        if (adEffectSeconds <= 0 && adsInAction)
        {
            adNo = 0;
            GameManager.Instance.visitorsPerSecond = GameManager.Instance.CalculateUpgradeEffectCombined();
            UpdatePrice();
            GameManager.Instance.CheckOverload();
            adsInAction = false;
        }
    }
}
