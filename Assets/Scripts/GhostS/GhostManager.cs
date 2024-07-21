using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public static GhostManager Instance { get; private set; }

    [SerializeField]
    private List<GameObject> Possesed;

    public List<GameObject> Possessed
    {
        get { return this.Possesed;}
    }

    public const string SOUL = "SOUl";
    public void ReapSoul(Parameters parameters) {
        GameObject soul = parameters.GetGameObjectExtra(SOUL);
        
        this.Possesed.Remove(soul);
    }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Possesed.Count; i++)
        {
            GameObject currentGhost = this.Possesed[i];
            Animator animator = currentGhost.GetComponent<Animator>();

            currentGhost.AddComponent<ReapedHandler>();
            currentGhost.GetComponent<ReapedHandler>().ID = i;
            currentGhost.GetComponentInChildren<ShineBehavior>().possessedID = i;

            //adding collider
            //if (this.Possesed[i].GetComponent<MeshCollider>() == null)
            //{
            //    currentGhost.AddComponent<BoxCollider>();
            //}

            animator.SetBool("isPossessed", true);
        }

        EventBroadcaster.Instance.AddObserver(EventNames.Reap_Events.ON_REAP, this.ReapSoul);

    }

    // Update is called once per frame
    void Update()
    {
        //if you win
        if(Possesed.Count == 0)
        {
            EventBroadcaster.Instance.PostEvent(EventNames.GameOver_Events.ON_FOUND);
        }
    }
}
