using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV1Chosen : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("Chosen:" + this.gameObject.GetComponent<Renderer>().material.mainTexture.name);
        DisplayWordHarakat.chosenAnswer = this.gameObject.GetComponent<Renderer>().material.mainTexture.name;
    }
}
