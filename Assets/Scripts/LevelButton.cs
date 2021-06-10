using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    private string prefkeyStr = "levelNum";
    public void ChangeLevel()
    {
        if (this.gameObject.name.Equals("Level1Btn"))
        {
            PlayerPrefs.SetInt(prefkeyStr,1);
        }
        if (this.gameObject.name.Equals("Level2Btn"))
        {
            PlayerPrefs.SetInt(prefkeyStr, 2);
        }
        if (this.gameObject.name.Equals("Level3Btn"))
        {
            PlayerPrefs.SetInt(prefkeyStr, 3);
        }
        SceneManager.LoadScene(2);
    }
}
