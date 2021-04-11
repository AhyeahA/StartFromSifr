using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonSound : MonoBehaviour
{
    static public bool inProx = false;
    Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.interactable = false;
        btn.onClick.AddListener(TaskOnClick);
        //Button Level2 = Unlock.GetComponent<Button>();
        //Level2.enabled = true;
        //myButton.interactable = false;
    }


    static public void characterInProximity(bool temp)
    {
        inProx = temp;
    }
    // Update is called once per frame
    void Update()
    {
        btn.interactable = inProx;
    }

    static public void TaskOnClick()
    {
        //SpawnBlocksInArea.isPressed = true;
        if(SpawnBlocksInArea.isPressed == false)
        {
            SpawnBlocksInArea.keyIndex++;
            Debug.Log("Key Index Value" + SpawnBlocksInArea.keyIndex);
            SpawnBlocksInArea.isPressed = true;
        }
        SoundLetterTrigger.soundButtonClicked();
        Debug.Log("You have clicked the button!");
     
    }
}
