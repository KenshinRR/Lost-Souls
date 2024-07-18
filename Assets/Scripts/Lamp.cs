using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour {

    [SerializeField] private GameObject CandleLight;

    private Light light;
    private List<GameObject> souls;

    private void Start() {
        this.light = GetComponent<Light>();
        this.souls = GhostManager.Instance.Possessed;
    }

    private void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        Animator animator = other.GetComponentInParent<Animator>();
        if (animator) {
            Debug.Log("I HATE UNITY");
        }
    }

    private void OnTriggerExit(Collider other) {
       
    }

}
