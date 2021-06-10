using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArabicSupport;
using System.Linq;

public class L3R3TextCollision : MonoBehaviour
{
    [SerializeField]
    TextMesh roomquadText;
    [SerializeField]
    Text onscreenOption;
    [SerializeField]
    Text onScreenSentence;

    private string sentence = "";
    static public bool isPressedToAdd;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressedToAdd)
        {
            onScreenSentence.color = Color.black;
            if (onscreenOption.text != "☺")
            {
                //adding to the sentence on screen
                //check if the word hasnt been added yet
                if (sentence.Contains(onscreenOption.text) == false)
                {
                    Debug.Log("Word has not been added yet!");
                    sentence = onscreenOption.text + " " + sentence;
                    Debug.Log("answer" + L3R3RainbowRoom.answer);

                    onScreenSentence.text = string.Join(" ", sentence.Split(' ').Distinct()).Trim();

                    Debug.Log("length onscreen " + onScreenSentence.text.Split(' ').Length);
                    Debug.Log("length from firebase" + L3R3RainbowRoom.answer.Split(' ').Length);
                    //onScreenSentence.text += sentence;  
                    if (onScreenSentence.text.Length == L3R3RainbowRoom.answer.Length)
                    {
                        Debug.Log("length matches");
                        if (onScreenSentence.text.Equals(L3R3RainbowRoom.answer))
                        {
                            Debug.Log("The right answer!");
                            onScreenSentence.color = Color.green;
                            //add to score
                        }
                        else
                        {
                            Debug.Log("The wrong answer!");
                            onScreenSentence.color = Color.red;
                            //decrease to score
                        }
                    }
                    onscreenOption.text = "";
                    
                }
            }
            isPressedToAdd = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //start animation
            Debug.Log("Collision ahs been made"); 
            //set text on screen for user
            onscreenOption.text = roomquadText.text;
        }
    }




}
