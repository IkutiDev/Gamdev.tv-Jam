using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    public float volume =1f;
    private void Awake()
    {
        if (FindObjectsOfType<AudioVolume>().Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void ChangeSliderValue(Single value)
    {

        volume = value;

    }

}
