using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

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

    string[] upgrades = {"Diversify your content!", "Allow for users to create their own content!", "Make a site-wide forum!",
        "Hire moderators and other people to take care of the website!", "Sponsor other people's blogs and content!"};
    string[] upgMessg = {"By diversifing your content you can reach a wider audience.",
        "Allowing users to create their own content means that the site now basically runs itself and official new content is not necessary anymore",
        "By making a site wide forum, you can take advantage of the fact that old problems repeat and that forums are generally preferred for discussing problems compared to an article.",
        "Hireing moderators can greately improve a websites reputation and help clean out the posts that offer no value to the site.",
        "By sponsoring other people's content you are reserving, usually, a permanent ad in content that may be viewed thousands or millions of times."};
    int[] upgCosts = { 10000, 100000, 500000, 900000, 1500000};
    int[] upgGains = { 100000, 300000, 700000, 1000000, 2500000};
    int upgIndex = 0;
    int upgGainsTotal = 0;
    public Button upgButton;
    public TMP_Text upgText;

    public GameObject messageBox;
    public TMP_Text messageText;
    public Button messageButton;

    void Start()
    {
        AdButton.onClick.AddListener(AdButtonAction);
        PayButton.onClick.AddListener(PayButtonAction);
        FixButton.onClick.AddListener(FixButtonAction);
        timeTillNextIssue = UnityEngine.Random.Range(10, 30) + 10;
        FixButton.gameObject.SetActive(false);
        Loading.SetActive(false);
        upgButton.onClick.AddListener(UpgradeButtonAction);
        messageBox.SetActive(false);
        messageButton.onClick.AddListener(messageBoxHide);
        upgText.text = upgrades[upgIndex] + "\nCost: " + upgCosts[upgIndex].ToString() + "\nGain: " + upgGains[upgIndex].ToString() + " visitors per second";
    }

    void messageBoxShow(string text)
    {
        messageBox.SetActive(true);
        messageText.text = text;
    }
    void messageBoxHide()
    {
        messageBox.SetActive(false);
    }

    void UpgradeButtonAction()
    {
        if(currentRevenue >= upgCosts[upgIndex])
        {
            upgGainsTotal += upgGains[upgIndex];
            visitorsPerSecond += upgGains[upgIndex];
            currentRevenue -= upgCosts[upgIndex];
            messageBoxShow(upgMessg[upgIndex]);
            upgIndex++;
            upgText.text = upgrades[upgIndex] + "\nCost: " + upgCosts[upgIndex].ToString() + "\nGain: " + upgGains[upgIndex].ToString() + " visitors per second";
        }
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
        visitorsPerSecond = upgGainsTotal;
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
        if(currentRevenue >= 1000000000)
        {
            SceneManager.LoadScene("FinishScene");
        }
    }
}
