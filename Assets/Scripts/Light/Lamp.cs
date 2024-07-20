using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour {

    [SerializeField] private GameObject CandleLight;

    private Light light;
    private List<GameObject> souls;
    private GameObject soul = null;

    public List<GameObject> Souls {
        get { return souls; }
        set { souls = value; }
    }

    private void CheckReapedSoul() {
        bool active = false;
        this.souls = GhostManager.Instance.Possessed;

        for (int i = 0; i < souls.Count; i++) {
            if (souls[i] == this.soul) {
                active = true;
            }
        }

        if (!active) {
            this.soul = null;
        }
    }

    private void CalculateDistance() {
        float distance = Vector3.Distance(this.transform.position, this.soul.transform.position);

        distance = Mathf.Clamp(distance, 0, 5.1f);

        float intensity = Mathf.Lerp(1.0f, 0.1f, distance / 5.1f);

        this.light.intensity = intensity;
    }

    private void Start() {
        this.light = this.CandleLight.GetComponent<Light>();
        this.souls = GhostManager.Instance.Possessed;
    }

    private void Update() {
        this.CheckReapedSoul();

        if (this.soul != null) {
            this.CalculateDistance();
        }
        else {
            if(this.light.intensity > 0.0f) {
                this.light.intensity -= 0.01f;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other) {
        ReapedHandler soul = other.GetComponentInParent<ReapedHandler>();
        if (soul) {
            for (int i = 0; i < this.souls.Count; i++) {
                if (this.souls[i] == soul.gameObject) {
                    this.soul = soul.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        this.soul = null;
    }

}
