using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Threading.Tasks;

public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager Instance { get; private set; }


    [SerializeField]
    private GameObject stats;

    [SerializeField]
    private Text swings;

    [SerializeField]
    private Text ghostTracker;
    [SerializeField]
    private Text reapFound;
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private GameObject loadingText;










    //fading text
    private Color textCol;

    
    //tracking ghosts
    private int ghostFound = 0;
    public int GhostFound
    {
        get { return ghostFound; }
        set { ghostFound = value; }
    }
    private int ghostTotal = 0;

    public int GhostTotal
    {
        get { return ghostTotal; }
        set { ghostTotal = value; }
    }

    //timer update
    private int seconds = 0;
    public int Seconds
    {
        get { return seconds; }
        set { seconds = value; }
    }
    private int minutes = 0;
    public int Minutes
    {
        get { return minutes; }
        set { minutes = value; }
    }

    //actual timer
    private float time;

    public float Times
    {
        get { return time; }
        set { time = value; }
    }

    private float timeInitial;

    public float TimeInitial
    {
        get { return timeInitial; }
        set { timeInitial = value; }
    }

    private bool win = false;

    public bool Win
    {
        get { return win; }
        set { win = value; }
    }

    private bool gameOver = false;
    //lazyness
    private bool runOnce = false;


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
       
        EventBroadcaster.Instance.AddObserver(EventNames.Reap_Events.ON_REAP, this.GhostCaught);
        EventBroadcaster.Instance.AddObserver(EventNames.GameOver_Events.ON_FOUND, this.GhostsFound);
        



        //setting reapFound to be not visible
        textCol = reapFound.color;
        textCol.a = 0.0f;
        reapFound.color = textCol;

        //setting timer in seconds
        time = 180;
        timeInitial = time;
        Timing(time);       
    }

    // Update is called once per frame
    void Update()
    {
        //due to possessed being 0 on start, getting the amount of ghosts is harder and i settled with just a lazy answer.
        if (!runOnce)
        {
            runOnce = true;
            ghostTotal = GhostManager.Instance.Possessed.Count;
            ghostTracker.text = string.Format("Souls Found: {0:00}/{1:00}", ghostFound, ghostTotal);
        }
      
        //if reapfound is visible, graduall make it invisible
        if (reapFound.color.a >0.0f)
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
        swings.text = "Swings Remaining: " + GhostManager.Instance.ReapAttempts.ToString();

        //this code will kill the game when zero, uncomment it if you just want a straight up test
        if (GhostManager.Instance.ReapAttempts == 0)
        {
            EventBroadcaster.Instance.PostEvent(EventNames.GameOver_Events.ON_TIMEOUT);
            TimeOut();
        }


    }
    
    public void Timing(float time)
    {
        seconds = Mathf.FloorToInt(time % 60);

        minutes = Mathf.FloorToInt(time / 60);

        if(!(minutes < 0 && seconds < 0))
        {
            if(timerText != null)
            {
                timerText.text = string.Format("Time Remaining: {0:00}:{1:00}", minutes, seconds);
            }
           
            
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
        ghostTracker.text = string.Format("Souls Found: {0:00}/{1:00}", ghostFound, ghostTotal);  

        //set reap found to visible
        textCol.a = 1.0f;
        reapFound.color = textCol;

        
    }

    private async void GhostsFound()
    {
        if(gameOver == false)
        {
            this.gameOver = true;
            this.win = true;


            stats.SetActive(false);
            
            loadingText.SetActive(true);

            await Task.Delay(20);

            

            SceneManager.LoadScene("GameWin");
           
        }
        

    }

    private async void TimeOut()
    {
        //gameOver is not needed in ui
        if(gameOver == false)
        {
            //do this
            this.gameOver = true;
            
            
            

            this.win = false;
            stats.SetActive(false);

            loadingText.SetActive(true);

            await Task.Delay(20);

            //text galore here, reuse

            SceneManager.LoadScene("GameLoss");
        }
        

    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.Reap_Events.ON_REAP);
        EventBroadcaster.Instance.RemoveObserver(EventNames.GameOver_Events.ON_FOUND);
    }
}
