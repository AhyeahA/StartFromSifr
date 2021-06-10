using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ControlButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down");
        Debug.Log(SceneManager.GetActiveScene().name);
        if(SceneManager.GetActiveScene().name.Equals("Level 3 Room 2") || SceneManager.GetActiveScene().name.Equals("Level 1 Room 3"))
        {
            isPressed = true;
        }else if (SceneManager.GetActiveScene().name.Equals("Level 3 Room 3"))
        {
            L3R3TextCollision.isPressedToAdd = true;
        }
            
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("pointer up");
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name.Equals("Level 3 Room 2") || SceneManager.GetActiveScene().name.Equals("Level 1 Room 3"))
        {
            isPressed = false;
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level 3 Room 3"))
        {
            L3R3TextCollision.isPressedToAdd = false;
        }
    }

    public void selectBtnL3R2()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (this.name.Equals("SelectButton"))
        {
            L3R3Script.setAnswer = true;
        }
        //check for scenes
    }

    public void selectBtnL3R1()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (this.name.Equals("SelectButton"))
        {
            L3R1SpawnNouns.setAnswer = true;
        }
        //check for scenes
    }
}
