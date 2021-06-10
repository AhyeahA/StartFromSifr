using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioChanger : MonoBehaviour
{

    //AudioSource m_MyAudioSource;
    public Slider volumeSlider;
    //Value from the slider, and it converts to volume level
    float m_MySliderValue;
    public AudioMixer bV;


    void Start()
    {
        float value;
        bool result = bV.GetFloat("BackgroundVolume", out value);
        if (result)
        {
            volumeSlider.value = value;
        }
        else
        {
            volumeSlider.value = 0.5f;
        }

        bV.SetFloat("BackgroundVolume", volumeSlider.value);
        
    }

   

    void OnGUI()
    {
        //Create a horizontal Slider that controls volume levels. Its highest value is 1 and lowest is 0
        //m_MySliderValue = GUI.HorizontalSlider(new Rect(25, 25, 200, 60), m_MySliderValue, 0.0F, 1.0F);
        //Makes the volume of the Audio match the Slider value

        //m_MyAudioSource.volume = volumeSlider.value;
        bV.SetFloat("BackgroundVolume", volumeSlider.value);
    }

}
