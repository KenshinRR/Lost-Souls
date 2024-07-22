using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YoureFired : MonoBehaviour
{
    private AudioSource _audioSrc;

    private float _ticks = 0.0f;

    private bool _liftOff = true;

    // Start is called before the first frame update
    void Start()
    {
        this._audioSrc = GetComponent<AudioSource>();
        this._audioSrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
        this._ticks += Time.deltaTime;

        if(this._ticks > 3.0f && this._liftOff)
        {
            this._liftOff = false;
            Rigidbody handler = GetComponent<Rigidbody>();

            handler.AddForce((Vector3.up + Vector3.forward) * 1000);
        }

        if (this._ticks > 5.0f)
            SceneManager.LoadScene("LostSoulsMainMenu");
    }
}
