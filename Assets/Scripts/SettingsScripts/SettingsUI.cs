using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{

	public void LoadCharacterSelection()
	{
		SceneManager.LoadScene(0); //load choose character scene
	}

	public void LoadRecentLevel()
    {
		string returnScene = PlayerPrefs.GetString("previousScene");
		SceneManager.LoadScene(returnScene);
    }
}
