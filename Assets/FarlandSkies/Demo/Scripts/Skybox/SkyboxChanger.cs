using UnityEngine;
using UnityEngine.UI;

public class SkyboxChanger : MonoBehaviour
{
   

    public Material[] Skyboxes;
    private int rotateIndex = 0;

    public void Awake()
    {
        InvokeRepeating("ChangeSkybox",0f,5f);
        
    }

    public void ChangeSkybox()
    {
        if (rotateIndex < 4)
        {
            RenderSettings.skybox = Skyboxes[rotateIndex++];
            RenderSettings.skybox.SetFloat("_Rotation", 0);
        }
        else
        {
            rotateIndex = 0;
            RenderSettings.skybox = Skyboxes[rotateIndex++];
            RenderSettings.skybox.SetFloat("_Rotation", 0);
        }
    }

    //public void NextSkybox()
    //{
    //    _dropdown.value = (_dropdown.value < Skyboxes.Length - 1) ? _dropdown.value + 1 : _dropdown.value = 0;
    //    ChangeSkybox();
    //}
}