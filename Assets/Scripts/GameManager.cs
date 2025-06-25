using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public double visitorsPerSecond = 0.0;
    public double maxVisitorsPerSecond = 0.0;

    public double currentRevenue = 0.0;
    public double adRevenuePerVisitor = 0.01;
    
    private string[] names = {"ESP-01", "Raspberry Pi Pico W", "ESP-32",
        "Raspberry Pi Zero W", "Raspberry Pi 3", "Raspberry Pi 4",
        "Raspberry Pi 5", "Dell Optiplex 7050"};
    private int[] maxes = { 5, 15, 20, 50, 100, 200, 450, 1000 };
    private int[] prices = { 15, 35, 40, 70, 170, 300, 500, 900 };

    public List<int> DeviceIndexes = new();

    public TMP_Text MoneyText;
    public TMP_Text DevicesText;

    public Button b0;
    public Button b1;
    public Button b2;
    public Button b3;
    public Button b4;
    public Button b5;
    public Button b6;
    public Button b7;

    public List<Button> buttons = new List<Button>();

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
    public void UpdateDevices()
    {
        DevicesText.text = "Devices: " + GetHumanReadableDeivceNames(DeviceIndexes, names);
        maxVisitorsPerSecond = CalcMaxVPS(DeviceIndexes, maxes);
    }
    void Start()
    {

        UpdateDevices();
        buttons.Add(b0);
        buttons.Add(b1);
        buttons.Add(b2);
        buttons.Add(b3);
        buttons.Add(b4);
        buttons.Add(b5);
        buttons.Add(b6);
        buttons.Add(b7);
        //--Example values--
        //--Remove later!!--
        visitorsPerSecond = 20f;
        DeviceIndexes.Add(0);
        DeviceIndexes.Add(1);
        UpdateDevices();
    }

    void Update()
    {
        double effectiveVisitors = System.Math.Min(visitorsPerSecond, maxVisitorsPerSecond);
        currentRevenue += effectiveVisitors * adRevenuePerVisitor * Time.deltaTime;
        MoneyText.text = "Money: " + currentRevenue.ToString("F2");
        displayButtons(currentRevenue, buttons);
    }
}
