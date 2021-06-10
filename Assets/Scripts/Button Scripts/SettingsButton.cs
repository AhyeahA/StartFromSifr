using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButton : MonoBehaviour
{
    private string settingpref = "previousScene";
    public void displaySettings()
    {
        PlayerPrefs.SetString(settingpref, SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Settings");
    }
}
