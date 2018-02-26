using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider Volume;
    public AudioSource BackgroundMusic;

    private void Start()
    {
        Volume.value = 0.25f;
    }

    void Update()
    {
        BackgroundMusic.volume = Volume.value;
    }
}
