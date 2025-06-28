using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public RectTransform Canvas;

    public double visitorsPerSecond = 0.0;
    public double maxVisitorsPerSecond = 0.0;

    public double currentRevenue = 0.0;
    public double adRevenuePerVisitor = 0.05;
    
    private string[] names = {"ESP-01", "Raspberry Pi Pico W", "ESP-32",
        "Raspberry Pi Zero W", "Raspberry Pi 3", "Raspberry Pi 4",
        "Raspberry Pi 5", "Dell Optiplex 7050"};
    private int[] maxes = { 5, 15, 20, 50, 100, 200, 450, 1000 };
    private int[] prices = { 15, 35, 40, 70, 170, 300, 500, 900 };

    private string[] upgrades = { "Index your website on search engines!",
                                  "Write new content!",
                                  "Get an HTTPS certificate!",
                                  "Make posts on social media!",
                                  "Start posting tutorials!",
                                  "Write about new topics!",
                                  "Explore recent events!",
                                  "Post about a debated subject!"};
    private string[] upgMessg = { "Search engines do not tipically pick up on new websites imediately. To make a search engine index your site, post a link on some other, already indexed site (like social media).",
                                  "New content will bring new visitors.",
                                  "An HTTPS certificate is free, greatly improves security and helps a lot with SEO.",
                                  "With social media, you can attract many new people to your website without needing investing in ads.", 
                                  "Most of the time, tutorials can stay relevant for longer periods of time compared to articles.",
                                  "New topics bring a new group of people that may have not been interested in your blog before.",
                                  "Recent events will usually bring a temporary, but big boost in visitors.",
                                  "Debated subjects will usually bring a large group of people to your site. Just make sure that you do a lot of research before to not acidentally spread missinformation!"};
    private int[] upgPrices = { 1,  5,  0, 50,  200, 800, 1500, 3000};
    private int[] upgGains =  { 4, 10, 20, 30,   70, 130,  250, 1000};

    public int currentUpgIndx = 0;

    public List<int> DeviceIndexes = new();

    public TMP_Text MoneyText;

    public Button b0;
    public Button b1;
    public Button b2;
    public Button b3;
    public Button b4;
    public Button b5;
    public Button b6;
    public Button b7;

    public List<Button> buttons = new List<Button>();

    public TMP_Text UpgradeText;
    public Button UpgradeButton;
    public TMP_Text OverloadText;

    public GameObject MsgPanel;
    public Button AnOkButton;
    public TMP_Text MsgText;

    public GameObject devicePrefab;
    public Sprite[] deviceSprites;

    public GameObject AdPanel;

    void displayButtons(double money, List<Button> buttons)
    {
        foreach (Button button in buttons)
        {
            button.enabled = false;
            button.gameObject.SetActive(false);
        }
        for(int i = 0; i < buttons.Count; i++)
        {
            if (prices[i] <= money)
            {
                buttons[i].enabled = true;
                buttons[i].gameObject.SetActive(true);
            }
        }
    }
    public void UpdateUpgrade()
    {
        UpgradeText.text = upgrades[currentUpgIndx] + "\nCost: " + upgPrices[currentUpgIndx].ToString() + "\nGain: +" + upgGains[currentUpgIndx] + " visitors per second";
    }
    double CalcMaxVPS(List<int> devices, int[] maxes)
    {
        double max = 0;
        for(int i = 0; i < devices.Count(); i++)
        {
            max += maxes[devices[i]];
        }
        return max;
    }
    string GetHumanReadableDeivceNames(List<int> devices, string[] names)
    {
        string name = "";
        foreach(int device in devices)
        {
            name += names[device] + ", ";
        }
        try
        {
            name = name.Remove(name.Length - 2);
        }
        catch
        {
            ;
        }
        return name;
    }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    List<GameObject> spawnedImages = new List<GameObject>();
    public void drawDevices()
    {
        foreach (GameObject imgGO in spawnedImages)
        {
            if (imgGO != null) // Check if the object still exists before destroying
            {
                Destroy(imgGO);
            }
        }
        spawnedImages.Clear();
        Vector3[] worldCorners = new Vector3[4];
        Canvas.GetWorldCorners(worldCorners);
        float minX = worldCorners[0].x;
        float maxX = worldCorners[2].x;
        float minY = worldCorners[0].y;
        float maxY = worldCorners[2].y;
        foreach (int device in DeviceIndexes)
        {
            int x = Random.Range((int)minX, (int)maxX);
            int y = Random.Range((int)minY, (int)maxY);
            GameObject uiObject = Instantiate(devicePrefab, Canvas);
            uiObject.transform.position = new Vector3(x, y, 0);
            uiObject.name = "device_img";
            Image imgComponent = uiObject.GetComponent<Image>();
            imgComponent.sprite = deviceSprites[device];
            spawnedImages.Add(uiObject);
        }
    }
    public void UpdateDevices()
    {
        drawDevices();
        maxVisitorsPerSecond = CalcMaxVPS(DeviceIndexes, maxes);
        CheckOverload();
    }

    public void CheckOverload()
    {
        if(maxVisitorsPerSecond < visitorsPerSecond)
        {
            OverloadText.text = "Overload! Buy more devices!";
        }
        else
        {
            OverloadText.text = "";
        }
    }
    void UpgradeButtonTask()
    {
        
        if (upgPrices[currentUpgIndx] <= currentRevenue)
        {
            visitorsPerSecond += upgGains[currentUpgIndx];
            showMessageBox(upgMessg[currentUpgIndx]);
            currentRevenue -= upgPrices[currentUpgIndx];
            CheckOverload();
            currentUpgIndx++;
            if(currentUpgIndx-1 >= upgrades.Count()-1)
            {
                UpgradeButton.onClick.RemoveAllListeners();
                UpgradeText.text = "There are no more upgrades!";
            }
            else
                UpdateUpgrade();
        }
    }
    public double CalculateUpgradeEffectCombined()
    {
        double upgEffCmbnd = 0;
        for(int i = 0; i < currentUpgIndx; i++)
        {
            upgEffCmbnd += upgGains[i];
        }
        return upgEffCmbnd + 1;
    }

    public void showMessageBox(string text)
    {
        MsgPanel.SetActive(true);
        MsgText.text = text;
    }
    void hideMessageBox()
    {
        MsgPanel.gameObject.SetActive(false);
    }

    void Start()
    {
        UpgradeButton.onClick.AddListener(UpgradeButtonTask);
        AnOkButton.onClick.AddListener(hideMessageBox);
        AdPanel.SetActive(false);
        hideMessageBox();
        UpdateDevices();
        UpdateUpgrade();
        buttons.Add(b0);
        buttons.Add(b1);
        buttons.Add(b2);
        buttons.Add(b3);
        buttons.Add(b4);
        buttons.Add(b5);
        buttons.Add(b6);
        buttons.Add(b7);
        //Sike!
        visitorsPerSecond = 1f;
        DeviceIndexes.Add(0);
        UpdateDevices();
    }

    public bool doIt = false;

    void Update()
    {
        if (doIt)
        {
            drawDevices();
            doIt = false;
        }
        if (currentRevenue >= 10000)
        {
            AdPanel.SetActive(true);
        }
        double effectiveVisitors = System.Math.Min(visitorsPerSecond, maxVisitorsPerSecond);
        currentRevenue += effectiveVisitors * adRevenuePerVisitor * Time.deltaTime;
        if(visitorsPerSecond <= 20)
            MoneyText.text = "Money: " + currentRevenue.ToString("F2");
        else
            MoneyText.text = "Money: " + currentRevenue.ToString("F0");
        displayButtons(currentRevenue, buttons);
    }
}
