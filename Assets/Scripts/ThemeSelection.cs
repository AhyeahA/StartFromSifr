using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThemeSelection : MonoBehaviour
{
    private string prefkeyStr = "theme";
    public void SelectTheme()
    {
        if(this.name.Equals("Vehicles"))
            PlayerPrefs.SetString(prefkeyStr, "Vehicles");
        else if (this.name.Equals("Animals"))
            PlayerPrefs.SetString(prefkeyStr, "Animals");
        else if (this.name.Equals("Food"))
            PlayerPrefs.SetString(prefkeyStr, "Food");

        SceneManager.LoadScene("MainFloor");
    }
}
