using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static  MainMenuManager Instance { get; private set; }

    public static string SceneToLoad { get; private set; }

    [SerializeField] private int timeToWait = 3;

    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject loadingText;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject mainMenu;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }
   
    public void StartPressed()
    {
        //Sceneloader("LostSouls");
        SceneManager.LoadScene("SoulsEscapingScene");

        SceneToLoad = "LostSouls";
    }

    public void TutorialPressed()
    {
        SceneManager.LoadScene("SoulsEscapingScene");
        SceneToLoad = "Tutorial";
    }

    public void CreditsPressed()
    {
       this.credits.SetActive(true);
        this.mainMenu.SetActive(false);
    }

    public void BackToMenuPressed()
    {
        this.credits.SetActive(false);
        this.mainMenu.SetActive(true);
    }

    public void QuitPressed()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Current scene to load: " + SceneToLoad);
            Debug.Log("Current HasWatchedTutorial: " + PlayerPrefs.GetInt("HasWatchedTutorial"));
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("HasWatchedTutorial", 0);
            PlayerPrefs.Save();
        }
    }
    private async void Sceneloader(string scene)
    {
        
        var sceneLoad = SceneManager.LoadSceneAsync(scene);

        sceneLoad.allowSceneActivation = false;

        buttons.SetActive(false);
        loadingText.SetActive(true);

        await Task.Delay(timeToWait);

        sceneLoad.allowSceneActivation = true;

    }
}
