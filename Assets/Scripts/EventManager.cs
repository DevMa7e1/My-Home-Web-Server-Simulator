using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public double time = 0;

    string[] eventNames = { "You've got a viral post!",
                            "A big influencer mentioned your blog!",
                            "One of your blogs is now the top search engine result!",
                            "A project you wrote about became really popular!",
                            "You posted about some important news first!",
                            "You've interviewed a very important person for one of your articles!",
                            "A big company has linked one of your articles as a fix to a bug they are having!",
                            "Your blog has just won a very big award!"};
    double[] eventTimes =   { 200, 400, 600, 800, 1000, 1200, 1400, 1600};
    double[] eventEffects = {  30,  50,  70,  90,  200,  300,  500,  900};
    int currentEventIndex = 0;
    double currentEventStartTime = 0;
    public bool eventInAction = false;

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
