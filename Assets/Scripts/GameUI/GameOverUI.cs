using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        settingText();

    }

    
    void settingText()
    {
        if (GameMenuManager.Instance.Win)
        {
            this.title.text = "You found all of the souls! Good Job.";
            this.paragraph.text = "The Lost souls have finally been retrieved. No one noticed, All is well. So its time to go back to hell.";

            GameMenuManager.Instance.Timing(GameMenuManager.Instance.TimeInitial - GameMenuManager.Instance.Times);
            this.timeTaken.text = string.Format("Time Taken: {0:00}:{1:00}", GameMenuManager.Instance.Minutes, GameMenuManager.Instance.Seconds);
            this.foundGhosts.text = string.Format("Souls Found:: {0:00}/{1:00}", GameMenuManager.Instance.GhostFound, GameMenuManager.Instance.GhostTotal);

        }
        else
        {
            if(GhostManager.Instance.ReapAttempts != 0)
            {
                //text galore here, reuse
                this.title.text = "You didn't find them in time. YOU FAILED!";
                this.paragraph.text = "Oh no! The souls have alerted other gods. They can't have souls just dissapear so you are FIRED!";

                //setup minutes and seconds as something gettable and check.
                GameMenuManager.Instance.Timing(GameMenuManager.Instance.TimeInitial - GameMenuManager.Instance.Times);
                this.timeTaken.text = string.Format("Time Taken: {0:00}:{1:00}", GameMenuManager.Instance.Minutes, GameMenuManager.Instance.Seconds);
                this.foundGhosts.text = string.Format("Souls Found:: {0:00}/{1:00}", GameMenuManager.Instance.GhostFound, GameMenuManager.Instance.GhostTotal);
            }
            else
            {
                //text galore here, reuse
                this.title.text = "STOP SWINGING SO MUCH. YOU FAILED!";
                this.paragraph.text = "The other gods have noticed with you swinging that Scythe around. You got fired for reckless use of a weapon.";

                //setup minutes and seconds as something gettable and check.
                GameMenuManager.Instance.Timing(GameMenuManager.Instance.TimeInitial - GameMenuManager.Instance.Times);
                this.timeTaken.text = string.Format("Time Taken: {0:00}:{1:00}", GameMenuManager.Instance.Minutes, GameMenuManager.Instance.Seconds);
                this.foundGhosts.text = string.Format("Souls Found:: {0:00}/{1:00}", GameMenuManager.Instance.GhostFound, GameMenuManager.Instance.GhostTotal);
            } 

        }
                
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuClicked()
    {
        SceneManager.LoadScene("LostSoulsMainMenu");
    }
}
