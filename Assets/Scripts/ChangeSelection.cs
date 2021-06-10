using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int characterIndex = 0; //initial
    private string playerprefkey="SelectedCharacter";
    private int selectedCharacterInt;
    
    void Start()
    {
        //display initial character
        characterIndex = 0; //might remove
        characters[characterIndex].SetActive(true);
    }

    public void NextCharacter()
    {
        characters[characterIndex].SetActive(false);
        characterIndex = (characterIndex + 1) % characters.Length;
        characters[characterIndex].SetActive(true);
    }

    public void PreviousCharacter()
    {
        characters[characterIndex].SetActive(false);
        characterIndex--;
        if (characterIndex < 0)
        {
            characterIndex = characters.Length-1;
        }
        characters[characterIndex].SetActive(true);
    }

    public void SelectCharacter()
    {
        selectedCharacterInt = characterIndex;
        PlayerPrefs.SetInt(playerprefkey, selectedCharacterInt);
        SceneManager.LoadScene(3); //go to choose mode screen
    }
}
