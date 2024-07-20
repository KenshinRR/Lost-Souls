using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {
    [SerializeField] private float flickerIntensity = 0.2f;
    [SerializeField] private float flickersPerSecond = 3.0f;
    [SerializeField] private float speedRandom = 1.0f;

    private float time;
    private float startingIntensity;
    private Light light;
    void Start() {
        this.light = GetComponent<Light>();
        this.startingIntensity = light.intensity;
    }

    // Update is called once per frame
    void Update() {
        this.time += Time.deltaTime * (1 - Random.Range(-this.speedRandom, this.speedRandom)) * Mathf.PI;
        this.light.intensity = this.startingIntensity + Mathf.Sin(this.time * this.flickersPerSecond) * this.flickerIntensity;
    }
}
