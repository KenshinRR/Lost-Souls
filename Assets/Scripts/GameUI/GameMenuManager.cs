using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager Instance { get; private set; }

    [SerializeField]
    private GameObject ghostTracking;
    [SerializeField]
    private GameObject timer;


    [SerializeField]
    private Text ghostTracker;
    [SerializeField]
    private Text reapFound;
    [SerializeField]
    private Text timerText;


    [SerializeField]
    private Text title;
    [SerializeField]
    private Text paragraph;
    [SerializeField]
    private Text timeTaken;
    [SerializeField]
    private Text foundGhosts;



    [SerializeField]
    private GameObject GameOver;



    //fading text
    private Color textCol;

    
    //tracking ghosts
    private int ghostFound = 0;
    private int ghostTotal = 0;

    //actual timer
    private float time;
    private float timeInitial;
    //timer update
    private int seconds = 0;
    private int minutes = 0;

    private bool gameOver = false;


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
        EventBroadcaster.Instance.AddObserver(EventNames.GameOver_Events.ON_FOUND, this.GhostsFound);


        ghostTracker.text = "Souls Found: " + ghostFound + "/" + ghostTotal;

        //setting reapFound to be not visible
        textCol = reapFound.color;
        textCol.a = 0.0f;
        reapFound.color = textCol;

        //setting timer in seconds
        time = 60;
        timeInitial = time;
        Timing(time);       
    }

    // Update is called once per frame
    void Update()
    {
        //if reapfound is visible, graduall make it invisible
        if(reapFound.color.a >0.0f)
        {
            float dissapearSpeed = 0.5f;
            textCol.a -= Time.deltaTime * dissapearSpeed;
            reapFound.color = textCol;
        }
        if (!gameOver)
        {
            time -= Time.deltaTime;
            Timing(time);
        }
        
        
        
    }
    private void Timing(float time)
    {
        seconds = Mathf.FloorToInt(time % 60);

        minutes = Mathf.FloorToInt(time / 60);

        if(!(minutes < 0 && seconds < 0))
        {
            timerText.text = string.Format("Time Remaining: {0:00}:{1:00}", minutes, seconds);
            
        }
        else
        {
            
            EventBroadcaster.Instance.PostEvent(EventNames.GameOver_Events.ON_TIMEOUT);
            this.TimeOut();
            
        }
        
    }

    private void GhostCaught(Parameters parameters)
    {
        ghostFound++;
        ghostTracker.text = string.Format("Souls Found:: {0:00}/{1:00}", ghostFound, ghostTotal);  

        //set reap found to visible
        textCol.a = 1.0f;
        reapFound.color = textCol;

        
    }

    private void GhostsFound()
    {
        if(gameOver == false)
        {
            this.gameOver = true;
            this.GameOver.SetActive(true);

            this.timer.SetActive(false);
            this.ghostTracking.SetActive(false);


            this.title.text = "You found all of the souls! Good Job.";
            this.paragraph.text = "The Lost souls have finally been retrieved. No one noticed, All is well. So its time to go back to hell.";

            Timing(timeInitial - time);
            this.timeTaken.text = string.Format("Time Taken: {0:00}:{1:00}", minutes, seconds);
            this.foundGhosts.text = string.Format("Souls Found:: {0:00}/{1:00}", ghostFound, ghostTotal);
        }
        

    }

    private void TimeOut()
    {
        if(gameOver == false)
        {
            this.gameOver = true;
            this.GameOver.SetActive(true);

            this.timer.SetActive(false);
            this.ghostTracking.SetActive(false);

            this.title.text = "You didn't find them in time. YOU FAILED!";
            this.paragraph.text = "Oh no! The souls have stayed long enough for other gods to notice. Great now you've gotta hear them complain" +
                " about this.";

            Timing(timeInitial);
            this.timeTaken.text = string.Format("Time Taken: {0:00}:{1:00}", minutes, seconds);
            this.foundGhosts.text = string.Format("Souls Found:: {0:00}/{1:00}", ghostFound, ghostTotal);
        }
        

    }
}
