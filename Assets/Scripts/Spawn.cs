using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    [SerializeField]GameObject[] characters;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] float scale;
    [SerializeField] int moveSpeed;
    private GameObject selectedChar;
    
    private int characterIndex;
    private string playerprefkey = "SelectedCharacter";

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("MainFloor"))
        {
            InstantiatePlayer();
        }
    }

    public GameObject InstantiatePlayer()
    {
        characterIndex = PlayerPrefs.GetInt(playerprefkey);
        Debug.Log("Character Index" + characterIndex);
        selectedChar = characters[characterIndex];
        selectedChar.transform.localScale = new Vector3(scale, scale, scale); //make character bigger
        selectedChar.tag = "Player";
        selectedChar.GetComponent<SimpleCharacterControl>().SetMoveSpeed(moveSpeed);
        return Instantiate(selectedChar, spawnPoint.transform.position, spawnPoint.transform.rotation);


    }
}
