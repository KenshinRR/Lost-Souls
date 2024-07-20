using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnimHandler : MonoBehaviour
{
    [SerializeField]
    private float _ticks = 0;

    [SerializeField]
    private List<AudioClip> _ghostLaugh;

    private Animator _animator;
    private AudioSource _audioSource;
    private bool _hasLaughed = false;

    //after 2 seconds show the ghosts

    // Start is called before the first frame update
    void Start()
    {
        this._audioSource = GetComponent<AudioSource>();
        if (this._audioSource == null) Debug.LogError("Audio source not found!");

        this._animator = GetComponent<Animator>();
        if (this._animator == null) Debug.LogError("Animator not found!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        this._ticks += Time.deltaTime;

        if (this._ticks > 1.5)
        {
            this._animator.SetBool("isEscaping", true);
        }

        if (this._ticks > 2 && !this._hasLaughed)
        {
            this._hasLaughed = true;
            this._audioSource.PlayOneShot(this._ghostLaugh[0]);
            this._audioSource.PlayOneShot(this._ghostLaugh[1]);
        }
    }
}
