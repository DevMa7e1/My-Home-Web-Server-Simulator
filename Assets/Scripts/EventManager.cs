using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public double time = 0;

    string[] eventNames = { "You've got a viral post!",
                            "A big influencer mentioned your blog!",
                            "One of your blogs is now the top search engine result!"};
    double[] eventTimes = { 200, 400, 600 };
    double[] eventEffects = { 30, 50, 70 };
    int currentEventIndex = 0;
    double currentEventStartTime = 0;
    public bool eventInAction = false;

    public double BeforeVPS;

    void Start()
    {
        
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
    void Update()
    {
        time += Time.deltaTime;
        if(time >= eventTimes[currentEventIndex] && !eventInAction && !AdsScript.Instance.adsInAction)
        {
            BeforeVPS = GameManager.Instance.visitorsPerSecond;
            GameManager.Instance.showMessageBox(eventNames[currentEventIndex]);
            currentEventStartTime = time;
            GameManager.Instance.visitorsPerSecond += eventEffects[currentEventIndex];
            eventInAction = true;
            GameManager.Instance.CheckOverload();
        }
        if(time >= currentEventStartTime + 50 && eventInAction)
        {
            GameManager.Instance.visitorsPerSecond -= eventEffects[currentEventIndex];
            currentEventIndex++;
            eventInAction = false;
            GameManager.Instance.CheckOverload();
        }
    }
}
