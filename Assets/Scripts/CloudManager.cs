using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using UnityEditorInternal;

public class CloudManager : MonoBehaviour
{
    double visitorsPerSecond = 0;
    public double currentRevenue = 1000000;
    double cloudBill = 0;
    double cloudTimer = 0;
    double adRevenuePerVisitor = 0.1;

    public double fixTimer = 0;
    public double issueTimer = 0;
    string[] issues = { "Update server!", "Restart server!", "Restart service!", "Update software!" };
    public double timeTillNextIssue = 30;
    public Button FixButton;
    public TMP_Text FixText;
    public GameObject Loading;
    public GameObject CloudPanel;
    bool issueInProgress = false;
    bool fixInProgress = false;

    long adButtonClickIndex = 0;

    public Button AdButton;
    public TMP_Text AdText;
    public Button PayButton;
    public TMP_Text PayText;
    public TMP_Text MoneyText;
    public TMP_Text VPSText;
    public TMP_Text CBText;

    bool canMakeMoney = true;

    void Start()
    {
        AdButton.onClick.AddListener(AdButtonAction);
        PayButton.onClick.AddListener(PayButtonAction);
        FixButton.onClick.AddListener(FixButtonAction);
        timeTillNextIssue = UnityEngine.Random.Range(10, 30) + 10;
        FixButton.gameObject.SetActive(false);
        Loading.SetActive(false);
    }

    double FloorIfNotZero(double no)
    {
        if(no > 999999)
        {
            return Math.Floor(no/1000000);
        }
        return 1;
    }

    void AdButtonAction()
    {
        if(currentRevenue >= Math.Pow(2, adButtonClickIndex) * 10000 * FloorIfNotZero(currentRevenue))
        {
            
            visitorsPerSecond += 100000 * FloorIfNotZero(currentRevenue) / Math.Pow(2, adButtonClickIndex);
            adButtonClickIndex++;
            currentRevenue -= Math.Pow(2, adButtonClickIndex) * 10000 * FloorIfNotZero(currentRevenue);
            AdText.text = "Buy ads! Price: " + Math.Pow(2, adButtonClickIndex) * 10000 * FloorIfNotZero(currentRevenue);
        }
    }
    
    void PayButtonAction()
    {
        currentRevenue -= cloudBill;
        cloudTimer = 0;
        cloudBill = 0;
        visitorsPerSecond = 0;
        adButtonClickIndex = 0;
        AdText.text = "Buy ads! Price: " + Math.Pow(2, adButtonClickIndex) * 10000 * FloorIfNotZero(currentRevenue);
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

    void FixButtonAction()
    {
        timeTillNextIssue = UnityEngine.Random.Range(10, 30) + 15;
        fixTimer = 0;
        Loading.SetActive(true);
        FixButton.gameObject.SetActive(false);
        fixInProgress = true;
    }

    void Update()
    {
        if(canMakeMoney) cloudTimer += Time.deltaTime;
        fixTimer += Time.deltaTime;
        if (fixInProgress) issueTimer += Time.deltaTime;
        if (fixInProgress && issueTimer >= 15) { issueTimer = 0; issueInProgress = false;  fixInProgress = false; FixButton.gameObject.SetActive(false); CloudPanel.SetActive(true); Loading.SetActive(false); canMakeMoney = true; }
        if(timeTillNextIssue <= fixTimer && !issueInProgress) { 
            canMakeMoney = false;
            CloudPanel.SetActive(false);
            FixButton.gameObject.SetActive(true);
            FixText.text = issues[UnityEngine.Random.Range(0, 4)];
            issueInProgress = true;
        }
        if(canMakeMoney) cloudBill += cloudTimer/1300 * visitorsPerSecond * Time.deltaTime;
        if(canMakeMoney) currentRevenue += visitorsPerSecond * adRevenuePerVisitor * Time.deltaTime;
        if (cloudTimer >= 120) PayButtonAction();
        PayText.text = "PAY " + padTimeString(Math.Floor((120-cloudTimer) / 60).ToString()) + ":" + padTimeString(Math.Round((120-cloudTimer) - Math.Floor((120-cloudTimer) / 60) * 60).ToString());
        MoneyText.text = "Money: " + currentRevenue.ToString("F0");
        VPSText.text = "Visitors per second: " + visitorsPerSecond.ToString();
        CBText.text = "Cloud bill: " + cloudBill.ToString("F0");
    }
}
