using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public static GhostManager Instance { get; private set; }

    [SerializeField]
    private List<GameObject> Possesed;

    [SerializeField]
    private List<GameObject> Ghostables;

    public List<GameObject> Possessed
    {
        get { return this.Possesed;}
    }

    [SerializeField]
    public int ReapAttempts;

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

        for(int x = 0; x < Ghostables.Count; x++)
        {
            //randomly makes each object have 50% chance of being possessed
            Randomizer(Ghostables[x]);
        }
        //setting a minimum just in case
        int min = 3;
        if (Possesed.Count < min)
        {
            Possessed.Clear();
            for (int x = 0; x < min; x++)
            {
                Possessed.Add(Ghostables[x]);
                Possesed[x].GetComponent<Animator>().SetBool("isPossessed", false);
                Possesed[x].gameObject.GetComponentInChildren<CharacterController>().gameObject.SetActive(false);
                Possesed[x].gameObject.GetComponentInChildren<LensFlare>().gameObject.SetActive(false);
            }

        }

        //setting up reaping and anything else
        for (int i = 0; i < Possesed.Count; i++)
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

        Parameters param = new Parameters();
        param.PutObjectExtra("POSSESS", Possesed.Count);

        
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

    void Randomizer(GameObject ghost) {
        float chances = Random.Range(0.0f, 1.0f);

       

        if(chances > 0.5)
        {
            Possesed.Add(ghost);
        }
        else
        {
            ghost.GetComponent<Animator>().SetBool("isPossessed", false);
            ghost.gameObject.GetComponentInChildren<CharacterController>().gameObject.SetActive(false);
            ghost.gameObject.GetComponentInChildren<LensFlare>().gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.Reap_Events.ON_REAP);
    }
}
