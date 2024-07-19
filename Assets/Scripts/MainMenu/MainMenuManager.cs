using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static  MainMenuManager Instance { get; private set; }

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
    }

    public void TutorialPressed()
    {
        SceneManager.LoadScene("LostSouls");
    }

    public void CreditsPressed()
    {
        SceneManager.LoadScene("LostSouls");
    }

    public void QuitPressed()
    {
        Application.Quit();
    }

}
