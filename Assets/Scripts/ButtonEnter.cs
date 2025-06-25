using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEnter : MonoBehaviour, IPointerEnterHandler
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
    public TMP_Text TMP_Text;
    public ButtonType myButtonType; // Set this in the Inspector for each button

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Pointer entered: {gameObject.name} (Type: {myButtonType})");

        // Perform actions specific to each button type when hovered
        switch (myButtonType)
        {
            case ButtonType.ButtonA:
                TMP_Text.text = "MAX VISITORS: 5\nCOST: 15";
                break;
            case ButtonType.ButtonB:
                TMP_Text.text = "MAX VISITORS: 15\nCOST: 35";
                break;
            case ButtonType.ButtonC:
                TMP_Text.text = "MAX VISITORS: 20\nCOST: 40";
                break;
            case ButtonType.ButtonD:
                TMP_Text.text = "MAX VISITORS: 50\nCOST: 70";
                break;
            case ButtonType.ButtonE:
                TMP_Text.text = "MAX VISITORS: 100\nCOST: 170";
                break;
            case ButtonType.ButtonF:
                TMP_Text.text = "MAX VISITORS: 200\nCOST: 300";
                break;
            case ButtonType.ButtonG:
                TMP_Text.text = "MAX VISITORS: 450\nCOST: 500";
                break;
            case ButtonType.ButtonH:
                TMP_Text.text = "MAX VISITORS: 1000\nCOST: 900";
                break;
            default:
                Debug.LogError("UNDEFINED HOVER ACTION");
                break;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
