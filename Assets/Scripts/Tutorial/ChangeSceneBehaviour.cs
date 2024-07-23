using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private bool _inTutorial = false;

    private float _ticks = 0.0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (this._inTutorial) return;

        this._ticks += Time.deltaTime;

        if (this._ticks > 1.5f)
        {
            if (MainMenuManager.SceneToLoad == "Tutorial")
            {
                SceneManager.LoadScene("TutorialAnimation");
            }
            else
            {
                if (PlayerPrefs.GetInt("HasWatchedTutorial") == 1)
                {
                    SceneManager.LoadScene("LostSouls");
                }
                else
                {
                    SceneManager.LoadScene("TutorialAnimation");
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (this._inTutorial)
        {
            PlayerPrefs.SetInt("HasWatchedTutorial", 1);
            PlayerPrefs.Save();

            if (MainMenuManager.SceneToLoad == "Tutorial")
            {
                SceneManager.LoadScene("LostSoulsMainMenu");
            }
            else if (MainMenuManager.SceneToLoad == "LostSouls")
            {
                SceneManager.LoadScene("LostSouls");
            }
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
