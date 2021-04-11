using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestryKeyOnCollision : MonoBehaviour
{
    static public bool keyCollected2 = false; //key collected in room 1 for room 2
    static public bool keyCollected3 = false; //key collected in room 2 for room 3
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Key Collision");
        Debug.Log(this.gameObject.tag);
        if(other.gameObject.tag.Equals("Player") && this.gameObject.tag.Equals("Key2"))
        {
            keyCollected2 = true;
            Destroy(this.gameObject);

        }else if (other.gameObject.tag.Equals("Player") && this.gameObject.tag.Equals("Key3"))
        {
            keyCollected3 = true;
            Destroy(this.gameObject);
        }

    }
}
