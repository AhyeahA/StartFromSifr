using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int characterIndex = 0; //initial
    // Start is called before the first frame update
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
}
