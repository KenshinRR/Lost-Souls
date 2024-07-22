using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAudioManager : MonoBehaviour
{
    public static TutorialAudioManager Instance;
    private AudioSource _audioSource;

    [SerializeField]
    private List<AudioClip> _audioClips = new List<AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this._audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(int index)
    {
        this._audioSource.PlayOneShot(this._audioClips[index]);
    }
}
