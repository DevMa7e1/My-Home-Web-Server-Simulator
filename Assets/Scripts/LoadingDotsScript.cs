using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;

public class LoadingDotsScript : MonoBehaviour
{
    public Image Main;
    public Sprite[] frames;
    int fIndex = 0;
    void Start()
    {

    }

    

    void Update()
    {
        Main.sprite = frames[fIndex];
        fIndex = (fIndex + 1) % 30;
        Thread.Sleep(100);
    }
}
