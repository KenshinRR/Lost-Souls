using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static  MainMenuManager Instance { get; private set; }

    public static string SceneToLoad { get; private set; }

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
        SceneManager.LoadScene("LostSouls");
        SceneToLoad = "LostSouls";
    }

    public void TutorialPressed()
    {
        SceneManager.LoadScene("SoulsEscapingScene");
        SceneToLoad = "Tutorial";
    }

    public void CreditsPressed()
    {
        SceneManager.LoadScene("LostSouls");
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
        }
    }
}
