using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour
{
    private string modePrefKey = "mode";
    public void SelectMode()
    {
        if (this.name.Equals("PracticeMode"))
            PlayerPrefs.SetString(modePrefKey, "Practice");
        else if (this.name.Equals("AssessmentMode"))
            PlayerPrefs.SetString(modePrefKey, "Assessment");
        
        SceneManager.LoadScene(1);
    }
}
