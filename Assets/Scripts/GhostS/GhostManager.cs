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

            //adding collider
            if (this.Possesed[i].GetComponent<MeshCollider>() == null)
            {
                currentGhost.AddComponent<BoxCollider>();
            }

            animator.SetBool("isPossessed", true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
