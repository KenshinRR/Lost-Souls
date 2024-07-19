using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScythSwooshSFXHandler : MonoBehaviour
{
    [SerializeField]
    private AudioSource _swooshSFX;

    private void OnEnable()
    {
        this._swooshSFX.Play();
    }
}
