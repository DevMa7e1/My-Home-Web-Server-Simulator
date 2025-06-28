using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CloudManager : MonoBehaviour
{
    double visitorsPerSecond = 0;
    double currentRevenue = 1000000;
    double cloudBill = 0;
    double cloudTimer = 0;
    double adRevenuePerVisitor = 0.05;

    long adButtonClickIndex = 0;

    public Button AdButton;
    public TMP_Text AdText;
    public Button PayButton;
    public TMP_Text PayText;
    public TMP_Text MoneyText;
    public TMP_Text VPSText;
    public TMP_Text CBText;

    void Start()
    {
        AdButton.onClick.AddListener(AdButtonAction);
        PayButton.onClick.AddListener(PayButtonAction);
    }

    void AdButtonAction()
    {
        if(currentRevenue >= Math.Pow(2, adButtonClickIndex) * 10000)
        {
            currentRevenue -= Math.Pow(2, adButtonClickIndex) * 10000;
            visitorsPerSecond += 100000/ Math.Pow(2, adButtonClickIndex);
            adButtonClickIndex++;
            AdText.text = "Buy ads! Price: " + Math.Pow(2, adButtonClickIndex) * 10000;
        }
    }
    
    void PayButtonAction()
    {
        currentRevenue -= cloudBill;
        cloudTimer = 0;
        cloudBill = 0;
        visitorsPerSecond = 0;
        adButtonClickIndex = 0;
        AdText.text = "Buy ads! Price: " + Math.Pow(2, adButtonClickIndex) * 10000;
    }

    public string padTimeString(string timeString)
    {
        if (timeString.Length == 1)
        {
            return "0" + timeString;
        }
        else
        {
            return timeString;
        }
    }

    void Update()
    {
        cloudTimer = cloudTimer + Time.deltaTime;
        cloudBill += cloudTimer/1300 * visitorsPerSecond * Time.deltaTime;
        currentRevenue += visitorsPerSecond * adRevenuePerVisitor * Time.deltaTime;
        if (cloudTimer >= 120) PayButtonAction();
        PayText.text = "PAY " + padTimeString(Math.Floor((120-cloudTimer) / 60).ToString()) + ":" + padTimeString(Math.Round((120-cloudTimer) - Math.Floor((120-cloudTimer) / 60) * 60).ToString());
        MoneyText.text = "Money: " + currentRevenue.ToString("F0");
        VPSText.text = "Visitors per second: " + visitorsPerSecond.ToString();
        CBText.text = "Cloud bill: " + cloudBill.ToString("F0");
    }
}
