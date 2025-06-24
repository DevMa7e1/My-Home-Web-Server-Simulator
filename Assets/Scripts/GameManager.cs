using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

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

    List<int> DeviceIndexes = new();

    public TMP_Text MoneyText;
    public TMP_Text DevicesText;
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
        name = name.Remove(name.Length - 2);
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
    void Start()
    {
        //--Example values--
        //--Remove later!!--
        visitorsPerSecond = 3f;
        DeviceIndexes.Add(0);
        DeviceIndexes.Add(1);
        maxVisitorsPerSecond = CalcMaxVPS(DeviceIndexes, maxes);
        DevicesText.text = "Devices: " + GetHumanReadableDeivceNames(DeviceIndexes, names);
    }

    void Update()
    {
        double effectiveVisitors = System.Math.Min(visitorsPerSecond, maxVisitorsPerSecond);
        currentRevenue += effectiveVisitors * adRevenuePerVisitor * Time.deltaTime;
        MoneyText.text = "Money: " + currentRevenue.ToString("F2");
    }
}
