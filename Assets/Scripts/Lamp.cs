using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour {

    [SerializeField] private GameObject CandleLight;

    private Light light;
    private List<GameObject> souls;
    private GameObject soul = null;


    private void CalculateDistance() {
        float distance = Vector3.Distance(this.transform.position, this.soul.transform.position);
        Debug.Log(distance);

        distance = Mathf.Clamp(distance, 0 , 5.1f);

        float intensity = Mathf.Lerp(1.0f, 0.1f, distance / 5.1f);

        this.light.intensity = intensity;
    }

    private void Start() {
        this.light = this.CandleLight.GetComponent<Light>();
        this.souls = GhostManager.Instance.Possessed;
    }

    private void Update() {
        if(this.soul != null) {
            this.CalculateDistance();
        }
        else {
            this.light.intensity = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Animator animator = other.GetComponentInParent<Animator>();
        if (animator) {
            for (int i = 0; i < this.souls.Count; i++) {
                if (this.souls[i] == animator.gameObject) {
                    this.soul = animator.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        this.soul = null;
    }

}
