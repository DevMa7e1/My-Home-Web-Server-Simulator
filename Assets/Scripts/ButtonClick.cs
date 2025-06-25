using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour, IPointerUpHandler
{
    public enum ButtonType
    {
        None, // Default or unassigned
        ButtonA,
        ButtonB,
        ButtonC,
        ButtonD,
        ButtonE,
        ButtonF,
        ButtonG,
        ButtonH
    }
    public ButtonType type;
    public void OnPointerUp(PointerEventData a)
    {
        switch (type)
        {
            case ButtonType.ButtonA:
                GameManager.Instance.DeviceIndexes.Add(0);
                GameManager.Instance.currentRevenue -= 15.0;
                break;
            case ButtonType.ButtonB:
                GameManager.Instance.DeviceIndexes.Add(1);
                GameManager.Instance.currentRevenue -= 35.0;
                break;
            case ButtonType.ButtonC:
                GameManager.Instance.DeviceIndexes.Add(2);
                GameManager.Instance.currentRevenue -= 40.0;
                break;
            case ButtonType.ButtonD:
                GameManager.Instance.DeviceIndexes.Add(3);
                GameManager.Instance.currentRevenue -= 70.0;
                break;
            case ButtonType.ButtonE:
                GameManager.Instance.DeviceIndexes.Add(4);
                GameManager.Instance.currentRevenue -= 170.0;
                break;
            case ButtonType.ButtonF:
                GameManager.Instance.DeviceIndexes.Add(5);
                GameManager.Instance.currentRevenue -= 300.0;
                break;
            case ButtonType.ButtonG:
                GameManager.Instance.DeviceIndexes.Add(6);
                GameManager.Instance.currentRevenue -= 500.0;
                break;
            case ButtonType.ButtonH:
                GameManager.Instance.DeviceIndexes.Add(7);
                GameManager.Instance.currentRevenue -= 900.0;
                break;
        }
        GameManager.Instance.UpdateDevices();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
