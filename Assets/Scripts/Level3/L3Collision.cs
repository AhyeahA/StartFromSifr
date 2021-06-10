using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3Collision : MonoBehaviour
{
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //this is for level 3 room 1 scene
        if (collision.other.tag.Equals("Player"))
        {
            Debug.Log("In contact with:" + this.gameObject.name);
            name = this.gameObject.name;
            L3R1SpawnNouns.getColliderObject(this.gameObject.name);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
