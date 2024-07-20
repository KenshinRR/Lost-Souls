using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineBehavior : MonoBehaviour {
    [SerializeField] GameObject player;
    private LensFlare flare;

    private bool Shine = false;
    private float distance = 0.0f;

    public int possessedID;

    public const string ID = "ID";

    private void FlareOut(Parameters parameters) {
        int id = parameters.GetIntExtra(ID, 0);

        if(id != this.possessedID) {
            //Debug.Log($"ID not Found on possessedID{this.possessedID}");
        }
        
        if(id == this.possessedID) {
            this.Shine = false;
        }

    }
    private void CheckShine() {
        if (this.Shine) {
            this.distance = Vector3.Distance(this.transform.position, this.player.transform.position);
            this.distance = Mathf.Clamp(distance, 0, 5.1f);
        }
        else {
            this.distance = 2.0f;
        }


        if (this.distance < 2.0f) {
            this.flare.enabled = true;
        }
        else {
            this.flare.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == this.player.name) {
            this.Shine = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        this.Shine = false;
    }

    private void Start() {
        this.flare = GetComponent<LensFlare>();
        this.flare.enabled = false;
    
        EventBroadcaster.Instance.AddObserver(EventNames.Light_Events.ON_OUT, this.FlareOut);
    }
    private void Update() {
        this.CheckShine();
    }
}
    