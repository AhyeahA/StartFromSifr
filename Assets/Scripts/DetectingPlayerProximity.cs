using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectingPlayerProximity : MonoBehaviour
{
    public Button btn;
    public GameObject spawnEffect;
    
    public float distance = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        btn.interactable = false;
        //btn.onClick.AddListener(SelectOnClick);
    }

    //public void SelectOnClick()
    //{
    //    Material m = this.gameObject.GetComponent<Material>();
    //    Debug.Log("Select on click" + m.name);
    //}

    // Update is called once per frame
    void Update()
    {
        //var player : Transform;
        //var distance = 50;
        //private var playerCloseEnough = false;
        //ParticleSystem ps = spawnEffect.GetComponent<ParticleSystem>();
        //var main = ps.main;

        //main.startSizeYMultiplier = 0.2f;



        if (Vector3.Distance(this.gameObject.transform.position, Camera.current.transform.position) < distance)
        {
            
            ButtonEnabledProximity.isCloseby = true;
            
            spawnEffect.transform.position = new Vector3(0, 0 , 0.5f);
           // Debug.Log(this.gameObject.name);
            
            Destroy(Instantiate(spawnEffect, this.gameObject.transform),1f);
            //Debug.Log(btn.interactable);
            Debug.Log(ButtonEnabledProximity.isClicked);
            if (ButtonEnabledProximity.isClicked)
            {
                Debug.Log("entering");
                
                Debug.Log("Object Chosen is" + this.gameObject.GetComponent<Renderer>().material.mainTexture.name);
                DisplayLetterForm.chosenAnswer = this.gameObject.GetComponent<Renderer>().material.mainTexture.name;
                ButtonEnabledProximity.isClicked = false;
            }
            
            

        }
        else
        {
    
            //ButtonEnabledProximity.isCloseby = false;
            Destroy(spawnEffect);
            

        }
    
}
}
