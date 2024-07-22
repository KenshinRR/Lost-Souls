using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineBehavior : MonoBehaviour {
    [SerializeField] GameObject player;
    private LensFlare flare;

    private bool Shine = true;
    private float distance = 0.0f;

    public int possessedID;

    private Coroutine flareShine;

    public const string ID = "ID";

    private void FlareOut(Parameters parameters) {
        int id = parameters.GetIntExtra(ID, 0);

        if(id != this.possessedID) {
            Debug.Log($"ID not Found on possessedID{this.possessedID}");
        }
        
        if(id == this.possessedID) {
            this.Shine = false;
        }

    }
    private void CheckShine() {
        
        this.distance = Vector3.Distance(this.transform.position, this.player.transform.position);
        this.distance = Mathf.Clamp(distance, 0, 5.1f);

        if (this.distance < 2.0f) {
            this.Shine = true;
            this.flareShine = this.StartCoroutine(this.ShineInterval(2.5f));
        }
        else {
            this.flare.enabled = false;
            this.Shine = false;
        }
    }

    private IEnumerator ShineInterval(float duration) {
        while(this.Shine) {
            Debug.Log("shine please");
            this.flare.enabled = true;
            yield return new WaitForSeconds(duration);
            this.flare.enabled = false;
            yield return new WaitForSeconds(2.0f);
        }
    }

    private void Start() {
        this.flare = GetComponent<LensFlare>();
        this.flare.enabled = false;
    
        EventBroadcaster.Instance.AddObserver(EventNames.Light_Events.ON_OUT, this.FlareOut);
    }

    private void OnDestroy() {
        EventBroadcaster.Instance.RemoveObserver(EventNames.Light_Events.ON_OUT);
    }
    private void Update() {
        this.CheckShine();
    }
}
    