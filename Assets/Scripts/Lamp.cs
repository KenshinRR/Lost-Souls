using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour {

    [SerializeField] private GameObject CandleLight;

    private Light light;

    private void Start() {
        this.light = GetComponent<Light>();
       
    }

    private void Update() {
        
    }



}
