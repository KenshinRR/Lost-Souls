using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAmbientManager : MonoBehaviour
{
    private float _ticks = 0.0f;
    private float _ambientTicks = 0.0f;

    private int _activateTime = 10;
    private int _activateAmbientTime = 16;

    private int _minActivateTime = 10;
    private int _maxActivateTime = 20;

    private int _minAmbientActivateTime = 18;
    private int _maxAmbientActivateTime = 28;

    private AudioSource _audioSource;

    [SerializeField]
    private List<AudioClip> _ambientClips = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> _ghostSoundClips = new List<AudioClip>();

    private void PlayAmbient()
    {
        int index = Random.Range(0, _ambientClips.Count);
        this._audioSource.PlayOneShot(_ambientClips[index]);
    }

    private void PlayGhostSound()
    {
        int index = Random.Range(0, _ghostSoundClips.Count);
        GameObject ghost = GhostManager.Instance.Possessed[
            Random.Range(
                0, GhostManager.Instance.Possessed.Count)
            ];
        AudioSource.PlayClipAtPoint(_ghostSoundClips[index], ghost.gameObject.transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        this._audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        this._ticks += Time.deltaTime;

        if (_ticks >= _activateTime)
        {
            _ticks = 0.0f;
            _activateTime = Random.Range(_minActivateTime, _maxActivateTime);

            this.PlayGhostSound();
        }

        this._ambientTicks += Time.deltaTime;

        if (_ambientTicks >= _activateAmbientTime)
        {
            _ambientTicks = 0.0f;

            if (this._audioSource.isPlaying) return;
            _activateAmbientTime = Random.Range(_minAmbientActivateTime, _maxAmbientActivateTime);

            this.PlayAmbient();
        }

    }
}
