using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{

    public void ChangeLevel()
    {
        if (this.gameObject.name.Equals("Level1Btn"))
        {
            SceneManager.LoadScene(0);
        }
        if (this.gameObject.name.Equals("Level2Btn"))
        {
            SceneManager.LoadScene(0);
        }
        if (this.gameObject.name.Equals("Level3Btn"))
        {
            SceneManager.LoadScene(0);
        }

    }

    //when clicked, show choose theme? or load the mainFloor scene? 
}
