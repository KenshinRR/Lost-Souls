using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager Instance { get; private set; }

    [SerializeField]
    private Text ghostTracker;
    [SerializeField]
    private Text ReapFound;
    [SerializeField]
    private Text Timer; // TODO


    private Color textCol;

    
    private float time;

    private int ghostFound = 0;
    private int ghostTotal = 0;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(Instance);
        }
    }
    void Start()
    {
        ghostTotal = GhostManager.Instance.Possessed.Count;
        EventBroadcaster.Instance.AddObserver(EventNames.Reap_Events.ON_REAP, this.GhostCaught);


        ghostTracker.text = "Souls Found: " + ghostFound + "/" + ghostTotal;

        //setting reapFound to be not visible
        textCol = ReapFound.color;
        textCol.a = 0.0f;
        ReapFound.color = textCol;

        //setting timer in seconds
        time = 180;
        Timing();       
    }

    // Update is called once per frame
    void Update()
    {
        //if reapfound is visible, graduall make it invisible
        if(ReapFound.color.a >0.0f)
        {
            float dissapearSpeed = 0.5f;
            textCol.a -= Time.deltaTime * dissapearSpeed;
            ReapFound.color = textCol;
        }
        time -= Time.deltaTime;
        Timing();
        
        
    }
    private void Timing()
    {
        int seconds = Mathf.FloorToInt(time % 60);

        int minutes = Mathf.FloorToInt(time / 60);

        if(!(minutes == 0 && seconds == 0))
        {
            Timer.text = string.Format("Time Remaining: {0:00}:{1:00}", minutes, seconds);
        }
        
    }

    private void GhostCaught(Parameters parameters)
    {
        ghostFound++;
        ghostTracker.text = "Souls Found: " + ghostFound + "/" + ghostTotal;

        //set reap found to visible
        textCol.a = 1.0f;
        ReapFound.color = textCol;

        
    }
}
